using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HighBerry
{
    public partial class AddBatch : Form 
    {
        private ConnectionDB database = new ConnectionDB();

        public AddBatch()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Заповнення комбінованого списку даними з таблиці Culture
            FillCultureComboBox();
        }

        private void FillCultureComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT culture_id, culture_name FROM Culture";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cultureId = reader.GetInt32(0);
                            string cultureName = reader.GetString(1);
                            comboBox_Culture.Items.Add(new CultureItem(cultureId, cultureName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні культур: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void button_save_Click_1(object sender, EventArgs e)
        {
            database.openConnection();

            var harvestId = textBox_HarvestId.Text;

            if (int.TryParse(harvestId, out _))
            {
                // Перевірка існування партії з введеним ідентифікатором
                if (IsHarvestIdExists(harvestId))
                {
                    MessageBox.Show("Партія з таким кодом вже існує!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CultureItem selectedCulture = comboBox_Culture.SelectedItem as CultureItem;

                // Перевірка, чи обрано культуру
                if (selectedCulture == null)
                {
                    MessageBox.Show("Оберіть культуру!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int cultureId = selectedCulture.Id;
                decimal amount = 0; // Змінив тип даних на decimal

                // Перевірка, чи поле ваги не порожнє
                if (!string.IsNullOrWhiteSpace(textBox_amount.Text))
                {
                    // Перевірка, чи введене значення є числом
                    if (decimal.TryParse(textBox_amount.Text, out amount)) // Змінив TryParse на decimal
                    {
                        // Перевірка, чи введене значення більше або рівне 0
                        if (amount < 0)
                        {
                            MessageBox.Show("Введіть коректну та не від'ємну кількість!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введіть коректне числове значення для кількості!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                var collectionDate = dateTimePicker_CollectionDate.Value.ToString("dd.MM.yyyy");
                var addQuery = "INSERT INTO Harvest (harvest_id, culture_id, amount, collection_date) VALUES (@harvest_id, @culture_id, @amount, CONVERT(datetime, @collection_date, 104))";

                using (SqlCommand command = new SqlCommand(addQuery, database.getConnection()))
                {
                    command.Parameters.AddWithValue("@harvest_id", harvestId);
                    command.Parameters.AddWithValue("@culture_id", cultureId);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@collection_date", collectionDate);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запис успішно створений!", "Успішно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Код партії повинен бути цілим числом!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            database.closeConnection();
        }



        private bool IsHarvestIdExists(string harvestId)
        {
            string query = "SELECT COUNT(*) FROM Harvest WHERE harvest_id = @harvest_id";

            using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
            {
                cmd.Parameters.AddWithValue("@harvest_id", harvestId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }

    // Клас для представлення елемента комбінованого списку
    public class CultureItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CultureItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

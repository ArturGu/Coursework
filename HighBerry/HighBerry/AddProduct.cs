using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HighBerry
{
    public partial class AddProduct : Form
    {
        ConnectionDB database = new ConnectionDB();

        public AddProduct()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Заповнення комбінованого списку даними з таблиці Processing
            FillProcessingComboBox();
        }

        private void FillProcessingComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT processing_id FROM Processing";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int processingId = reader.GetInt32(0);
                    comboBox_packagingCode.Items.Add(new ProcessingItem(processingId));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні обробки/фасування: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private bool IsProductIdExists(string productId)
        {
            string query = $"SELECT COUNT(*) FROM Product WHERE product_id = '{productId}'";
            SqlCommand cmd = new SqlCommand(query, database.getConnection());
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private string GetCultureName(int processingId)
        {
            string query = "SELECT Culture.culture_name FROM Processing " +
                           "INNER JOIN Harvest ON Processing.harvest_id = Harvest.harvest_id " +
                           "INNER JOIN Culture ON Harvest.culture_id = Culture.culture_id " +
                           $"WHERE Processing.processing_id = {processingId}";

            SqlCommand cmd = new SqlCommand(query, database.getConnection());
            return cmd.ExecuteScalar()?.ToString();
        }

        private string GetPackagingName(int processingId)
        {
            string query = "SELECT Packaging.packaging_name FROM Processing " +
                           "INNER JOIN Packaging ON Processing.packaging_id = Packaging.packaging_id " +
                           $"WHERE Processing.processing_id = {processingId}";

            SqlCommand cmd = new SqlCommand(query, database.getConnection());
            return cmd.ExecuteScalar()?.ToString();
        }

        private bool IsProcessingIdExists(int processingId)
        {
            string query = "SELECT COUNT(*) FROM Product WHERE processing_id = @processingId";
            SqlCommand cmd = new SqlCommand(query, database.getConnection());
            cmd.Parameters.AddWithValue("@processingId", processingId);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void button_save_Click_1(object sender, EventArgs e)
        {
            database.openConnection();

            var product_id = textBox_productId.Text;

            if (int.TryParse(product_id, out _))
            {
                if (IsProductIdExists(product_id))
                {
                    MessageBox.Show("Продукт з таким кодом вже існує!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBox_packagingCode.SelectedItem == null)
                {
                    MessageBox.Show("Будь ласка, оберіть код фасування!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int processingId = ((ProcessingItem)comboBox_packagingCode.SelectedItem).Id;

                if (IsProcessingIdExists(processingId))
                {
                    MessageBox.Show("Продукт з таким кодом фасування вже існує!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cultureName = GetCultureName(processingId);

                decimal price;

                if (string.IsNullOrWhiteSpace(textBox_productPrice.Text))
                {
                    price = 0;
                }
                else
                {
                    if (decimal.TryParse(textBox_productPrice.Text, out price))
                    {
                        if (price < 0)
                        {
                            MessageBox.Show("Ціна повинна бути невід'ємним числом!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введіть коректне числове значення для ціни!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                var expiration_date = dateTimePicker_expirationDate.Value;

                string packagingName = GetPackagingName(processingId);

                var product_name = $"{cultureName} {packagingName}";

                var addQuery = "INSERT INTO Product (product_id, processing_id, product_name, price, expiration_date) " +
                               "VALUES (@product_id, @processingId, @productName, @price, @expirationDate)";

                var command = new SqlCommand(addQuery, database.getConnection());
                command.Parameters.AddWithValue("@product_id", product_id);
                command.Parameters.AddWithValue("@processingId", processingId);
                command.Parameters.AddWithValue("@productName", product_name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@expirationDate", expiration_date);

                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно створений!", "Успішно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Код продукту повинен бути цілим числом!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            database.closeConnection();
        }


    }

    // Клас для представлення елемента комбінованого списку
    public class ProcessingItem
    {
        public int Id { get; set; }

        public ProcessingItem(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}

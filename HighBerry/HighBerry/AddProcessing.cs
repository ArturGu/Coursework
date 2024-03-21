using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HighBerry
{
    public partial class AddProcessing : Form
    {
        ConnectionDB database = new ConnectionDB();

        public AddProcessing()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Заповнення комбінованого списку даними з таблиці Harvest
            FillHarvestComboBox();

            // Заповнення комбінованого списку даними з таблиці Phase
            FillPhaseComboBox();

            // Заповнення комбінованого списку даними з таблиці Packaging
            FillPackagingComboBox();
        }

        private void FillHarvestComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT harvest_id FROM Harvest";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int harvestId = reader.GetInt32(0);
                    comboBox_HarvestId.Items.Add(new HarvestItem(harvestId));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні урожаю: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void FillPhaseComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT phase_id, phase_name FROM Phase";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int phaseId = reader.GetInt32(0);
                    string phaseName = reader.GetString(1);
                    comboBox_processingStage.Items.Add(new PhaseItem(phaseId, phaseName));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні етапів обробки: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void FillPackagingComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT packaging_id, packaging_name FROM Packaging";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int packagingId = reader.GetInt32(0);
                    string packagingName = reader.GetString(1);
                    comboBox_typePackaging.Items.Add(new PackagingItem(packagingId, packagingName));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні типів упаковки: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void button_save_Click_1(object sender, EventArgs e)
        {
            database.openConnection();

            var processingId = textBox_ProcessingId.Text;

            if (!int.TryParse(processingId, out _))
            {
                ShowErrorMessage("Код фасування повинен бути цілим числом!");
                database.closeConnection();
                return;
            }

            if (IsProcessingIdExists(processingId))
            {
                ShowErrorMessage("Обробка з таким кодом вже існує!");
                database.closeConnection();
                return;
            }

            if (!CheckComboBoxSelection(comboBox_HarvestId, "Будь ласка, оберіть код партії!") ||
                !CheckComboBoxSelection(comboBox_processingStage, "Будь ласка, оберіть стадію обробки!") ||
                !CheckComboBoxSelection(comboBox_typePackaging, "Будь ласка, оберіть тип фасування!"))
            {
                database.closeConnection();
                return;
            }

            int harvestId = ((HarvestItem)comboBox_HarvestId.SelectedItem).Id;
            int phaseId = ((PhaseItem)comboBox_processingStage.SelectedItem).Id;
            int packagingId = ((PackagingItem)comboBox_typePackaging.SelectedItem).Id;

            int? quantity = null; // Кількість може бути NULL
            if (!string.IsNullOrEmpty(textBox_quantity.Text))
            {
                if (int.TryParse(textBox_quantity.Text, out int parsedQuantity) && parsedQuantity >= 0)
                {
                    quantity = parsedQuantity;
                }
                else
                {
                    ShowErrorMessage(parsedQuantity < 0 ? "Кількість не може бути менше нуля!" : "Кількість повинна мати цілий числовий формат!");
                    database.closeConnection();
                    return;
                }
            }
            else
            {
                // Якщо поле кількості порожнє, встановлюємо значення за замовчуванням (0)
                quantity = 0;
            }

            if (!DateTime.TryParseExact(dateTimePicker_packingDate.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime packingDate))
            {
                ShowErrorMessage("Неправильний формат дати!");
                database.closeConnection();
                return;
            }

            var addQuery = "INSERT INTO Processing (processing_id, harvest_id, phase_id, packaging_id, quantity, processing_date) " +
                           "VALUES (@processing_id, @harvestId, @phaseId, @packagingId, @quantity, @packingDate)";

            using (SqlCommand command = new SqlCommand(addQuery, database.getConnection()))
            {
                command.Parameters.Add("@processing_id", SqlDbType.Int).Value = processingId;
                command.Parameters.Add("@harvestId", SqlDbType.Int).Value = harvestId;
                command.Parameters.Add("@phaseId", SqlDbType.Int).Value = phaseId;
                command.Parameters.Add("@packagingId", SqlDbType.Int).Value = packagingId;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity ?? (object)DBNull.Value;
                command.Parameters.Add("@packingDate", SqlDbType.DateTime).Value = packingDate;

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Запис успішно створений!", "Успішно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            database.closeConnection();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool CheckComboBoxSelection(ComboBox comboBox, string errorMessage)
        {
            if (comboBox.SelectedItem == null)
            {
                ShowErrorMessage(errorMessage);
                return false;
            }
            return true;
        }

        private bool IsProcessingIdExists(string processingId)
        {
            string query = $"SELECT COUNT(*) FROM Processing WHERE processing_id = '{processingId}'";
            SqlCommand cmd = new SqlCommand(query, database.getConnection());
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

    }

    // Клас для представлення елемента комбінованого списку для Harvest
    public class HarvestItem
    {
        public int Id { get; set; }

        public HarvestItem(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    // Клас для представлення елемента комбінованого списку для Phase
    public class PhaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PhaseItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Клас для представлення елемента комбінованого списку для Packaging
    public class PackagingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PackagingItem(int id, string name)
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

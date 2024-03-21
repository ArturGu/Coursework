using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HighBerry
{
    public partial class AddClient : Form
    {
        private ConnectionDB database = new ConnectionDB();

        public AddClient()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            // Перевірка заповненості обов'язкових полів
            if (string.IsNullOrWhiteSpace(textBox_customerName.Text) || string.IsNullOrWhiteSpace(textBox_EDRPOU.Text) || string.IsNullOrWhiteSpace(textBox_address.Text) || string.IsNullOrWhiteSpace(textBox_contacts.Text))
            {
                MessageBox.Show("Будь ласка, заповніть всі обов'язкові поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Перевірка довжини EDRPOU
            if (textBox_EDRPOU.Text.Length != 8)
            {
                MessageBox.Show("ЄДРПОУ повинен містити рівно 8 символів!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int clientId;

            // Перевірка чи введений ID клієнта коректний
            if (!int.TryParse(textBox_ClientId.Text, out clientId))
            {
                MessageBox.Show("Введіть коректне ціле значення для ID клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Параметри для вставки запису
            string query = "INSERT INTO Client (client_id, client_name, EDRPOU, address, contacts) VALUES (@clientId, @clientName, @edrpou, @address, @contacts)";

            // Виконання запиту
            using (SqlCommand command = new SqlCommand(query, database.getConnection()))
            {
                command.Parameters.AddWithValue("@clientId", clientId);
                command.Parameters.AddWithValue("@clientName", textBox_customerName.Text);
                command.Parameters.AddWithValue("@edrpou", textBox_EDRPOU.Text);
                command.Parameters.AddWithValue("@address", textBox_address.Text);
                command.Parameters.AddWithValue("@contacts", textBox_contacts.Text);

                try
                {
                    database.openConnection();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запис успішно створений!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при створенні запису: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    database.closeConnection();
                }
            }
        }
    }
}

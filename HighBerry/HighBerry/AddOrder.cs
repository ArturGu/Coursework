using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HighBerry
{
    public partial class AddOrder : Form
    {
        private ConnectionDB database = new ConnectionDB();

        public AddOrder()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Заполнение комбинированного списка данными из таблицы Client
            FillClientComboBox();
        }

        private void FillClientComboBox()
        {
            try
            {
                database.openConnection();

                string query = "SELECT client_id, client_name FROM Client";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int clientId = reader.GetInt32(0);
                            string clientName = reader.GetString(1);
                            comboBox_clientList.Items.Add(new ClientItem(clientId, clientName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні клієнтів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            // Проверяем, был ли выбран клиент
            if (comboBox_clientList.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, виберіть клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newOrderId = CreateNewOrder(); // Создаем новый заказ

            if (newOrderId == -1) // Проверяем, была ли ошибка при создании заказа
            {
                MessageBox.Show("Помилка при створенні замовлення!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем экземпляр формы FillOrder, передавая ему ID нового заказа
            FillOrder fillOrder = new FillOrder(newOrderId);
            this.Hide(); // Скрываем текущую форму
            fillOrder.Show();
        }

        private int CreateNewOrder()
        {
            try
            {
                database.openConnection();

                // Создаем запрос для вставки нового заказа и получения его ID
                string query = "INSERT INTO Orders (client_id, order_date) OUTPUT INSERTED.order_id VALUES (@client_id, @order_date)";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    // Получаем ID выбранного клиента
                    int selectedClientId = ((ClientItem)comboBox_clientList.SelectedItem).Id;

                    // Устанавливаем параметры запроса
                    cmd.Parameters.AddWithValue("@client_id", selectedClientId);
                    cmd.Parameters.AddWithValue("@order_date", DateTime.Now);

                    // Выполняем запрос и возвращаем ID созданного заказа
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при створенні замовлення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Возвращаем -1 в случае ошибки
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_addClient_Click(object sender, EventArgs e)
        {
            СustomerBase customerBaseForm = new СustomerBase();
            customerBaseForm.Show();
            this.Close();
        }
    }

    // Класс для представления элемента комбинированного списка клиентов
    public class ClientItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ClientItem(int id, string name)
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

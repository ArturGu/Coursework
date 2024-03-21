using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HighBerry
{
    enum RowStateClient
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class СustomerBase : Form
    {
        ConnectionDB database = new ConnectionDB();
        int selectedRow;

        public СustomerBase()
        {
            InitializeComponent();
        }

        private void СustomerBase_Load(object sender, EventArgs e)
        {
            CreateColumnsClient();
            RefreshDataGridClient(dataGridViewClient);
            LoadCustomerDataFromFirstRow();
        }

        private void LoadCustomerDataFromFirstRow()
        {
            if (dataGridViewClient.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridViewClient.Rows[0];

                textBox_ClientId.Text = firstRow.Cells["client_id"].Value?.ToString() ?? "";
                textBox_customerName.Text = firstRow.Cells["client_name"].Value?.ToString() ?? "";
                textBox_EDRPOU.Text = firstRow.Cells["EDRPOU"].Value?.ToString() ?? "";
                textBox_address.Text = firstRow.Cells["address"].Value?.ToString() ?? "";
                textBox_contacts.Text = firstRow.Cells["contacts"].Value?.ToString() ?? "";
            }
            else
            {
                // Якщо таблиця порожня, очистити тексти введення
                textBox_ClientId.Text = "";
                textBox_customerName.Text = "";
                textBox_EDRPOU.Text = "";
                textBox_address.Text = "";
                textBox_contacts.Text = "";
            }
        }

        private void CreateColumnsClient()
        {
            dataGridViewClient.Columns.Add("client_id", "ID Клієнта");
            dataGridViewClient.Columns.Add("client_name", "Назва клієнта");
            dataGridViewClient.Columns.Add("EDRPOU", "ЄДРПОУ");
            dataGridViewClient.Columns.Add("address", "Адреса");
            dataGridViewClient.Columns.Add("contacts", "Контактні дані");
            dataGridViewClient.Columns.Add("IsNew", String.Empty); // Додаємо стовпець для позначення нових записів

            dataGridViewClient.Columns[5].Visible = false; // Сховаємо стовпець "IsNew"

            foreach (DataGridViewColumn column in dataGridViewClient.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.Gray;
                column.HeaderCell.Style.Font = new Font("Noto Sans", 13, FontStyle.Bold);
            }
        }

        private void ClearFieldsClient()
        {
            if (dataGridViewClient.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridViewClient.Rows[0];

                textBox_ClientId.Text = firstRow.Cells["client_id"].Value?.ToString() ?? "";
                textBox_customerName.Text = firstRow.Cells["client_name"].Value?.ToString() ?? "";
                textBox_EDRPOU.Text = firstRow.Cells["EDRPOU"].Value?.ToString() ?? "";
                textBox_address.Text = firstRow.Cells["address"].Value?.ToString() ?? "";
                textBox_contacts.Text = firstRow.Cells["contacts"].Value?.ToString() ?? "";
            }
            else
            {
                textBox_ClientId.Text = "";
                textBox_customerName.Text = "";
                textBox_EDRPOU.Text = "";
                textBox_address.Text = "";
                textBox_contacts.Text = "";
            }
        }

        private void ReadSingleRowClient(DataGridView dgw, IDataRecord record)
        {
            int clientId = record.GetInt32(0);
            string clientName = record.GetString(1);
            string edrpou = record.GetString(2);
            string address = record.GetString(3);
            string contacts = record.GetString(4);

            dgw.Rows.Add(clientId, clientName, edrpou, address, contacts, RowStateClient.ModifiedNew);
        }

        private void RefreshDataGridClient(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();

            string queryString = "SELECT client_id, client_name, EDRPOU, address, contacts FROM Client";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowClient(dataGridView, reader);
            }

            reader.Close();
        }

        private void dataGridViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewClient.Rows[selectedRow];

                textBox_ClientId.Text = row.Cells["client_id"].Value?.ToString() ?? "";
                textBox_customerName.Text = row.Cells["client_name"].Value?.ToString() ?? "";
                textBox_EDRPOU.Text = row.Cells["EDRPOU"].Value?.ToString() ?? "";
                textBox_address.Text = row.Cells["address"].Value?.ToString() ?? "";
                textBox_contacts.Text = row.Cells["contacts"].Value?.ToString() ?? "";
            }
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT client_id, client_name, EDRPOU, address, contacts FROM Client WHERE " +
                                  $"CONVERT(NVARCHAR, client_id) LIKE N'%{textBox_search.Text}%' OR " +
                                  $"client_name LIKE N'%{textBox_search.Text}%' OR " +
                                  $"EDRPOU LIKE N'%{textBox_search.Text}%' OR " +
                                  $"address LIKE N'%{textBox_search.Text}%' OR " +
                                  $"contacts LIKE N'%{textBox_search.Text}%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowClient(dgw, reader);
            }
            reader.Close();
        }

        private void DeleteRow()
        {
            int index = dataGridViewClient.CurrentCell?.RowIndex ?? -1;

            if (index >= 0 && index < dataGridViewClient.Rows.Count)
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Попередження!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var selectedRow = dataGridViewClient.Rows[index];

                    if (selectedRow != null)
                    {
                        selectedRow.Visible = false;

                        if (selectedRow.Cells[0].Value != null && string.IsNullOrEmpty(selectedRow.Cells[0].Value.ToString()))
                        {
                            selectedRow.Cells[5].Value = RowStateClient.Deleted;
                            Update(); // Оновити дані в базі даних
                            return;
                        }

                        selectedRow.Cells[5].Value = RowStateClient.Deleted;
                        Update(); // Оновити дані в базі даних
                    }
                }
            }
        }


        private new void Update()
        {
            database.openConnection();

            try
            {
                foreach (DataGridViewRow row in dataGridViewClient.Rows)
                {
                    var rowState = row.Cells["IsNew"].Value as RowStateClient?;

                    if (rowState == RowStateClient.Deleted)
                    {
                        if (row.Cells["client_id"].Value != null)
                        {
                            int clientId = Convert.ToInt32(row.Cells["client_id"].Value);
                            string deleteQuery = "DELETE FROM Client WHERE client_id = @ClientId";

                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, database.getConnection());
                            deleteCommand.Parameters.AddWithValue("@ClientId", clientId);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }
                    else if (rowState == RowStateClient.Modified)
                    {
                        int clientId = Convert.ToInt32(row.Cells["client_id"].Value);
                        string clientName = Convert.ToString(row.Cells["client_name"].Value);
                        string edrpou = Convert.ToString(row.Cells["EDRPOU"].Value);
                        string address = Convert.ToString(row.Cells["address"].Value);
                        string contacts = Convert.ToString(row.Cells["contacts"].Value);

                        // Перевірка на коректність введених даних, пропускаємо некоректні рядки
                        if (string.IsNullOrWhiteSpace(clientName) || string.IsNullOrWhiteSpace(edrpou) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(contacts))
                        {
                            continue;
                        }
                        if (edrpou.Length != 8)
                        {
                            MessageBox.Show("ЄДРПОУ повинен містити рівно 8 символів!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }


                        string updateQuery = "UPDATE Client SET client_name = @ClientName, EDRPOU = @EDRPOU, address = @Address, contacts = @Contacts WHERE client_id = @ClientId";

                        SqlCommand updateCommand = new SqlCommand(updateQuery, database.getConnection());
                        updateCommand.Parameters.AddWithValue("@ClientName", clientName);
                        updateCommand.Parameters.AddWithValue("@EDRPOU", edrpou);
                        updateCommand.Parameters.AddWithValue("@Address", address);
                        updateCommand.Parameters.AddWithValue("@Contacts", contacts);
                        updateCommand.Parameters.AddWithValue("@ClientId", clientId);
                        updateCommand.ExecuteNonQuery();
                    }
                }

                // Після успішного оновлення закриваємо з'єднання з базою даних
                database.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при оновленні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Change()
        {
            if (dataGridViewClient.CurrentRow != null)
            {
                var selectedRowIndex = dataGridViewClient.CurrentRow.Index;
                var clientId = Convert.ToInt32(textBox_ClientId.Text);
                var clientName = textBox_customerName.Text;
                var edrpou = textBox_EDRPOU.Text;
                var address = textBox_address.Text;
                var contacts = textBox_contacts.Text;

                // Перевірка коректності введених даних
                if (string.IsNullOrWhiteSpace(clientName))
                {
                    MessageBox.Show("Будь ласка, введіть назву клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(edrpou))
                {
                    MessageBox.Show("Будь ласка, введіть EDRPOU!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("Будь ласка, введіть адресу клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(contacts))
                {
                    MessageBox.Show("Будь ласка, введіть контактні дані клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Оновлення значень у вибраному рядку
                dataGridViewClient.Rows[selectedRowIndex].SetValues(clientId, clientName, edrpou, address, contacts);
                dataGridViewClient.Rows[selectedRowIndex].Cells[5].Value = RowStateClient.Modified;
            }
        }


        private void button_plus_Click(object sender, EventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.Show();
        }

        private void button_pen_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?", "Підтвердження!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Change();
                Update();
            }
        }

        private void button_trash_Click(object sender, EventArgs e)
        {
            DeleteRow();
            ClearFieldsClient();
            Update();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridClient(dataGridViewClient);
            ClearFieldsClient();
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridViewClient);

        }

       
    }
}

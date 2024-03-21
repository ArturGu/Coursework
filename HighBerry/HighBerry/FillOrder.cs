using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace HighBerry
{
    public partial class FillOrder : Form
    {
        private int selectedOrderId;
        private ConnectionDB database = new ConnectionDB();

        public FillOrder(int orderId)
        {
            InitializeComponent();
            selectedOrderId = orderId;
            label_orderNumber.Text = "Замовлення № " + selectedOrderId;
            FillDataGridView();
            CalculateTotalPrice();
        }

        public void FillDataGridView()
        {
            try
            {
                database.openConnection();

                string query = @"SELECT p.product_id AS 'Код товару', 
                                    p.product_name AS 'Назва товару', 
                                    co.quantity_goods AS 'Кількість',
                                    (p.price * co.quantity_goods) AS 'Ціна'
                            FROM Product p
                            INNER JOIN Content_Order co ON p.product_id = co.product_id
                            WHERE co.order_id = @orderId";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@orderId", selectedOrderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewContent.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (DataGridViewRow row in dataGridViewContent.Rows)
            {
                if (row.Cells["Ціна"].Value != null)
                {
                    totalPrice += Convert.ToDecimal(row.Cells["Ціна"].Value);
                }
            }

            label_totalPrice.Text = "Загальна сума: " + totalPrice + " ₴";
        }

        private void button_plus_Click(object sender, EventArgs e)
        {
            AddFillOrder addFillOrder = new AddFillOrder(selectedOrderId);
            addFillOrder.Show();
        }

        private void dataGridViewContent_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateTotalPrice();
        }

        private void button_trash_Click(object sender, EventArgs e)
        {
            if (dataGridViewContent.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Отримуємо індекс виділеного рядка
                        int selectedIndex = dataGridViewContent.SelectedRows[0].Index;

                        // Отримуємо ID товару, який потрібно видалити
                        int productId = Convert.ToInt32(dataGridViewContent.Rows[selectedIndex].Cells["Код товару"].Value);

                        // Видаляємо запис з бази даних
                        DeleteRecord(selectedOrderId, productId);

                        // Оновлюємо dataGridViewContent
                        FillDataGridView();

                        // Оновлюємо загальну суму
                        CalculateTotalPrice();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при видаленні запису: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть запис для видалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRecord(int orderId, int productId)
        {
            try
            {
                database.openConnection();

                string deleteQuery = @"DELETE FROM Content_Order WHERE order_id = @orderId AND product_id = @productId";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, database.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@productId", productId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                database.closeConnection();
            }
        }

    }
}

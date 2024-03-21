using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HighBerry
{
    public partial class AddFillOrder : Form
    {
        private int selectedOrderId; // Змінна для зберігання ідентифікатора замовлення
        private ConnectionDB database = new ConnectionDB(); // Підключення до бази даних

        public AddFillOrder(int orderId)
        {
            InitializeComponent();
            selectedOrderId = orderId; // Зберігаємо ідентифікатор замовлення у змінній

            // Заповнення комбінованого списку даними з таблиці Product
            FillProductComboBox();
        }

        private void FillProductComboBox()
        {
            try
            {
                database.openConnection();

                string query = @"SELECT p.product_id, p.product_name, pr.quantity, 
                         ISNULL(SUM(co.quantity_goods), 0) AS totalOrderedQuantity
                         FROM Product p
                         INNER JOIN Processing pr ON p.processing_id = pr.processing_id
                         LEFT JOIN Content_Order co ON p.product_id = co.product_id
                         GROUP BY p.product_id, p.product_name, pr.quantity";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = reader.GetInt32(0);
                            string productName = reader.GetString(1);
                            int totalQuantity = reader.GetInt32(2);
                            int totalOrderedQuantity = reader.GetInt32(3);
                            int availableQuantity = totalQuantity - totalOrderedQuantity;

                            if (availableQuantity > 0)
                            {
                                comboBox_productName.Items.Add(new ProductItem(productId, productName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні товарів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }


        private void button_save_Click_1(object sender, EventArgs e)
        {
            if (comboBox_productName.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, виберіть товар!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_quantity.Text))
            {
                MessageBox.Show("Будь ласка, введіть кількість товару!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int quantity;
            if (!int.TryParse(textBox_quantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректне значення кількості товару!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedProductId = ((ProductItem)comboBox_productName.SelectedItem).Id;

            try
            {
                database.openConnection();

                int totalOrderedQuantity = GetTotalOrderedQuantity(selectedProductId);
                int availableQuantity = GetAvailableQuantity(selectedProductId, totalOrderedQuantity);

                if (quantity > availableQuantity)
                {
                    MessageBox.Show($"На складі залишилося тільки {availableQuantity} одиниць обраного товару!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = @"INSERT INTO Content_Order (product_id, order_id, quantity_goods)
                         VALUES (@productId, @orderId, @quantity)";

                using (SqlCommand cmd = new SqlCommand(query, database.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@productId", selectedProductId);
                    cmd.Parameters.AddWithValue("@orderId", selectedOrderId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);

                    cmd.ExecuteNonQuery();
                }

                FillOrder fillOrderForm = (FillOrder)Application.OpenForms["FillOrder"];
                if (fillOrderForm != null)
                {
                    fillOrderForm.FillDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
            this.Close();
        }

        private int GetTotalOrderedQuantity(int productId)
        {
            try
            {
                string sumQuery = @"SELECT SUM(co.quantity_goods) FROM Content_Order co
                    INNER JOIN Product p ON co.product_id = p.product_id
                    WHERE p.product_id = @productId";
                using (SqlCommand cmdSum = new SqlCommand(sumQuery, database.getConnection()))
                {
                    cmdSum.Parameters.AddWithValue("@productId", productId);
                    object result = cmdSum.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при отриманні суми замовленого товару: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private int GetAvailableQuantity(int productId, int totalOrderedQuantity)
        {
            try
            {
                string quantityQuery = @"SELECT pr.quantity FROM Product p
                             INNER JOIN Processing pr ON p.processing_id = pr.processing_id
                             WHERE p.product_id = @productId";
                using (SqlCommand cmdQuantity = new SqlCommand(quantityQuery, database.getConnection()))
                {
                    cmdQuantity.Parameters.AddWithValue("@productId", productId);
                    int availableQuantity = Convert.ToInt32(cmdQuantity.ExecuteScalar()) - totalOrderedQuantity;
                    return availableQuantity;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при отриманні кількості товару на складі: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void comboBox_productName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_productName.SelectedItem != null)
            {
                int selectedProductId = ((ProductItem)comboBox_productName.SelectedItem).Id;

                try
                {
                    database.openConnection();

                    // Отримуємо кількість товару на складі для обраного продукту
                    string quantityQuery = @"SELECT pr.quantity FROM Product p
                                     INNER JOIN Processing pr ON p.processing_id = pr.processing_id
                                     WHERE p.product_id = @productId";
                    using (SqlCommand cmdQuantity = new SqlCommand(quantityQuery, database.getConnection()))
                    {
                        cmdQuantity.Parameters.AddWithValue("@productId", selectedProductId);
                        int totalQuantity = Convert.ToInt32(cmdQuantity.ExecuteScalar());

                        // Отримуємо суму значень quantity_goods для обраного продукту
                        string sumQuery = @"SELECT SUM(quantity_goods) FROM Content_Order WHERE product_id = @productId";
                        using (SqlCommand cmdSum = new SqlCommand(sumQuery, database.getConnection()))
                        {
                            cmdSum.Parameters.AddWithValue("@productId", selectedProductId);
                            object result = cmdSum.ExecuteScalar();
                            int totalOrderedQuantity = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                            // Обчислюємо залишок товару
                            int availableQuantity = totalQuantity - totalOrderedQuantity;

                            // Оновлюємо значення label_amount
                            label_amount.Text = $"Доступно: {availableQuantity} шт.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при завантаженні кількості товару: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    database.closeConnection();
                }
            }
        }


        // Клас для представлення елемента комбінованого списку товарів
        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ProductItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return $"Код:{Id} | {Name}"; // Повертаємо рядок з об'єднаним ID та назвою продукту
            }
        }
        
    }
}


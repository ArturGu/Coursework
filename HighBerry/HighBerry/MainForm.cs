using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HighBerry
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class MainForm : Form
    {
        ConnectionDB database = new ConnectionDB();
        List<CultureItem> cultureList; // Список для зберігання культур
        List<HarvestItem> harvestList; // Список для зберігання партій врожаю
        List<PhaseItem> phaseList;     // Список для зберігання етапів обробки
        List<PackagingItem> packagingList; // Список для зберігання типів упаковки
        List<ProcessingItem> processingList; // Список для зберігання типів упаковки
        List<ClientItem> clientList; // Список для зберігання клієнтів

        int selectedRow;

        public MainForm()
        {
            InitializeComponent();
            cultureList = new List<CultureItem>();
            harvestList = new List<HarvestItem>();
            phaseList = new List<PhaseItem>();
            packagingList = new List<PackagingItem>();
            processingList = new List<ProcessingItem>();
            clientList = new List<ClientItem>(); 

            LoadCultureList();
            LoadHarvestList();
            LoadPhaseList();
            LoadPackagingList();
            LoadPackagingCodeList();
            LoadClientList();

            InitializeComboBoxCulture();
            InitializeComboBoxHarvest();
            InitializeComboBoxProcessingStage();
            InitializeComboBoxTypePackaging();
            InitializeComboBoxPackagingCode();
            InitializeComboBoxClient();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateColumnsHarvest();
            CreateColumnsProcessing();
            CreateColumnsProduct();
            CreateColumnsOrders();

            RefreshDataGrid(dataGridView1);
            RefreshProcessingDataGrid(dataGridView2);
            RefreshProductDataGrid(dataGridView3);
            RefreshOrdersDataGrid(dataGridView4);


            if (dataGridView1.Rows.Count == 0)
            {
                ClearFields();
            }
            if (dataGridView2.Rows.Count == 0)
            {
                ClearProcessingFields();
            }
            if (dataGridView3.Rows.Count == 0)
            {
                ClearProductFields();
            }
            if (dataGridView4.Rows.Count == 0)
            {
                ClearOrderFields();
            }
        }

        //Вкладка Партії врожаю------------------------------------------------------------------------------------------------

        private void LoadCultureList()
        {
            try
            {
                database.openConnection();

                string query = "SELECT culture_id, culture_name FROM Culture";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int cultureId = reader.GetInt32(0);
                    string cultureName = reader.GetString(1);
                    cultureList.Add(new CultureItem(cultureId, cultureName));
                }

                reader.Close();
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

        // Метод для ініціалізації комбінованого списку
        private void InitializeComboBoxCulture()
        {
            comboBox_culture.DataSource = cultureList;
            comboBox_culture.DisplayMember = "Name";
            comboBox_culture.ValueMember = "Id";
        }

        private void ClearFields()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridView1.Rows[0];

                textBox_harvest.Text = firstRow.Cells["harvest_id"].Value?.ToString() ?? "";
                comboBox_culture.Text = firstRow.Cells["culture_id"].Value?.ToString() ?? "";
                textBox_amount.Text = firstRow.Cells["amount"].Value?.ToString() ?? "";
                dateTimePicker_сollectionDate.Value = firstRow.Cells["collection_date"].Value != null
                    ? Convert.ToDateTime(firstRow.Cells["collection_date"].Value)
                    : DateTime.Now;
            }
            else
            {
                textBox_harvest.Text = "";
                comboBox_culture.SelectedIndex = -1;
                textBox_amount.Text = "";
                dateTimePicker_сollectionDate.Value = DateTime.Now;
            }
        }

        private void CreateColumnsHarvest()
        {
            dataGridView1.Columns.Add("harvest_id", "Код партії");
            dataGridView1.Columns.Add("culture_id", "Культура");
            dataGridView1.Columns.Add("amount", "Вага (кг)");
            dataGridView1.Columns.Add("collection_date", "Дата збору");
            dataGridView1.Columns["collection_date"].DefaultCellStyle.Format = "dd.MM.yyyy";

            dataGridView1.Columns.Add("IsNew", String.Empty);
         
            dataGridView1.Columns[4].Visible = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.Gray;
                column.HeaderCell.Style.Font = new Font("Noto Sans", 13, FontStyle.Bold);
            }
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            int harvestId = record.GetInt32(0);
            string cultureName = record.GetString(1);
            decimal amount = record.GetDecimal(2);
            DateTime collectionDate = record.GetDateTime(3);

            dgw.Rows.Add(harvestId, cultureName, amount, collectionDate, RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = "SELECT harvest_id, Culture.culture_name, amount, collection_date FROM Harvest INNER JOIN Culture ON Harvest.culture_id = Culture.culture_id";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox_harvest.Text = row.Cells[0].Value.ToString();
                comboBox_culture.Text = row.Cells[1].Value.ToString();
                textBox_amount.Text = row.Cells[2].Value.ToString();
                dateTimePicker_сollectionDate.Value = Convert.ToDateTime(row.Cells[3].Value);
            }
        }        

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            
            string searchString = $"SELECT harvest_id, Culture.culture_name, amount, collection_date FROM Harvest INNER JOIN Culture ON Harvest.culture_id = Culture.culture_id WHERE " +
                                  $"CONVERT(NVARCHAR, harvest_id) LIKE N'%{textBox_search1.Text}%' OR " +
                                  $"Culture.culture_name LIKE N'%{textBox_search1.Text}%' OR " +
                                  $"CONVERT(NVARCHAR, amount) LIKE N'%{textBox_search1.Text}%' OR " +
                                  $"CONVERT(NVARCHAR, collection_date, 23) LIKE N'%{textBox_search1.Text}%' OR " +
                                  $"CONVERT(NVARCHAR, collection_date, 104) LIKE N'%{textBox_search1.Text}%' OR " +
                                  $"CONVERT(NVARCHAR, collection_date, 4) LIKE N'%{textBox_search1.Text}%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            database.openConnection();
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell?.RowIndex ?? -1;

            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Попередження!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var selectedRow = dataGridView1.Rows[index];

                    if (selectedRow != null)
                    {
                        selectedRow.Visible = false;

                        if (selectedRow.Cells[0].Value != null && string.IsNullOrEmpty(selectedRow.Cells[0].Value.ToString()))
                        {
                            selectedRow.Cells[4].Value = RowState.Deleted;
                            return;
                        }

                        selectedRow.Cells[4].Value = RowState.Deleted;
                    }
                }
            }
        }

        private new void Update()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = dataGridView1.Rows[index].Cells[4].Value as RowState?;

                if (rowState == RowState.Existed)
                {
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    if (dataGridView1.Rows[index].Cells[0].Value != null)
                    {
                        var harvest_id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        var deleteQuery = "DELETE FROM Harvest WHERE harvest_id = @HarvestId";

                        var command = new SqlCommand(deleteQuery, database.getConnection());
                        command.Parameters.AddWithValue("@HarvestId", harvest_id);
                        command.ExecuteNonQuery();
                    }
                }

                if (rowState == RowState.Modified)
                {
                    var harvest_id = dataGridView1.Rows[index].Cells[0].Value;
                    var culture_name = dataGridView1.Rows[index].Cells[1].Value?.ToString();
                    var amount = dataGridView1.Rows[index].Cells[2].Value;
                    var collection_date = dataGridView1.Rows[index].Cells[3].Value;

                    if (harvest_id == null || string.IsNullOrEmpty(culture_name) || amount == null || collection_date == null)
                    {
                        continue;
                    }

                    var changeQuery = "UPDATE Harvest SET culture_id = @CultureId, amount = @Amount, collection_date = @CollectionDate WHERE harvest_id = @HarvestId";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    var cultureId = GetCultureIdByName(culture_name);

                    command.Parameters.AddWithValue("@CultureId", cultureId);
                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount == DBNull.Value ? 0 : Convert.ToDecimal(amount);
                    command.Parameters.Add("@CollectionDate", SqlDbType.DateTime).Value = Convert.ToDateTime(collection_date);
                    command.Parameters.AddWithValue("@HarvestId", Convert.ToInt32(harvest_id));
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }

        private void Change()
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedRowIndex = dataGridView1.CurrentRow.Index;
                var harvest_id = Convert.ToInt32(textBox_harvest.Text);
                var culture_name = comboBox_culture.Text;
                var collection_date = dateTimePicker_сollectionDate.Value;

                decimal amount;

                if (!GetDecimalValue(textBox_amount.Text, out amount))
                {
                    MessageBox.Show("Будь ласка, введіть коректну вагу (число)!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (amount < 0)
                {
                    MessageBox.Show("Вага повинна бути більше або рівною 0!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataGridView1.Rows[selectedRowIndex].SetValues(harvest_id, culture_name, amount, collection_date);
                dataGridView1.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
            }
        }

        // Функція для отримання ідентифікатора культури за її іменем
        private int GetCultureIdByName(string cultureName)
        {
            foreach (CultureItem culture in cultureList)
            {
                if (culture.Name == cultureName)
                {
                    return culture.Id;
                }
            }

            return -1; // Повернути -1, якщо ідентифікатор не знайдено
        }

        private bool GetDecimalValue(string value, out decimal result)
        {
            if (decimal.TryParse(value, out result))
            {
                return true;
            }

            result = 0;
            return false;
        }

        private void textBox_search1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button_refresh1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void button_trash1_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
            Update();
        }

        private void button_pen1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?", "Підтвердження!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Change();
                Update();
            }
        }

        private void button_plus1_Click(object sender, EventArgs e)
        {
            AddBatch add_form = new AddBatch();
            add_form.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                textBox_harvest.Text = selectedRow.Cells[0].Value.ToString();
                comboBox_culture.Text = selectedRow.Cells[1].Value.ToString();
                textBox_amount.Text = selectedRow.Cells[2].Value.ToString();
                dateTimePicker_сollectionDate.Value = Convert.ToDateTime(selectedRow.Cells[3].Value);
            }
        }

        //Вкладка Обробка та фасування--------------------------------------------------------------------------------------------------------------

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedRowIndex];

                textBox_packagingId.Text = selectedRow.Cells[0].Value.ToString();
                comboBox_harvestId.Text = selectedRow.Cells[1].Value.ToString();
                comboBox_processingStage.Text = selectedRow.Cells[2].Value.ToString();
                dateTimePicker_packingDate.Value = Convert.ToDateTime(selectedRow.Cells[3].Value);
                comboBox_typePackaging.Text = selectedRow.Cells[4].Value.ToString();
                textBox_quantity.Text = selectedRow.Cells[5].Value.ToString();
            }
        }


        private void InitializeComboBoxHarvest()
        {
            comboBox_harvestId.DataSource = harvestList;
            comboBox_harvestId.DisplayMember = "Id";
        }

        private void InitializeComboBoxProcessingStage()
        {
            comboBox_processingStage.DataSource = phaseList;
            comboBox_processingStage.DisplayMember = "Name";
            comboBox_processingStage.ValueMember = "Id";
        }

        private void InitializeComboBoxTypePackaging()
        {
            comboBox_typePackaging.DataSource = packagingList;
            comboBox_typePackaging.DisplayMember = "Name";
            comboBox_typePackaging.ValueMember = "Id";
        }

        private void LoadHarvestList()
        {
            try
            {
                database.openConnection();

                string query = "SELECT harvest_id FROM Harvest";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int harvest_id = reader.GetInt32(0);
                    harvestList.Add(new HarvestItem(harvest_id));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні партій врожаю: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }

        private void LoadPhaseList()
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
                    phaseList.Add(new PhaseItem(phaseId, phaseName));
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

        private void LoadPackagingList()
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
                    packagingList.Add(new PackagingItem(packagingId, packagingName));
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

        private void CreateColumnsProcessing()
        {
            dataGridView2.Columns.Add("processing_id", "Код фасування");
            dataGridView2.Columns.Add("harvest_id", "Код Партії");
            dataGridView2.Columns.Add("phase_id", "Стадія обробки");
            dataGridView2.Columns.Add("processing_date", "Дата фасування");
            dataGridView2.Columns["processing_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView2.Columns.Add("packaging_id", "Тип пакування");
            dataGridView2.Columns.Add("quantity", "Кількість прод.");
            dataGridView2.Columns.Add("IsNew", String.Empty);

            dataGridView2.Columns[6].Visible = false;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.Gray;
                column.HeaderCell.Style.Font = new Font("Noto Sans", 13, FontStyle.Bold);
            }
        }

        private void RefreshProcessingDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            var queryString = "SELECT processing_id, Harvest.harvest_id, phase_name, processing_date, Packaging.packaging_name, quantity FROM Processing " +
                              "INNER JOIN Harvest ON Processing.harvest_id = Harvest.harvest_id " +
                              "INNER JOIN Phase ON Processing.phase_id = Phase.phase_id " +
                              "INNER JOIN Packaging ON Processing.packaging_id = Packaging.packaging_id";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleProcessingRow(dgw, reader);
            }

            reader.Close();
            database.closeConnection();
        }

        private void ReadSingleProcessingRow(DataGridView dgw, IDataRecord record)
        {
            int processingId = record.GetInt32(0);
            int harvestId = record.GetInt32(1);
            string phaseName = record.GetString(2);
            DateTime processingDate = record.GetDateTime(3);
            string packagingName = record.GetString(4);
            int quantity = record.IsDBNull(5) ? 0 : record.GetInt32(5);

            dgw.Rows.Add(processingId, harvestId, phaseName, processingDate, packagingName, quantity, RowState.Existed);
        }

        private void UpdateProcessing()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    if (dataGridView2.Rows[index].Cells[0].Value != null)
                    {
                        var processing_id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                        var deleteQuery = $"DELETE FROM Processing WHERE processing_id = {processing_id}";

                        var command = new SqlCommand(deleteQuery, database.getConnection());
                        command.ExecuteNonQuery();
                    }
                }

                if (rowState == RowState.Modified)
                {
                    var processing_id = dataGridView2.Rows[index].Cells[0].Value?.ToString();
                    var harvest_id = dataGridView2.Rows[index].Cells[1].Value?.ToString();
                    var phase_name = dataGridView2.Rows[index].Cells[2].Value?.ToString();
                    var processing_date = Convert.ToDateTime(dataGridView2.Rows[index].Cells[3].Value);
                    var packaging_name = dataGridView2.Rows[index].Cells[4].Value?.ToString();
                    var quantity = Convert.ToInt32(dataGridView2.Rows[index].Cells[5].Value);

                    if (string.IsNullOrEmpty(processing_id) || string.IsNullOrEmpty(harvest_id) || string.IsNullOrEmpty(phase_name) || string.IsNullOrEmpty(packaging_name))
                    {
                        continue;
                    }

                    // Отримати phase_id за допомогою назви фази
                    var phaseIdQuery = "SELECT phase_id FROM Phase WHERE phase_name = @PhaseName";
                    var phaseIdCommand = new SqlCommand(phaseIdQuery, database.getConnection());
                    phaseIdCommand.Parameters.AddWithValue("@PhaseName", phase_name);
                    var phase_id = Convert.ToInt32(phaseIdCommand.ExecuteScalar());

                    // Отримати packaging_id за допомогою назви упаковки
                    var packagingIdQuery = "SELECT packaging_id FROM Packaging WHERE packaging_name = @PackagingName";
                    var packagingIdCommand = new SqlCommand(packagingIdQuery, database.getConnection());
                    packagingIdCommand.Parameters.AddWithValue("@PackagingName", packaging_name);
                    var packaging_id = Convert.ToInt32(packagingIdCommand.ExecuteScalar());

                    var changeQuery = "UPDATE Processing SET harvest_id = @HarvestId, phase_id = @PhaseId, " +
                                      "processing_date = @ProcessingDate, packaging_id = @PackagingId, quantity = @Quantity " +
                                      "WHERE processing_id = @ProcessingId";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.Parameters.AddWithValue("@HarvestId", harvest_id);
                    command.Parameters.AddWithValue("@PhaseId", phase_id);
                    command.Parameters.AddWithValue("@ProcessingDate", processing_date);
                    command.Parameters.AddWithValue("@PackagingId", packaging_id);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ProcessingId", processing_id);
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }

        private void ChangeProcessing()
        {
            if (dataGridView2.CurrentRow != null)
            {
                var selectedRowIndex = dataGridView2.CurrentRow.Index;
                var processing_id = textBox_packagingId.Text;
                var harvest_id = comboBox_harvestId.Text;
                var phase_id = comboBox_processingStage.Text;
                var packing_date = dateTimePicker_packingDate.Value;
                var packaging_id = comboBox_typePackaging.Text;
                var quantity = string.IsNullOrEmpty(textBox_quantity.Text) ? "0" : textBox_quantity.Text;

                if (!int.TryParse(quantity, out int result) || result < 0)
                {
                    MessageBox.Show("Будь ласка, введіть коректну кількість (ціле число більше або рівне 0).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(processing_id))
                {
                    dataGridView2.Rows[selectedRowIndex].SetValues(processing_id, harvest_id, phase_id, packing_date, packaging_id, quantity);
                    dataGridView2.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                }
            }
        }

        private void deleteProcessingRow()
        {
            int index = dataGridView2.CurrentCell?.RowIndex ?? -1;

            if (index >= 0 && index < dataGridView2.Rows.Count)
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Попередження!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var selectedRow = dataGridView2.Rows[index];

                    if (selectedRow != null)
                    {
                        selectedRow.Visible = false;

                        if (selectedRow.Cells[0].Value != null && string.IsNullOrEmpty(selectedRow.Cells[0].Value.ToString()))
                        {
                            selectedRow.Cells[6].Value = RowState.Deleted;
                            return;
                        }

                        selectedRow.Cells[6].Value = RowState.Deleted;
                    }
                }
            }
        }



        private void ClearProcessingFields()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridView2.Rows[0];

                textBox_packagingId.Text = firstRow.Cells["processing_id"].Value?.ToString() ?? "";
                comboBox_harvestId.Text = firstRow.Cells["harvest_id"].Value?.ToString() ?? "";
                comboBox_processingStage.Text = firstRow.Cells["phase_id"].Value?.ToString() ?? "";
                dateTimePicker_packingDate.Value = firstRow.Cells["processing_date"].Value != null
                    ? Convert.ToDateTime(firstRow.Cells["processing_date"].Value)
                    : DateTime.Now;
                comboBox_typePackaging.Text = firstRow.Cells["packaging_id"].Value?.ToString() ?? "";
                textBox_quantity.Text = firstRow.Cells["quantity"].Value?.ToString() ?? "";
            }
            else
            {
                textBox_packagingId.Text = "";
                comboBox_harvestId.SelectedIndex = -1;
                comboBox_processingStage.SelectedIndex = -1;
                dateTimePicker_packingDate.Value = DateTime.Now;
                comboBox_typePackaging.SelectedIndex = -1;
                textBox_quantity.Text = "";
            }
        }


        private void SearchProcessing(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = @"SELECT Processing.processing_id, Harvest.harvest_id, Phase.phase_name, 
                           Processing.processing_date, Packaging.packaging_name, Processing.quantity
                        FROM Processing
                        INNER JOIN Harvest ON Processing.harvest_id = Harvest.harvest_id
                        INNER JOIN Phase ON Processing.phase_id = Phase.phase_id
                        INNER JOIN Packaging ON Processing.packaging_id = Packaging.packaging_id
                        WHERE CONVERT(NVARCHAR, Processing.processing_id) LIKE @SearchText
                           OR CONVERT(NVARCHAR, Harvest.harvest_id) LIKE @SearchText
                           OR Phase.phase_name LIKE @SearchText
                           OR CONVERT(NVARCHAR, Processing.processing_date, 104) LIKE @SearchText
                           OR Packaging.packaging_name LIKE @SearchText
                           OR CONVERT(NVARCHAR, Processing.quantity) LIKE @SearchText";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            command.Parameters.AddWithValue("@SearchText", "%" + textBox_search2.Text + "%");

            database.openConnection();
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleProcessingRow(dgw, read);
            }

            read.Close();
        }

        private void button_plus2_Click_1(object sender, EventArgs e)
        {
            AddProcessing add_form = new AddProcessing();
            add_form.Show();
        }

        private void button_pen2_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?", "Підтвердження!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Перевірка наявності harvestId в базі даних
                string harvestIdText = comboBox_harvestId.Text;
                if (!string.IsNullOrEmpty(harvestIdText) && int.TryParse(harvestIdText, out int harvestId))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Harvest WHERE harvest_id = @HarvestId";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, database.getConnection()))
                    {
                        checkCmd.Parameters.AddWithValue("@HarvestId", harvestId);
                        database.openConnection();
                        int count = (int)checkCmd.ExecuteScalar();
                        database.closeConnection();

                        if (count > 0)
                        {
                            ChangeProcessing();
                            UpdateProcessing();
                        }
                        else
                        {
                            MessageBox.Show("Обраної партії не існує в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректне значення для партії.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button_trash2_Click_1(object sender, EventArgs e)
        {
            deleteProcessingRow();
            ClearProcessingFields();
            UpdateProcessing();
        }

        private void button_refresh2_Click_1(object sender, EventArgs e)
        {
            RefreshProcessingDataGrid(dataGridView2);
            ClearProcessingFields();
        }

        private void textBox_search2_TextChanged_1(object sender, EventArgs e)
        {
            SearchProcessing(dataGridView2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];

                textBox_packagingId.Text = row.Cells[0].Value.ToString();
                comboBox_harvestId.Text = row.Cells[1].Value.ToString();
                comboBox_processingStage.Text = row.Cells[2].Value.ToString();
                dateTimePicker_packingDate.Value = Convert.ToDateTime(row.Cells[3].Value);
                comboBox_typePackaging.Text = row.Cells[4].Value.ToString();
                textBox_quantity.Text = row.Cells[5].Value.ToString();
            }
        }

        //Вкладка Готова продукція-------------------------------------------------------------------------------------------------------------

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView3.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView3.Rows[selectedRowIndex];

                textBox_productId.Text = selectedRow.Cells[0].Value.ToString();
                comboBox_packagingCode.Text = selectedRow.Cells[1].Value.ToString();
                textBox_productName.Text = selectedRow.Cells[2].Value.ToString();
                textBox_productPrice.Text = selectedRow.Cells[3].Value.ToString();
                dateTimePicker_expirationDate.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
            }
        }

        private void InitializeComboBoxPackagingCode()
        {
            comboBox_packagingCode.DataSource = processingList;
            comboBox_packagingCode.DisplayMember = "Id";
        }

        private void LoadPackagingCodeList()
        {
            try
            {
                database.openConnection();

                string query = "SELECT processing_id FROM Processing";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int processing_id = reader.GetInt32(0);
                    processingList.Add(new ProcessingItem(processing_id));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні кодів фасування: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }
        }


        private void CreateColumnsProduct()
        {
            dataGridView3.Columns.Add("product_id", "Код продукції");
            dataGridView3.Columns.Add("processing_id", "Код фасування");
            dataGridView3.Columns.Add("product_name", "Назва продукції");
            dataGridView3.Columns.Add("price", "Ціна (од.)");
            dataGridView3.Columns.Add("expiration_date", "Термін придатності");
            dataGridView3.Columns.Add("balance", "Залишок");
            dataGridView3.Columns["expiration_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView3.Columns.Add("IsNew", String.Empty);

            dataGridView3.Columns["IsNew"].Visible = false;

            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.Gray;
                column.HeaderCell.Style.Font = new Font("Noto Sans", 13, FontStyle.Bold);
            }
        }

        private int GetProductBalance(int productId)
        {
            int balance = 0;

            try
            {
                database.openConnection();

                string query = @"SELECT pr.quantity FROM Product p
                        INNER JOIN Processing pr ON p.processing_id = pr.processing_id
                        WHERE p.product_id = @productId";

                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                cmd.Parameters.AddWithValue("@productId", productId);

                object result = cmd.ExecuteScalar();
                int totalQuantity = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                string sumQuery = @"SELECT SUM(quantity_goods) FROM Content_Order WHERE product_id = @productId";

                using (SqlCommand cmdSum = new SqlCommand(sumQuery, database.getConnection()))
                {
                    cmdSum.Parameters.AddWithValue("@productId", productId);
                    result = cmdSum.ExecuteScalar();
                    int totalOrderedQuantity = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    balance = totalQuantity - totalOrderedQuantity;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при обчисленні залишку товару: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }

            return balance;
        }


        private void RefreshProductDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            var queryString = "SELECT product_id, Processing.processing_id, product_name, price, expiration_date FROM Product " +
                              "INNER JOIN Processing ON Product.processing_id = Processing.processing_id";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleProductRow(dgw, reader);
            }

            reader.Close();
            database.closeConnection();

            UpdateProductBalance(dgw);

        }

        private void ReadSingleProductRow(DataGridView dgw, IDataRecord record)
        {
            int productId = record.GetInt32(0);
            int processingId = record.GetInt32(1);
            string productName = record.GetString(2);
            decimal price = record.GetDecimal(3);
            DateTime expirationDate = record.GetDateTime(4);

            dgw.Rows.Add(productId, processingId, productName, price, expirationDate, RowState.Existed);
        }

        private void UpdateProduct()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView3.Rows.Count; index++)
            {
                var rowState = dataGridView3.Rows[index].Cells[6].Value as RowState?;

                if (rowState == RowState.Existed)
                {
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    var product_id = dataGridView3.Rows[index].Cells[0].Value?.ToString();

                    if (!string.IsNullOrEmpty(product_id))
                    {
                        var deleteQuery = $"DELETE FROM Product WHERE product_id = {product_id}";

                        var command = new SqlCommand(deleteQuery, database.getConnection());
                        command.ExecuteNonQuery();
                    }
                }

                if (rowState == RowState.Modified)
                {
                    var product_id = dataGridView3.Rows[index].Cells[0].Value?.ToString();
                    var processing_id = dataGridView3.Rows[index].Cells[1].Value?.ToString();
                    var product_name = dataGridView3.Rows[index].Cells[2].Value?.ToString();
                    var price = dataGridView3.Rows[index].Cells[3].Value;
                    var expiration_date = dataGridView3.Rows[index].Cells[4].Value;

                    if (!string.IsNullOrEmpty(product_id) && !string.IsNullOrEmpty(processing_id) && !string.IsNullOrEmpty(product_name))
                    {
                        // Отримати processing_id за допомогою коду обробки
                        var processingIdQuery = "SELECT processing_id FROM Processing WHERE processing_id = @ProcessingId";
                        var processingIdCommand = new SqlCommand(processingIdQuery, database.getConnection());
                        processingIdCommand.Parameters.AddWithValue("@ProcessingId", processing_id);
                        var processing_id_value = Convert.ToInt32(processingIdCommand.ExecuteScalar());

                        var changeQuery = "UPDATE Product SET processing_id = @ProcessingId, product_name = @ProductName, " +
                                          "price = @Price, expiration_date = @ExpirationDate " +
                                          "WHERE product_id = @ProductId";

                        var command = new SqlCommand(changeQuery, database.getConnection());
                        command.Parameters.AddWithValue("@ProcessingId", processing_id_value);
                        command.Parameters.AddWithValue("@ProductName", product_name);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@ExpirationDate", expiration_date);
                        command.Parameters.AddWithValue("@ProductId", product_id);

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Помилка при оновленні продукту з кодом {product_id}: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            database.closeConnection();
        }


        private void ChangeProduct()
        {
            if (dataGridView3.CurrentRow != null)
            {
                // Отримання вибраного рядка в dataGridView3
                var selectedRowIndex = dataGridView3.CurrentRow.Index;

                // Отримання значень з полів форми
                var product_id = textBox_productId.Text;
                var packaging_id = comboBox_packagingCode.Text;
                var product_name = textBox_productName.Text;
                var priceText = textBox_productPrice.Text;

                // Перевірка наявності коду фасування в базі даних
                if (!string.IsNullOrEmpty(packaging_id) && int.TryParse(packaging_id, out int packagingCode))
                {
                    // Перевірка наявності ціни продукту
                    if (string.IsNullOrEmpty(priceText))
                    {
                        // Якщо ціна порожня, встановлюємо 0
                        priceText = "0";
                    }

                    // Перевірка, чи ціна є числом та більше або рівне 0
                    if (decimal.TryParse(priceText, out decimal price) && price >= 0)
                    {
                        // Отримання значення терміну придатності
                        var expiration_date = dateTimePicker_expirationDate.Value;

                        // Оновлення значень у вибраному рядку dataGridView3
                        if (!string.IsNullOrEmpty(product_id))
                        {
                            // Якщо редагуємо код фасування, перевіряємо його унікальність
                            if (dataGridView3.Rows[selectedRowIndex].Cells[1].Value?.ToString() != packaging_id)
                            {
                                bool packagingCodeExists = dataGridView3.Rows.Cast<DataGridViewRow>().Any(row => row.Cells[1].Value?.ToString() == packaging_id);

                                if (packagingCodeExists)
                                {
                                    MessageBox.Show("Запис з обраним кодом фасування вже існує в таблиці.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            dataGridView3.Rows[selectedRowIndex].SetValues(product_id, packaging_id, product_name, price, expiration_date);
                            dataGridView3.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, введіть коректну ціну для продукту.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректне значення для коду фасування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void deleteProductRow()
        {
            int index = dataGridView3.CurrentCell?.RowIndex ?? -1;

            if (index >= 0 && index < dataGridView3.Rows.Count)
            {
                var row = dataGridView3.Rows[index];

                if (row != null)
                {
                    var product_id = row.Cells[0].Value?.ToString();

                    if (!string.IsNullOrEmpty(product_id))
                    {
                        var result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Попередження!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            row.Visible = false;

                            if (string.IsNullOrEmpty(product_id))
                            {
                                row.Cells[6].Value = RowState.Deleted;
                                return;
                            }

                            row.Cells[6].Value = RowState.Deleted;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, оберіть рядок для видалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void ClearProductFields()
        {
            if (dataGridView3.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridView3.Rows[0];

                textBox_productId.Text = firstRow.Cells["product_id"].Value?.ToString() ?? "";
                comboBox_packagingCode.Text = firstRow.Cells["processing_id"].Value?.ToString() ?? "";
                textBox_productName.Text = firstRow.Cells["product_name"].Value?.ToString() ?? "";
                textBox_productPrice.Text = firstRow.Cells["price"].Value?.ToString() ?? "";
                dateTimePicker_expirationDate.Value = firstRow.Cells["expiration_date"].Value != null
                    ? Convert.ToDateTime(firstRow.Cells["expiration_date"].Value)
                    : DateTime.Now;
            }
            else
            {
                textBox_productId.Text = "";
                comboBox_packagingCode.SelectedIndex = -1;
                textBox_productName.Text = "";
                textBox_productPrice.Text = "";
                dateTimePicker_expirationDate.Value = DateTime.Now;
            }
        }


        private void SearchProduct(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = @"SELECT Product.product_id, Processing.processing_id, Product.product_name, 
                    Product.price, Product.expiration_date
                 FROM Product
                 INNER JOIN Processing ON Product.processing_id = Processing.processing_id
                 WHERE CONVERT(NVARCHAR, Product.product_id) LIKE @SearchText
                    OR CONVERT(NVARCHAR, Processing.processing_id) LIKE @SearchText
                    OR Product.product_name LIKE @SearchText
                    OR CONVERT(NVARCHAR, Product.price) LIKE @SearchText
                    OR CONVERT(NVARCHAR, Product.expiration_date, 104) LIKE @SearchText";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            command.Parameters.AddWithValue("@SearchText", "%" + textBox_search3.Text + "%");

            database.openConnection();
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleProductRow(dgw, read);
            }

            read.Close();
            UpdateProductBalance(dgw);
        }

        private void UpdateProductBalance(DataGridView dgw)
        {
            foreach (DataGridViewRow row in dgw.Rows)
            {
                int productId = Convert.ToInt32(row.Cells["product_id"].Value);
                int balance = GetProductBalance(productId);
                row.Cells["balance"].Value = balance;
            }
        }


        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[selectedRow];

                textBox_productId.Text = row.Cells[0].Value.ToString();
                comboBox_packagingCode.Text = row.Cells[1].Value.ToString();
                textBox_productName.Text = row.Cells[2].Value.ToString();
                textBox_productPrice.Text = row.Cells[3].Value.ToString();
                dateTimePicker_expirationDate.Value = Convert.ToDateTime(row.Cells[4].Value);
            }
        }

        private void button_plus3_Click_1(object sender, EventArgs e)
        {
            AddProduct addProductForm = new AddProduct();
            addProductForm.Show();
        }

        private void button_pen3_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?", "Підтвердження!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Перевірка наявності коду фасування в базі даних
                string packagingCodeText = comboBox_packagingCode.Text;
                if (!string.IsNullOrEmpty(packagingCodeText) && int.TryParse(packagingCodeText, out int packagingCode))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Processing WHERE processing_id = @PackagingCode";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, database.getConnection()))
                    {
                        checkCmd.Parameters.AddWithValue("@PackagingCode", packagingCode);
                        database.openConnection();
                        int count = (int)checkCmd.ExecuteScalar();
                        database.closeConnection();

                        if (count > 0)
                        {
                            ChangeProduct();
                            UpdateProduct();
                        }
                        else
                        {
                            MessageBox.Show("Обраного коду фасування не існує в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректне значення для коду фасування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button_trash3_Click_1(object sender, EventArgs e)
        {
            deleteProductRow();
            ClearProductFields();
            UpdateProduct();
        }

        private void button_refresh3_Click_1(object sender, EventArgs e)
        {
            RefreshProductDataGrid(dataGridView3);
            ClearProductFields();
        }

        private void textBox_search3_TextChanged_1(object sender, EventArgs e)
        {
            SearchProduct(dataGridView3);
        }



        //Вкладка Замовлення ---------------------------------------------------------------------------------------------------------------

        private void LoadClientList()
        {
            try
            {
                database.openConnection();

                string query = "SELECT client_id, client_name FROM Client";
                SqlCommand cmd = new SqlCommand(query, database.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int clientId = reader.GetInt32(0);
                    string clientName = reader.GetString(1);
                    clientList.Add(new ClientItem(clientId, clientName));
                }

                reader.Close();
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

        private void InitializeComboBoxClient()
        {
            comboBox_listCustomers.DataSource = clientList; // Встановлюємо джерело даних для comboBox
            comboBox_listCustomers.DisplayMember = "Name"; // Встановлюємо поле, яке буде відображатися
            comboBox_listCustomers.ValueMember = "Id"; // Встановлюємо значення, яке буде повертатися
        }


        private void ClearOrderFields()
        {
            if (dataGridView4.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridView4.Rows[0];

                textBox_orderNumber.Text = firstRow.Cells["order_id"].Value?.ToString() ?? "";
                comboBox_listCustomers.Text = firstRow.Cells["client_id"].Value?.ToString() ?? ""; // Встановлюємо значення за ідентифікатором клієнта
                dateTimePicker_orderDate.Value = firstRow.Cells["order_date"].Value != null
                    ? Convert.ToDateTime(firstRow.Cells["order_date"].Value)
                    : DateTime.Now;
            }
            else
            {
                textBox_orderNumber.Text = "";
                comboBox_listCustomers.SelectedIndex = -1;
                dateTimePicker_orderDate.Value = DateTime.Now;
            }
        }


        private void CreateColumnsOrders()
        {
            dataGridView4.Columns.Add("order_id", "№ Замовлення");
            dataGridView4.Columns.Add("client_id", "Клієнт"); // Замініть "client_id" на "client_name"
            dataGridView4.Columns.Add("order_date", "Дата замовлення");
            dataGridView4.Columns["order_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView4.Columns.Add("total_price", "Вартість замовлення");


            // Додайте колонку "IsNew" для відстеження стану рядка
            dataGridView4.Columns.Add("IsNew", String.Empty);
            dataGridView4.Columns["IsNew"].Visible = false; // Приховати колонку "IsNew"

            // Змініть властивості відображення заголовків колонок
            foreach (DataGridViewColumn column in dataGridView4.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.Gray;
                column.HeaderCell.Style.Font = new Font("Noto Sans", 13, FontStyle.Bold);
            }
        }

        private decimal GetTotalPriceForOrder(int orderId)
        {
            decimal totalPrice = 0;

            string queryString = @"SELECT SUM(p.price * co.quantity_goods) AS TotalPrice
                           FROM Product p
                           INNER JOIN Content_Order co ON p.product_id = co.product_id
                           WHERE co.order_id = @orderId";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            command.Parameters.AddWithValue("@orderId", orderId);

            try
            {
                database.openConnection();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalPrice = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при обчисленні вартості замовлення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                database.closeConnection();
            }

            return totalPrice;
        }


        private void RefreshOrdersDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = "SELECT o.order_id, c.client_name, o.order_date FROM Orders o INNER JOIN Client c ON o.client_id = c.client_id";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleOrder(dgw, reader); // Викликаємо метод ReadSingleOrder для додавання рядка у DataGridView
            }

            reader.Close();

            UpdateTotalPriceColumn(dgw);

            database.closeConnection();
        }


        private void ReadSingleOrder(DataGridView dgw, IDataRecord record)
        {
            int orderId = record.GetInt32(0); // Отримуємо номер замовлення
            string clientName = record.GetString(1); // Отримуємо ім'я клієнта
            DateTime orderDate = record.GetDateTime(2); // Отримуємо дату замовлення

            dgw.Rows.Add(orderId, clientName, orderDate, RowState.ModifiedNew); // Додаємо рядок до DataGridView
        }

        private void UpdateTotalPriceColumn(DataGridView dgw)
        {
            foreach (DataGridViewRow row in dgw.Rows)
            {
                int orderId = Convert.ToInt32(row.Cells["order_id"].Value);
                decimal totalPrice = GetTotalPriceForOrder(orderId);
                row.Cells["total_price"].Value = totalPrice;
            }
        }


        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[selectedRow];

                textBox_orderNumber.Text = row.Cells[0].Value.ToString();
                comboBox_listCustomers.Text = row.Cells[1].Value.ToString();
                dateTimePicker_orderDate.Value = Convert.ToDateTime(row.Cells[2].Value);
            }
        }



        private void SearchOrders(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"SELECT order_id, client_name, order_date FROM Orders INNER JOIN Client ON Orders.client_id = Client.client_id WHERE " +
                                   $"CONVERT(NVARCHAR, order_id) LIKE N'%{textBox_search4.Text}%' OR " +
                                   $"client_name LIKE N'%{textBox_search4.Text}%' OR " +
                                   $"CONVERT(NVARCHAR, order_date, 23) LIKE N'%{textBox_search4.Text}%' OR " +
                                   $"CONVERT(NVARCHAR, order_date, 104) LIKE N'%{textBox_search4.Text}%' OR " +
                                   $"CONVERT(NVARCHAR, order_date, 4) LIKE N'%{textBox_search4.Text}%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleOrder(dgw, reader); // Викликаємо метод ReadSingleOrder для додавання рядка у DataGridView
            }
            reader.Close();
            UpdateTotalPriceColumn(dgw);
        }


        private void deleteOrder()
        {
            int index = dataGridView4.CurrentCell?.RowIndex ?? -1;

            if (index >= 0 && index < dataGridView4.Rows.Count)
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Попередження!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var selectedRow = dataGridView4.Rows[index];

                    if (selectedRow != null)
                    {
                        selectedRow.Visible = false;

                        if (selectedRow.Cells["order_id"].Value != null && string.IsNullOrEmpty(selectedRow.Cells["order_id"].Value.ToString()))
                        {
                            selectedRow.Cells["IsNew"].Value = RowState.Deleted;
                            return;
                        }

                        selectedRow.Cells["IsNew"].Value = RowState.Deleted;
                    }
                }
            }
        }


        private void UpdateOrders()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView4.Rows.Count; index++)
            {
                var rowState = dataGridView4.Rows[index].Cells[4].Value as RowState?;

                if (rowState == RowState.Existed)
                {
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    if (dataGridView4.Rows[index].Cells[0].Value != null)
                    {
                        var orderId = Convert.ToInt32(dataGridView4.Rows[index].Cells[0].Value);
                        var deleteQuery = "DELETE FROM Orders WHERE order_id = @OrderId";

                        var command = new SqlCommand(deleteQuery, database.getConnection());
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.ExecuteNonQuery();
                    }
                }

                if (rowState == RowState.Modified)
                {
                    var orderId = dataGridView4.Rows[index].Cells[0].Value;
                    var clientName = dataGridView4.Rows[index].Cells[1].Value?.ToString();
                    var orderDate = dataGridView4.Rows[index].Cells[2].Value;

                    if (orderId == null || string.IsNullOrEmpty(clientName) || orderDate == null)
                    {
                        continue;
                    }

                   
                    var changeQuery = "UPDATE Orders SET client_id = @ClientId, order_date = @OrderDate WHERE order_id = @OrderId";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    var clientId = GetClientIdByName(clientName);

                    command.Parameters.AddWithValue("@ClientId", clientId);                    
                    command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = Convert.ToDateTime(orderDate);
                    command.Parameters.AddWithValue("@OrderId", Convert.ToInt32(orderId));
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }

        private int GetClientIdByName(string clientName)
        {
            foreach (ClientItem client in clientList)
            {
                if (client.Name == clientName)
                {
                    return client.Id;
                }
            }

            return -1; // Повернути -1, якщо ідентифікатор не знайдено
        }


        private void ChangeOrder()
        {
            if (dataGridView4.CurrentRow != null)
            {
                var selectedRowIndex = dataGridView4.CurrentRow.Index;

                // Отримання значень з елементів керування форми
                var orderId = Convert.ToInt32(textBox_orderNumber.Text);
                var clientName = comboBox_listCustomers.Text;
                var orderDate = dateTimePicker_orderDate.Value;

                dataGridView4.Rows[selectedRowIndex].SetValues(orderId, clientName, orderDate);
                dataGridView4.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
            }
        }



        private void button_client_Click(object sender, EventArgs e)
        {
            СustomerBase customerBaseForm = new СustomerBase();
            customerBaseForm.Show();

        }

        private void button_plus4_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            addOrder.Show();
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView4.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView4.Rows[selectedRowIndex];

                textBox_orderNumber.Text = selectedRow.Cells[0].Value.ToString();
                comboBox_listCustomers.Text = selectedRow.Cells[1].Value.ToString();
                dateTimePicker_orderDate.Value = Convert.ToDateTime(selectedRow.Cells[2].Value);

            }
        }




        private void button_pen4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете зберегти зміни?", "Підтвердження!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ChangeOrder();
                UpdateOrders();
            }
        }

        private void button_trash4_Click(object sender, EventArgs e)
        {
            deleteOrder();
            ClearOrderFields();
            UpdateOrders();
        }

        private void button_refresh4_Click(object sender, EventArgs e)
        {
            RefreshOrdersDataGrid(dataGridView4);
            ClearOrderFields();
        }

        private void textBox_search4_TextChanged(object sender, EventArgs e)
        {
            SearchOrders(dataGridView4);
        }

        private void button_view_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи вибрано який-небудь рядок у dataGridView4
            if (dataGridView4.SelectedCells.Count > 0)
            {
                // Отримуємо індекс обраного рядка
                int selectedRowIndex = dataGridView4.SelectedCells[0].RowIndex;

                // Отримуємо значення client_id з обраного рядка
                int clientId = Convert.ToInt32(dataGridView4.Rows[selectedRowIndex].Cells[0].Value);

                // Створюємо екземпляр форми FillOrder, передаючи значення clientId у конструктор
                FillOrder fillOrder = new FillOrder(clientId);

                // Показуємо форму FillOrder
                fillOrder.Show();
            }
            else
            {
                // Якщо жоден рядок не був вибраний, виводимо повідомлення
                MessageBox.Show("Будь ласка, виберіть замовлення для перегляду.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

namespace HighBerry
{
    partial class СustomerBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СustomerBase));
            this.panel14 = new System.Windows.Forms.Panel();
            this.button_plus = new System.Windows.Forms.Button();
            this.button_pen = new System.Windows.Forms.Button();
            this.button_trash = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.dataGridViewClient = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_customerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_EDRPOU = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox_contacts = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_ClientId = new System.Windows.Forms.TextBox();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClient)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.BackColor = System.Drawing.Color.Crimson;
            this.panel14.Controls.Add(this.button_plus);
            this.panel14.Controls.Add(this.button_pen);
            this.panel14.Controls.Add(this.button_trash);
            this.panel14.Controls.Add(this.button_refresh);
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1422, 80);
            this.panel14.TabIndex = 16;
            // 
            // button_plus
            // 
            this.button_plus.BackColor = System.Drawing.Color.Crimson;
            this.button_plus.BackgroundImage = global::HighBerry.Properties.Resources.plus;
            this.button_plus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_plus.FlatAppearance.BorderSize = 0;
            this.button_plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_plus.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.button_plus.ForeColor = System.Drawing.Color.White;
            this.button_plus.Location = new System.Drawing.Point(30, 18);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(44, 44);
            this.button_plus.TabIndex = 23;
            this.button_plus.UseVisualStyleBackColor = false;
            this.button_plus.Click += new System.EventHandler(this.button_plus_Click);
            // 
            // button_pen
            // 
            this.button_pen.BackColor = System.Drawing.Color.Crimson;
            this.button_pen.BackgroundImage = global::HighBerry.Properties.Resources.pen;
            this.button_pen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_pen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_pen.FlatAppearance.BorderSize = 0;
            this.button_pen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pen.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.button_pen.ForeColor = System.Drawing.Color.White;
            this.button_pen.Location = new System.Drawing.Point(90, 18);
            this.button_pen.Name = "button_pen";
            this.button_pen.Size = new System.Drawing.Size(44, 44);
            this.button_pen.TabIndex = 22;
            this.button_pen.UseVisualStyleBackColor = false;
            this.button_pen.Click += new System.EventHandler(this.button_pen_Click);
            // 
            // button_trash
            // 
            this.button_trash.BackColor = System.Drawing.Color.Crimson;
            this.button_trash.BackgroundImage = global::HighBerry.Properties.Resources.trash;
            this.button_trash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_trash.FlatAppearance.BorderSize = 0;
            this.button_trash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_trash.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.button_trash.ForeColor = System.Drawing.Color.White;
            this.button_trash.Location = new System.Drawing.Point(150, 18);
            this.button_trash.Name = "button_trash";
            this.button_trash.Size = new System.Drawing.Size(44, 44);
            this.button_trash.TabIndex = 21;
            this.button_trash.UseVisualStyleBackColor = false;
            this.button_trash.Click += new System.EventHandler(this.button_trash_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.BackgroundImage = global::HighBerry.Properties.Resources.arrow_rotate;
            this.button_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_refresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_refresh.FlatAppearance.BorderSize = 0;
            this.button_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_refresh.Location = new System.Drawing.Point(210, 18);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(44, 44);
            this.button_refresh.TabIndex = 3;
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // panel15
            // 
            this.panel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel15.BackColor = System.Drawing.SystemColors.Window;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel15.Controls.Add(this.textBox_search);
            this.panel15.Controls.Add(this.pictureBox3);
            this.panel15.Location = new System.Drawing.Point(1037, 20);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(345, 40);
            this.panel15.TabIndex = 2;
            // 
            // textBox_search
            // 
            this.textBox_search.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_search.Font = new System.Drawing.Font("Noto Sans", 13.8F);
            this.textBox_search.Location = new System.Drawing.Point(11, 3);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(289, 32);
            this.textBox_search.TabIndex = 0;
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox3.Image = global::HighBerry.Properties.Resources.search_icon;
            this.pictureBox3.Location = new System.Drawing.Point(304, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.pictureBox3.Size = new System.Drawing.Size(34, 34);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // dataGridViewClient
            // 
            this.dataGridViewClient.AllowUserToAddRows = false;
            this.dataGridViewClient.AllowUserToDeleteRows = false;
            this.dataGridViewClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewClient.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewClient.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewClient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewClient.ColumnHeadersHeight = 35;
            this.dataGridViewClient.Location = new System.Drawing.Point(335, 86);
            this.dataGridViewClient.Name = "dataGridViewClient";
            this.dataGridViewClient.ReadOnly = true;
            this.dataGridViewClient.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Noto Sans", 13.2F);
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridViewClient.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewClient.RowTemplate.Height = 24;
            this.dataGridViewClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClient.Size = new System.Drawing.Size(1075, 705);
            this.dataGridViewClient.TabIndex = 27;
            this.dataGridViewClient.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClient_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.textBox_customerName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 100);
            this.panel1.TabIndex = 29;
            // 
            // textBox_customerName
            // 
            this.textBox_customerName.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.textBox_customerName.Location = new System.Drawing.Point(20, 45);
            this.textBox_customerName.Name = "textBox_customerName";
            this.textBox_customerName.Size = new System.Drawing.Size(275, 35);
            this.textBox_customerName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "Назва клієнта:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.textBox_EDRPOU);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 100);
            this.panel2.TabIndex = 30;
            // 
            // textBox_EDRPOU
            // 
            this.textBox_EDRPOU.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.textBox_EDRPOU.Location = new System.Drawing.Point(20, 45);
            this.textBox_EDRPOU.Name = "textBox_EDRPOU";
            this.textBox_EDRPOU.Size = new System.Drawing.Size(275, 35);
            this.textBox_EDRPOU.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "ЄДРПОУ:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.textBox_address);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 404);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(317, 100);
            this.panel3.TabIndex = 31;
            // 
            // textBox_address
            // 
            this.textBox_address.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.textBox_address.Location = new System.Drawing.Point(20, 45);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(275, 35);
            this.textBox_address.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Адреса:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel4.Controls.Add(this.textBox_contacts);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(12, 510);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(317, 100);
            this.panel4.TabIndex = 32;
            // 
            // textBox_contacts
            // 
            this.textBox_contacts.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.textBox_contacts.Location = new System.Drawing.Point(20, 45);
            this.textBox_contacts.Name = "textBox_contacts";
            this.textBox_contacts.Size = new System.Drawing.Size(275, 35);
            this.textBox_contacts.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 30);
            this.label4.TabIndex = 4;
            this.label4.Text = "Контактні дані:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(15, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(138, 30);
            this.label16.TabIndex = 4;
            this.label16.Text = "ID Клієнта:";
            // 
            // textBox_ClientId
            // 
            this.textBox_ClientId.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.textBox_ClientId.Location = new System.Drawing.Point(20, 45);
            this.textBox_ClientId.Name = "textBox_ClientId";
            this.textBox_ClientId.ReadOnly = true;
            this.textBox_ClientId.Size = new System.Drawing.Size(275, 35);
            this.textBox_ClientId.TabIndex = 8;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel23.Controls.Add(this.textBox_ClientId);
            this.panel23.Controls.Add(this.label16);
            this.panel23.Location = new System.Drawing.Point(12, 86);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(317, 100);
            this.panel23.TabIndex = 28;
            // 
            // СustomerBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1422, 803);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel23);
            this.Controls.Add(this.dataGridViewClient);
            this.Controls.Add(this.panel14);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "СustomerBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Довідник Клієнтів";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.СustomerBase_Load);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClient)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.Button button_pen;
        private System.Windows.Forms.Button button_trash;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.DataGridView dataGridViewClient;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_customerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_EDRPOU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox_contacts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_ClientId;
        private System.Windows.Forms.Panel panel23;
    }
}
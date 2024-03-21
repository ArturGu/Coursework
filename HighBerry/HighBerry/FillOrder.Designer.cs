namespace HighBerry
{
    partial class FillOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillOrder));
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_trash = new System.Windows.Forms.Button();
            this.button_plus = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_orderNumber = new System.Windows.Forms.Label();
            this.dataGridViewContent = new System.Windows.Forms.DataGridView();
            this.label_totalPrice = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Crimson;
            this.panel2.Controls.Add(this.button_trash);
            this.panel2.Controls.Add(this.button_plus);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label_orderNumber);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1423, 80);
            this.panel2.TabIndex = 39;
            // 
            // button_trash
            // 
            this.button_trash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_trash.BackColor = System.Drawing.Color.Crimson;
            this.button_trash.BackgroundImage = global::HighBerry.Properties.Resources.trash;
            this.button_trash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_trash.FlatAppearance.BorderSize = 0;
            this.button_trash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_trash.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.button_trash.ForeColor = System.Drawing.Color.White;
            this.button_trash.Location = new System.Drawing.Point(1349, 18);
            this.button_trash.Name = "button_trash";
            this.button_trash.Size = new System.Drawing.Size(44, 44);
            this.button_trash.TabIndex = 26;
            this.button_trash.UseVisualStyleBackColor = false;
            this.button_trash.Click += new System.EventHandler(this.button_trash_Click);
            // 
            // button_plus
            // 
            this.button_plus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_plus.BackColor = System.Drawing.Color.Crimson;
            this.button_plus.BackgroundImage = global::HighBerry.Properties.Resources.plus;
            this.button_plus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_plus.FlatAppearance.BorderSize = 0;
            this.button_plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_plus.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.button_plus.ForeColor = System.Drawing.Color.White;
            this.button_plus.Location = new System.Drawing.Point(1289, 18);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(44, 44);
            this.button_plus.TabIndex = 24;
            this.button_plus.UseVisualStyleBackColor = false;
            this.button_plus.Click += new System.EventHandler(this.button_plus_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HighBerry.Properties.Resources.box_icon;
            this.pictureBox1.Location = new System.Drawing.Point(30, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label_orderNumber
            // 
            this.label_orderNumber.AutoSize = true;
            this.label_orderNumber.Font = new System.Drawing.Font("Noto Sans", 16.2F, System.Drawing.FontStyle.Bold);
            this.label_orderNumber.ForeColor = System.Drawing.Color.White;
            this.label_orderNumber.Location = new System.Drawing.Point(86, 19);
            this.label_orderNumber.Name = "label_orderNumber";
            this.label_orderNumber.Size = new System.Drawing.Size(252, 38);
            this.label_orderNumber.TabIndex = 0;
            this.label_orderNumber.Text = "Замовлення №: ";
            // 
            // dataGridViewContent
            // 
            this.dataGridViewContent.AllowUserToAddRows = false;
            this.dataGridViewContent.AllowUserToDeleteRows = false;
            this.dataGridViewContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewContent.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewContent.ColumnHeadersHeight = 35;
            this.dataGridViewContent.Location = new System.Drawing.Point(30, 112);
            this.dataGridViewContent.Name = "dataGridViewContent";
            this.dataGridViewContent.ReadOnly = true;
            this.dataGridViewContent.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Noto Sans", 13.2F);
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridViewContent.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewContent.RowTemplate.Height = 24;
            this.dataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewContent.Size = new System.Drawing.Size(1363, 542);
            this.dataGridViewContent.TabIndex = 40;
            this.dataGridViewContent.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewContent_RowsAdded);
            // 
            // label_totalPrice
            // 
            this.label_totalPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_totalPrice.AutoSize = true;
            this.label_totalPrice.Font = new System.Drawing.Font("Noto Sans", 13.2F, System.Drawing.FontStyle.Bold);
            this.label_totalPrice.ForeColor = System.Drawing.Color.Black;
            this.label_totalPrice.Location = new System.Drawing.Point(45, 685);
            this.label_totalPrice.Name = "label_totalPrice";
            this.label_totalPrice.Size = new System.Drawing.Size(229, 30);
            this.label_totalPrice.TabIndex = 44;
            this.label_totalPrice.Text = "Загальна вартість:";
            // 
            // FillOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1422, 753);
            this.Controls.Add(this.label_totalPrice);
            this.Controls.Add(this.dataGridViewContent);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FillOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = " ";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_orderNumber;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.DataGridView dataGridViewContent;
        private System.Windows.Forms.Label label_totalPrice;
        private System.Windows.Forms.Button button_trash;
    }
}
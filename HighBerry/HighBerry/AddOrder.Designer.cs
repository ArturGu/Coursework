namespace HighBerry
{
    partial class AddOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrder));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_addClient = new System.Windows.Forms.Button();
            this.comboBox_clientList = new System.Windows.Forms.ComboBox();
            this.button_next = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Crimson;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button_addClient);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(724, 80);
            this.panel2.TabIndex = 24;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HighBerry.Properties.Resources.handshake;
            this.pictureBox1.Location = new System.Drawing.Point(50, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(105, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Оберіть клієнта із списку";
            // 
            // button_addClient
            // 
            this.button_addClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addClient.AutoSize = true;
            this.button_addClient.BackColor = System.Drawing.Color.Crimson;
            this.button_addClient.BackgroundImage = global::HighBerry.Properties.Resources.user_plus1;
            this.button_addClient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_addClient.FlatAppearance.BorderSize = 0;
            this.button_addClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_addClient.Font = new System.Drawing.Font("Noto Sans", 10.8F, System.Drawing.FontStyle.Bold);
            this.button_addClient.ForeColor = System.Drawing.Color.White;
            this.button_addClient.Location = new System.Drawing.Point(630, 18);
            this.button_addClient.Name = "button_addClient";
            this.button_addClient.Size = new System.Drawing.Size(44, 44);
            this.button_addClient.TabIndex = 34;
            this.button_addClient.UseVisualStyleBackColor = false;
            this.button_addClient.Click += new System.EventHandler(this.button_addClient_Click);
            // 
            // comboBox_clientList
            // 
            this.comboBox_clientList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_clientList.BackColor = System.Drawing.Color.White;
            this.comboBox_clientList.Font = new System.Drawing.Font("Noto Sans", 12F);
            this.comboBox_clientList.FormattingEnabled = true;
            this.comboBox_clientList.Location = new System.Drawing.Point(50, 120);
            this.comboBox_clientList.Name = "comboBox_clientList";
            this.comboBox_clientList.Size = new System.Drawing.Size(624, 35);
            this.comboBox_clientList.TabIndex = 30;
            // 
            // button_next
            // 
            this.button_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_next.BackColor = System.Drawing.Color.Crimson;
            this.button_next.FlatAppearance.BorderSize = 0;
            this.button_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_next.Font = new System.Drawing.Font("Noto Sans", 10.8F, System.Drawing.FontStyle.Bold);
            this.button_next.ForeColor = System.Drawing.Color.White;
            this.button_next.Location = new System.Drawing.Point(498, 207);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(176, 45);
            this.button_next.TabIndex = 32;
            this.button_next.Text = "Продовжити";
            this.button_next.UseVisualStyleBackColor = false;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.BackColor = System.Drawing.Color.Crimson;
            this.button_cancel.FlatAppearance.BorderSize = 0;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("Noto Sans", 10.8F, System.Drawing.FontStyle.Bold);
            this.button_cancel.ForeColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(297, 207);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(176, 45);
            this.button_cancel.TabIndex = 35;
            this.button_cancel.Text = "Скасувати";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(724, 293);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.comboBox_clientList);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Створення замовлення";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_clientList;
        private System.Windows.Forms.Button button_addClient;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_cancel;
    }
}
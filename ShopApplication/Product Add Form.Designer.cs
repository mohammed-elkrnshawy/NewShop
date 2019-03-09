namespace ShopApplication
{
    partial class Product_Add_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_SPrice = new System.Windows.Forms.TextBox();
            this.txt_Price = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.bt_edit = new System.Windows.Forms.Button();
            this.bt_save = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1386, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "اضافة و تعديل الاصناف";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(123, 354);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(196, 20);
            this.txtQuantity.TabIndex = 18;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(371, 346);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 30);
            this.label6.TabIndex = 17;
            this.label6.Text = "الكمية";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_SPrice
            // 
            this.txt_SPrice.Location = new System.Drawing.Point(123, 311);
            this.txt_SPrice.Name = "txt_SPrice";
            this.txt_SPrice.Size = new System.Drawing.Size(196, 20);
            this.txt_SPrice.TabIndex = 14;
            this.txt_SPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_SPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SPrice_KeyPress);
            // 
            // txt_Price
            // 
            this.txt_Price.Location = new System.Drawing.Point(123, 267);
            this.txt_Price.Name = "txt_Price";
            this.txt_Price.Size = new System.Drawing.Size(196, 20);
            this.txt_Price.TabIndex = 13;
            this.txt_Price.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Price_KeyPress);
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(123, 225);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(196, 20);
            this.txt_Name.TabIndex = 12;
            this.txt_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(371, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "سعر البيع";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(371, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 30);
            this.label3.TabIndex = 10;
            this.label3.Text = "سعر الشراء";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtQuantity);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txt_SPrice);
            this.panel3.Controls.Add(this.txt_Price);
            this.panel3.Controls.Add(this.txt_Name);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(650, 560);
            this.panel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(371, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "اسم الصنف";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(189)))), ((int)(((byte)(212)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(647, 470);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Location = new System.Drawing.Point(-1, 98);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(647, 470);
            this.panel5.TabIndex = 20;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(518, 38);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 20);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(108, 38);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(404, 20);
            this.textBox6.TabIndex = 20;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.textBox6);
            this.panel6.Location = new System.Drawing.Point(-1, 11);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(650, 81);
            this.panel6.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(736, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(650, 560);
            this.panel4.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1386, 680);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1386, 652);
            this.panel2.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.button3);
            this.panel7.Controls.Add(this.bt_edit);
            this.panel7.Controls.Add(this.bt_save);
            this.panel7.Location = new System.Drawing.Point(0, 575);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1386, 63);
            this.panel7.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(189)))), ((int)(((byte)(212)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(280, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "الغاء";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // bt_edit
            // 
            this.bt_edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(189)))), ((int)(((byte)(212)))));
            this.bt_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_edit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_edit.ForeColor = System.Drawing.Color.White;
            this.bt_edit.Location = new System.Drawing.Point(181, 12);
            this.bt_edit.Name = "bt_edit";
            this.bt_edit.Size = new System.Drawing.Size(81, 39);
            this.bt_edit.TabIndex = 1;
            this.bt_edit.Text = "تعديل";
            this.bt_edit.UseVisualStyleBackColor = false;
            this.bt_edit.Click += new System.EventHandler(this.bt_edit_Click);
            // 
            // bt_save
            // 
            this.bt_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(189)))), ((int)(((byte)(212)))));
            this.bt_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_save.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_save.ForeColor = System.Drawing.Color.White;
            this.bt_save.Location = new System.Drawing.Point(85, 12);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(81, 39);
            this.bt_save.TabIndex = 0;
            this.bt_save.Text = "حفظ";
            this.bt_save.UseVisualStyleBackColor = false;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // Product_Add_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1386, 680);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Product_Add_Form";
            this.Text = "Product_Add_Form";
            this.Load += new System.EventHandler(this.Product_Add_Form_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_SPrice;
        private System.Windows.Forms.TextBox txt_Price;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button bt_edit;
        private System.Windows.Forms.Button bt_save;
    }
}
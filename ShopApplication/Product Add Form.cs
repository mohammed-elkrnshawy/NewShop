﻿using KeepAutomation.Barcode.Crystal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApplication
{
    public partial class Product_Add_Form : Form
    {
        private DataSet ds;
        private int Customer_ID;

        public Product_Add_Form()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void Product_Add_Form_Load(object sender, EventArgs e)
        {
            RefForm();
           dataGridView1.Columns[0].AutoSizeMode
                = dataGridView1.Columns[6].AutoSizeMode
                = dataGridView1.Columns[3].AutoSizeMode
                = dataGridView1.Columns[4].AutoSizeMode
                = dataGridView1.Columns[5].AutoSizeMode
                = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RefForm()
        {
            txtQuantity.Enabled = true;
            txtCode.Enabled = true;
            bt_save.Enabled = true;
            bt_edit.Enabled = false;


            txt_Name.Text =txtCode.Text = "";
            txt_Price.Text=txtQuantity.Text=txt_SPrice.Text = "0";

            using (ds = Ezzat.GetDataSet("Product_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }

            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ValidDataSave()
        {
            if (ValidText(txt_Name) && ValidText(txt_Price) && ValidText(txt_SPrice) && ValidText(txtQuantity) && ValidText(txtCode))
            {
                SaveData();
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("insert_Product",
                    new SqlParameter("@Product_Name", txt_Name.Text),
                    new SqlParameter("@Product_Price", double.Parse(txt_Price.Text)),
                    new SqlParameter("@Product_Sell", double.Parse(txt_SPrice.Text)),
                    new SqlParameter("@Product_Quantity", int.Parse(txtQuantity.Text)),
                    new SqlParameter("@Product_Code", txtCode.Text)
                    );

            MessageBox.Show(SharedClass.Successful_Message);
        }

        private bool ValidText(TextBox text)
        {
            if (text.Text == "")
                return false;
            else
                return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtQuantity.Enabled = false;
            txtCode.Enabled = false;
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Product_selectSearch_BYID",
                out con, new SqlParameter("@Product_ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_Name.Text = dataReader["Product_Name"].ToString();
                    txt_Price.Text = dataReader["Product_Price"].ToString();
                    txt_SPrice.Text = dataReader["Product_Sell"].ToString();
                    txtQuantity.Text = dataReader["Product_Quantity"].ToString();
                    txtCode.Text = dataReader["Product_Code"].ToString();

                }
            }
            con.Close();


        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            ValidDataEdit();
        }

        private void ValidDataEdit()
        {
            if (ValidText(txt_Name) && ValidText(txt_Price) && ValidText(txt_SPrice))
            {
                EditData();
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void EditData()
        {
            Ezzat.ExecutedNoneQuery("Product_updateProduct"
                , new SqlParameter("@Product_Name", txt_Name.Text)
                , new SqlParameter("@Product_Price",double.Parse(txt_Price.Text))
                , new SqlParameter("@Product_Sell",double.Parse(txt_SPrice.Text))
                , new SqlParameter("@Product_ID", Customer_ID)
                );
            MessageBox.Show(SharedClass.Edit_Message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Product_selectSearch", "X", new SqlParameter("text", textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void txt_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_Price, e);
        }

        private void txt_SPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_SPrice, e);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtQuantity, e);
        }

        private void txt_SPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtCode, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barcode= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string name= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string price= dataGridView1.CurrentRow.Cells[5].Value.ToString();

            Shared_Barcode_Form barcode_Form = new Shared_Barcode_Form(barcode, name, double.Parse(price));
            barcode_Form.ShowDialog();

        }
    }
}

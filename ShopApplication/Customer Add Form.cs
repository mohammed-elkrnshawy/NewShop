using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApplication
{
    public partial class Customer_Add_Form : Form
    {
        private DataSet ds;
        private int Customer_ID;

        public Customer_Add_Form()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if (ValidText(tb_name) && ValidText(tb_phone) && ValidText(txtAddress) && ValidText(tb_phone) && ValidText(txtMoney))
            {
                SaveData();
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void RefForm()
        {
            txtMoney.Enabled = true;
            bt_save.Enabled = true;
            bt_edit.Enabled = false;
          

            tb_name.Text = tb_phone.Text = txtAddress.Text= "";
            txtMoney.Text = "0";

            using (ds = Ezzat.GetDataSet("Customer_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("insert_Customer",
                    new SqlParameter("@customerName", tb_name.Text),
                    new SqlParameter("@customerphone", tb_phone.Text),
                    new SqlParameter("@customerAddress", txtAddress.Text),
                    new SqlParameter("@customerMoney", double.Parse(txtMoney.Text))
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Customer_selectSearch", "X", new SqlParameter("text", textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMoney.Enabled = false;
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Customer_selectSearch_BYID", out con, new SqlParameter("@Customer_Id", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    tb_name.Text = dataReader["Customer_Name"].ToString();
                    txtAddress.Text = dataReader["Customer_Address"].ToString();
                    txtMoney.Text = dataReader["Customer_Money"].ToString();
                    tb_phone.Text = dataReader["Customer_Phone"].ToString();

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
            if (ValidText(tb_name) && ValidText(txtAddress) && ValidText(tb_phone))
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
            Ezzat.ExecutedNoneQuery("Customer_updateCustomer"
                , new SqlParameter("@Customer_Name", tb_name.Text)
                , new SqlParameter("@Customer_Address", txtAddress.Text)
                , new SqlParameter("@Customer_Phone", tb_phone.Text)
                , new SqlParameter("@Customer_ID", Customer_ID)
                );
            MessageBox.Show(SharedClass.Edit_Message);
        }

        private void Customer_Add_Form_Load(object sender, EventArgs e)
        {
            RefForm();
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

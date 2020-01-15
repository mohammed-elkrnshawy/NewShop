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
    public partial class Supplier_Add_Form : Form
    {
        private DataSet ds;
        private int Customer_ID;

        public Supplier_Add_Form()
        {
            InitializeComponent();
        }

        private void Supplier_Add_Form_Load(object sender, EventArgs e)
        {
            RefForm();
           dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RefForm()
        {
            txt_Money.Enabled = true;
            bt_save.Enabled = true;
            bt_edit.Enabled = false;


            txt_Name.Text=txt_Phone.Text=txt_Address.Text = "";
            txt_Money.Text = "0";

            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void bt_save_Click_1(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if (ValidText(txt_Name) && ValidText(txt_Address) && ValidText(txt_Money) && ValidText(txt_Phone))
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
            Ezzat.ExecutedNoneQuery("insert_Supplier",
                    new SqlParameter("@Supplier_Name", txt_Name.Text),
                    new SqlParameter("@Supplier_Address", txt_Address.Text),
                    new SqlParameter("@Supplier_Phone", txt_Phone.Text),
                    new SqlParameter("@total", double.Parse(txt_Money.Text))
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
            txt_Money.Enabled = false;
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Supplier_selectSearch_BYID", out con, new SqlParameter("@Supplier_ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_Name.Text = dataReader["Supplier_Name"].ToString();
                    txt_Address.Text = dataReader["Supplier_Address"].ToString();
                    txt_Phone.Text = dataReader["Supplier_Phone"].ToString();
                    txt_Money.Text = dataReader["Supplier_Money"].ToString();

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
            if (ValidText(txt_Name) && ValidText(txt_Address) && ValidText(txt_Phone))
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
            Ezzat.ExecutedNoneQuery("Supplier_updateSupplier"
                , new SqlParameter("@Supplier_Name", txt_Name.Text)
                , new SqlParameter("@Supplier_Address", txt_Address.Text)
                , new SqlParameter("@Supplier_Phone", txt_Phone.Text)
                , new SqlParameter("@Supplier_ID", Customer_ID)
                );
            MessageBox.Show(SharedClass.Edit_Message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Supplier_selectSearch", "X", new SqlParameter("@text", textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void txt_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_Phone, e);
        }

        private void txt_Money_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_Money, e);
        }
    }
}

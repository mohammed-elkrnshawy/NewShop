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
    public partial class User_Add_Form : Form
    {
        private DataSet ds;
        private int Customer_ID;

        public User_Add_Form()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if (ValidText(tb_name) && ValidText(tb_phone))
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
            bt_save.Enabled = true;
            bt_edit.Enabled = false;


            tb_name.Text = tb_phone.Text = "";

            using (ds = Ezzat.GetDataSet("Users_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("insert_Users",
                    new SqlParameter("@Username", tb_name.Text),
                    new SqlParameter("@Password", tb_phone.Text),
                    new SqlParameter("@customerType", radioButton1.Checked)
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

        private void User_Add_Form_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Users_selectSearch", "X", new SqlParameter("@text", textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Users_selectSearch_BYID", out con, new SqlParameter("@user_ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    tb_name.Text = dataReader["Username"].ToString();
                    tb_phone.Text = dataReader["Password"].ToString();
                    bool isAdmin = (bool)dataReader["isAdmin"];
                    if (isAdmin)
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
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
            if (ValidText(tb_name) && ValidText(tb_phone))
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
            Ezzat.ExecutedNoneQuery("User_updateUser"
                , new SqlParameter("@Username", tb_name.Text)
                , new SqlParameter("@userType", radioButton1.Checked)
                , new SqlParameter("@Password", tb_phone.Text)
                , new SqlParameter("@user_ID", Customer_ID)
                );
            MessageBox.Show(SharedClass.Edit_Message);
        }
    }
}

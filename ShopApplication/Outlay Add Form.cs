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
    public partial class Outlay_Add_Form : Form
    {
        public Outlay_Add_Form()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if(txt_Name.Text!="")
            {
                Ezzat.ExecutedNoneQuery("insert_Outlay", new SqlParameter("@outlay_name", txt_Name.Text));
                MessageBox.Show(SharedClass.Successful_Message);
                txt_Name.Text = "";
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }
    }
}

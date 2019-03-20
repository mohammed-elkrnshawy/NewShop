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
    public partial class Supplier_Transaction_Form : Form
    {
        DataSet ds;

        public Supplier_Transaction_Form()
        {
            InitializeComponent();
        }

        private void RefForm()
        {
            txtExport.Text = txtImport.Text = txtAfter.Text = "0.00";

        
            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_name.DataSource = ds.Tables["X"];
                combo_name.DisplayMember = "اسم المورد";
                combo_name.ValueMember = "رقم المسلسل";
                combo_name.Text = "";
                combo_name.SelectedText = "اختار اسم المورد";
            }

          

        }

        private void Supplier_Transaction_Form_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (combo_name.SelectedIndex >= 0)
            {

                using (ds = Ezzat.GetDataSet("Supplier_selectSupplierAccount", "X"
                    , new SqlParameter("@Day", dateTimePicker1.Value)
                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    , new SqlParameter("@Supplier_ID", combo_name.SelectedValue)))  {
                    dataGridView1.DataSource = ds.Tables["X"];

                    //dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                object o = Ezzat.ExecutedScalar("Supplier_selectSupplierAccount_Purchasing", new SqlParameter("@Day", dateTimePicker1.Value)
                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    , new SqlParameter("@Supplier_ID", combo_name.SelectedValue));

                if (o.Equals(null))
                    txtImport.Text = "0.00";
                else
                    txtImport.Text = o.ToString();

                o = Ezzat.ExecutedScalar("Supplier_selectSupplierAccount_Pay", new SqlParameter("@Day", dateTimePicker1.Value)
                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    , new SqlParameter("@Supplier_ID", combo_name.SelectedValue));

                if (o.Equals(null))
                    txtExport.Text = "0.00";
                else
                    txtExport.Text = o.ToString();

                txtAfter.Text = String.Format("{0:0.00}", Math.Round((double.Parse(txtImport.Text) - double.Parse(txtExport.Text)), 2));
            }
            else { }
        }
    }
}

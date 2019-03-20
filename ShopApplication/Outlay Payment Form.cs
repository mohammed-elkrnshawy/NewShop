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
    public partial class Form_Outlay_Payment : Form
    {
        private DataSet ds;

        public Form_Outlay_Payment()
        {
            InitializeComponent();
        }

        private void RefForm()
        {
            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);

            object o = Ezzat.ExecutedScalar("Outlay_selectID");


            textBox1.Text = o + "";

            using (ds = Ezzat.GetDataSet("Band_selectAll", "X"))
            {
                comboBox1.DataSource = ds.Tables["X"];
                comboBox1.DisplayMember = "Outlay_Name";
                comboBox1.ValueMember = "Outlay_ID";
                comboBox1.Text = "";
                comboBox1.SelectedText = "اختار بند مصروفات";
            }

            richTextBox1.Text = "لا يوجد ملاحظات";
            textBox4.Text = "0";
        }

        private void Form_Outlay_Payment_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void btuSave_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                AddOutlayTansaction();
                EditSafe();
                MessageBox.Show(SharedClass.Successful_Message);
                RefForm();
            }
            else
                MessageBox.Show(SharedClass.Check_Message);
        }

        private void EditSafe()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateDecrease", new SqlParameter("@Money_Quantity", float.Parse(textBox4.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Bill_ID", int.Parse(textBox1.Text)),
                new SqlParameter("@Bill_Type", "مصاريف"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(textBox4.Text))
                );
        }

        private void AddOutlayTansaction()
        {
            // اضافة تعامل مصاريف
            Ezzat.ExecutedNoneQuery("Outlay_insertOutlayTransaction",
                new SqlParameter("@Report_Total", float.Parse(textBox4.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Band", comboBox1.SelectedValue)
                );
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(textBox4, e);
        }
    }
}

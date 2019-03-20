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
    public partial class Supplier_PaybackForm : Form
    {
        private DataSet ds;

        public Supplier_PaybackForm()
        {
            InitializeComponent();
        }

        private void RefForm()
        {
            txt_Payment.Text = txt_oldTotal.Text = txt_Render.Text = "0.00";

            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);


            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_name.DataSource = ds.Tables["X"];
                combo_name.DisplayMember = "اسم المورد";
                combo_name.ValueMember = "رقم المسلسل";
                combo_name.Text = "";
                combo_name.SelectedText = "اختار اسم المورد";
            }

            object o = Ezzat.ExecutedScalar("IMPayback_selectID");
            txt_billNumber.Text = o + "";


        }

        private void Supplier_PaybackForm_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void combo_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_name.Focused)
            {
                ShowDetailsSupplier((int)combo_name.SelectedValue);
            }
        }

        private void ShowDetailsSupplier(int selectedValue)
        {
            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Supplier_selectSearch_BYID", out con, new SqlParameter("@Customer_ID", selectedValue));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_oldTotal.Text = dataReader["Supplier_Money"].ToString();
                }
            }
            con.Close();

        }

        private void txt_Payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_Payment, e);
        }

        private void txt_Payment_TextChanged(object sender, EventArgs e)
        {
            SharedClass.Change(txt_Payment);
            Calcolate();
        }
    
        private void Calcolate()
        {
            txt_Render.Text = String.Format("{0:0.00}", (double.Parse(txt_oldTotal.Text) - double.Parse(txt_Payment.Text)));
        }

        private void btuSave_Click(object sender, EventArgs e)
        {
            if (combo_name.SelectedIndex >= 0)
            {
                EditSuplierAccount();
                AddPaybackBill();
                EditSafe();
                MessageBox.Show(SharedClass.Successful_Message);
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void EditSuplierAccount()
        {
            Ezzat.ExecutedNoneQuery("Supplier_updateTotalMoney"
                , new SqlParameter("@Supplier_ID", combo_name.SelectedValue)
                , new SqlParameter("@Total_Money", double.Parse(txt_Render.Text)));
        }

        private void AddPaybackBill()
        {
            Ezzat.ExecutedNoneQuery("Supplier_insertPaybackBill"
                       , new SqlParameter("@Purchasing_ID", int.Parse(txt_billNumber.Text))
                       , new SqlParameter("@Supplier_ID", (int)combo_name.SelectedValue)
                       , new SqlParameter("@Bill_Date", DateTime.Parse(DateTime.Now.ToString()))
                       , new SqlParameter("@Total_oldMoney", double.Parse(txt_oldTotal.Text))
                       , new SqlParameter("@Payment_Money", double.Parse(txt_Payment.Text))
                       , new SqlParameter("@After_Payment", double.Parse(txt_Render.Text))
                       , new SqlParameter("@Bill_Details", richTextBox1.Text)
                       , new SqlParameter("@Bill_Number_Supplier", int.Parse("0"))
               );
        }

        private void EditSafe()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateDecrease", new SqlParameter("@Money_Quantity", float.Parse(txt_Payment.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Bill_ID", int.Parse(txt_billNumber.Text)),
                new SqlParameter("@Bill_Type", "تسديد الى مورد"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(txt_Payment.Text))
                );
        }

    }
}

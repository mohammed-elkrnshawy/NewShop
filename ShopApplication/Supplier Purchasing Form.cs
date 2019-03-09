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
    public partial class Supplier_Purchasing_Form : Form
    {
        private double totalMaterial = 0;
        private DataSet ds;

        public Supplier_Purchasing_Form()
        {
            InitializeComponent();
        }

        private void Supplier_Purchasing_Form_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {

            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);

            using (ds = Ezzat.GetDataSet("Product_selectAll", "X"))
            {
                comboProduct.DataSource = ds.Tables["X"];
                comboProduct.DisplayMember = "اسم المنتج";
                comboProduct.ValueMember = "رقم المنتج";
                comboProduct.Text = "";
                comboProduct.SelectedText = "اختار اسم المنتج";
            }

            object o = Ezzat.ExecutedScalar("IMPurchasing_selectID");
            txt_billNumber.Text = o + "";


            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_name.DataSource = ds.Tables["X"];
                combo_name.DisplayMember = "اسم المورد";
                combo_name.ValueMember = "رقم المسلسل";
                combo_name.Text = "";
                combo_name.SelectedText = "اختار اسم المورد";
            }

            dataGridView1.Rows.Clear();

            txt_quantity.Text = txt_Price.Text = "0";

            txt_totalMaterial.Text = txt_discount.Text = txt_afterDiscount.Text = "0";
            txt_oldTotal.Text = txt_payment.Text = "0";
            txt_total.Text = "0";
            txt_afterPayment.Text = "0";
            txt_totalMaterial.Text = "0";
            txt_afterDiscount.Text = "0";
            totalMaterial = 0;


        }

        private void comboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProduct.Focused)
            {
                ShowDetailsProduct((int)comboProduct.SelectedValue);
            }
        }

        private void ShowDetailsProduct(int customer_ID)
        {

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Product_selectSearch_BYID", out con, new SqlParameter("@Customer_ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_Price.Text = dataReader["Product_Price"].ToString();
                }
            }
            con.Close();


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

        private void txt_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_quantity, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow()
        {
            if (isNotExist((int)comboProduct.SelectedValue)&&txt_quantity.Text != "" && txt_Price.Text != "" && comboProduct.SelectedIndex >= 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = comboProduct.SelectedValue;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = comboProduct.Text;
                dataGridView1[2, dataGridView1.Rows.Count - 1].Value = txt_Price.Text;
                dataGridView1[3, dataGridView1.Rows.Count - 1].Value = txt_quantity.Text;
                dataGridView1[4, dataGridView1.Rows.Count - 1].Value = "وحدة";
                dataGridView1[5, dataGridView1.Rows.Count - 1].Value = String.Format("{0:0.00}", Math.Round((double.Parse(txt_quantity.Text) * double.Parse(txt_Price.Text)), 2));


                totalMaterial += double.Parse(dataGridView1[5, dataGridView1.Rows.Count - 1].Value.ToString());
                Calcolate();
            }
            else
                MessageBox.Show(SharedClass.Check_Message);
        }

        private void Calcolate()
        {
            txt_totalMaterial.Text = String.Format("{0:0.00}", totalMaterial);
            txt_afterDiscount.Text = String.Format("{0:0.00}", (double.Parse(txt_totalMaterial.Text) - double.Parse(txt_discount.Text)));
            txt_total.Text = String.Format("{0:0.00}", (double.Parse(txt_afterDiscount.Text) + double.Parse(txt_oldTotal.Text)));
            txt_afterPayment.Text = String.Format("{0:0.00}", (double.Parse(txt_total.Text) - double.Parse(txt_payment.Text)));
        }

        private bool isNotExist(int Product_ID)
        {
            bool isNotExist = true;

            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if((int)row.Cells[0].Value==Product_ID)
                {
                    isNotExist = false;
                    break;
                }
            }
            return isNotExist;
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            SharedClass.Change(txt_discount);
            Calcolate();
        }

        private void txt_discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_discount, e);
        }

        private void txt_payment_TextChanged(object sender, EventArgs e)
        {
            SharedClass.Change(txt_payment);
        }

        private void txt_payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txt_payment, e);
        }

        private void btuSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (combo_name.SelectedIndex >= 0 && dataGridView1.Rows.Count > 0)
                {
                    EditSuplierAccount();
                    AddPurchasingBill();
                    AddIMBill_Details();
                    EditStore();
                    EditSafe();
                    RefForm();
                }
                else
                    MessageBox.Show(SharedClass.Check_Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

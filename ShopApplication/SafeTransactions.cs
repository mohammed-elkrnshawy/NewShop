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
    public partial class SafeTransactions : Form
    {
        
        private SqlDataReader dataReader;

        public SafeTransactions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ReturnData();
            CalcolateTotal();
        }

        private void ReturnData()
        {

            SqlConnection con;

            dataReader = Ezzat.GetDataReader("_Safe_AllTransaction_During", out con,
                new SqlParameter("@Day", DateTime.Parse(dateTimePicker1.Value.ToString())),
                new SqlParameter("@Day2", DateTime.Parse(dateTimePicker2.Value.ToString())));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = dataReader[2].ToString();
                    if (bool.Parse(dataReader[1].ToString())) 
                    {
                        dataGridView1[1, dataGridView1.Rows.Count - 1].Value = dataReader[5].ToString();
                        dataGridView1[2, dataGridView1.Rows.Count - 1].Value = "0.0000";
                        dataGridView1[3, dataGridView1.Rows.Count - 1].Value = dataReader[5].ToString();
                    }
                    else
                    {
                        dataGridView1[2, dataGridView1.Rows.Count - 1].Value = dataReader[5].ToString();
                        dataGridView1[1, dataGridView1.Rows.Count - 1].Value = "0.0000";
                        dataGridView1[3, dataGridView1.Rows.Count - 1].Value = (double.Parse(dataReader[5].ToString())*-1);
                    }
                    dataGridView1[4, dataGridView1.Rows.Count - 1].Value = dataReader[4].ToString();
                    dataGridView1[5, dataGridView1.Rows.Count - 1].Value = dataReader[3].ToString();
                }
                dataGridView1.Columns[1].AutoSizeMode =
                dataGridView1.Columns[2].AutoSizeMode =
                dataGridView1.Columns[3].AutoSizeMode =
                dataGridView1.Columns[4].AutoSizeMode =
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            con.Close();




        }

        private void CalcolateTotal()
        {

            double Total = 0, debit = 0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Total += double.Parse(item.Cells[1].Value.ToString());
                debit += double.Parse(item.Cells[2].Value.ToString());
            }

            textBox1.Text = Total + "";
            textBox2.Text = debit + "";
            textBox3.Text = (Total - debit) + "";



            textBox4.Text = Ezzat.ExecutedScalar("select_SafeMoney") + "";


        }
    }
}

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
    public partial class Form_Outlay_Account : Form
    {
        private DataSet ds;
        public Form_Outlay_Account()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
            SumOulay();
            SumBand();
        }

        private void SumBand()
        {
            dataGridView2.Rows.Clear();
            SqlConnection con;

            SqlDataReader dataReader = Ezzat.GetDataReader("Band_selectAll", out con);

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    dataGridView2.Rows.Add();
                    dataGridView2[0, dataGridView2.Rows.Count - 1].Value = dataReader[1].ToString();
                    dataGridView2[1, dataGridView2.Rows.Count - 1].Value =
                        Ezzat.ExecutedScalar("Outlay_sumBand",
                        new SqlParameter("@Day", dateTimePicker1.Value),
                        new SqlParameter("@Day2", dateTimePicker2.Value),
                        new SqlParameter("@Band_iD", dataReader[0].ToString())
                        );
                    if (dataGridView2[1, dataGridView2.Rows.Count - 1].Value.ToString().Length == 0)
                        dataGridView2[1, dataGridView2.Rows.Count - 1].Value = "0.0000";
                }
            }

            con.Close();

        }

        private void SumOulay()
        {
            object o = Ezzat.ExecutedScalar("Outlay_During_Sum", new SqlParameter("@Day", dateTimePicker1.Value), new SqlParameter("@Day2", dateTimePicker2.Value));
            textBox1.Text = String.Format("{0:0.00}", o);
        }

        private void Search()
        {
            using (ds = Ezzat.GetDataSet("Outlay_During", "X", new SqlParameter("@Day", dateTimePicker1.Value), new SqlParameter("@Day2", dateTimePicker2.Value)))
            {
                dataGridView1.DataSource = ds.Tables["x"];


                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            }
        }
    }
}

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
    public partial class Product_Transaction_Form : Form
    {

        private DataSet ds;

        public Product_Transaction_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            StoreAllTransactions();
        }

        private void StoreAllTransactions()
        {
            using (ds = Ezzat.GetDataSet("_Store_AllTransaction_DUring", "X",
                   new SqlParameter("@Day", DateTime.Parse(dateTimePicker1.Value.ToString())),
                   new SqlParameter("@Day2", DateTime.Parse(dateTimePicker2.Value.ToString()))))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //Add_GridButtun();
            }

            FillGrid_During(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void FillGrid_During(DateTime StartDateTime, DateTime EndDateTime)
        {
            dataGridView2.Rows.Clear();
            SqlConnection con;

            SqlDataReader dataReader = Ezzat.GetDataReader("Select_All", out con);


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2[0, dataGridView2.Rows.Count - 1].Value = dataReader[0].ToString();
                    dataGridView2[1, dataGridView2.Rows.Count - 1].Value = dataReader[1].ToString();
                    dataGridView2[2, dataGridView2.Rows.Count - 1].Value = Ezzat.ExecutedScalar("select_SUMofProduct__IM"
                                                        , new SqlParameter("@Day", dateTimePicker1.Value)
                                                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                                                        , new SqlParameter("@Product_ID", dataReader[0])
                                                        );
                    //dataGridView2[3, dataGridView2.Rows.Count - 1].Value = Ezzat.ExecutedScalar("select_SUMofProduct__EX"
                    //                                    , new SqlParameter("@Day", dateTimePicker1.Value)
                    //                                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    //                                    , new SqlParameter("@product_ID", dataReader[0])
                    //                                    );
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

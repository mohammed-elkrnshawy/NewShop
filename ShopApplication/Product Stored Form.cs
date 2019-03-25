using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApplication
{
    public partial class Product_Stored_Form : Form
    {
        private DataSet ds;

        public Product_Stored_Form()
        {
            InitializeComponent();
        }

        private void Product_Stored_Form_Load(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Product_selectGard", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }
    }
}

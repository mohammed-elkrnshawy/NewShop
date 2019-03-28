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
    public partial class Shared_Login_Form : Form
    {
        public Shared_Login_Form()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            SharedClass.DrawRoundRect(v, Pens.Black, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 10);
            //Without rounded corners
            //e.Graphics.DrawRectangle(Pens.Blue, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        private void Shared_Login_Form_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            isValidData();
        }

        private void isValidData()
        {
            int o = (int)Ezzat.ExecutedScalar("select_CountUsers");
            if (o == 0)
            {
                User_Add_Form users = new User_Add_Form();
                users.Show();
            }
            else
            {
                object q = Ezzat.ExecutedScalar("select_isValid"
                     , new SqlParameter("@Username", textBox1.Text)
                     , new SqlParameter("@Password", textBox2.Text)
                     , new SqlParameter("@isAdmin", radioButton1.Checked)
                     );

                if (q != null)
                {
                    OpenHome();
                }
                else
                {
                    MessageBox.Show(SharedClass.Check_Message);
                }
            }
        }

        private void OpenHome()
        {
            SharedHomeForm home = new SharedHomeForm(radioButton1.Checked);
            home.ShowDialog();
            this.Close();
        }
    }
}

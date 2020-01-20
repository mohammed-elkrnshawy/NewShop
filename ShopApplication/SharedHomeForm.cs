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
    public partial class SharedHomeForm : Form
    {
        Image closeImage, closeImageAct;
        private bool isAdmin;

        public SharedHomeForm(bool isAdmin)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // minh viet san, khoi mat thoi gian
            Rectangle rect = tabControl1.GetTabRect(e.Index);
            Rectangle imageRec = new Rectangle(rect.Right - closeImage.Width,
                rect.Top + (rect.Height - closeImage.Height) / 2,
                closeImage.Width, closeImage.Height);
            // tang size rect
            rect.Size = new Size(rect.Width + 20, 38);

            Font f;
            Brush br = Brushes.Black;
            StringFormat strF = new StringFormat(StringFormat.GenericDefault);
            // neu tab dang duoc chon
            if (tabControl1.SelectedTab == tabControl1.TabPages[e.Index])
            {
                // hinh mau do, hinh nay them tu properti
                e.Graphics.DrawImage(closeImageAct, imageRec);
                f = new Font("Arial", 10, FontStyle.Bold);
                // Ten tabPage
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    f, br, rect, strF);
            }
            else
            {
                // Tap dang mo, nhung ko dc chon, hinh mau den
                e.Graphics.DrawImage(closeImage, imageRec);
                f = new Font("Arial", 9, FontStyle.Regular);
                // Ten tabPage
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    f, br, rect, strF);
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            // Su kien click dong tabpage
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                // giong o DrawItem
                Rectangle rect = tabControl1.GetTabRect(i);
                Rectangle imageRec = new Rectangle(rect.Right - closeImage.Width,
                    rect.Top + (rect.Height - closeImage.Height) / 2,
                    closeImage.Width, closeImage.Height);

                if (imageRec.Contains(e.Location) && i != 0)
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void SharedHomeForm_Load(object sender, EventArgs e)
        {
            Size mysize = new System.Drawing.Size(20, 20); // co anh chen vao
            Bitmap bt = new Bitmap(Properties.Resources.closeBlack);
            // anh nay ban dau minh da them vao
            Bitmap btm = new Bitmap(bt, mysize);
            closeImageAct = btm;
            //
            //
            Bitmap bt2 = new Bitmap(Properties.Resources.closeBlack);
            // anh nay ban dau minh da them vao
            Bitmap btm2 = new Bitmap(bt2, mysize);
            closeImage = btm2;
            tabControl1.Padding = new Point(30);
        }

        private void اضافةالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة و تعديل الاصناف", new Product_Add_Form());
        }

        private void خردالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("المخزون", new Product_Stored_Form());
        }

        private void تحركاتالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("تحركات المحازن", new Product_Transaction_Form());
        }

        private void اضافةسيارةعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة عميل جديد", new Customer_Add_Form ());
        }

        private void فاتورةصيانةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة فاتورة جديد", new Customer_Purchassing_Form ());
        }

        private void تسديدمنعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("تسديد من العميل", new Customer_Payback_Form());
        }

        private void حسابعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("حساب العميل", new Customer_Transaction_Form());
        }

        private void اضافةوتغديلموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة مورد", new Supplier_Add_Form());
        }

        private void شراءمنموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("شراء من المورد", new Supplier_Purchasing_Form());
        }

        private void تسديدالىموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("تسدسد المورد", new Supplier_PaybackForm());
        }

        private void مرتجعالىموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("مرتجع المورد", new Supplier_Returning_Form());
        }

        private void حسابموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("حساب المورد", new Supplier_Transaction_Form());
        }

        private void اضافةبندمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outlay_Add_Form outlay = new Outlay_Add_Form();
            outlay.ShowDialog();
        }

        private void حسابمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("حساب المصروفات", new Form_Outlay_Account());
        }

        private void تحريربيانمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("بيان مصروفات", new Form_Outlay_Payment());
        }

        private void حركاتالخزنةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("حركة الخزنة", new SafeTransactions());
        }

        private void نسخالمحتوىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة مستخدمين جدد", new User_Add_Form());
        }

        private void فاتورةمرتجعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("فاتورة مرتجع من العميل", new Customer_Returning_Form ());
        }

        private void Add_Tab(string Name, Form form)
        {
            TabPage tp = new TabPage(Name);


            form.TopLevel = false;
            tp.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();

            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedIndex = tabControl1.Controls.Count - 1;
        }
    }
}

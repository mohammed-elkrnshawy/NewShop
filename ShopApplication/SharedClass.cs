using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApplication
{
    class SharedClass
    {
        public static string Check_Message = "من فضلك راجع البيانات";
        public static string Add_Message = "تمت الاضافة";
        public static string Edit_Message = "تمت التعديل";
        public static string Delete_Message = "تمت الحذف";
        public static string Successful_Message = "تمت بنجاح";
        public static string No_Message = "لا يوجد ملاحظات";
        public static string Exsisting_Message = "هذا الصنف موجود مسبقا";


        public static void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }

        public static void KeyPress(TextBox textBox, KeyPressEventArgs e)
        {
            if (textBox.Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void Leave(TextBox textBox)
        {
            textBox.Text = String.Format("{0:0.00}", (double.Parse(textBox.Text)));
        }

        public static void Change(TextBox textBox)
        {
            if (textBox.Text == ".")
                textBox.Text = "0.";
            if (textBox.Text == "")
                textBox.Text = "0";
        }
    }
}
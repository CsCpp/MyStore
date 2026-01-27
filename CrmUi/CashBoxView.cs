using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public class CashBoxView
    {
         CashDesk cashDesk;
        public Label Label { get; set; }
        public NumericUpDown NumericUpDown { get; set; }
        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            this.cashDesk = cashDesk;

            Label = new Label();
            NumericUpDown = new NumericUpDown();
         
            Label.AutoSize = true;
            Label.Location = new System.Drawing.Point(x, y);
            Label.Name = "label" + number;
            Label.Size = new System.Drawing.Size(35, 13);
            Label.TabIndex = number;
            Label.Text = cashDesk.ToString(); 
            // 
            // numericUpDown1
            // 
            NumericUpDown.Location = new System.Drawing.Point(101, y);
            NumericUpDown.Name = "numericUpDown"+number;
            NumericUpDown.Size = new System.Drawing.Size(120, 20);
            NumericUpDown.TabIndex = cashDesk.count;
        }
    }
}

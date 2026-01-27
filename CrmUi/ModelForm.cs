using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ModelForm : Form
    {
         ShopComputerModel model = new ShopComputerModel();
        public ModelForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var cashBoxes = new List<CashBoxView>();
            for (int i = 0; i < model.CashDesks.Count; i++)
            {
                var box = new CashBoxView(model.CashDesks[i], i, 26, (26 * i)+26);
                cashBoxes.Add(box);
                Controls.Add(box.CashDeskName);
                Controls.Add(box.Price);
                Controls.Add(box.QueueLeght);
                Controls.Add(box.LeaveCustomersCount);

            }

            model.Start();

            
        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.Stop();

        }
    }
}

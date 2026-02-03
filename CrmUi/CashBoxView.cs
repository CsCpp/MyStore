using CrmBl.Model;
using System;
using System.Windows.Forms;

namespace CrmUi
{
    public class CashBoxView
    {
         CashDesk cashDesk;
        public Label CashDeskName { get; set; }
        public NumericUpDown Price { get; set; }
        public ProgressBar QueueLeght { get; set; }
        public Label LeaveCustomersCount { get; set; }
        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            this.cashDesk = cashDesk;

            CashDeskName = new Label();
            Price = new NumericUpDown();
            QueueLeght = new ProgressBar();
            LeaveCustomersCount = new Label();


            LeaveCustomersCount.AutoSize = true;
            LeaveCustomersCount.Location = new System.Drawing.Point(x+400, y);
            LeaveCustomersCount.Name = "label2" + number;
            LeaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            LeaveCustomersCount.TabIndex = number;
            LeaveCustomersCount.Text = "";

            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y );
            CashDeskName.Name = "label" + number;
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = cashDesk.ToString();

            Price.Location = new System.Drawing.Point(101, y);
            Price.Name = "numericUpDown"+number;
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = cashDesk.count;
            Price.Maximum = 100000000000;
            cashDesk.CheckClosed += CashDesk_CheckClosed;

            QueueLeght.Location = new System.Drawing.Point(300, y);
            QueueLeght.Maximum = cashDesk.MaxQueueLenght;
            QueueLeght.Name = "progressBar"+number;
            QueueLeght.Size = new System.Drawing.Size(83, 23);
            QueueLeght.TabIndex = number;
            QueueLeght.Value = 0;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {
           Price?.Invoke((Action)delegate 
           { 
               Price.Value += e.Price;
               QueueLeght.Value = cashDesk.count;
               LeaveCustomersCount.Text = cashDesk.ExitCustomer.ToString();
           });
            //  NumericUpDown.Invoke((Action)delegate { - обертка чтобы небыло exeption  созданно в другом потоке
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBl.Model;

namespace CrmUi
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;
      
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(), db)
            {
                IsModel = false
            };
        }

         private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products, db);
            catalogProduct.Show();
        }

        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers, db);
            catalogSeller.Show();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers, db);
            catalogCustomer.Show();
        }

        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCheck = new Catalog<Check>(db.Checks, db);
            catalogCheck.Show();
        }

        private void CustomerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
           // form.Show();
            if(form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }

        private void SellerAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            // form.Show();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
        }

        private void ProductAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            // form.Show();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(()=>
            {
                StoreList.Invoke((Action)delegate
                {
                    StoreList.Items.AddRange(db.Products.ToArray());
                    UpdateCart();
                });
            });
        }
        private void StoreList_DoubleClick(object sender, EventArgs e)
        {
           if( StoreList.SelectedItem is Product product)
            {
                cart.Add(product);
                CartList.Items.Add(product);
                UpdateCart();
            }
        }
        private void UpdateCart()
        {
                CartList.Items.Clear();
                CartList.Items.AddRange(cart.GetAll().ToArray());
                label1.Text = $"ИТОГО: {cart.Price}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new Login();
            form.ShowDialog();
            if(form.DialogResult == DialogResult.OK)
            {
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempCustomer != null)
                {
                    customer = tempCustomer;
                  
                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    customer = form.Customer;
                }
                cart.Customer = customer;
            }
            linkLabel1.Text = customer.Name;
        }

        private void buttonCloseCheck_Click(object sender, EventArgs e)
        {
            if(customer != null)
            {
                cashDesk.Enqueue(cart);
               var price = cashDesk.Dequeue();
                CartList.Items.Clear();
                cart = new Cart(customer);
                MessageBox.Show("Вы оплатили: " + price+ " руб.", "Успешная покупка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Гость не может совершать покупки!", "Авторизуйтесь.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;


namespace CrmBl.Model
{
    public class CashDesk
    {
        CrmContext db = new CrmContext();
        public int Number { get; set; }
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLenght { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        public int count => Queue.Count;
        

        public event EventHandler<Check> CheckClosed;

        public CashDesk (int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart> ();
            IsModel = true;
            MaxQueueLenght = 12;
        }

        public void Enqueue(Cart cart)
        {
            if (Queue.Count <= MaxQueueLenght)
            {
                Queue.Enqueue (cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        public decimal Dequeue()
        {
            decimal sum = 0;
            if (Queue.Count == 0)
            {
                return 0;
            }
            var cart = Queue.Dequeue ();
            if (cart!=null)
            {
                var check = new Check()
                {
                    SellerId = Seller.SellerId,
                    Seller = Seller,
                    CustomerId = cart.Customer.CustomerId, 
                    Customer = cart.Customer,
                    Created = DateTime.Now
                    
                };
                if(!IsModel)
                {
                    db.Checks.Add (check);
                }
                else
                {
                    check.CheckId = 0;
                }
                var sells = new List<Sell>();
                foreach (Product product in cart)
                {
                    if(product.Count >0)

                    { 
                   var sell = new Sell()
                    {
                        CheckId = check.CheckId,
                        Check = check,
                        ProductId = product.ProductId,
                        Product = product
                    };
                    sells.Add(sell);
                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }
                        product.Count--;
                        sum += product.Price;
                    }
                }
                check.Price = sum;
                if(!IsModel)
                {
                    db.SaveChanges();
                }
                CheckClosed?.Invoke(this, check);
            }
            return sum;
        }
        public override string ToString()
        {
            return $"Касса №{Number}";
        }

    }
}

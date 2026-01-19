
using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    internal class Check
    {
        public int CheckId { get; set;  }

        public int CustomerId { get; set;  }

        public virtual Customer Customer { get; set;  }

        public int SelerId { get; set; }

        public virtual Seller Seller { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return $"№{CheckId} от {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
    }
}

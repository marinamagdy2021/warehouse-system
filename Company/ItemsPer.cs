using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class ItemsPer
    {
        public string ProductName{get;set;}
        public  int ItemQuantity { get; set; }
        public int oldQuantity { get; set; }
        public DateTime ProdDate { get; set; }
        public DateTime ExpiDate { get; set; }
        public string oldRecord { get; set; }
    }
}

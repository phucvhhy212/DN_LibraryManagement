using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCore.Models;

namespace LibraryCore.ViewModels
{
    public class CustomResponseOrder
    {
        public CustomResponseOrder()
        {
            
        }
        public int OrderId { get; set; }
        public string Address { get; set; }

        public double Total { get; set; }


        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public ICollection<CustomResponseOrderDetail> OrderDetails { get; set; }
    }

    public class CustomResponseOrderDetail
    {
        public CustomResponseOrderDetail()
        {
            
        }
        public double Price { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}

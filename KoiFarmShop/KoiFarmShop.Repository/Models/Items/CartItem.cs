using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.Models.Items
{
    public class CartItem
    {
        public long KoiFishId { get; set; }
        public string? KoiFishName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

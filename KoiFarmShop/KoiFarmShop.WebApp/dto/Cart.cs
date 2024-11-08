using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.WebApp.dto
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public double? totalAmount { get; set; }


        public void AddKoi(CartItem koi)
        {
            var exist = Items.Where(x => x.KoiId == koi.KoiId).FirstOrDefault();
            if (exist != null)
            {
                exist.Quantity = exist.Quantity + koi.Quantity;
                exist.price += koi.price;
                return;
            }
            CartItem cartItem = new CartItem();
            cartItem.KoiId = koi.KoiId;
            cartItem.price = koi.price;
            cartItem.Quantity = 1;
            Items.Add(cartItem);

        }
        public void RemoveKoi(CartItem koi)
        {
            var exist = Items.Where(x => x.KoiId == koi.KoiId).FirstOrDefault();
            if (exist != null)
            {
                if (exist.Quantity > 1)
                {
                    Items.Find(x => x.KoiId == koi.KoiId).Quantity = Items.Find(x => x.KoiId == koi.KoiId).Quantity - 1; //Items.Remove(exist);
                    exist.price -= koi.price;
                }
                else
                {
                    Items.Remove(exist);
                }
            }
        }
        public double? TotalAmount()
        {
            totalAmount = 0;
            foreach (var item in Items)
            {
                totalAmount += item.price * item.Quantity;
            }
            return totalAmount;
        }

        public List<CartItem> GetItems() { return Items; }
    }

}

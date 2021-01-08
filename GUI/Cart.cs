using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Cart
    {
        public event EventHandler<CartEventArgs> OnPriceChanged;


        double currentPrice;

        public Cart(double currentPrice)
        {
            this.currentPrice = currentPrice;

        }
        public void PriceAdded(double Price)
        {
           
            currentPrice += Price;

            if(OnPriceChanged!=null)
               OnPriceChanged(this, new CartEventArgs(currentPrice));
        }
        public double CurrentPrice()
        {
            return currentPrice;
        }
    }

   public class CartEventArgs : EventArgs
    {
        public double CurrentPrice;

        public CartEventArgs(double currentPrice)
        {
            CurrentPrice = currentPrice;
        }
    }
}

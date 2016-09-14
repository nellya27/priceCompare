using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopingLogic
{
    public class ShopingCart
    {
        public List<Product> ListOfProducts { get; private set; }

        public ShopingCart()
        {
            ListOfProducts = new List<Product>();
        }
       
        public void AddProductToCart(Product product)
        {
              ListOfProducts.Add(product);
            
        }

        public void clearShopingCart()
        {
            ListOfProducts.Clear();
        }

        public double GetTotalPrice()
        {
            return ListOfProducts.Sum(Product => Product.ProductPrice);
        }

    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopingLogic;

namespace shopingCompareUI
{
    public partial class ShopingCartView : Form
    {
        private ShopingCart shopingCart = new ShopingCart();

        public ShopingCartView(ShopingCart cart)
        {
            
            InitializeComponent();
            shopingCart = cart;
            InitializeShopingCartList();
        }

        private void InitializeShopingCartList()
        {
            List<Product> products = shopingCart.ListOfProducts;
            foreach(Product product in products)
            {
                string productInfo = string.Format("Name: {0}  Quantity:{1}  Price: {2} ", product.ProductName, product.ProductQuntity, product.ProductPrice);
                productList.Items.Add(productInfo);
            }
        }

        private void totalPrice_Click(object sender, EventArgs e)
        {

            label1.Visible = true;
            TotalCostLabel.Text = shopingCart.GetTotalPrice().ToString();
        
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            shopingCart.clearShopingCart();
            productList.Items.Clear();
            label1.Visible = false;
            TotalCostLabel.Visible = false;
        }
    }
}

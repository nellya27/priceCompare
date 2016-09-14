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
    public partial class ProductsChart : Form
    {
        private Dictionary<string, Chain> chain;
       private ShopingCatalogManager catalog = new ShopingCatalogManager();

        public ProductsChart(Dictionary<string,Chain> chains)
        {
            InitializeComponent();
            chain = chains;
            ShowMostCheap();
            ShowMostExpensive(); 
        }

        private void ShowMostExpensive()
        {

            foreach (string name in chain.Keys.ToList())
            {
                List<string> listOfProducts = catalog.GetThreeMostExpensiveProducts(chain[name].ChainId);
                int index = 1;
                foreach (string product in listOfProducts)
                {
                    string messege = string.Format("{0}:{1}{2}. {3} ", name, System.Environment.NewLine, index, product);
                    mostExpeList.Items.Add(messege);
                    index++;
                }

            }
        }

        private void ShowMostCheap()
        {

            foreach (string name in chain.Keys.ToList())
            {
                List<string> listOfProducts = catalog.GetThreeMostCheapProducts(chain[name].ChainId);
                int index = 1;
                foreach (string product in listOfProducts)
                {
                    string messege = string.Format("{0} :{1}{2}. {3} ", name, System.Environment.NewLine, index, product);
                    mostCheapList.Items.Add(messege);
                    index++;

                }
               
            }
        }

    }
}

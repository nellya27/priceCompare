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
    public partial class shopingView : Form
    {        
        private ShopingCatalogManager maneger;
        private Dictionary<string, Chain> chainDic;
        private List<CheckBox> chainCheckBoxes = new List<CheckBox>();
        private List<CheckBox> areaCheckBoxes = new List<CheckBox>();
        private List<ComboBox> cityComboBoxes = new List<ComboBox>();
        private List<ComboBox> storeComboBoxes = new List<ComboBox>();
        private List<ListBox> priceList = new List<ListBox>();
        private List<ListBox> productList = new List<ListBox>();
        private List<NumericUpDown> countersList = new List<NumericUpDown>();
        private ShopingCart shopingCart = new ShopingCart();
       
      

       
        public shopingView()
        {
            
            InitializeComponent();
            maneger = new ShopingCatalogManager();
            chainDic = maneger.GetChainFromDataBase();
            InitializeChainCheckers();
            InitializeStoreComboBoxes();
            InitializeAreaCheckBoxes();
            InitializeCityComboBoxes();
            InitializeProductsControls();  
        }   
        private void InitializeProductsControls()
        {
            productList.Add(productsListBox);
            productList.Add(productListBox2);
            productList.Add(productListBox3);
            priceList.Add(priceListBox1);
            priceList.Add(priceListBox2);
            priceList.Add(priceListBox3);
            countersList.Add(productCounter1);
            countersList.Add(productCounter2);
            countersList.Add(productCounter3);
        }
       

        private void InitializeChainCheckers()
        {
            string[] names = chainDic.Keys.ToArray();
            chainCheckBox1.Text = names[0];
            chainCheckBox2.Text = names[1];
            chainCheckBox3.Text = names[2];
            chainCheckBoxes.Add(chainCheckBox1);
            chainCheckBoxes.Add(chainCheckBox2);
            chainCheckBoxes.Add(chainCheckBox3);

            foreach (CheckBox box in chainCheckBoxes)
            {
                box.CheckedChanged += new System.EventHandler(chainChekChanged);
            }
        }

        private void InitializeStoreComboBoxes()
        {
            storeComboBoxes.Add(storesComboBox);
            storeComboBoxes.Add(storeComboBox2);
            storeComboBoxes.Add(storeComboBox3);
                                 
        }

        private void InitializeAreaCheckBoxes()
        {
            areaCheckBoxes.Add(isFilterByCity);
            areaCheckBoxes.Add(isFilterByCity2);
            areaCheckBoxes.Add(isFilterByCity3);
            foreach(CheckBox box in areaCheckBoxes)
            {
                box.CheckedChanged += new System.EventHandler(isFilterByCity_CheckedChanged);
            }
        }

        private void InitializeCityComboBoxes()
        {
            cityComboBoxes.Add(cityComboBox);
            cityComboBoxes.Add(CitycomboBox2);
            cityComboBoxes.Add(cityComboBox3);
            foreach(ComboBox box in cityComboBoxes)
            {
                box.SelectedIndexChanged += new System.EventHandler(cityComboBox_SelectedIndexChanged);
            }
        }

        private void chainChekChanged(object sender, EventArgs e)
        {
            int index = 0;
            foreach (CheckBox box in chainCheckBoxes)
            {
                if (box.Checked)
                {

                    if (!isFilterByCity.Checked)
                    {
                        List<Store> stores = maneger.GetStoresByChainId(chainDic[box.Text.ToString()].ChainId);
                        UpdateStoreData(storeComboBoxes[index], stores);
                    }
                }
                else
                clearAllData(index);
                
                index++; 
            }
        }

        private void clearAllData(int index)
        {
            ClearComboBoxes(storeComboBoxes[index]);
            if (productList[index].Visible && priceList[index].Visible)
            {
                productList[index].Visible = false;
                priceList[index].Visible = false;
            }
        }

        private void isFilterByCity_CheckedChanged(object sender, EventArgs e)
        {
            int index = 0;

            foreach (CheckBox box in areaCheckBoxes)
            {
                if (box.Checked)
                {
                    cityComboBoxes[index].Enabled = true;
                    string name = chainCheckBoxes[index].Text.ToString();
                    List<string> cities = maneger.GetListOfCitiesByChain(chainDic[name].ChainId);
                    UpdateComboBox(cityComboBoxes[index], cities);
                }
                else
                {
                    cityComboBoxes[index].Enabled = false;
                    ClearComboBoxes(cityComboBoxes[index]);
                }
                index++;
            }

        }

        private void cityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;
            ComboBox currentStore = new ComboBox();
            string [] chains = chainDic.Keys.ToArray();
            string currentChain=string.Empty;
            for (int i= 0;i< 3;i++)
            {
               if(cityComboBoxes[i].Name==currentComboBox.Name)
                {
                    currentChain = chains[i];
                    currentStore = storeComboBoxes[i];
                    break;
                }  
            }           
            List<Store> stores= maneger.GetStoresByPlace(currentComboBox.SelectedItem.ToString(), chainDic[currentChain].ChainId);
            UpdateStoreData(currentStore, stores);

        }
        private void UpdateStoreData(ComboBox box,List<Store> stores)
        {
            ClearComboBoxes(box);
            foreach (Store store in stores)
            {
                box.Items.Add(store.StoreName);
            }

        }

        private void UpdateComboBox(ComboBox box, List<string> items)
        {
            ClearComboBoxes(box);
            foreach (string item in items)
            {
                box.Items.Add(item);
            }
        }
        

        private void ClearComboBoxes(ComboBox box)
        {
            if(box.Items.Count>0)
            {
                box.Items.Clear();
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            foreach (CheckBox box in chainCheckBoxes)
            {
                if (box.Checked)
                {
                    productList[index].Visible = true;
                    priceList[index].Visible = true;
                    string storeName = storeComboBoxes[index].Text.ToString();
                    string storeId = maneger.GetStoreIdByName(chainDic[box.Text].ChainId, storeName);
                    List<Product> products = maneger.GetProductsByStoreId(storeId);
                    UpdateProductListBox(productList[index],priceList[index] ,products);                   
                }
                else
                    productList[index].Visible = false;
                index++;
            }

        }

        private void UpdateProductListBox(ListBox list1,ListBox list2,List<Product> products)
        {
            int index = 1;
            foreach (Product product in products)
            {               
                list1.Items.Add(product.ProductName);
                list2.Items.Add(product.ProductPrice);
                index++;         
            }
        }

        private void ClearListBox(ListBox list)
        {
            if(list.Items.Count>0)
            {
                list.Items.Clear();
            }
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
           int index=0;
           foreach(CheckBox box in chainCheckBoxes)
            {
                if (box.Checked)
                {
                    AddProductToShopingCart(countersList[index],index);
                    index++;
                }
                else
                    continue;
            }
        }

        private void AddProductToShopingCart(NumericUpDown numberOfProduct, int index)
        {
           
            if (numberOfProduct.Value != 0)
            {
                string productName = productList[index].SelectedItem.ToString();
                string storeName = storeComboBoxes[index].Text;
                string chainName = chainCheckBoxes[index].Text.ToString();
                string storeId = maneger.GetStoreIdByName(chainDic[chainName].ChainId,storeName);
                Product product = maneger.GetProductByName(productName,storeId);
                for (int i = 0; i < numberOfProduct.Value; i++)
                {
                    shopingCart.AddProductToCart(product);
                }
                ShowProductAddedToCart();           
            }            
        }

        private void ShowProductAddedToCart()
        {
            MessageBox.Show("המוצר נוסף לעגלת הקניות", "מוצרים", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.RtlReading);
            
        }

        private void shopingCartButton_Click(object sender, EventArgs e)
        {
           ShopingCartView shopingCartView = new ShopingCartView(shopingCart);
           shopingCartView.Show();
        }

        private void chart_Click(object sender, EventArgs e)
        {
            ProductsChart prodChart = new ProductsChart(chainDic);
            prodChart.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShopingLogic
{
    public class XmlParser
    {
        private const string storePathName = "Store*";
        private const string productPathName = "PriceFull*";
        private Dictionary<Chain, List<Store>>  storeDictionary=new Dictionary<Chain, List<Store>>();
        private Dictionary<string, List<Product>> productDictionary = new Dictionary<string, List<Product>>();
      
        public XmlParser()
        {
            Initializer();
        }
          
        private  void Initializer()
        {
            FindXmlFiles(storePathName);
            FindXmlFiles(productPathName);
        }


        public Dictionary<Chain,List<Store>> GetStore
        {
            get {return storeDictionary; }
        }

        public Dictionary<string,List<Product>> GetProduct
        {
            get { return productDictionary; }
        }

        private void FindXmlFiles(string docName)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(@"C:\Users\nelia_000\Desktop\ProductFiles");
            foreach (string subdirectory in subdirectoryEntries)
            {
                string[] files = Directory.GetFiles(subdirectory, docName);
                foreach (string file in files)
                {
                     LoadXmlDocument(file);
                }
            }
        }


        public void LoadXmlDocument(string docName)
        {
            XmlDocument productDocument = new XmlDocument();
            productDocument.Load(docName);
            if (docName.Contains("PriceFull"))
                SetProductKey(productDocument);
            else
                SetStoreKey(productDocument);
        }


        private void SetProductKey(XmlDocument productDocument)
        {
            XmlNode root = productDocument.SelectSingleNode("Root");
            string chainId = root.SelectSingleNode("ChainId").InnerText;
            string storeId = root.SelectSingleNode("StoreId").InnerText;
            if (!productDictionary.ContainsKey(storeId))
            {
                List<Product> productList = new List<Product>();
                productDictionary.Add(storeId, productList);
            }

            SetProductValues(root,storeId, chainId);
        } 


        private void SetProductValues(XmlNode root ,string storeId,string chainId)
        {

            XmlNode product = root.SelectSingleNode("Items");
            XmlNodeList products = product.SelectNodes("Item");
            foreach (XmlNode node in products)
            {
                Product newProduct = new Product();                
                newProduct.ProductCode = node.SelectSingleNode("ItemCode").InnerText;
                newProduct.ProductId = node.SelectSingleNode("ItemId").InnerText;
                newProduct.ProductName = node.SelectSingleNode("ItemName").InnerText;
                newProduct.ProductPrice = float.Parse(node.SelectSingleNode("ItemPrice").InnerText);
                newProduct.ProductQuntity = node.SelectSingleNode("Quantity").InnerText;
                newProduct.UnitOfMesure = node.SelectSingleNode("UnitOfMeasure").InnerText;
                newProduct.chainId = chainId;
                productDictionary[storeId].Add(newProduct);
            }
        }

        private void SetStoreKey(XmlDocument productDocument)
        {
            XmlNode root = productDocument.SelectSingleNode("Root");
            Chain newChain = new Chain();
            newChain.ChainId = root.SelectSingleNode("ChainId").InnerText;
            newChain.ChainName = root.SelectSingleNode("ChainName").InnerText;

            if (!storeDictionary.ContainsKey(newChain))
            {
                List<Store> storeList = new List<Store>();
                storeDictionary.Add(newChain, storeList);
            }
            SetStoreValues(root, newChain);
        }

        private void SetStoreValues(XmlNode root,Chain newChain)
        {
            XmlNode productDocumentList = root.SelectSingleNode("SubChains").SelectSingleNode("SubChain");
            XmlNode storeNode = productDocumentList.SelectSingleNode("Stores");
            XmlNodeList storeListNode = storeNode.SelectNodes("Store");

            foreach (XmlNode node in storeListNode)
            {
                Store newStore = new Store();
                newStore.StoreId = node.SelectSingleNode("StoreId").InnerText;
                newStore.StoreName = node.SelectSingleNode("StoreName").InnerText;
                newStore.StoreAdress = node.SelectSingleNode("Address").InnerText;
                newStore.StoreCity = node.SelectSingleNode("City").InnerText;
                storeDictionary[newChain].Add(newStore);
            }
        }
   
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace ShopingLogic.DataBase
{
    public class DataInserter
    {
        private DbConnector connection;
        private XmlParser parsedData;
        private Dictionary<Chain, List<Store>> storeList;
        private Dictionary<string, List<Product>> productList;

        public DataInserter()
        {
            Initialize();

        }
        
        private void Initialize()
        {
            connection = new DbConnector();
            parsedData = new XmlParser();  
            storeList = parsedData.GetStore;
            productList = parsedData.GetProduct;
            //InsertDataChainsTable();
            InsertDataPriceProducts();
            //InsertDataStores();

        }
        private void InsertDataChainsTable()
        {
            StringBuilder query =new StringBuilder("INSERT INTO chains (chain_id,chain_name) VALUES");
            List<string> raws = new List<string>();
            List<Chain> listOfChains = storeList.Keys.ToList();
            foreach (Chain chain in listOfChains)
            {
                raws.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString(chain.ChainId), MySqlHelper.EscapeString(chain.ChainName)));                
            }
            query.Append(string.Join(",", raws));
            query.Append(";");
            connection.Insert(query.ToString());
        }

        private void InsertDataPriceProducts()
        {
            StringBuilder query = new StringBuilder("INSERT INTO prices (store_id,price,chain_id,unitofmessure,quntity,product_name)VALUES");
            List<string> raws = new List<string>();
                foreach(KeyValuePair<string,List<Product>> item in productList)
                    foreach(Product product in item.Value)
                    {
                        raws.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')", MySqlHelper.EscapeString(item.Key), MySqlHelper.EscapeString(product.ProductPrice.ToString()), MySqlHelper.EscapeString(product.chainId), MySqlHelper.EscapeString(product.UnitOfMesure), MySqlHelper.EscapeString(product.ProductQuntity),MySqlHelper.EscapeString(product.ProductName)));
                    }

                query.Append(string.Join(",", raws));
                query.Append(";");
                connection.Insert(query.ToString());                 
        }


        private void InsertDataStores()
        {
            StringBuilder query =new StringBuilder("INSERT INTO stores (store_id,chain_id,store_name,store_addres,city)VALUES");
            List<string> raws = new List<string>();
            foreach(KeyValuePair<Chain,List<Store>> item in storeList)
            {
                foreach(Store store in item.Value)
                {
                    raws.Add(string.Format("('{0}','{1}','{2}','{3}','{4}')", MySqlHelper.EscapeString(store.StoreId), MySqlHelper.EscapeString(item.Key.ChainId), MySqlHelper.EscapeString(store.StoreName), MySqlHelper.EscapeString(store.StoreAdress), MySqlHelper.EscapeString(store.StoreCity)));
                }
            }
            query.Append(string.Join(",", raws));
            query.Append(";");
            connection.Insert(query.ToString());       
        }
    }
}

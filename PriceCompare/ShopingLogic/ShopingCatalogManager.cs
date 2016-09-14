using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopingLogic.DataBase;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ShopingLogic
{
    public class ShopingCatalogManager
    {
       private DbConnector connection = new DbConnector();
       private MySqlDataReader dataReader;
       private MySqlCommand command;
    
     
        private void MakeCommand(string query)
        {
            connection.OpenConnection();
            command = new MySqlCommand(query, connection.Connection);
            dataReader = command.ExecuteReader();
        }

        private void CloseConnection()
        {
            dataReader.Close();
            connection.CloseConnection();
        }
         
       
        public Dictionary<string,Chain> GetChainFromDataBase()
        {
            string query = "SELECT * FROM shopingcart.chains";
            Dictionary<string,Chain> chains = new Dictionary<string, Chain>();
            MakeCommand(query);
            while (dataReader.Read())
            {
               Chain chain = new Chain();
               chain.ChainId = (dataReader["chain_id"].ToString());
               chain.ChainName = (dataReader["chain_name"].ToString());
               chains[chain.ChainName]=chain;
            }
             CloseConnection();
            
            return chains;
        }
    
        public List<Store> GetStoresByChainId(string chain_id)
        {
            string query = string.Format("SELECT *FROM shopingcart.stores WHERE chain_id={0}", MySqlHelper.EscapeString(chain_id));
            List<Store> stores =new List<Store>();
            MakeCommand(query);
            StoreDataReader(stores);
            CloseConnection();
            return stores;
        }


        public List<Store> GetStoresByPlace(string city, string chainId)
        {
            string query = string.Format("SELECT *FROM shopingcart.stores WHERE city='{0}' AND chain_id={1}", MySqlHelper.EscapeString(city), MySqlHelper.EscapeString(chainId));
            List<Store> stores = new List<Store>();
            MakeCommand(query);
            StoreDataReader(stores);
            CloseConnection();
            return stores;
        }


        private void StoreDataReader(List<Store> stores)
        {
            while (dataReader.Read())
            {
                Store store = new Store();
                store.StoreId = (dataReader["store_id"].ToString());
                store.StoreName = (dataReader["store_name"].ToString());
                store.StoreCity = (dataReader["city"].ToString());
                store.StoreAdress = (dataReader["store_addres"].ToString());
                stores.Add(store);
            }
        }


        public List<Product> GetProductsByStoreId(string store_id)
        {
            string query = string.Format("SELECT *FROM shopingcart.prices WHERE store_id={0}", MySqlHelper.EscapeString(store_id));
            List<Product> products = new List<Product>();
            MakeCommand(query);
            while (dataReader.Read())
            {
                Product product = new Product();
                ProductDataReader(product);  
                products.Add(product);     
            }
            CloseConnection();
            return products;
        }

        public Product GetProductByName(string name, string store_id)
        {
            string query = string.Format("SELECT *FROM shopingcart.prices WHERE store_id={0} AND product_name='{1}'", MySqlHelper.EscapeString(store_id), MySqlHelper.EscapeString(name));
            Product product = new Product();
            MakeCommand(query);
            while (dataReader.Read())
            {
                ProductDataReader(product);
            }
            CloseConnection();
            return product;
        }

        private void ProductDataReader(Product product)
        {
     
            product.ProductPrice = float.Parse((dataReader["price"].ToString()));
            product.UnitOfMesure = (dataReader["unitofmessure"].ToString());
            product.ProductQuntity = (dataReader["quntity"].ToString());
            product.ProductName = (dataReader["product_name"].ToString());
            product.chainId = (dataReader["chain_id"].ToString());
        }

    
        public List<string> GetListOfCitiesByChain(string chain)
        {
            string query =string.Format("SELECT DISTINCT city FROM shopingcart.stores WHERE chain_id={0}",MySqlHelper.EscapeString(chain));
            List<string> cities = new List<string>();
            MakeCommand(query);
            while (dataReader.Read())
            {
               string city = (dataReader["city"].ToString());
               cities.Add(city);
            }
            CloseConnection();
            return cities;
        }

        public string GetStoreIdByName(string chainId,string storeName)
        {

            string query = string.Format("SELECT *FROM shopingcart.stores WHERE store_name='{0}' AND chain_id={1}", MySqlHelper.EscapeString(storeName), MySqlHelper.EscapeString(chainId));
            string storeId=string.Empty;
            MakeCommand(query);
            while (dataReader.Read())
            {            
                storeId = (dataReader["store_id"].ToString());               
            }
            CloseConnection();
            return storeId;
        }
        
        public List<string> GetThreeMostExpensiveProducts(string chain_id)
        {
            string query = string.Format("SELECT product_name FROM shopingcart.prices WHERE chain_id='{0}' ORDER BY price DESC",chain_id);
            string storeId = string.Empty;
            List<string> names = new List<string>();
            MakeCommand(query);
            while (dataReader.Read())
            {
                storeId = (dataReader["product_name"].ToString());
                names.Add(storeId);
            }
            CloseConnection();

            return names.Take(3).ToList();
        } 
        
        public List<string> GetThreeMostCheapProducts(string chain_id)
        {
            string query = string.Format("SELECT product_name FROM shopingcart.prices WHERE chain_id={0} ORDER BY price ASC", chain_id);
            string storeId = string.Empty;
            MakeCommand(query);
            List<string> names = new List<string>();

            while (dataReader.Read())
            {
                storeId = (dataReader["product_name"].ToString());
                names.Add(storeId);
            }
            CloseConnection();

            return names.Take(3).ToList();
        }
          
    }
}




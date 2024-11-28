using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PusokAlexLab7.Models;

namespace PusokAlexLab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Shoplist>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }
        public Task<int> DeleteProductAsync(Product product, Shoplist shop1)
        {
            return _database.DeleteAsync(product);
        }
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }
    

        public Task<List<Shoplist>> GetShopListsAsync()
        {
            return _database.Table<Shoplist>().ToListAsync();
        }
        public Task<Shoplist> GetShopListAsync(int id)
        {
            return _database.Table<Shoplist>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveShopListAsync(Shoplist slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteShopListAsync(Shoplist slist)
        {
            return _database.DeleteAsync(slist);
        }
        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }
        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
            "select P.ID, P.Description from Product P"
            + " inner join ListProduct LP"
            + " on P.ID = LP.ProductID where LP.ShopListID = ?",
            shoplistid);
        }



    }
}

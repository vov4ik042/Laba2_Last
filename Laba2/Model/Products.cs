using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2Model
{
    internal class Products
    {
        public string NameOfProduct { get; set; }
        public int ArticleOfProduct { get; set; }
        public double PriceOfProduct { get; set; }
        public int NumberOfProducts { get; set; }
        public string TypeOfProduct { get; set; }

        public static List<Products> ProductsListBase = new List<Products>();
        public static List<Products> ProductsListClient = new List<Products>();

        public Products()
        {

        }

        public Products(int yes)
        {
            InitializeListOfProducts();
        }

        public Products(string name, int article, double price, string type, int amount )
        {
            NameOfProduct = name;
            ArticleOfProduct = article;
            PriceOfProduct = price;
            TypeOfProduct = type;
            NumberOfProducts = amount;
        }

        private void InitializeListOfProducts()
        {
            ProductsListBase.Add(new Products("Apple", 1111, 10.90, "pcs", 10));
            ProductsListBase.Add(new Products("Banana", 1211, 15.40, "pcs", 10));
            ProductsListBase.Add(new Products("Bread", 1121, 21, "pcs", 12));
            ProductsListBase.Add(new Products("Milk", 1112, 19.80, "pcs", 52));
            ProductsListBase.Add(new Products("Pineapple", 2111, 40.35, "pcs", 60));
            ProductsListBase.Add(new Products("Eggs", 2211, 25.57, "pcs", 10));
            ProductsListBase.Add(new Products("Sugar", 2221, 17.50, "pcs", 34));
            ProductsListBase.Add(new Products("Water", 2222, 9.50, "pcs", 22));
            ProductsListBase.Add(new Products("Koka-kola", 3111, 14.50, "pcs", 53));
            ProductsListBase.Add(new Products("Vine", 1311, 60.10, "pcs", 34));
            ProductsListBase.Add(new Products("Chocolate", 1131, 16.39, "pcs", 25));
            ProductsListBase.Add(new Products("Oil", 1113, 36.70, "pcs", 20));
        }
    }
}

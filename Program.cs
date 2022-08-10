using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_46_Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Client
    {
        public int Money { get; private set; }

        public BasketProducts _basketProducts = new BasketProducts();

        public Client()
        {
            DepositMoneyRandomly();
        }

        private void DepositMoneyRandomly()
        {
            Random random = new Random();
            int minLimit = 0;
            int maxLimit = 1000;
            Money = random.Next(minLimit, maxLimit);
        }
    }

    class BasketProducts
    {
        private List<Product> _products = new List<Product>();

        private void PutProduct()
        {
            _products.Add(new Product());
        }

        private void RemoveRandomProduct()
        {
            Random random = new Random();
            int minLimit = 0;
            int indexDelete = random.Next(minLimit, _products.Count);
            _products.RemoveAt(indexDelete);
        }
    }

    class Product
    {
        public string Name { get; private set; }

        public int Cost { get; private set; }

        private Dictionary<string, int> _typesProducts = new Dictionary<string, int>();

        public Product()
        {
            CreateAssortment();
            AssignType();
        }

        public void AssignType()
        {
            Random random = new Random();
            int minLimit = 0;
            int index = random.Next(minLimit, _typesProducts.Count);
            Name = _typesProducts.ElementAt(index).Key;
            Cost = _typesProducts.ElementAt(index).Value;
        }

        private void CreateAssortment()
        {
            _typesProducts.Add("Хлеб", 20);
            _typesProducts.Add("Молоко", 55);
            _typesProducts.Add("Печенье", 32);
            _typesProducts.Add("Мясо", 234);
            _typesProducts.Add("Сыр", 134);
            _typesProducts.Add("Картошка", 45);
            _typesProducts.Add("Зелень", 15);
        }
    }

    class Supermarket
    {
        private Queue<Client> _clients = new Queue<Client>();

        private void CreateQueue()
        {
            int numberClients = 10;

            for (int i = 0; i < numberClients; i++)
            {
                _clients.Enqueue(new Client());
            }

            Console.WriteLine($"\nОчередь создана в колличестве {numberClients} человек\n");
        }

        private void CalculatePurchaseAmount()
        {
            int amount = 0;

            for (int i = 0; i < 1; i++)
            {

            }
        }
    }
}

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
            Supermarket supermarket = new Supermarket();
            supermarket.StartGame();
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
        public List<Product> _products = new List<Product>();

        public BasketProducts()
        {
            FillProducts();
        }

        private void FillProducts()
        {
            Random random = new Random();
            int minLimit = 0;
            int maxLimit = 10;
            int numberProducts = random.Next(minLimit, maxLimit);

            for (int i = 0; i < numberProducts; i++)
			{
                _products.Add(new Product());
			}
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

        private void AssignType()
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

        public void StartGame()
        {
            bool isWork = true;

            CreateQueue();
            ShowPurchaseInfo();

            while (isWork == true)
            {
                Console.WriteLine("1.   \n2. Начать бой \n2. Выход");

                switch (Console.ReadLine())
                {
                    case "1":

                        break;

                    case "2":

                        break;

                    case "3":
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("\nНеккоректный ввод\n");
                        break;
                }
            }
        }

        private void ShowPurchaseInfo()
        {
            Console.WriteLine("\nНовый клиент подошёл на кассу\n");
            Console.WriteLine($"\nОбщая сумма покупки: {CalculatePurchaseAmount()}");
            Console.WriteLine($"Денег у клиента {_clients.Peek().Money}");
        }

        private void CreateQueue()
        {
            int numberClients = 10;

            for (int i = 0; i < numberClients; i++)
            {
                _clients.Enqueue(new Client());
            }

            Console.WriteLine($"\nОчередь создана в колличестве {numberClients} человек\n");
        }

        private int CalculatePurchaseAmount()
        {
            int amount = 0;

            for (int i = 0; i < _clients.Peek()._basketProducts._products.Count; i++)
            {
                amount += _clients.Peek()._basketProducts._products[i].Cost;
            }

            return amount;
        }
    }
}

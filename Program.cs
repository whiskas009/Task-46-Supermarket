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

        private Basket _basket;

        public Client(Random random, List<Product> products)
        {
            DepositMoneyRandomly(random);
            _basket = new Basket(products);
        }

        public void ViewBasket()
        {
            _basket.ShowContent();
        }

        public void DeleteProductBasket()
        {
            _basket.RemoveRandomProduct();
        }

        public int GetCountNumberProducts()
        {
            return _basket.ReturnNumberProducts();
        }

        public int GetCostProduct(int indexProduct)
        {
            return _basket.ReturnCostProduct(indexProduct);
        }

        private void DepositMoneyRandomly(Random random)
        {
            int minLimit = 0;
            int maxLimit = 1000;
            Money = random.Next(minLimit, maxLimit);
        }
    }

    class Basket
    {
        private List<Product> _products = new List<Product>();

        public Basket(List<Product> products)
        {
            _products = products;
        }

        public void RemoveRandomProduct()
        {
            Random random = new Random();
            int minLimit = 0;
            int indexDelete = random.Next(minLimit, _products.Count);
            Console.WriteLine($"\n{_products[indexDelete].Name} убрано из корзины\n");
            _products.RemoveAt(indexDelete);
        }

        public void ShowContent()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Console.WriteLine($"{_products[i].Name} - {_products[i].Cost} руб.");
            }
        }

        public int ReturnNumberProducts()
        {
            return _products.Count;
        }

        public int ReturnCostProduct(int indexProduct)
        {
            return _products[indexProduct].Cost;
        }
    }

    class Product
    {
        public string Name { get; private set; }

        public int Cost { get; private set; }

        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }
    }

    class Supermarket
    {
        private Queue<Client> _clients = new Queue<Client>();

        private Dictionary<string, int> _typesProducts = new Dictionary<string, int>();

        private Random _random = new Random();

        public Supermarket()
        {
            AddTypeProduct();
        }

        public void StartGame()
        {
            bool isWork = true;
            CreateQueue();

            while (isWork == true)
            {
                Console.WriteLine($"\nДлина очереди: {_clients.Count} человек\n");
                Console.WriteLine("\nНовый клиент подошёл на кассу\n");
                Console.WriteLine("\n1. Начать работу \n2. Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        ServeCustomers();
                        break;

                    case "2":
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("\nНеккоректный ввод\n");
                        break;
                }
            }
        }

        private void ServeCustomers()
        {
            while (_clients.Count < 0)
            {
                ShowPurchaseInformation();

                Console.WriteLine($"\n1. Убрать товар из корзины \n2. Принять оплату");

                switch (Console.ReadLine())
                {
                    case "1":
                        IdentifyPossibilityPayment();
                        break;

                    case "2":
                        AcceptPayment();
                        break;

                    default:
                        Console.WriteLine("\nНеккоректный ввод\n");
                        break;
                }
            }
        }

        private void ShowPurchaseInformation()
        {
            _clients.Peek().ViewBasket();
            Console.WriteLine($"\nОбщая сумма покупки: {CalculatePurchaseAmount()}");
            Console.WriteLine($"Денег у клиента {_clients.Peek().Money}");
        }

        private void AcceptPayment()
        {
            if (CalculatePurchaseAmount() < _clients.Peek().Money)
            {
                _clients.Dequeue();
                Console.WriteLine("\nКлиент обслужен!\n");
                Console.WriteLine($"\nДлина очереди: {_clients.Count} человек\n");
                Console.WriteLine("\nНовый клиент подошёл на кассу\n");
            }
            else
            {
                Console.WriteLine("\nУ клиента недостаточно денег, чтобы оплатить покупку\n");
            }
        }

        private void IdentifyPossibilityPayment()
        {
            if (CalculatePurchaseAmount() > _clients.Peek().Money)
            {
                _clients.Peek().DeleteProductBasket();
            }
            else
            {
                Console.WriteLine("\nУ клиента достаточно денег, чтобы оплатить покупку!\n");
            }
        }

        private void CreateQueue()
        {
            int numberClients = 10;

            for (int i = 0; i < numberClients; i++)
            {
                _clients.Enqueue(new Client(_random, CreateListProduct()));
            }

            Console.WriteLine($"\nОчередь создана!\n");
        }

        private int CalculatePurchaseAmount()
        {
            int amount = 0;

            for (int i = 0; i < _clients.Peek().GetCountNumberProducts(); i++)
            {
                amount += _clients.Peek().GetCostProduct(i);
            }

            return amount;
        }

        private void AddTypeProduct()
        {
            _typesProducts.Add("Хлеб", 20);
            _typesProducts.Add("Молоко", 55);
            _typesProducts.Add("Печенье", 32);
            _typesProducts.Add("Мясо", 234);
            _typesProducts.Add("Сыр", 134);
            _typesProducts.Add("Картошка", 45);
            _typesProducts.Add("Зелень", 15);
        }

        private List<Product> CreateListProduct()
        {
            List<Product> products = new List<Product>();
            int minLimit = 0;
            int maxLimit = 10;
            int indexTypesProduct;
            int numberProducts = _random.Next(minLimit, maxLimit);

            for (int i = 0; i < numberProducts; i++)
            {
                indexTypesProduct = _random.Next(minLimit, _typesProducts.Count);
                products.Add(new Product(_typesProducts.ElementAt(indexTypesProduct).Key, _typesProducts.ElementAt(indexTypesProduct).Value));
            }

            return products;
        }
    }
}

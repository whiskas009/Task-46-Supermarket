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

        public BasketProducts BasketProducts { get; private set; }

        public Client(Random random)
        {
            DepositMoneyRandomly(random);
            BasketProducts = new BasketProducts(random);
        }

        private void DepositMoneyRandomly(Random random)
        {
            int minLimit = 0;
            int maxLimit = 1000;
            Money = random.Next(minLimit, maxLimit);
        }
    }

    class BasketProducts
    {
        private List<Product> _products = new List<Product>();

        public BasketProducts(Random random)
        {
            FillBasket(random);
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

        public int CountNumberProducts()
        {
            return _products.Count;
        }

        public int GetCostProduct(int indexProduct)
        {
            return _products[indexProduct].Cost;
        }

        private void FillBasket(Random random)
        {
            int minLimit = 0;
            int maxLimit = 10;
            int numberProducts = random.Next(minLimit, maxLimit);

            for (int i = 0; i < numberProducts; i++)
            {
                _products.Add(new Product(random));
            }
        }
    }

    class Product
    {
        private Dictionary<string, int> _typesProducts = new Dictionary<string, int>();

        public string Name { get; private set; }

        public int Cost { get; private set; }

        public Product(Random random)
        {
            AddProductType();
            AssignType(random);
        }

        private void AssignType(Random random)
        {
            int minLimit = 0;
            int indexTypesProduct = random.Next(minLimit, _typesProducts.Count);
            Name = _typesProducts.ElementAt(indexTypesProduct).Key;
            Cost = _typesProducts.ElementAt(indexTypesProduct).Value;
        }

        private void AddProductType()
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

        private Random _random = new Random();

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
                        ServeCustomer();
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

        private void ServeCustomer()
        {
            bool isWork = true;

            while (isWork == true)
            {
                ShowPurchaseInformation();

                Console.WriteLine($"\n1. Убрать товар из корзины \n2. Принять оплату");

                switch (Console.ReadLine())
                {
                    case "1":
                        CheckPossibilityPayment();
                        break;

                    case "2":
                        AcceptPayment();
                        break;

                    default:
                        Console.WriteLine("\nНеккоректный ввод\n");
                        break;
                }

                if (_clients.Count < 0)
                {
                    isWork = false;
                }
            }
        }

        private void ShowPurchaseInformation()
        {
            _clients.Peek().BasketProducts.ShowContent();
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

        private void CheckPossibilityPayment()
        {
            if (CalculatePurchaseAmount() > _clients.Peek().Money)
            {
                _clients.Peek().BasketProducts.RemoveRandomProduct();
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
                _clients.Enqueue(new Client(_random));
            }

            Console.WriteLine($"\nОчередь создана!\n");
        }

        private int CalculatePurchaseAmount()
        {
            int amount = 0;

            for (int i = 0; i < _clients.Peek().BasketProducts.CountNumberProducts(); i++)
            {
                amount += _clients.Peek().BasketProducts.GetCostProduct(i);
            }

            return amount;
        }
    }
}

using System;
using System.Collections.Generic;

namespace LLDRevision
{
    // 1. ENUM: Used constantly in LLD for states, types, and statuses
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    // 2. INTERFACE: C# convention prefixes interfaces with 'I'
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    // 3. CLASS IMPLEMENTING INTERFACE (Uses ':' instead of 'implements')
    public class UpiPayment : IPaymentStrategy
    {
        private readonly string _upiId; // private readonly field

        public UpiPayment(string upiId)
        {
            _upiId = upiId;
        }

        public void Pay(double amount)
        {
            Console.WriteLine($"Paid ₹{amount} via UPI ({_upiId})");
        }
    }

    // 4. ABSTRACT CLASS: Shared base blueprint
    public abstract class User
    {
        // Auto-implemented properties (replaces boilerplate getters/setters)
        public int Id { get; }
        public string Name { get; set; }

        protected User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract string GetRole();
    }

    // 5. INHERITANCE: Customer inherits from User
    public class Customer : User
    {
        public Customer(int id, string name) : base(id, name) { }

        // Must use 'override' keyword when implementing abstract/virtual methods
        public override string GetRole() => "Customer";
    }

    // 6. MAIN PROGRAM & COLLECTIONS REVISION
    class Program
    {
        static void Main(string[] args)
        {
            // Object creation & Polymorphism
            User customer = new Customer(101, "Rahul");
            OrderStatus status = OrderStatus.Pending;
            Console.WriteLine($"{customer.GetRole()}: {customer.Name} | Status: {status}");

            // Strategy Interface usage
            IPaymentStrategy payment = new UpiPayment("rahul@upi");
            payment.Pay(499.50);
            status = OrderStatus.Paid;

            // --- ESSENTIAL C# COLLECTIONS FOR LLD ---

            // A. List<T> (Dynamic Array)
            List<string> cart = new List<string> { "Laptop", "Mouse" };
            cart.Add("Keyboard");
            Console.WriteLine($"Cart items count: {cart.Count}");

            // B. Dictionary<TKey, TValue> (HashMap)
            Dictionary<string, int> stock = new Dictionary<string, int>();
            stock["Laptop"] = 5;
            stock["Mouse"] = 12;

            if (stock.TryGetValue("Laptop", out int qty))
            {
                Console.WriteLine($"Laptops in stock: {qty}");
            }

            // C. HashSet<T> (Unique items)
            HashSet<int> visitedIds = new HashSet<int> { 101, 102 };
            visitedIds.Add(101); // Ignored since duplicate

            // D. PriorityQueue<TElement, TPriority> (Min-Heap for Elevator/Uber LLD)
            PriorityQueue<string, int> driverQueue = new PriorityQueue<string, int>();
            driverQueue.Enqueue("Driver_A", 5); // 5 mins away
            driverQueue.Enqueue("Driver_B", 2); // 2 mins away
            Console.WriteLine($"Nearest driver: {driverQueue.Dequeue()}"); // Outputs Driver_B
        }
    }
}

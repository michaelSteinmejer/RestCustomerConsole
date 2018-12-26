using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestCustomerConsole.Model;

namespace RestCustomerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(9000);

            Console.WriteLine("Get command");
            foreach (var i in GetCustomersAsync().Result)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine("added");
            Console.WriteLine(Asyncadd(new Customer(4, "blag", "bah", 1990)).Result);
           
            Console.WriteLine("Get command");
            foreach (var i in GetCustomersAsync().Result)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine("Deleted");
            AsyncDelete(0);
            
            Console.WriteLine("Get command");
          foreach (var i in GetCustomersAsync().Result)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine("update action");

            UpdateAsync(new Customer(1,"Michael","Steinmeister",1990),1);

            Console.WriteLine("Get command");
            foreach (var i in GetCustomersAsync().Result)
            {
                Console.WriteLine(i.ToString());
            }


            Console.ReadKey();
        }

        public static string CustomerUri { get; set; } = "http://localhost:63152/api/Customer/";
        public static async Task<IList<Customer>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string CustomersUri = "http://localhost:63152/api/customer";
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);

              
                return cList;
            }
        }
        public static async Task<string> Asyncadd(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                
                Console.WriteLine("Add:" + customer);

                var jsonString = JsonConvert.SerializeObject(customer);
                
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
               HttpResponseMessage response = await client.PostAsync(CustomerUri , content);
                string str = await response.Content.ReadAsStringAsync();
                //Int32 sumStr = JsonConvert.DeserializeObject<Int32>(str);
                return str;
            }
        
        }
        public static void AsyncDelete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                
                var response = client.DeleteAsync(CustomerUri + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("customer with id: " + id + " deleted");
                }
                else
                {
                    Console.WriteLine("error");
                }
            }

        }
       
        public static void UpdateAsync(Customer c, int i)
        {
            using (HttpClient client = new HttpClient())
            {
               var jsonString = JsonConvert.SerializeObject(c);
               
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                
                Console.WriteLine("inserting customer: " + i);
                HttpResponseMessage response = client.PutAsync(CustomerUri + i, content).Result;
                //string str = await response.Content.ReadAsStringAsync();
                //Int32 sumStr = JsonConvert.DeserializeObject<Int32>(str);
                //return str;
            }
        }

    }
}

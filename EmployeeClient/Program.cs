using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmployeeClient
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            client.BaseAddress = new Uri("https://localhost:7204/api/employee/");

            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Employee Management Client =====");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. View Employee by ID");
                Console.WriteLine("3. View Employees by Department");
                Console.WriteLine("4. Add Employee");
                Console.WriteLine("5. Update Employee");
                Console.WriteLine("6. Update Employee Email");
                Console.WriteLine("7. Delete Employee");
                Console.WriteLine("0. Exit");
                Console.Write("\nEnter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) choice = -1;

                switch (choice)
                {
                    case 1:
                        await GetAllEmployees();
                        break;
                    case 2:
                        await GetEmployeeById();
                        break;
                    case 3:
                        await GetEmployeesByDept();
                        break;
                    case 4:
                        await AddEmployee();
                        break;
                    case 5:
                        await UpdateEmployee();
                        break;
                    case 6:
                        await UpdateEmployeeEmail();
                        break;
                    case 7:
                        await DeleteEmployee();
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }

            } while (choice != 0);
        }

        static async Task GetAllEmployees()
        {
            var response = await client.GetAsync("");
            var data = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nAll Employees:\n" + data);
        }

        static async Task GetEmployeeById()
        {
            Console.Write("Enter Employee ID: ");
            var id = Console.ReadLine();
            var response = await client.GetAsync($"{id}"); 
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine("\nResponse:\n" + await response.Content.ReadAsStringAsync());
        }


        static async Task GetEmployeesByDept()
        {
            Console.Write("Enter Department: ");
            var dept = Console.ReadLine();
            var encodedDept = Uri.EscapeDataString(dept); 
            var response = await client.GetAsync($"bydept?department={encodedDept}"); 
            Console.WriteLine("\nResponse:\n" + await response.Content.ReadAsStringAsync());
        }


        static async Task AddEmployee()
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();
            Console.Write("Enter Mobile No (10 digits): ");
            string mobile = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            var newEmp = new { Id = id, Name = name, Department = dept, MobileNo = mobile, Email = email };

            var json = JsonConvert.SerializeObject(newEmp);
            var response = await client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));

            Console.WriteLine("\nAdd Employee Status: " + response.StatusCode);
        }

        static async Task UpdateEmployee()
        {
            Console.Write("Enter Employee ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Department: ");
            string dept = Console.ReadLine();
            Console.Write("Enter New Mobile No: ");
            string mobile = Console.ReadLine();
            Console.Write("Enter New Email: ");
            string email = Console.ReadLine();

            var emp = new { Id = id, Name = name, Department = dept, MobileNo = mobile, Email = email };

            var json = JsonConvert.SerializeObject(emp);
            var response = await client.PutAsync($"{id}", new StringContent(json, Encoding.UTF8, "application/json"));

            Console.WriteLine("\nUpdate Employee Status: " + response.StatusCode);
        }

        static async Task UpdateEmployeeEmail()
        {
            Console.Write("Enter Employee ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Email: ");
            string email = Console.ReadLine();

            var json = JsonConvert.SerializeObject(email);
            var response = await client.PatchAsync($"{id}/email", new StringContent(json, Encoding.UTF8, "application/json"));

            Console.WriteLine("\nUpdate Email Status: " + response.StatusCode);
        }

        static async Task DeleteEmployee()
        {
            Console.Write("Enter Employee ID to Delete: ");
            int id = int.Parse(Console.ReadLine());

            string endpoint = $"{id}";

            Console.WriteLine($"DELETE: {client.BaseAddress}{endpoint}");

            var response = await client.DeleteAsync(endpoint);
            Console.WriteLine("\nDelete Status: " + response.StatusCode);
        }

    }
}

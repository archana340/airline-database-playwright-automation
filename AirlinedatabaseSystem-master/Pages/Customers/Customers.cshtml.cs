using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace AirlinedatabaseSystem.Pages.Customers
{
    public class CustomersModel : PageModel
    {
        public List<Customerinfo> listCustomers = new List<Customerinfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Customers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customerinfo customerinfo = new Customerinfo();
                                customerinfo.customer_id = reader.GetInt32(0).ToString();
                                customerinfo.First_name = reader.GetString(1);
                                customerinfo.Last_name = reader.GetString(2);
                                customerinfo.email = reader.GetString(3);
                                customerinfo.Phone_number = reader.GetString(4);
                                listCustomers.Add(customerinfo);


                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.ToString());
            }
        }
    }

    public class Customerinfo
    {
        public string customer_id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string First_name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Last_name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]

        public string Phone_number { get; set; }
    }

}
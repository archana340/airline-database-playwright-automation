using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AirlinedatabaseSystem.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Customerinfo customerinfo { get; set; }

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String customer_id = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Customers WHERE customer_id=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", customer_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerinfo = new Customerinfo
                                {
                                    customer_id = reader.GetInt32(0).ToString(),
                                    First_name = reader.GetString(1),
                                    Last_name = reader.GetString(2),
                                    email = reader.GetString(3),
                                    Phone_number = reader.GetString(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return with error messages
                errorMessage = "Please fix the validation errors.";
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Customers SET First_name = @FirstName, Last_name = @LastName, email = @Email, Phone_number = @Phone WHERE customer_id = @customer_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@customer_id", customerinfo.customer_id);
                        command.Parameters.AddWithValue("@FirstName", customerinfo.First_name);
                        command.Parameters.AddWithValue("@LastName", customerinfo.Last_name);
                        command.Parameters.AddWithValue("@Email", customerinfo.email);
                        command.Parameters.AddWithValue("@Phone", customerinfo.Phone_number);
                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Customer information updated successfully!";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Customers");
        }
    }
}

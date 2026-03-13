using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.Customers
{
    public class CreateModel : PageModel
    {


        public Customerinfo customerinfo=new Customerinfo();
        public String errormessage = "";
        public String successMessage = "";
        
        public void OnGet()
        {
        }


        public void OnPost()
        {

            //customerinfo.customer_id = Request.Form["ID"];
            customerinfo.First_name = Request.Form["FirstName"];
            customerinfo.Last_name = Request.Form["LastName"];
            customerinfo.email = Request.Form["Email"];
            customerinfo.Phone_number = Request.Form["PhoneNumber"];

            if (customerinfo.First_name.Length == 0 || customerinfo.Last_name.Length == 0 || customerinfo.Phone_number.Length == 0 || customerinfo.email.Length == 0)
            {
                errormessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Customers (First_name, Last_name, email, Phone_number) VALUES (@FirstName, @LastName, @Email, @Phone)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        //command.Parameters.AddWithValue("@ID", customerinfo.customer_id);
                        command.Parameters.AddWithValue("@FirstName", customerinfo.First_name);
                        command.Parameters.AddWithValue("@LastName", customerinfo.Last_name);
                        command.Parameters.AddWithValue("@Email", customerinfo.email);
                        command.Parameters.AddWithValue("@Phone", customerinfo.Phone_number);
                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "New Customer Added Successfully";
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
            }

            // Clear form fields after submission
            //customerinfo.customer_id = "";
            customerinfo.First_name = "";
            customerinfo.Last_name = "";
            customerinfo.email = "";
            customerinfo.Phone_number = "";
        }
    }
}
    
    
    
    
    
    
    
    
    
    


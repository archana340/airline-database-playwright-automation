using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.SpecialServices
{
    public class SpecialServicesModel : PageModel
    {
        public List<SpecialService> SpecialServices { get; set; }

        public void OnGet()
        {
            SpecialServices = new List<SpecialService>();

            string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM SpecialServices";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SpecialService service = new SpecialService
                            {
                                ServiceID = reader.GetInt32(0),
                                reservation_id = reader.GetInt32(1),
                                ServiceType = reader.GetString(2)
                            };
                            SpecialServices.Add(service);
                        }
                    }
                }
            }
        }
    }

    public class SpecialService
    {
        public int ServiceID { get; set; }
        public int reservation_id { get; set; }
        public string ServiceType { get; set; }
    }
}

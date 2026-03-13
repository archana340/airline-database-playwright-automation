using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.Airports
{
    public class AirportsModel : PageModel

    {

        public List<AirportInfo> listAirports = new List<AirportInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Airports";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AirportInfo airportInfo = new AirportInfo();
                                airportInfo.AirportCode = reader.GetString(0);
                                airportInfo.AirportName = reader.GetString(1);
                                airportInfo.City = reader.GetString(2);
                                airportInfo.Country = reader.GetString(3);
                                listAirports.Add(airportInfo);
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

    public class AirportInfo
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
        
    


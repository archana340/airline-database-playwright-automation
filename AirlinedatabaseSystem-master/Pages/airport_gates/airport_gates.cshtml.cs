using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.airport_gates
{
    public class airport_gatesModel : PageModel
    {
        public List<AirportGateInfo> ListAirportGates = new List<AirportGateInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM airport_gates";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AirportGateInfo airportGateInfo = new AirportGateInfo();
                                airportGateInfo.GateId = reader.GetInt32(0);
                                airportGateInfo.AirportCode = reader.GetString(1);
                                airportGateInfo.GateNumber = reader.GetString(2);
                                ListAirportGates.Add(airportGateInfo);
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

    public class AirportGateInfo
    {
        public int GateId { get; set; }
        public string AirportCode { get; set; }
        public string GateNumber { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.Aircrafts
{
    public class AircraftsModel : PageModel
    {
        public List<AircraftsInfo> listAircrafts = new List<AircraftsInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Aircrafts"; // Select data from the Aircrafts table
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AircraftsInfo aircraftInfo = new AircraftsInfo();
                               
                                aircraftInfo.AircraftId = reader.GetInt32(0).ToString();
                                aircraftInfo.AircraftName = reader.GetString(1);
                                aircraftInfo.Capacity = reader.GetInt32(2).ToString();
                                listAircrafts.Add(aircraftInfo);
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

    public class AircraftsInfo
    {
        public string AircraftId { get; set; }
        public string AircraftName { get; set; }
        public string Capacity { get; set; }
    }
}

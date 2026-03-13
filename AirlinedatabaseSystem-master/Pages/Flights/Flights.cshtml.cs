using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.Flights
{
    public class FlightsModel : PageModel
    {
        public List<FlightInfo> listFlights = new List<FlightInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Flights";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FlightInfo flightInfo = new FlightInfo();
                                flightInfo.flight_id = reader.GetInt32(0);
                                flightInfo.flight_number = reader.GetString(1);
                                flightInfo.Departure_airport_code = reader.GetString(2);
                                flightInfo.Departure_time = reader.GetDateTime(3);
                                flightInfo.Arrival_time = reader.GetDateTime(4);
                                flightInfo.aircraft_id = reader.GetInt32(5); ;
                                listFlights.Add(flightInfo);
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

    public class FlightInfo
    {
        public int flight_id { get; set; }
        public string flight_number { get; set; }
        public string Departure_airport_code { get; set; }
        public DateTime Departure_time { get; set; }
        public DateTime Arrival_time { get; set; }

        public int aircraft_id { get; set;}
    }
}

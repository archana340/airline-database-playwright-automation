using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AirlinedatabaseSystem.Pages.Flights
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public FlightInfo flightInfo { get; set; }

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String flight_id = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Flights WHERE Flight_id = @ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", flight_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                flightInfo = new FlightInfo
                                {
                                    flight_number = reader.GetString(1),
                                    Departure_airport_code = reader.GetString(2),
                                    Departure_time = reader.GetDateTime(3),
                                    Arrival_time = reader.GetDateTime(4)
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

                errorMessage = "Please fix the validation errors.";
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Flights SET Flight_number = @FlightNumber, Departure_airport_code = @DepartureAirportCode, Departure_time = @DepartureTime, Arrival_time = @ArrivalTime WHERE Flight_id in (select flight_id from flights where flight_number = @FlightNumber)"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //command.Parameters.AddWithValue("@FlightID", flightInfo.flight_id);
                        command.Parameters.AddWithValue("@FlightNumber", flightInfo.flight_number);
                        command.Parameters.AddWithValue("@DepartureAirportCode", flightInfo.Departure_airport_code);
                        command.Parameters.AddWithValue("@DepartureTime", flightInfo.Departure_time);
                        command.Parameters.AddWithValue("@ArrivalTime", flightInfo.Arrival_time);
                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Flight information updated successfully!";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Flights/Flights");
        }
    }
}




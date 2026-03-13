using AirlinedatabaseSystem.Pages.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;

namespace AirlinedatabaseSystem.Pages.Flights
{
    public class CreateModel : PageModel
    {
        public FlightInfo flightInfo = new FlightInfo();
        public String errormessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            flightInfo.flight_number = Request.Form["FlightNumber"];
            flightInfo.Departure_airport_code = Request.Form["DepartureAirportCode"];

            // Convert the DepartureTime from string to DateTime
            if (!DateTime.TryParse(Request.Form["DepartureTime"], out DateTime departureTime))
            {
                errormessage = "Invalid Departure Time format.";
                return;
            }

            // Convert the ArrivalTime from string to DateTime
            if (!DateTime.TryParse(Request.Form["ArrivalTime"], out DateTime arrivalTime))
            {
                errormessage = "Invalid Arrival Time format.";
                return;
            }

            // Assign the converted times to FlightInfo
            flightInfo.Departure_time = departureTime;
            flightInfo.Arrival_time = arrivalTime;

            // Check if any required fields are empty
            if (flightInfo.flight_number.Length == 0 ||
                flightInfo.Departure_airport_code.Length == 0)
            {
                errormessage = "Flight Number and Departure Airport Code are required fields.";
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Flights (FlightNumber, DepartureAirportCode, DepartureTime, ArrivalTime) VALUES (@FlightNumber, @DepartureAirportCode, @DepartureTime, @ArrivalTime)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FlightNumber", flightInfo.flight_number);
                        command.Parameters.AddWithValue("@DepartureAirportCode", flightInfo.Departure_airport_code);
                        command.Parameters.AddWithValue("@DepartureTime", flightInfo.Departure_time);
                        command.Parameters.AddWithValue("@ArrivalTime", flightInfo.Arrival_time);
                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "New Flight Added Successfully";
            }
            catch (Exception ex)
            {
                errormessage = "Error: " + ex.Message;
            }

            // Clear form fields after submission
            flightInfo.flight_number = "";
            flightInfo.Departure_airport_code = "";
            flightInfo.Departure_time = DateTime.MinValue;
            flightInfo.Arrival_time = DateTime.MinValue;
        }
    }
}

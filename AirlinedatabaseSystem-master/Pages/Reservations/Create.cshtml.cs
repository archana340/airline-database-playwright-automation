using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        public ReservationInfo reservationInfo = new ReservationInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            reservationInfo.CustomerId = int.Parse(Request.Form["CustomerID"]);
            reservationInfo.FlightId = int.Parse(Request.Form["FlightID"]);
            reservationInfo.SeatNumber = Request.Form["SeatNumber"];

            if (!DateTime.TryParse(Request.Form["BookingDate"], out DateTime bookingDate))
            {
                ErrorMessage = "Invalid Booking Date format.";
                return;
            }

            reservationInfo.BookingDate = bookingDate;

            if (reservationInfo.CustomerId <= 0 || reservationInfo.FlightId <= 0 || string.IsNullOrWhiteSpace(reservationInfo.SeatNumber))
            {
                ErrorMessage = "Customer ID, Flight ID, and Seat Number are required fields.";
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //string sql = "INSERT INTO Reservations (CustomerID, FlightID, SeatNumber, BookingDate) VALUES (@CustomerID, @FlightID, @SeatNumber, @BookingDate)";
                    string sql = "INSERT INTO Reservations (customer_id, flight_id, seat_number, booking_date) VALUES (@CustomerID, @FlightID, @SeatNumber, @BookingDate)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", reservationInfo.CustomerId);
                        command.Parameters.AddWithValue("@FlightID", reservationInfo.FlightId);
                        command.Parameters.AddWithValue("@SeatNumber", reservationInfo.SeatNumber);
                        command.Parameters.AddWithValue("@BookingDate", reservationInfo.BookingDate);
                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "New Reservation Added Successfully";
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error: " + ex.Message;
            }

            // Clear form fields after submission
            reservationInfo.CustomerId = 0;
            reservationInfo.FlightId = 0;
            reservationInfo.SeatNumber = "";
            reservationInfo.BookingDate = DateTime.MinValue;
        }
    }
}

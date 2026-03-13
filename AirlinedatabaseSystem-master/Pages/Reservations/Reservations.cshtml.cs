using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.Reservations
{
    public class ReservationsModel : PageModel
    {
        public List<ReservationInfo> listReservations = new List<ReservationInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Reservations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReservationInfo reservationInfo = new ReservationInfo();
                                reservationInfo.ReservationId = reader.GetInt32(0);
                                reservationInfo.CustomerId = reader.GetInt32(1);
                                reservationInfo.FlightId = reader.GetInt32(2);
                                reservationInfo.SeatNumber = reader.GetString(3);
                                reservationInfo.BookingDate = reader.GetDateTime(4);
                                listReservations.Add(reservationInfo);
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

    public class ReservationInfo
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int FlightId { get; set; }
        public string SeatNumber { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.TicketPrices
{
    public class TicketPricesModel : PageModel
    {
        public List<TicketPriceInfo> listTicketPrices = new List<TicketPriceInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ticket_prices";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TicketPriceInfo ticketPriceInfo = new TicketPriceInfo();
                                ticketPriceInfo.PriceId = reader.GetInt32(0);
                                ticketPriceInfo.FlightId = reader.GetInt32(1);
                                ticketPriceInfo.Class = reader.GetString(2);
                                ticketPriceInfo.Price = reader.GetDecimal(3);
                                listTicketPrices.Add(ticketPriceInfo);
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

    public class TicketPriceInfo
    {
        public int PriceId { get; set; }
        public int FlightId { get; set; }
        public string Class { get; set; }
        public decimal Price { get; set; }
    }
}

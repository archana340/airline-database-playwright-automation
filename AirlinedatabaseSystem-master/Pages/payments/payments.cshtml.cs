using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AirlinedatabaseSystem.Pages.payments
{
    public class paymentsModel : PageModel
    {
        public List<PaymentInfo> listPayments = new List<PaymentInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM payments";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PaymentInfo payment = new PaymentInfo();
                                payment.PaymentId = reader.GetInt32(0);
                                payment.ReservationId = reader.GetInt32(1);
                                payment.Amount = reader.GetDecimal(2);
                                payment.PaymentDate = reader.GetDateTime(3);
                                listPayments.Add(payment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, perhaps log the error
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class PaymentInfo
    {
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

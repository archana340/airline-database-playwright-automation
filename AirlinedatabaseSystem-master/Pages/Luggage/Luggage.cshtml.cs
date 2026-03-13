using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.Luggage
{
    public class LuggageModel : PageModel
    {
        public List<LuggageInfo> LuggageList { get; set; }
        public string ErrorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            LuggageList = new List<LuggageInfo>();

            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Luggage";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LuggageInfo luggage = new LuggageInfo
                                {
                                    LuggageId = reader.GetInt32(0),
                                    ReservationId = reader.GetInt32(1),
                                    Weight = reader.GetDecimal(2)
                                };
                                LuggageList.Add(luggage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error: " + ex.Message;
            }
        }
    }

    public class LuggageInfo
    {
        public int LuggageId { get; set; }
        public int ReservationId { get; set; }
        public decimal Weight { get; set; }
    }
}

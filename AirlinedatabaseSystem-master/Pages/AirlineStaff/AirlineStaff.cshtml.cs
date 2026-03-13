using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AirlinedatabaseSystem.Pages.AirlineStaff
{
    public class AirlineStaffModel : PageModel
    {
        public List<StaffInfo> listStaff = new List<StaffInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-V2E7HK5I;Initial Catalog=AirlineDatabaseSystem;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Airline_staff";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StaffInfo staffInfo = new StaffInfo();
                                staffInfo.StaffId = reader.GetInt32(0);
                                staffInfo.StaffName = reader.GetString(1);
                                staffInfo.Role = reader.GetString(2);
                                listStaff.Add(staffInfo);
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

    public class StaffInfo
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Role { get; set; }
    }
}

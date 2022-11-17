using System;
using System.Data.SqlClient;

namespace SepCommon.DAL
{
    public static class BackgroundProcesses
    {
        public static void BackgroundSMSSave(string toUserId, string Message, DateTime SendDateTime)
        {

            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PhoneNUmber", toUserId)) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioPhoneNumber")))
            {
                var sProcessId = SepFunctions.GetIdentity();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, @ProcessName, @IntervalSeconds, @Status, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessID", sProcessId);
                        cmd.Parameters.AddWithValue("@ProcessName", "SMS");
                        cmd.Parameters.AddWithValue("@IntervalSeconds", 1);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO BG_SMS (ProcessID, To_Phone, From_Phone, Message_Body, Send_Date) VALUES(@ProcessID, @To_Phone, @From_Phone, @Message_Body, @Send_Date)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcessId", sProcessId);
                        cmd.Parameters.AddWithValue("@To_Phone", SepFunctions.FormatPhone(SepFunctions.GetUserInformation("PhoneNUmber", toUserId)));
                        cmd.Parameters.AddWithValue("@From_Phone", SepFunctions.Setup(989, "TwilioPhoneNumber"));
                        cmd.Parameters.AddWithValue("@Message_Body", Message);
                        cmd.Parameters.AddWithValue("@Send_Date", SendDateTime);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}

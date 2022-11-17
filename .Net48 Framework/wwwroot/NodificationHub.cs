using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace wwwroot
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {

        Int16 totalNewMessages = 0;
        Int16 totalNewCircles = 0;
        Int16 totalNewJobs = 0;
        Int16 totalNewNotification = 0;

        [HubMethodName("sendNotifications")]
        public string SendNotifications()
        {

            using (var connection = new SqlConnection(SepCommon.SepFunctions.Database_Connection()))
            {
                string query = "SELECT  NewMessageCount, NewCircleRequestCount, NewNotificationsCount, NewJobNotificationsCount FROM [dbo].[Modeling_NewMessageNotificationCount] WHERE UserProfileId=" + "61764";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    DataTable dt = new DataTable();
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        totalNewMessages = Int16.Parse(dt.Rows[0]["NewMessageCount"].ToString());
                        totalNewCircles = Int16.Parse(dt.Rows[0]["NewCircleRequestCount"].ToString());
                        totalNewJobs = Int16.Parse(dt.Rows[0]["NewJobNotificationsCount"].ToString());
                        totalNewNotification = Int16.Parse(dt.Rows[0]["NewNotificationsCount"].ToString());
                    }
                }
            }

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            return context.Clients.All.RecieveNotification(totalNewMessages, totalNewCircles, totalNewJobs, totalNewNotification);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotificationHub nHub = new NotificationHub();
                nHub.SendNotifications();
            }
        }

    }

}
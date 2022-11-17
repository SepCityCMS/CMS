using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZoomNet;
namespace SepCommon
{
    /// <summary>
    /// Zoom Integration
    /// </summary>
    public class Zoom
    {

        private readonly string API_KEY = SepFunctions.Setup(989, "ZoomAPIKey");
        private readonly string API_SECRET = SepFunctions.Setup(989, "ZoomAPISecret");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromEmailAddress"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="topic"></param>
        /// <param name="agenda"></param>
        /// <param name="startDateTime"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public string createMeeting(string fromEmailAddress, string toEmailAddress, string topic, string agenda, DateTime startDateTime, int duration)
        {
            var connectionInfo = new JwtConnectionInfo(API_KEY, API_SECRET);

            var zoomClient = new ZoomClient(connectionInfo);
            var myMeeting = zoomClient.Meetings.CreateScheduledMeetingAsync(fromEmailAddress, topic, agenda, startDateTime, duration);

            meetingEmail(fromEmailAddress, toEmailAddress, myMeeting.Result.StartUrl, topic, startDateTime, duration);

            return myMeeting.Result.Uuid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> getUsers()
        {
            List<string> returnUsers = new List<string>();

            var connectionInfo = new JwtConnectionInfo(API_KEY, API_SECRET);

            var zoomClient = new ZoomClient(connectionInfo);
            var zoomUsers = await zoomClient.Users.GetAllAsync(ZoomNet.Models.UserStatus.Active, null, 30, null).ConfigureAwait(false);

            var user = zoomUsers.Records;
            for (int i = 0; i < user.Length; i++)
            {
                returnUsers.Add(user[i].Email);
            }

            return returnUsers;

        }

        private void meetingEmail(string fromEmail, string toEmail, string location, string topic, DateTime startDateTime, int duration)
        {
            // Create calendar event
            SepFunctions.VCalendar calEvent = new SepFunctions.VCalendar
            {
                summary = topic,
                location = location,
                year = startDateTime.Year,
                month = startDateTime.Month,
                day = startDateTime.Day,
                hour = startDateTime.Hour,
                minute = startDateTime.Minute,
                second = startDateTime.Second,
                duration = duration,
                method = "REQUEST"
            };

            SepFunctions.Send_Email(toEmail, fromEmail, topic, calEvent.summary, 0, calEvent: CreateCalendarRecord(calEvent));
        }

        private string CreateCalendarRecord(SepFunctions.VCalendar calEvent)
        {
            string calRecord;
            calRecord = "BEGIN:VCalendar" + Environment.NewLine;
            calRecord += "METHOD:" + calEvent.method + Environment.NewLine;
            calRecord += "BEGIN:VEVENT" + Environment.NewLine;
            calRecord += "DTSTART:" + calEvent.year.ToString("0000") + calEvent.month.ToString("00") + calEvent.day.ToString("00");

            calRecord += "T" + calEvent.hour.ToString("00") + calEvent.minute.ToString("00") + calEvent.second.ToString("00") + "Z" + Environment.NewLine;

            calEvent.hour += calEvent.duration;
            calRecord += "DTEND:" + calEvent.year.ToString("0000") + calEvent.month.ToString("00") + calEvent.day.ToString("00");

            calRecord += "T" + calEvent.hour.ToString("00") + calEvent.minute.ToString("00") + calEvent.second.ToString("00") + "Z" + Environment.NewLine;

            calRecord += "LOCATION:" + calEvent.location + Environment.NewLine;
            calRecord += "SUMMARY:" + calEvent.summary + Environment.NewLine;

            // Calculate unique ID based on current DateTime and its MD5 hash
            string strHash = string.Empty;
            foreach (byte b in (new System.Security.Cryptography.MD5CryptoServiceProvider()).ComputeHash(System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString())))
            {
                strHash += b.ToString("X2");
            }
            calRecord += "UID:" + strHash + Environment.NewLine;
            calRecord += "END:VEVENT" + Environment.NewLine;
            calRecord += "END:VCalendar";
            return calRecord;
        }

    }
}

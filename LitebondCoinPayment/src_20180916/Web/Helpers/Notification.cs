using System;

namespace WebUI.Helpers
{
    public class Notification
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public bool IsSuccess { get; set; }

        public static Notification AddNotification(bool isSuccess, string message, Exception ex)
        {
            var notification = new Notification { IsSuccess = isSuccess, Message = message, Exception = ex };
            return notification;
        }

        public static Notification AddNotification(Notification data)
        {
            return data;
        }
    }
}
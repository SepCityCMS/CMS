using SepCityCMS.Client.Models;

namespace SepCityCMS.Client.Services
{
    public interface IAlertService
    {
        event Action<Alert> OnAlert;
        void Success(string message, bool keepAfterRouteChange = false, bool autoClose = false);
        void Error(string? message, bool keepAfterRouteChange = false, bool autoClose = false);
        void Info(string message, bool keepAfterRouteChange = false, bool autoClose = false);
        void Warn(string message, bool keepAfterRouteChange = false, bool autoClose = false);
        void Alert(Alert alert);
        void Clear(string id = null);
    }

    public class AlertService : IAlertService
    {
        private const string _defaultId = "default-alert";
        public event Action<Alert> OnAlert;

        public void Success(string? message, bool keepAfterRouteChange = false, bool autoClose = false)
        {
            if(message == null)
            {
                message = "Unknown Success";
            }
            this.Alert(new Alert
            {
                Type = AlertType.Success,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        public void Error(string? message, bool keepAfterRouteChange = false, bool autoClose = false)
        {
            if (message == null)
            {
                message = "Unknown Error";
            }
            this.Alert(new Alert
            {
                Type = AlertType.Error,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        public void Info(string? message, bool keepAfterRouteChange = false, bool autoClose = false)
        {
            if (message == null)
            {
                message = "Unknown Information";
            }
            this.Alert(new Alert
            {
                Type = AlertType.Info,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        public void Warn(string? message, bool keepAfterRouteChange = false, bool autoClose = false)
        {
            if (message == null)
            {
                message = "Unknown Warning";
            }
            this.Alert(new Alert
            {
                Type = AlertType.Warning,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        public void Alert(Alert alert)
        {
            alert.Id = alert.Id ?? _defaultId;
            this.OnAlert?.Invoke(alert);
        }

        public void Clear(string id = _defaultId)
        {
            this.OnAlert?.Invoke(new Alert { Id = id });
        }
    }
}
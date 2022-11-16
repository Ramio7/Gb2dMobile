using UnityEngine;
using UnityEngine.UI;
using Tool.PushNotifications;
using Tool.PushNotifications.Settings;

namespace Tool.Notifications.Examples
{
    internal class NotificationWindow : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private NotificationSettings _settings;

        [Header("Scene Components")]
        [SerializeField] private Button _buttonNotification;

        private INotificationScheduler _scheduler;


        private void Awake()
        {
            var schedulerFactory = new NotificationSchedulerFactory(_settings);
            _scheduler = schedulerFactory.Create();
        }

        private void OnEnable()
        {
            _buttonNotification.onClick.AddListener(CreateNotification); 

            foreach (NotificationData notificationData in _settings.Notifications)
                if (notificationData.RepeatType != NotificationRepeat.OnButtonClick || notificationData.RepeatType != NotificationRepeat.OnGameExit)
                    _scheduler.ScheduleNotification(notificationData);
        }

        private void OnDisable()
        {
            _buttonNotification.onClick.RemoveAllListeners();
            foreach (NotificationData notificationData in _settings.Notifications)
                if (notificationData.RepeatType == NotificationRepeat.OnGameExit)
                    _scheduler.ScheduleNotification(notificationData);
        }

        private void CreateNotification()
        {
            foreach (NotificationData notificationData in _settings.Notifications)
                if (notificationData.RepeatType == NotificationRepeat.OnButtonClick)
                _scheduler.ScheduleNotification(notificationData);
        }
    }
}
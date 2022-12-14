using System;
using Tool.PushNotifications.Settings;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
#endif

namespace Tool.PushNotifications
{
    internal class AndroidNotificationScheduler : INotificationScheduler
    {
        public void RegisterChannel(ChannelSettings channelSettings)
        {
#if UNITY_ANDROID
            var androidNotificationChannel = new AndroidNotificationChannel
            (
                channelSettings.Id,
                channelSettings.Name,
                channelSettings.Description,
                channelSettings.Importance
            );

            AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
#endif
        }

        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_ANDROID
            AndroidNotification androidNotification = CreateAndroidNotification(notificationData);
            AndroidNotificationCenter.SendNotification(androidNotification, notificationData.Id);
#endif
        }

#if UNITY_ANDROID
        private AndroidNotification CreateAndroidNotification(NotificationData notificationData) =>
            notificationData.RepeatType switch
            {
                NotificationRepeat.Once => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime
                ),

                NotificationRepeat.Repeatable => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime,
                    notificationData.RepeatInterval
                ),

                NotificationRepeat.OnGameStart => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    DateTime.UtcNow
                ),

                NotificationRepeat.OnGameExit => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    DateTime.UtcNow
                ),

                NotificationRepeat.OnButtonClick => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    DateTime.UtcNow
                ),

                _ => throw new ArgumentOutOfRangeException(nameof(notificationData.RepeatType))
            };
#endif
    }
}

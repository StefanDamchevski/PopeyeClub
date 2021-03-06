﻿namespace PopeyeClub.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public string UserFromId { get; set; }
        public string UserFromName { get; set; }
        public string UserFromImage { get; set; }
        public ViewModelEnums.FollowStatus Status { get; set; }
        public ViewModelEnums.NotificationViewModelType Type { get; set; }
        public string DateSent { get; set; }
        public string NotificationMessage { get; set; }
    }
}

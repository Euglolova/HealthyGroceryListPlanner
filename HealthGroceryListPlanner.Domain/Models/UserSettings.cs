using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Domain.Models

{
        public class UserSettings
        {
            public int Id { get; set; }

            
            public string Theme { get; set; } = "Light";

            public bool NotificationsEnabled { get; set; }
            public bool AutoSaveEnabled { get; set; }

            public string ReminderFrequency { get; set; } = "Off";

            public int UserId { get; set; }
            public User? User { get; set; }
        }
}
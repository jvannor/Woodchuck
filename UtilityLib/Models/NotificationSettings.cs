using System;
using System.Collections.Generic;

namespace UtilityLib.Models
{
    public partial class NotificationSettings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public bool Enabled { get; set; }
    }
}

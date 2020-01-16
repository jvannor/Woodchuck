using System;
using System.Collections.Generic;

namespace UtilityLib.Models
{
    public partial class CameraSettings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}

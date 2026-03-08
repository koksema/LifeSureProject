using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeSureProject.Models.ViewModels
{
    public class ServicesAIViewModel
    {
        public string ServicesCardDescription { get; set; }

        public string ServicesCardImageUrl { get; set; }

        public string ServicesCardTitle { get; set; }

        public string ServicesCardIcon { get; set; }
        public int? ServiceId { get; set; }
    }
}  
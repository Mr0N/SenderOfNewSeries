using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Model
{
    internal class Info
    {
        [Key]
        public int Id { set; get; }
        public string UriViews { set; get; }
        public string NameSerial { set; get; }
        public string SezonAndSeria { set; get; }
        public string UriImagePrevies { set; get; }
        public bool CheckIsSendToTelegram { set; get; }
    }
}

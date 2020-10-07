using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.ViewModels
{
    public class SendingEmailViewModel
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string ReceiverEmail { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}

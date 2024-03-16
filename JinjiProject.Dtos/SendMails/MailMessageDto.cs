using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.SendMails
{
    public class MailMessageDto
    {
        public MailMessageDto(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;

        }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}

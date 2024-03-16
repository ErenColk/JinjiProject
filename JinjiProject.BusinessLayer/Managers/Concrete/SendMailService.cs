using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Dtos.SendMails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class SendMailService : ISendMailService
    {
        private readonly IOptions<EmailConfigurationDto> configuration;

        public SendMailService(IOptions<EmailConfigurationDto> configuration)
        {
            this.configuration = configuration;
        }
        private async Task<MimeMessage> CreateEmailContent(MailMessageDto message)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Jinji Destek", configuration.Value.From);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", message.To);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Content;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = message.Subject;

            return mimeMessage;
        }

        /// <summary>
        /// Oluşturulan mesajın gönderilmesi işlemi gerçekleştirilir.
        /// </summary>
        /// <param name="message">Gönderilecek mesaj</param>
        private async Task SendMail(MailMessageDto message)
        {
            var mailMessage = await CreateEmailContent(message);

            SmtpClient client = new SmtpClient();
            client.Connect("win-ft.wlsrv.com", 465, true);
            client.Authenticate("destek@jinji.com.tr", "835m$hL1u");
            client.Send(mailMessage);
            client.Disconnect(true);
        }

        public async Task SendEmailRenewPassword(RenewPasswordDto renewPasswordDto)
        {
            var htmlContent = $@"<div>Şifrenizi yenilemek için <a href=""{renewPasswordDto.Link}"">buraya tıklayınız.</a></div>";

            MailMessageDto message = new MailMessageDto(renewPasswordDto.Email, "Jinji Şifremi Unuttum", htmlContent);

            await SendMail(message);
        }
    }
}

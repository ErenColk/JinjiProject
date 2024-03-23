using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Genres;
using JinjiProject.Dtos.Products;
using JinjiProject.Dtos.SendMails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
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
        private readonly ISubscriberService _subscriberService;

        public SendMailService(IOptions<EmailConfigurationDto> configuration, ISubscriberService subscriberService)
        {
            this.configuration = configuration;
            _subscriberService = subscriberService;
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

        private async Task<MimeMessage> CreateImageEmailContent(MailMessageDto message,BodyBuilder bodyBuilder)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Jinji Destek", configuration.Value.From);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", message.To);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

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

        private async Task SendImageMail(MailMessageDto message,BodyBuilder bodyBuilder)
        {
            var mailMessage = await CreateImageEmailContent(message,bodyBuilder);

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

        public async Task<DataResult<Subscriber>> SendMailAllSubscriber(List<ListProductDto> listProductDtos, string urL)
        {

            BodyBuilder bodyBuilder = new BodyBuilder();
            var subscribers = await _subscriberService.GetAllSubscriber();

            foreach (var subscriber in subscribers.Data)
            {
                var htmlContent = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
        }}
        .card-container {{display: flex;
    overflow-y: auto;
    max-height: 600px; 
    justify-content: space-between;
  }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .product {{
    padding: 20px;
    margin: 10px 0;
    border-bottom: 1px solid #ccc;
    border: 1px solid;
    border-radius: 10px;
    margin-right:10px;
        }}
        .product-name {{
            font-weight: bold;
            font-size: 18px;
            color: #007bff;
        }}
        .product-price {{
            color: #dc3545;
            text-decoration: line-through;
        }}
        .product-old-price {{
            color: #28a745;
        }}
        .product-link {{
            color: #007bff;
            text-decoration: none;
        }}
        .product-link:hover {{
            text-decoration: underline;
        }}
        .signature {{
            margin-top: 20px;
            font-style: italic;
            color: #6c757d;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <p>Merhaba,</p>
        <p>Size ürünler hakkında bir güncelleme yapmak istiyoruz. Aşağıda bulunan ürünler şu anda indirime girdi! İndirimli fiyatlarımızı kaçırmayın.</p><div class=""card-container"">";

                foreach (var productDto in listProductDtos)
                {                  
                    var image = bodyBuilder.LinkedResources.Add($"wwwroot/{productDto.ImagePath}");
                    image.ContentId = MimeUtils.GenerateMessageId();
                    {
                  
                        htmlContent += $@"
        <div class='product'>
            <img style='max-width:100px; max-height:100px;' src='cid:{image.ContentId}' />
            <p class='product-name'><strong>{productDto.Name}</strong></p>
            <p><span class='product-price'>{productDto.Price}₺</span> <span class='product-old-price'>{productDto.OldPrice}₺</span></p>
            <a class='product-link' href='{urL + '/' + productDto.Id}'>Ürüne Git</a>
        </div>";
                    }
                }

                htmlContent += $@"</div>
        <p>Ürünlere gitmek için yanlarındaki bağlantıya tıklayabilirsiniz.</p>
        <p>Fırsatları kaçırmayın!</p>
        <p class='signature'>Saygılarımla,<br>
        [Jinji]</p>
    </div>
</body>
</html>";

                bodyBuilder.HtmlBody = htmlContent;
                MailMessageDto message = new MailMessageDto(subscriber.Email, "Ürün İndirimi Hakkında", htmlContent);
                await SendImageMail(message,bodyBuilder);
            }

            return new SuccessDataResult<Subscriber>(Messages.SendMailSuccess);


        }
    }

}




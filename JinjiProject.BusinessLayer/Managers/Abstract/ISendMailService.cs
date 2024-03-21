using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Products;
using JinjiProject.Dtos.SendMails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface ISendMailService
    {

        Task<DataResult<Subscriber>> SendMailAllSubscriber(List<ListProductDto> listProductDtos, string urL);
        Task SendEmailRenewPassword(RenewPasswordDto renewPasswordDto);
    }
}

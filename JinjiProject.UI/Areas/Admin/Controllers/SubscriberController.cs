using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.SubscriberValidations;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Products;
using JinjiProject.Dtos.Subscribers;
using JinjiProject.VMs.Subscriber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    public class SubscriberController : AdminBaseController
    {
        private readonly ISubscriberService subscriberService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ISendMailService _sendMailService;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper, IProductService productService, ISendMailService sendMailService)
        {
            this.subscriberService = subscriberService;
            _mapper = mapper;
            _productService = productService;
            _sendMailService = sendMailService;
        }

        [HttpGet]
        public async Task<IActionResult> SubscriberList(bool showWarning = true)
        {
            var subscriberListResult = await subscriberService.GetAllByExpression(subscriber => subscriber.Status != Status.Deleted);
            var discountProductList= await _productService.GetAllByExpression(src => src.IsDiscount == true && src.Status != Status.Deleted);
            ListSubscriberVm listSubscriberVm = new();
            listSubscriberVm.ListSubscriberDtos = subscriberListResult.Data;
            listSubscriberVm.ListProductDtos = discountProductList.Data;


            if ((subscriberListResult.Data.Count <= 0 || SubscriberList == null) && showWarning)
            {

                NotifyError(subscriberListResult.Message);
            }
            else if (showWarning)
            {
                NotifySuccess(subscriberListResult.Message);
            }

            return View(listSubscriberVm);
        }
        [HttpPost]
        public async Task<IActionResult> GetSubscribersByGivenValues(ListSubscriberDto listSubscriberDto)
        {
            var subscriberListResponse = await subscriberService.GetSubscribersBySearchValues(listSubscriberDto.FullName, listSubscriberDto.Email, listSubscriberDto.CreatedDate.ToString());

            if (subscriberListResponse.Data == null)
                return RedirectToAction("SubscriberList");

            return View("SubscriberList", subscriberListResponse.Data);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDto createSubscriberDto)
        {
            CreateSubscriberValidator validations = new CreateSubscriberValidator();
            var result = validations.Validate(createSubscriberDto);

            if (result.IsValid)
            {
                var createSubscriberResult = await subscriberService.CreateSubscriberAsync(createSubscriberDto); ;
                if (createSubscriberResult.IsSuccess)
                {
                    NotifySuccess(createSubscriberResult.Message);
                }
                else
                {
                    NotifyError(createSubscriberResult.Message);

                }
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["NameError"] += item.ErrorMessage + "\n";
                }
                else
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";

                }
            }

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            var softDeleteResult = await subscriberService.SoftDeleteSubscriberAsync(id);

            if (softDeleteResult.IsSuccess)
            {
                NotifySuccess(softDeleteResult.Message);


                return RedirectToAction(nameof(SubscriberList), new { showWarning = false });
            }
            else
            {
                if (softDeleteResult.Data == null)
                {
                    NotifyError(softDeleteResult.Message);

                    return RedirectToAction(nameof(SubscriberList), new { showWarning = false });
                }
                NotifyError(softDeleteResult.Message);

                return RedirectToAction(nameof(SubscriberList), new { showWarning = false });
            }

        }


        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            var hardDeleteResult = await subscriberService.HardDeleteSubscriberAsync(id);

            if (hardDeleteResult.IsSuccess)
            {
                NotifySuccess(hardDeleteResult.Message);


                return RedirectToAction(nameof(DeletedSubscriberList), new { showWarning = false });
            }
            else
            {
                if (hardDeleteResult.Data == null)
                {
                    NotifyError(hardDeleteResult.Message);

                    return RedirectToAction(nameof(DeletedSubscriberList), new { showWarning = false });
                }
                NotifyError(hardDeleteResult.Message);

                return RedirectToAction(nameof(DeletedSubscriberList), new { showWarning = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletedSubscriberList(bool showWarning = true)
        {

            var deletedSubscriber = await subscriberService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedSubscriberListDto> deletedSubscriberList = _mapper.Map<List<DeletedSubscriberListDto>>(deletedSubscriber.Data);

            if ((deletedSubscriberList.Count <= 0 || deletedSubscriberList == null) && showWarning)
            {
                NotifyError("Silinen Abone Listesi Boş");
            }
            else if (showWarning)
            {
                NotifySuccess("Silinen Aboneler Listelendi");

            }

            return View(deletedSubscriberList);

        }


        [HttpPost]
        public async Task<IActionResult> SendMailToSubscribers(ListSubscriberVm productIds)
        {


            var discountedProducts = await _productService.GetAllByExpression(x => x.Status != Status.Deleted && x.IsDiscount == true && productIds.SelectedProductIds.Contains(x.Id));
            var subscribers = await subscriberService.GetAllByExpression(x => x.Status != Status.Deleted);

            string link = Url.Action("ProductDetails", "product", new { Area = "" }, Request.Scheme);

            var sendMailResult = await _sendMailService.SendMailAllSubscriber(discountedProducts.Data, link);
            if (sendMailResult.IsSuccess)
            {
                NotifySuccess(sendMailResult.Message);
                return RedirectToAction(nameof(SubscriberList), new { showWarning = false });
            }
            else
            {
                NotifyError(sendMailResult.Message);
                return RedirectToAction(nameof(SubscriberList), new { showWarning = false });

            }

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainSubscriber(int id)
        {
            var subscriberToAdded = await subscriberService.GetSubscriberById(id);

            if (subscriberToAdded.Data == null)
            {
                NotifyError(subscriberToAdded.Message);
                return RedirectToAction(nameof(DeletedSubscriberList), new { showWarning = false });
            }
            else
            {

                subscriberToAdded.Data.Status = Status.Active;
                UpdateSubscriberDto updateSubscriberDto = _mapper.Map<UpdateSubscriberDto>(subscriberToAdded.Data);

                var subscriberToUpdated = await subscriberService.UpdateSubscriberAsync(updateSubscriberDto);
                NotifySuccess("Abone yeniden eklendi.");

                return RedirectToAction(nameof(DeletedSubscriberList), new { showWarning = false });
            }
        }
        [HttpGet]
        public async Task<DetailSubscriberDto> GetDetailSubscriber(int subcsriberid)
        {

            var subscriber = await subscriberService.GetSubscriberById(subcsriberid);
            var subscriberResult = _mapper.Map<DetailSubscriberDto>(subscriber.Data);

            return subscriberResult;
        }

        [HttpGet]
        public async Task<GetSubscriberDto> GetSubscriber(int subscriberid)
        {
            var subscriberResult = await subscriberService.GetSubscriberById(subscriberid);
            return subscriberResult.Data;
        }
    }
}

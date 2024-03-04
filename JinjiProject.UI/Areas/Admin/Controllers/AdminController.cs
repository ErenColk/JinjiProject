using AutoMapper;
using FluentValidation;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.AdminValidations;
using JinjiProject.BusinessLayer.Validator.CategoryValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Admins;
using JinjiProject.Dtos.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public AdminController(IAdminService adminService,IMapper mapper,UserManager<AppUser> userManager)
        {
            this.adminService = adminService;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> AdminList()
        {
            var result = await adminService.GetAllByExpression(admin => admin.Status != Status.Deleted);
            var softDeleteSuccess = TempData["SoftDeleteSuccess"];
            var softDeleteError = TempData["SoftDeleteError"];
            var hardDeleteSuccess = TempData["HardDeleteSuccess"];
            var hardDeleteError = TempData["HardDeleteError"];
            ViewBag.Errors = TempData["CreateAdminError"];
            if (softDeleteSuccess != null)
                NotifySuccess($"{softDeleteSuccess}");
            else if (softDeleteError != null)
                NotifyError($"{softDeleteError}");
            else if (hardDeleteSuccess != null)
                NotifySuccess($"{hardDeleteSuccess}");
            else if (hardDeleteError != null)
                NotifyError($"{hardDeleteError}");

            if (result.IsSuccess)
            {
                var adminList = mapper.Map<IEnumerable<ListAdminDto>>(result.Data);
                return View(adminList);
            }
            else
            {
                NotifyError(result.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(CreateAdminDto createAdminDto)
        {
            CreateAdminValidator validations = new CreateAdminValidator();
            var result = validations.Validate(createAdminDto);
            if(result.IsValid)
            {
                var admin = await adminService.CreateAdminAsync(createAdminDto);
                if (admin.IsSuccess)
                {
                    NotifySuccess(admin.Message);
                }
                else
                {
                    NotifyError(admin.Message);
                }
            }
            else
            {
                var errorsString = "";
                foreach (var item in result.Errors)
                {
                    errorsString += "<li>"+item.ErrorMessage+"</li>";
                }
                TempData["CreateAdminError"] = errorsString;
            }
            
            return RedirectToAction("AdminList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAdmin(int id)
        {
            var updateAdminResult = await adminService.GetAdminById(id);
            if (updateAdminResult.IsSuccess)
            {
                UpdateAdminDto updateAdmin = mapper.Map<UpdateAdminDto>(updateAdminResult.Data);
                return View(updateAdmin);

            }
            else
            {
                UpdateAdminDto updateAdmin = mapper.Map<UpdateAdminDto>(updateAdminResult.Data);
                NotifyError(updateAdminResult.Message);
                return RedirectToAction(nameof(AdminList));
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddAgainAdmin(int id)
        {
            var adminToAdded = await adminService.GetAdminById(id);

            if (adminToAdded.Data == null)
            {
                NotifyError(adminToAdded.Message);
                return RedirectToAction(nameof(AdminList));
            }
            else
            {

                adminToAdded.Data.Status = Status.Active;
                UpdateAdminDto updatedToAdmin = mapper.Map<UpdateAdminDto>(adminToAdded.Data);

                var adminToUpdated = await adminService.UpdateAdminAsync(updatedToAdmin,true);

                NotifySuccess("Admin yeniden eklendi.");

                return RedirectToAction(nameof(AdminList));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminDto updateAdminDto)
        {
            UpdateAdminValidator updateAdminValidator = new UpdateAdminValidator();
            var result = updateAdminValidator.Validate(updateAdminDto);

            if (result.IsValid)
            {
                var updateAdminResult = await adminService.UpdateAdminAsync(updateAdminDto);

                if (updateAdminResult.IsSuccess)
                {

                    NotifySuccess(updateAdminResult.Message);

                }
                else
                {
                    NotifyError(updateAdminResult.Message);
                }

                return RedirectToAction(nameof(AdminList));
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["FirstName"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "2") 
                {
                    ViewData["LastName"] += item.ErrorMessage + "\n";
                }
                else if(item.ErrorCode == "3")
                {
                    ViewData["BirthDate"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "4")
                {
                    ViewData["Gender"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "5")
                {
                    ViewData["Email"] += item.ErrorMessage + "\n";
                }
                else if (item.ErrorCode == "6")
                {
                    ViewData["UploadPath"] += item.ErrorMessage + "\n";
                }
            }
            return View(updateAdminDto);
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            var softDeleteAdmin = await adminService.SoftDeleteAdminAsync(id);
            if (softDeleteAdmin.IsSuccess)
            {
                TempData["SoftDeleteSuccess"] = softDeleteAdmin.Message;
            }
            else
            {
                TempData["SoftDeleteError"] = softDeleteAdmin.Message;
            }
            return RedirectToAction(nameof(AdminList));
        }



        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            var hardDeleteAdmin = await adminService.HardDeleteAdminAsync(id);
            if (hardDeleteAdmin.IsSuccess)
            {
                TempData["HardDeleteSuccess"] = hardDeleteAdmin.Message;
            }
            else
            {
                TempData["HardDeleteError"] = hardDeleteAdmin.Message;
            }
            return RedirectToAction(nameof(AdminList));
        }

        [HttpGet]
        public async Task<IActionResult> DeletedAdminList()
        {

            var deletedAdmin = await adminService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedAdminListDto> deletedAdminList = mapper.Map<List<DeletedAdminListDto>>(deletedAdmin.Data);
            return View(deletedAdminList);

        }

        [HttpGet]
        public async Task<GetAdminDto> GetAdmin(int adminId)
        {

            var admin = await adminService.GetAdminById(adminId);

            return admin.Data;
        }
    }
}

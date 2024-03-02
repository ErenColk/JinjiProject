using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
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

        public AdminController(IAdminService adminService,IMapper mapper)
        {
            this.adminService = adminService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> AdminList()
        {
            var result = await adminService.GetAllByExpression(admin => admin.Status != Status.Deleted);
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

            var admin = await adminService.CreateAdminAsync(createAdminDto);
            if (admin.IsSuccess)
            {
                NotifySuccess(admin.Message);
            }
            else
            {
                NotifyError(admin.Message);
            }
            return RedirectToAction("AdminList");
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            await adminService.SoftDeleteAdminAsync(id);

            return RedirectToAction(nameof(AdminList));
        }



        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            await adminService.HardDeleteAdminAsync(id);
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

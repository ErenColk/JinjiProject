using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Dtos.Admins;
using System.Security.Claims;

namespace JinjiProject.UI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<string> GetUserName(this ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            var adminService = serviceProvider.GetService<IAdminService>();

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var adminDto = await adminService.GetByIdentityIdAsync(userId);

            var adminFullName = adminDto.Data?.FirstName + " " + adminDto.Data?.LastName;

            return adminFullName;
        }

        public static async Task<string> GetAdminImage(this ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            var adminService = serviceProvider.GetService<IAdminService>();

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var trainerDto = await adminService.GetByIdentityIdAsync(userId);

            return trainerDto.Data.ImagePath;
        }

        public static async Task<string> GetAdminEmail(this ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            var adminService = serviceProvider.GetService<IAdminService>();

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var trainerDto = await adminService.GetByIdentityIdAsync(userId);

            return trainerDto.Data.Email;
        }

        public static async Task<int> GetAdminID(this ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            var adminService = serviceProvider.GetService<IAdminService>();

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var trainerDto = await adminService.GetByIdentityIdAsync(userId);

            return trainerDto.Data.Id;
        }

        public static async Task<GetAdminDto> GetAdmin(this ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            var adminService = serviceProvider.GetService<IAdminService>();

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var trainerDto = await adminService.GetByIdentityIdAsync(userId);

            return new GetAdminDto() { Id = trainerDto.Data.Id,Email = trainerDto.Data.Email,ImagePath = trainerDto.Data.ImagePath};
        }
    }
}

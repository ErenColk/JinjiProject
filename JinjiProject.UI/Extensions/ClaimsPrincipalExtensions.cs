using JinjiProject.BusinessLayer.Managers.Abstract;
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
    }
}

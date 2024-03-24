using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Admins;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JinjiProject.Core.Enums;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.Dtos.Categories;
using JinjiProject.Core.Utilities.Results.Abstract;
using JinjiProject.Dtos.Genres;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public AdminManager(IAdminRepository adminRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<DataResult<Admin>> CreateAdminAsync(CreateAdminDto createAdminDto)
        {
            if(await userManager.FindByEmailAsync(createAdminDto.Email) != null)
            {
                return new ErrorDataResult<Admin>("Bu mail adresi daha önce kullanılmış!");
            }

            if (createAdminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.CreateAdminError);
            }
            else
            {
                AppUser appUser = new AppUser()
                {
                    Email = createAdminDto.Email,
                    UserName = "user" + createAdminDto.Email,
                    CreatedDate = DateTime.Now,
                    Status = Status.Active,
                    LockoutEnabled = false,
                };

                DataResult<Admin> result = new ErrorDataResult<Admin>();

                var strategy = await adminRepository.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using var transactionScope = await adminRepository.BeginTransactionAsync().ConfigureAwait(false);
                    try
                    {
                        IdentityResult identityResult = await userManager.CreateAsync(appUser, "newPassword+0");
                        if (!identityResult.Succeeded)
                        {                        
                            result = new ErrorDataResult<Admin>(identityResult.ToString());
                            transactionScope.Rollback();
                            return;
                        }
                        appUser.LockoutEnabled = false;
                        await userManager.AddToRoleAsync(appUser, "Admin");
                        var user = await userManager.FindByEmailAsync(appUser.Email);
                        createAdminDto.AppUserId = user.Id;
                        if (createAdminDto.UploadPath != null)
                        {
                            using (var image = Image.Load(createAdminDto.UploadPath.OpenReadStream()))
                            {
                                image.Mutate(x => x.Resize(300, 300));
                                Guid guid = Guid.NewGuid();
                                image.Save($"wwwroot/images/adminPhotos/{guid}.jpg");
                                createAdminDto.ImagePath = $"/images/adminPhotos/{guid}.jpg";
                            }
                        }

                        Admin admin = mapper.Map<Admin>(createAdminDto);
                        admin.AppUserId = user.Id;

                        await adminRepository.Create(admin);
                        await adminRepository.SaveChange();

                        result = new SuccessDataResult<Admin>(mapper.Map<Admin>(admin), Messages.CreateAdminSuccess);
                        transactionScope.Commit();
                    }
                    catch (Exception ex)
                    {
                        result = new ErrorDataResult<Admin>($"{Messages.CreateAdminError} - {ex.Message}");
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        transactionScope.Dispose();
                    }
                });              
                 return result;
            }
        }

        public async Task<DataResult<GetAdminDto>> GetAdminById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetAdminDto>(Messages.AdminNotFound);
            else
            {
                GetAdminDto getAdminDto = mapper.Map<GetAdminDto>(await adminRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetAdminDto>(getAdminDto, Messages.AdminFoundSuccess);
            }
        }
        public async Task<IDataResult<GetAdminDto>> GetByIdentityIdAsync(string identityId)
        {
            var admin = await adminRepository.GetByIdentityIdAsync(identityId);

            if (admin is null)
            {
                return new ErrorDataResult<GetAdminDto>(Messages.AdminNotFound);
            }

            return new SuccessDataResult<GetAdminDto>(mapper.Map<GetAdminDto>(admin), Messages.AdminFoundSuccess);
        }
        public async Task<DataResult<List<ListAdminDto>>> GetAllAdmin()
        {
            var admins = await adminRepository.GetAllAsync();
            return new SuccessDataResult<List<ListAdminDto>>(mapper.Map<List<ListAdminDto>>(admins), Messages.AdminListedSuccess);
        }

        public async Task<DataResult<List<ListAdminDto>>> GetAllByExpression(Expression<Func<Admin, bool>> expression)
        {
            var admins = await adminRepository.GetAllByExpression(expression);
            return new SuccessDataResult<List<ListAdminDto>>(mapper.Map<List<ListAdminDto>>(admins), Messages.AdminListedSuccess);
        }

        public async Task<DataResult<GetAdminDto>> GetFilteredAdmin(Expression<Func<Admin, bool>> expression)
        {
            var adminDto = await adminRepository.GetFilteredFirstOrDefault(expression);
            if (adminDto == null)
            {
                return new ErrorDataResult<GetAdminDto>(Messages.AdminFilteredError);
            }
            else
            {
                GetAdminDto getAdminDto = mapper.Map<GetAdminDto>(adminDto);
                return new SuccessDataResult<GetAdminDto>(getAdminDto, Messages.AdminFilteredSuccess);
            }
        }

        public async Task<DataResult<Admin>> HardDeleteAdminAsync(int id,string userId)
        {
            var adminDto = await adminRepository.GetByIdAsync(id);
            if (adminDto.AppUserId != userId && userId != SuperAdmin.IdentityId)
            {
                    return new ErrorDataResult<Admin>("Yetkisiz Admin!");
            }
            if (adminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.AdminNotFound);
            }
            else
            {
                if(adminDto.AppUserId == SuperAdmin.IdentityId) // Süper adminin Id'si gelecek
                {
                    return new ErrorDataResult<Admin>("Süper Admin Silinemez!");
                }
                AppUser appUser = await userManager.FindByIdAsync(adminDto.AppUserId);
                IdentityResult result = await userManager.DeleteAsync(appUser);
                if (result.Succeeded)
                {
                    await adminRepository.HardDelete(adminDto);
                    if (File.Exists($"wwwroot/{adminDto.ImagePath}"))
                    {
                        File.Delete($"wwwroot/{adminDto.ImagePath}");
                    }
                    return new SuccessDataResult<Admin>(Messages.AdminDeletedSuccess);
                }
                else
                    return new ErrorDataResult<Admin>(Messages.AdminDeletedRepoError);
            }
        }

        public async Task<DataResult<Admin>> SoftDeleteAdminAsync(int id,string userId)
        {
            var adminDto = await adminRepository.GetByIdAsync(id);
            if (adminDto.AppUserId != userId && userId != SuperAdmin.IdentityId)
            {
                    return new ErrorDataResult<Admin>("Yetkisiz Admin!");
            }
            if (adminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.AdminNotFound);
            }
            else
            {
                if(adminDto.AppUserId == SuperAdmin.IdentityId) // Süper adminin Id'si gelecek
                {
                    return new ErrorDataResult<Admin>("Süper Admin Silinemez!");
                }
                bool result = await adminRepository.SoftDelete(adminDto);
                if (result)
                {
                    AppUser user = await userManager.FindByIdAsync(adminDto.AppUserId);
                    user.LockoutEnabled = true;
                    await userManager.UpdateAsync(user);
                    return new SuccessDataResult<Admin>(Messages.AdminDeletedSuccess);
                }
                else
                    return new ErrorDataResult<Admin>(Messages.AdminDeletedRepoError);
            }
        }

        public async Task<DataResult<Admin>> UpdateAdminAsync(UpdateAdminDto updateAdminDto, string userId, bool addAgain = false)
        {
            if (updateAdminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.UpdateAdminError);
            }
            else
            {
                Admin admin = await adminRepository.GetByIdAsync(updateAdminDto.Id);
                if(admin.AppUserId != userId && userId != SuperAdmin.IdentityId)
                {
                        return new ErrorDataResult<Admin>("Bu admini güncelleyemezsiniz!");
                }
                if (updateAdminDto.UploadPath != null)
                {
                    using (var image = Image.Load(updateAdminDto.UploadPath.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(300, 300));
                        Guid guid = Guid.NewGuid();
                        image.Save($"wwwroot/images/adminPhotos/{guid}{Path.GetExtension(updateAdminDto.UploadPath.FileName)}");
                        updateAdminDto.ImagePath = $"/images/adminPhotos/{guid}{Path.GetExtension(updateAdminDto.UploadPath.FileName)}";
                    }
                }
                updateAdminDto.Status = Status.Active;
                admin = mapper.Map(updateAdminDto, admin);
                bool result = await adminRepository.Update(admin);
                if (result)
                {
                    if (addAgain == true)
                    {
                        AppUser user = await userManager.FindByIdAsync(admin.AppUserId);
                        user.LockoutEnabled = false;
                        await userManager.UpdateAsync(user);
                    }
                    return new SuccessDataResult<Admin>(admin, Messages.UpdateAdminSuccess);
                }
                else
                    return new ErrorDataResult<Admin>(admin, Messages.UpdateAdminRepoError);
            }
        }
    }
}

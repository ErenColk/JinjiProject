using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Admins;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public AdminManager(IAdminRepository adminRepository, UserManager<AppUser> userManager,IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<DataResult<Admin>> CreateAdminAsync(CreateAdminDto createAdminDto)
        {
            if(createAdminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.CreateAdminError);
            }
            else
            {
                Admin admin = mapper.Map<Admin>(createAdminDto);
                bool result = await adminRepository.Create(admin);
                if (result)
                    return new SuccessDataResult<Admin>(admin, Messages.CreateAdminSuccess);
                else
                    return new ErrorDataResult<Admin>(admin, Messages.CreateAdminRepoError);

            }
        }

        public async Task<DataResult<GetAdminDto>> GetAdminById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetAdminDto>(Messages.AdminNotFound);
            else
            {
               GetAdminDto getAdminDto =  mapper.Map<GetAdminDto>(await adminRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetAdminDto>(getAdminDto,Messages.AdminFoundSuccess);
            }
        }

        public async Task<DataResult<List<ListAdminDto>>> GetAllAdmin()
        {
            var admins = await adminRepository.GetAllAsync();
            return new SuccessDataResult<List<ListAdminDto>>(mapper.Map<List<ListAdminDto>>(admins), Messages.AdminListedSuccess);
        }

        public async Task<DataResult<GetAdminDto>> GetFilteredAdmin(Expression<Func<Admin, bool>> expression)
        {
            var adminDto = await adminRepository.GetFilteredFirstOrDefault(expression);
            if(adminDto == null)
            {
                return new ErrorDataResult<GetAdminDto>(Messages.AdminFilteredError);
            }
            else
            {
                GetAdminDto getAdminDto = mapper.Map<GetAdminDto>(adminDto);
                return new SuccessDataResult<GetAdminDto>(getAdminDto, Messages.AdminFilteredSuccess);
            }
        }

        public async Task<DataResult<Admin>> HardDeleteAdminAsync(int id)
        {
            var adminDto = await adminRepository.GetByIdAsync(id);
            if( adminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.AdminNotFound);
            }
            else
            {
                bool result = await adminRepository.HardDelete(adminDto);
                if (result)
                    return new SuccessDataResult<Admin>(Messages.AdminDeletedSuccess);
                else
                    return new ErrorDataResult<Admin>(Messages.AdminDeletedRepoError);
            }
        }

        public async Task<DataResult<Admin>> SoftDeleteAdminAsync(int id)
        {
            var adminDto = await adminRepository.GetByIdAsync(id);
            if (adminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.AdminNotFound);
            }
            else
            {
                bool result = await adminRepository.SoftDelete(adminDto);
                if (result)
                    return new SuccessDataResult<Admin>(Messages.AdminDeletedSuccess);
                else
                    return new ErrorDataResult<Admin>(Messages.AdminDeletedRepoError);
            }
        }

        public async Task<DataResult<Admin>> UpdateAdminAsync(UpdateAdminDto updateAdminDto)
        {
            if (updateAdminDto == null)
            {
                return new ErrorDataResult<Admin>(Messages.UpdateAdminError);
            }
            else
            {
                Admin admin = mapper.Map<Admin>(updateAdminDto);
                bool result = await adminRepository.Update(admin);
                if (result)
                    return new SuccessDataResult<Admin>(admin, Messages.UpdateAdminSuccess);
                else
                    return new ErrorDataResult<Admin>(admin, Messages.UpdateAdminRepoError);
            }
        }
    }
}

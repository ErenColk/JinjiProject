﻿using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Abstract;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Admins;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface IAdminService
    {
        public Task<DataResult<Admin>> CreateAdminAsync(CreateAdminDto createAdminDto);
        public Task<DataResult<Admin>> UpdateAdminAsync(UpdateAdminDto updateAdminDto, string userId,bool addAgain = false);
        public Task<DataResult<Admin>> SoftDeleteAdminAsync(int id,string userId);
        public Task<DataResult<Admin>> HardDeleteAdminAsync(int id,string userId);
        Task<IDataResult<GetAdminDto>> GetByIdentityIdAsync(string identityId);
        public Task<DataResult<List<ListAdminDto>>> GetAllAdmin();
        public Task<DataResult<List<ListAdminDto>>> GetAllByExpression(Expression<Func<Admin, bool>> expression);
        public Task<DataResult<GetAdminDto>> GetAdminById(int id);
        public Task<DataResult<GetAdminDto>> GetFilteredAdmin(Expression<Func<Admin,bool>> expression);
    }
}

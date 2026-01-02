using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class RoleService : IRoleService
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;

        public RoleService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<RoleModel>> GetAsync()
        {
            try
            {
                var Languagelist = await _genericRepository.GetAsync<Role>(x=>x.RoleId!=99);
                return _mapper.Map<List<RoleModel>>(Languagelist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoleModel> GetByIdAsync(int id)
        {
            try
            {
                var role = await _genericRepository.GetByIDAsync<Role>(id);
                return _mapper.Map<RoleModel>(role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(RoleModel model)
        {
            try
            {
                var map = _mapper.Map<Role>(model);
                var res = await _genericRepository.InsertAsync(map);
                if (res > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RecordExistAsync(RoleModel model)
        {
            try
            {
                var map = _mapper.Map<Role>(model);
                return await _genericRepository.ExistsAsync<Role>();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<bool> UpdateAsync(RoleModel request)
        {
            try
            {               
                var role = await _genericRepository.GetByIDAsync<Role>(request.RoleId);
                role.RoleName = request.RoleName;
                role.Details = request.Details;
                role.IsActive = request.IsActive;
                role.UpdatedOn = request.UpdatedOn;
                role.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<Role>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

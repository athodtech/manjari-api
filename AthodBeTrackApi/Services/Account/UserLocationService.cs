using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class UserLocationService : IUserLocationService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IAccountRepository _accountRepository;
        public UserLocationService(IMapper mapper, IGenericRepository genericRepository, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<UserLocationModel>> GetAsync(int userId)
        {
            try
            {
                var dataTable = await Task.FromResult(_accountRepository.GetUserLocation(userId));
                return ExtensionMethods.ConvertToList<UserLocationModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserLocationModel> GetByIdAsync(int userLocId)
        {
            try
            {
                var location = await _genericRepository.GetByIDAsync<UserLocation>(userLocId);
                return _mapper.Map<UserLocationModel>(location);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddAsync(UserLocationModel model)
        {
            try
            {
                var location = _mapper.Map<UserLocation>(model);               
                location.UserLocId = 0;

                var userLocId = await _genericRepository.InsertAsync(location);

                return userLocId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> LocationExist(int userId)
        {
            return await _genericRepository.ExistsAsync<UserLocation>(x => x.UserId == userId);
        }

        public async Task<bool> UpdateAsync(UserLocationModel request)
        {
            try
            {
                var location = await _genericRepository.GetByIDAsync<UserLocation>(request.UserLocId);
                location.LocationLevel = request.LocationLevel;
                location.StateId = request.StateId;
                location.DistrictId = request.DistrictId;
                location.BlockId = request.BlockId;
                location.VillageId = request.VillageId;
                location.IsActive = request.IsActive;
                location.UpdatedOn = request.UpdatedOn;
                location.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(location);
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
                await _genericRepository.DeleteAsync<UserLocation>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

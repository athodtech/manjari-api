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
    public class EmailConfigService : IEmailConfigService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        public EmailConfigService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }
        public async Task<List<EmailConfigModel>> GetAsync()
        {
            try
            {
                var list = await _genericRepository.GetAsync<EmailConfiguration>();

                return _mapper.Map<List<EmailConfigModel>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmailConfigModel> GetAsync(int id)
        {
            try
            {
                var emailConfiguration = await _genericRepository.GetByIDAsync<EmailConfiguration>(id);
                return _mapper.Map<EmailConfigModel>(emailConfiguration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddAsync(EmailConfigModel request)
        {
            try
            {
                var emailConfiguration = _mapper.Map<EmailConfiguration>(request);
                return await _genericRepository.InsertAsync(emailConfiguration);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(EmailConfigModel request)
        {
            try
            {
                var model = await _genericRepository.GetByIDAsync<EmailConfiguration>(request.Id);
                if (!string.IsNullOrEmpty(request.Password))
                    model.Password = request.Password;

                model.UserName = request.UserName;
                model.Port = request.Port;
                model.Server = request.Server;
                model.Sslstatus = request.SSLStatus;
                model.Signature = request.Signature;
                model.FriendlyName = request.FriendlyName;
                model.IsActive = request.IsActive;
                model.UpdatedOn = request.UpdatedOn;
                model.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(model);
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
                await _genericRepository.DeleteAsync<EmailConfiguration>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

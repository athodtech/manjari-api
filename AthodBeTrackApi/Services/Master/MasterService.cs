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
    public class MasterService : IMasterService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IMasterRepository _masterRepository;
        public MasterService(IMapper mapper, IGenericRepository genericRepository, IMasterRepository masterRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _masterRepository = masterRepository;
        }

        public async Task<List<QuestionTypeModel>> GetQuestionTypeAsync()
        {
            try
            {
                var questions = await _genericRepository.GetAsync<QuestionType>();
                return _mapper.Map<List<QuestionTypeModel>>(questions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddLogsAsync(ApplicationLoggingModel model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                model.IsRead = false;
                var map = _mapper.Map<ApplicationLogging>(model);
                var res = await _genericRepository.InsertLongAsync(map);
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

        public async Task<List<GetApplicationLogs_Result>> GetApplicationLogsAsync(string activityCategoryIds, string userIds, string strwhere)
        {
            try
            {
                var dataTable = await _masterRepository.GetApplicationLogsAsync(activityCategoryIds,userIds, strwhere);
                return ExtensionMethods.ConvertToList<GetApplicationLogs_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ReadLog(int userId, int logId, bool isRead)
        {
            try
            {
                var applicationLogging = await _genericRepository.GetFirstOrDefaultAsync<ApplicationLogging>(x => x.Id == logId);
                if (applicationLogging != null)
                {
                    applicationLogging.IsRead = isRead;
                    applicationLogging.UpdatedBy = userId;
                    applicationLogging.UpdatedOn = DateTime.Now;
                    await _genericRepository.UpdateAsync(applicationLogging);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

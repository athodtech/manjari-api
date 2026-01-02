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
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly ILocationRepository _locationRepository;

        public LocationService(IMapper mapper, IGenericRepository genericRepository, IMasterRepository masterRepository, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _masterRepository = masterRepository;
            _locationRepository = locationRepository;

        }
        #region State
        public async Task<List<StateModel>> GetStateAsync()
        {
            try
            {
                var list = await _genericRepository.GetAsync<State>();
                return _mapper.Map<List<StateModel>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StateModel> GetStateByIdAsync(int id)
        {
            try
            {
                var state = await _genericRepository.GetByIDAsync<State>(id);
                return _mapper.Map<StateModel>(state);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddStateAsync(StateModel request)
        {
            try
            {
                var state = _mapper.Map<State>(request);
                var res = await _genericRepository.InsertAsync(state);
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
        public async Task<bool> UpdateStateAsync(StateModel request)
        {
            try
            {
                //var model = await _genericRepository.GetByIDAsync<State>(request.StateId);             
                //model.StateName = request.StateName;
                //model.StateCode = request.StateCode;
                //model.IsActive = request.IsActive;
                //model.UpdatedOn = request.UpdatedOn;
                //model.UpdatedBy = request.UpdatedBy;
                //await _genericRepository.UpdateAsync(model);
                return await _locationRepository.UpdateState(request);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task DeleteStateAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<State>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region District

        public async Task<List<DistrictModel>> GetDistrictAsync()
        {
            try
            {
                var list = await Task.FromResult(_genericRepository.GetIQueryable<District>().Include(s => s.State));
                return _mapper.Map<List<DistrictModel>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<DistrictModel> GetDistrictByIdAsync(int id)
        {
            try
            {
                var district = await _genericRepository.GetByIDAsync<District>(id);
                return _mapper.Map<DistrictModel>(district);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AddDistrictAsync(DistrictModel model)
        {
            try
            {
                var district = _mapper.Map<District>(model);
                district.State = null;
                var res = await _genericRepository.InsertAsync(district);
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
        public async Task<bool> UpdateDistrictAsync(DistrictModel request)
        {
            try
            {
                //var model = await _genericRepository.GetByIDAsync<District>(request.DistrictId);
                //model.StateId = request.StateId;
                //model.DistrictCode = request.DistrictCode;
                //model.DistrictName = request.DistrictName;
                //model.IsActive = request.IsActive;
                //model.UpdatedOn = request.UpdatedOn;
                //model.UpdatedBy = request.UpdatedBy;
                //await _genericRepository.UpdateAsync(model);
                return await _locationRepository.UpdateDistrict(request);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteDistrictAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<District>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Block

        public async Task<List<BlockModel>> GetBlockAsync(int? stateId, int? districtId)
        {
            try
            {
                var list = await Task.FromResult(_genericRepository.GetIQueryable<Block>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0))).Include(s => s.State).Include(d => d.District));
                return _mapper.Map<List<BlockModel>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BlockModel> GetBlockByIdAsync(int id)
        {
            try
            {
                var block = await _genericRepository.GetByIDAsync<Block>(id);
                return _mapper.Map<BlockModel>(block);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AddBlockAsync(BlockModel model)
        {
            try
            {
                var block = _mapper.Map<Block>(model);
                block.District = null;
                block.State = null;
                var res = await _genericRepository.InsertAsync(block);
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
        public async Task<bool> UpdateBlockAsync(BlockModel request)
        {
            try
            {
                //var model = await _genericRepository.GetByIDAsync<Block>(request.BlockId);
                //model.StateId = request.StateId;
                //model.DistrictId = request.DistrictId;
                //model.BlockCode = request.BlockCode;
                //model.BlockName = request.BlockName;
                //model.IsActive = request.IsActive;
                //model.UpdatedOn = request.UpdatedOn;
                //model.UpdatedBy = request.UpdatedBy;
                //await _genericRepository.UpdateAsync(model);
                //return true;
                return await _locationRepository.UpdateBlock(request);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task DeleteBlockAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<Block>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Village
        public async Task<List<VillageModel>> GetVillageAsync(int? stateId, int? districtId, int? blockId)
        {
            try
            {
                var list = await Task.FromResult(_genericRepository.GetIQueryable<Village>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && (blockId.HasValue ? x.BlockId == blockId.Value : (1 > 0))).Include(s => s.State).Include(d => d.District).Include(b => b.Block));
                return _mapper.Map<List<VillageModel>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<VillageModel> GetVillageByIdAsync(int id)
        {
            try
            {
                var block = await _genericRepository.GetByIDAsync<Village>(id);
                return _mapper.Map<VillageModel>(block);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AddVillageAsync(VillageModel model)
        {
            try
            {
                var village = _mapper.Map<Village>(model);
                village.Block = null;
                village.District = null;
                village.State = null;
                var res = await _genericRepository.InsertAsync(village);
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
        public async Task<bool> UpdateVillageAsync(VillageModel request)
        {
            try
            {
                //var model = await _genericRepository.GetByIDAsync<Village>(request.VillageId);
                //model.StateId = request.StateId;
                //model.DistrictId = request.DistrictId;
                //model.BlockId = request.BlockId;
                //model.VillageName = request.VillageName;
                //model.VillageCode = request.VillageCode;
                //model.LocationType = request.LocationType;
                //model.IsActive = request.IsActive;
                //model.UpdatedOn = request.UpdatedOn;
                //model.UpdatedBy = request.UpdatedBy;
                //await _genericRepository.UpdateAsync(model);
                //return true;

                return await _locationRepository.UpdateVillage(request);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task DeleteVillageAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<Village>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetVillages_Result>> GetVillages(int start, int length, string orderBy, string searchText, string whereLoc)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                //string limitQuery = " LIMIT " + length + " OFFSET " + start;
                string limitQuery = " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = "where s.StateName like '%" + searchText + "%' or d.DistrictName like '%" + searchText + "%' or b.BlockName like '%" + searchText + "%' or v.VillageName like '%" + searchText + "%' or v.VillageCode like '%" + searchText + "%' or v.IsActive like '%" + searchText + "%'";
                }
                if (!string.IsNullOrEmpty(whereClause))
                {
                    if (!string.IsNullOrEmpty(whereLoc))
                        whereClause += " and " + whereLoc;
                }
                else
                {
                    if (!string.IsNullOrEmpty(whereLoc))
                        whereClause = "where " + whereLoc;
                }
                completeQuery = string.Format("{0} {1} {2}", whereClause, orderBy, limitQuery);
                var dataTable = await _masterRepository.GetVillagesAsync(completeQuery);

                return ExtensionMethods.ConvertToList<GetVillages_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> TotalVillageCount()
        {
            try
            {
                return await _genericRepository.Count<Village>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> GetFilterVillageCount(string searchText, string whereLoc)
        {
            int recordsFiltered = 0;
            try
            {

                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = "where s.StateName like '%" + searchText + "%' or d.DistrictName like '%" + searchText + "%' or b.BlockName like '%" + searchText + "%' or v.VillageName like '%" + searchText + "%' or v.VillageCode like '%" + searchText + "%' or v.IsActive like '%" + searchText + "%'";
                }

                if (!string.IsNullOrEmpty(whereClause))
                {
                    if (!string.IsNullOrEmpty(whereLoc))
                        whereClause += " and " + whereLoc;
                }
                else
                {
                    if (!string.IsNullOrEmpty(whereLoc))
                        whereClause = "where " + whereLoc;
                }

                var dataTable = await _masterRepository.GetFilterVillageCountAsync(whereClause);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recordsFiltered;
        }


        public async Task<ActivityQuestionSetUniqueIdentity> CheckVillageIsUsed(int villageId)
        {
            return await _genericRepository.GetFirstOrDefaultAsync<ActivityQuestionSetUniqueIdentity>(x => x.VillageId == villageId);
        }
        public async Task<ActivityQuestionSetUniqueIdentity> CheckBlockIsUsed(int blockId)
        {
            return await _genericRepository.GetFirstOrDefaultAsync<ActivityQuestionSetUniqueIdentity>(x => x.BlockId == blockId);
        }

        public async Task<ActivityQuestionSetUniqueIdentity> CheckDistictIsUsed(int distictId)
        {
            return await _genericRepository.GetFirstOrDefaultAsync<ActivityQuestionSetUniqueIdentity>(x => x.DistrictId == distictId);
        }

        #endregion


        #region User Location Details

        public async Task<List<GetUserLoationDetails_Result>> GetUserLoationDetailsAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, int start, int length, string orderBy, string searchText)
        {
            try
            {
                string whereClause = "";
                string limitQuery = "";
                string limit = " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = "where StateName like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or VillageName like '%" + searchText + "%' or TotalHousehold like '%" + searchText + "%'";
                }

                limitQuery = string.Format("{0} {1}", orderBy, limit);
                var dataTable = await _masterRepository.GetUserLoationDetailsAsync(userId, stateIds, districtIds, blockIds, villageIds, whereClause, limitQuery);

                return ExtensionMethods.ConvertToList<GetUserLoationDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetUserLoationDetailsCountAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string searchText)
        {
            int recordsFiltered = 0;
            try
            {
                string whereClause = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = "where StateName like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or VillageName like '%" + searchText + "%' or TotalHousehold like '%" + searchText + "%'";
                }

                var dataTable = await _masterRepository.GetUserLoationDetailsCountAsync(userId, stateIds, districtIds, blockIds, villageIds, whereClause);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recordsFiltered;
        }

        #endregion
    }
}

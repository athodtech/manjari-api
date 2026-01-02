using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface ILocationService
    {
        #region State
        Task<bool> AddStateAsync(StateModel model);
        Task DeleteStateAsync(int id);
        Task<List<StateModel>> GetStateAsync();
        Task<StateModel> GetStateByIdAsync(int id);
        Task<bool> UpdateStateAsync(StateModel request);
        #endregion

        #region District
        Task<List<DistrictModel>> GetDistrictAsync();
        Task<DistrictModel> GetDistrictByIdAsync(int id);
        Task<bool> AddDistrictAsync(DistrictModel model);
        Task<bool> UpdateDistrictAsync(DistrictModel request);
        Task DeleteDistrictAsync(int id);
        #endregion

        #region Block
        Task<List<BlockModel>> GetBlockAsync(int? stateId, int? districtId);
        Task<BlockModel> GetBlockByIdAsync(int id);
        Task<bool> AddBlockAsync(BlockModel model);
        Task<bool> UpdateBlockAsync(BlockModel request);
        Task DeleteBlockAsync(int id);
        Task<List<VillageModel>> GetVillageAsync(int? stateId, int? districtId, int? blockId);
        Task<VillageModel> GetVillageByIdAsync(int id);
        Task<bool> AddVillageAsync(VillageModel model);
        Task<bool> UpdateVillageAsync(VillageModel request);
        Task DeleteVillageAsync(int id);
        Task<List<GetVillages_Result>> GetVillages(int start, int length, string orderBy, string searchText, string whereLoc);
        Task<int> TotalVillageCount();
        Task<int> GetFilterVillageCount(string searchText, string whereLoc);
        Task<ActivityQuestionSetUniqueIdentity> CheckVillageIsUsed(int villageId);
        Task<ActivityQuestionSetUniqueIdentity> CheckBlockIsUsed(int blockId);
        Task<ActivityQuestionSetUniqueIdentity> CheckDistictIsUsed(int distictId);
        Task<List<GetUserLoationDetails_Result>> GetUserLoationDetailsAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, int start, int length, string orderBy, string searchText);
        Task<int> GetUserLoationDetailsCountAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string searchText);
        #endregion

    }
}
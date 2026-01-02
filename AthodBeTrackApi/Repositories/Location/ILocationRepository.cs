using AthodBeTrackApi.Models;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface ILocationRepository
    {
        Task<bool> UpdateBlock(BlockModel request);
        Task<bool> UpdateDistrict(DistrictModel request);
        Task<bool> UpdateState(StateModel request);
        Task<bool> UpdateVillage(VillageModel request);
    }
}
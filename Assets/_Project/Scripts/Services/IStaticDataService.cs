using OctanGames.StaticData;

namespace OctanGames.Services
{
    public interface IStaticDataService : IService
    {
        void LoadAllStaticData();
        PlayerStaticData ForPlayer();
    }
}
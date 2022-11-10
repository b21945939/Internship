using GameFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.DataAccess.Abstract
{
    public interface IGameRepository
    {
        List<Game> GetAllGames();
        Game GetGameById(int id);
        Game CreateGame(Game game);
        Game UpdateGame(Game game);
        Game AddType(string type,int id);
        Game RemoveType(string type, int id);
        Game AddPlatform(string platform, int id);
        Game RemovePlatform(string platform, int id);
        Game UpdateImage(string image, int id);
        Game UpdateVideo(string video, int id);
        void DeleteGame(int id);
        void RefreshRates();
    }
}

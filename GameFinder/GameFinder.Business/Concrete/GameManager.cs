using GameFinder.Business.Abstract;
using GameFinder.DataAccess.Abstract;
using GameFinder.DataAccess.Concrete;
using GameFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Business.Concrete
{
    public class GameManager : IGameService
    {
        private IGameRepository _gameRepository;
        public GameManager(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public Game CreateGame(Game game)
        {
            return _gameRepository.CreateGame(game);
        }

        public void DeleteGame(int id)
        {
            _gameRepository.DeleteGame(id);
        }

        public List<Game> GetAllGames()
        {
            return _gameRepository.GetAllGames();
        }

        public Game GetGameById(int id)
        {
            if (id > 0)
            {
                return _gameRepository.GetGameById(id);
            }
            throw new Exception("id cannot be less than 1");
        }

        public Game UpdateGame(Game game)
        {
            return _gameRepository.UpdateGame(game);
        }

        public Game AddType(string type, int id)
        {
            return _gameRepository.AddType(type,id);
        }

        public Game RemoveType(string type, int id)
        {
            return _gameRepository.RemoveType(type,id);
        }

        public void RefreshRates()
        {
            _gameRepository.RefreshRates();
        }

        public Game AddPlatform(string platform, int id)
        {
            return _gameRepository.AddPlatform(platform, id);
        }

        public Game RemovePlatform(string platform, int id)
        {
            return _gameRepository.RemovePlatform(platform, id);
        }

        public Game UpdateImage(string image, int id)
        {
            return _gameRepository.UpdateImage(image, id);
        }

        public Game UpdateVideo(string video, int id)
        {
            return _gameRepository.UpdateVideo(video, id);
        }
    }
}

using GameFinder.Business.Abstract;
using GameFinder.Business.Concrete;
using GameFinder.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        /// <summary>
        /// Get All Games
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Game> Get()
        {
            return _gameService.GetAllGames();
        }
        /// <summary>
        /// Get Game By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Game Get(int id)
        {
            return _gameService.GetGameById(id);
        }
        /// <summary>
        /// Create a Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost]
        public Game Post([FromBody] Game game)
        {
            return _gameService.CreateGame(game);
        }
        /// <summary>
        /// Update the Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPut]
        public Game Put([FromBody] Game game)
        {
            return _gameService.UpdateGame(game);
        }
        /// <summary>
        /// Delete the Game
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _gameService.DeleteGame(id);
        }

        /// <summary>
        /// Add Type to the Game
        /// </summary>
        /// <param name="type"></param>
        [HttpPut("type/{id}/add/{type}")]
        public Game PutType(string type,int id)
        {
            return _gameService.AddType(type,id);
        }

        /// <summary>
        /// Remove Type From the Game
        /// </summary>
        /// <param name="type"></param>
        [HttpPut("type/{id}/remove/{type}")]
        public Game PutRType(string type, int id)
        {
            return _gameService.RemoveType(type,id);
        }

        [HttpGet("refresh")]

        public void Refresh()
        {
            _gameService.RefreshRates();
        }

        /// <summary>
        /// Add Platform to the Game
        /// </summary>
        /// <param name="platform"></param>
        [HttpPut("platform/{id}/add/{platform}")]
        public Game PutPlatform(string platform, int id)
        {
            return _gameService.AddPlatform(platform, id);
        }

        /// <summary>
        /// Remove Platform From the Game
        /// </summary>
        /// <param name="platform"></param>
        [HttpPut("platform/{id}/remove/{platform}")]
        public Game PutRPlatform(string platform, int id)
        {
            return _gameService.RemovePlatform(platform, id);
        }

        /// <summary>
        /// Add Image to the Game
        /// </summary>
        /// <param name="image"></param>
        [HttpPut("image/{id}/{image}")]
        public Game UpdateImage(string image, int id)
        {
            return _gameService.UpdateImage(image, id);
        }

        /// <summary>
        /// Add Video to the Game
        /// </summary>
        /// <param name="video"></param>
        [HttpPut("video/{id}/{video}")]
        public Game UpdateVideo(string video, int id)
        {
            return _gameService.UpdateVideo(video, id);
        }
    }
}
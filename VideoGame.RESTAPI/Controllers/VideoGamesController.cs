using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VideoGame.DAL.Models;
using VideoGame.DAL.Repository;
using VideoGame.RESTAPI.Helpers.BasicAuth;
using VideoGame.DAL.Entities;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoGame.RESTAPI.Controllers
{
    [BasicAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {

        private readonly ILogger<VideoGamesController> _logger;
        private readonly IGamesRepository _repository;

        public VideoGamesController(ILogger<VideoGamesController> logger, IGamesRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: api/VideoGames
        // GET: api/VideoGames?cat_name=&orderby=
        [HttpGet]
        public  ActionResult Get(string cat_name=null,string orderby=null)
        {
            List<Game> _games= new List<Game>();
            try
            {
                List<string> lstOrderBy = new List<string> { "name", "name dsc","year","year dsc" };
                if (!lstOrderBy.Contains(orderby)&&!string.IsNullOrEmpty(orderby))
                    return StatusCode(StatusCodes.Status400BadRequest, "Order by parmater is not in correct format.");

                //-----------------get all games in the json list---------------------
                if (string.IsNullOrEmpty(cat_name) && string.IsNullOrEmpty(orderby))
                {
                    _games =  _repository.GetAll();
                    if (_games.Count == 0)
                    {
                        _logger.LogInformation($"No games are found in the list");
                    }
                    return Ok(_games);
                }
                else if (string.IsNullOrEmpty(cat_name) && !string.IsNullOrEmpty(orderby))
                {
                    //------------get games order by name or year-------------------------------
                    _games = _repository.GetGamesOrderby(orderby);
                    if (_games.Count == 0)
                    {
                        _logger.LogInformation($"No games are ordered with the '{orderby}' type in the list");
                        return NotFound();
                    }
                    return Ok(_games);
                }
                else if (!string.IsNullOrEmpty(cat_name) && string.IsNullOrEmpty(orderby))
                {
                    //--------------get games with the specified category name----------------------
                    _games = _repository.GetSpecCatGame(cat_name);
                    if (_games.Count==0)
                    {
                        _logger.LogInformation($"No games are found with the '{cat_name}' in the list");
                        return NotFound($"Game with category name {cat_name} not found");
                    }
                    return Ok(_games);
                }
                else
                {
                    //---------------Get specific category game and order by name or by year--------------------------------
                    _games = _repository.GetSpecCatGameOrderby(cat_name, orderby);
                    if (_games.Count == 0)
                    {
                        _logger.LogInformation($"No games are found with the '{cat_name}' in the list");
                        return NotFound($"Game with category name {cat_name} not found");

                    }
                    return Ok(_games);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting all games : {ex}");
               return StatusCode(StatusCodes.Status500InternalServerError, "Status Code: 500- A problem occured while handling your request. Please try again.");
                //return NotFound();
            }

        }


        [HttpGet("{name}")]
        public ActionResult<Game> Getgame(string name = null)
        {
            try
            {
               var _game = _repository.GetGame(name);
                if (_game == null)
                {
                    _logger.LogInformation($"Game not found in the list");
                    return NotFound($"Game with game name {name} not found ");

                }
                return _game;
            }

            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured specific game games : {ex}");
                return NotFound();
               
            }
        }


        // POST api/VideoGames/AddGame
        [BasicAuth]
        [HttpPost("AddGame")]
        public ActionResult<Game> Post(Game objGame)
        {
            try
            {
                if (objGame == null)
                    return BadRequest();

                var _game = _repository.GetGame(objGame.name);

                if (_game != null)
                {
                    ModelState.AddModelError("Game", "Game is already present");
                    return BadRequest(ModelState);
                }

                var createdGame = _repository.Create(objGame);

                _logger.LogInformation("New game created");
                //returns statuscode of 201 created response
                return CreatedAtAction(nameof(Getgame),
                    new { name =createdGame.name}, createdGame);
               
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while adding a new game : {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Status Code: 500- A problem occured while adding a new game");
            }
        }

        // PUT api/VideoGames/UpdateGame/ 
        [HttpPut("UpdateGame")]
        public ActionResult<Game> Put(Game objGame)
        {
            try
            {
                if (objGame == null)
                    return BadRequest();

                var _game = _repository.GetGame(objGame.name);

                if (_game == null)
                {
                    return NotFound($"Game with name {objGame.name} not found to update");
                }
                _logger.LogInformation("Game updated");
                return _repository.Update(objGame);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while updating game : {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Status Code: 500- A problem occured while updating game");
            }

        }

        // DELETE api/VideoGames/DeleteGame/
        [HttpDelete("DeleteGame")]
        public ActionResult Delete(string name)
        {
            try
            {
                if (name == null)
                    return BadRequest();

                var _game = _repository.GetGame(name);

                if (_game == null)
                {
                    return NotFound($"Game with game name {name} not found");
                }
                _repository.Delete(_game);
                _logger.LogInformation("Game Deleted");
                return Ok($"Game with name {name} deleted");
                

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while deleting game : {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Status Code: 500- A problem occured while deleting game");
            }
        }
    }
}

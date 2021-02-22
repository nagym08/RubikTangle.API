using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RubikTangle.API.Models;
using RubikTangle.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikTangle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private readonly IHighscoreService highscoreService;

        public HighscoresController(IHighscoreService highscoreService)
        {
            this.highscoreService = highscoreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Highscore>>> Get()
        {
            var result = await highscoreService.GetAll();
            if(result == null)
            {
                return NoContent();
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Highscore highscore)
        {
            var createdItem = await highscoreService.Save(highscore);
            if(createdItem == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

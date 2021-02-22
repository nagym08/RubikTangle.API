using RubikTangle.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikTangle.API.Services
{
    public interface IHighscoreService
    {
        Task<List<Highscore>> GetAll();
        Task<Highscore> Save(Highscore highscore);
    }
}

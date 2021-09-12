using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGame.DAL.Entities;
using VideoGame.DAL.Models;

namespace VideoGame.DAL.Repository
{
   public interface IGamesRepository
    {
        //public T Create(T _object);
        //public void Update(T _object);
        //public T GetAll();
        //public void Delete(T name);
        //public T GetSpecCatGameOrderbyName(T categorName);
        //public T GetSpecCatGameOrderbyYear(T categorName);
        //public T GetSpecCatGame(T categorName);
        //public T GetGamesOrderbyYear(T categorName);

        //public T GetGamesOrderbyname(T categorName);

        //public T Serialize(object _object);

        Game Create(Game _object);
        Game GetGame(string _object);
        Game Update(Game _object);
        List<Game> GetAll();
        void Delete(Game name);
        List<Game> GetSpecCatGameOrderby(string categorName,string orderby);
        List<Game> GetSpecCatGame(string categorName);
        List<Game> GetGamesOrderby(string orderby);
    }
}

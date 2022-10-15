using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRA.Entities.Entities;
using QRA.UseCases.contracts;
using QRA.UseCases.DTOs;

namespace QRA.API.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        public readonly IDatabaseService idatabase;
        public readonly IGetDatabaseQuery idatabaseQuery;
        public readonly IMapper imapper;


        public DatabaseController(IMapper mapper, IDatabaseService database, IGetDatabaseQuery databaseQuery)
        {
            imapper = mapper;
            idatabase = database;
            idatabaseQuery = databaseQuery;
        }

        //get
        [HttpGet("listDB")]
        public List<Db> GetDatabase()
        {
            return idatabaseQuery.GetDatabse();
        }
        [HttpGet("getDB")]
        public Db GetDatabasebyID(long id)
        {
            return idatabaseQuery.GetDatabasebyId(id);
        }

        //create
        [HttpPost("createDB")]
        public long CreateDatabase(DatabaseDTO database)
        {
            Db db = imapper.Map<Db>(database);
            long idDB = idatabase.create(db);

            return idDB;
        }


        //update 
        [HttpPost("updateDB")]
        public bool UpdateDatabase(DatabaseDTO database)
        {
            Db db = imapper.Map<Db>(database);
            return idatabase.update(db);
        }


        //delete 
        [HttpPost("deleteDB")]
        public bool DeleteDatabase(DatabaseDTO database)
        {
            Db db = imapper.Map<Db>(database);
            return idatabase.delete(db);
        }

    }
}

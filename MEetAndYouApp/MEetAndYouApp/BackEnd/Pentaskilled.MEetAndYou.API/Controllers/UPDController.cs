using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPDController : ControllerBase
    {
        private readonly IUPDManager _UPDManager;
        private readonly MEetAndYouDBContext _dbcontext;



        public UPDController(IUPDManager updmanager, MEetAndYouDBContext dbcontext) { 
           this._UPDManager = updmanager;
           this._dbcontext = dbcontext;

        }


        /// <summary>
        /// HTTP get method for fetching the itineraries of the user 
        /// </summary>
        /// <param name="id"> id of the user</param
        /// <returns> Itinerary response</returns>
        [HttpGet]
        [Route("/GetUPDData")]
        public async Task<ActionResult<UPDataResponse>> GetUPDData(int userID)
        {
            if (userID > 0)
            {
                UPDataResponse response = await _UPDManager.GetUPData(userID);
                return response;
            }
            
            return new UPDataResponse("UP data could not be gotten from the controller", false, null, null);
        } 
    }
}

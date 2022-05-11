using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using System.Web.Http.Cors;


namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ApiController]
    [Route("[controller]")]
    public class UADController : Controller
    {

        private readonly IUADManager _uADManager;

        public UADController(IUADManager uADManager)
        {
            _uADManager = uADManager;
        }


        [HttpGet]
        [Route("/GetRegCount")]

        public async Task<ActionResult<List<int>>> AsynCGetRegistrationCount()
        {
            try
            {
        
                var result = await _uADManager.GetRegisteredAccountNumber();
                return Ok(result);
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

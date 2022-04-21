using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Managers;

namespace Pentaskiled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionManager _suggestionManager;

        public SuggestionController(SuggestionManager suggestionController)
        {
            _suggestionManager = suggestionController;
        }

        [HttpGet]
        [Route("Suggestion/GetSuggestion")]
        public ActionResult<JObject> GetEventCat()
        {
            JObject result = _suggestionManager.GetEventByCategory();
            return result;
        }
    }
}

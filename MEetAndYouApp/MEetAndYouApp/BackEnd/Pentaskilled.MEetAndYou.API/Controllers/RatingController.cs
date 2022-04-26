using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly AuthorizationManager _authorizationManager;
        private readonly IRatingManager _ratingManager;
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingController(AuthorizationManager authzManager, IRatingManager _ratingManager, MEetAndYouDBContext dbcontext)
        {
            _authorizationManager = authzManager;
            this._ratingManager = _ratingManager;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("PostRatingCreation")]
        public ActionResult<BaseResponse> PostRatingCreation(int eventID, int itineraryID, int userRating)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("PostNoteCreaton")]
        public ActionResult<BaseResponse> PostNoteCreaton(int itineraryID, string noteContent)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("PutRatingModification")]
        public ActionResult<BaseResponse> PutRatingModification(int eventID, int itineraryID, int userRating)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("PutNoteModification")]
        public ActionResult<BaseResponse> PutNoteModification(int itineraryID, string noteContent)
        {
            throw new NotImplementedException();
        }
    }
}

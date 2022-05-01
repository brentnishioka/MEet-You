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
        //private readonly AuthorizationManager _authorizationManager;
        private readonly IRatingManager _ratingManager;
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingController(IRatingManager _ratingManager, MEetAndYouDBContext dbcontext)
        {
            //_authorizationManager = authzManager;
            this._ratingManager = _ratingManager;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("PostRatingCreation")]
        public ActionResult<BaseResponse> PostRatingCreation([FromBody] UserEventRatingJSON model)
        {
            var eventID = model.eventID;
            var itineraryID = model.itineraryID;
            var userRating = model.userRating;
            BaseResponse postRatingCreationResult = _ratingManager.CreateRating(eventID, itineraryID, userRating);
            return postRatingCreationResult;
        }

        [HttpPost]
        [Route("PostNoteCreaton")]
        public ActionResult<BaseResponse> PostNoteCreaton(int itineraryID, string noteContent)
        {
            BaseResponse postNoteCreationResult = _ratingManager.CreateItineraryNote(itineraryID, noteContent);
            return postNoteCreationResult;
        }

        [HttpPut]
        [Route("PutRatingModification")]
        public ActionResult<BaseResponse> PutRatingModification(int eventID, int itineraryID, int userRating)
        {
            BaseResponse putRatingModificationResult = _ratingManager.ModifyRating(eventID, itineraryID, userRating);
            return putRatingModificationResult;
        }

        [HttpPut]
        [Route("PutNoteModification")]
        public ActionResult<BaseResponse> PutNoteModification(int itineraryID, string noteContent)
        {
            BaseResponse putNoteModificationResult = _ratingManager.ModifyItineraryNote(itineraryID, noteContent);
            return putNoteModificationResult;
        }
    }

    public class UserEventRatingJSON
    {
        public int eventID { get; set; }
        public int itineraryID { get; set; }
        public int userRating { get; set; }
    }
}

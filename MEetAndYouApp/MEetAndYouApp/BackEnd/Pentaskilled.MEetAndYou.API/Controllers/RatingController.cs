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

        [HttpGet]
        [Route("GetUserItinerary")]
        public async Task<ActionResult<ItineraryResponse>> GetUserItinerary(int userID, int itineraryID)
        {
            ItineraryResponse getItineraryResult = await _ratingManager.RetrieveUserItinerary(userID, itineraryID);
            return getItineraryResult;
        }

        [HttpGet]
        [Route("GetUserEventRatings")]
        public async Task<ActionResult<RatingResponse>> GetUserEventRatings(int itineraryID)
        {
            RatingResponse getRatingResult = await _ratingManager.RetrieveUserRatings(itineraryID);
            return getRatingResult;
        }

        [HttpGet]
        [Route("GetUserNote")]
        public async Task<ActionResult<NoteResponse>> GetUserNote(int itineraryID)
        {
            NoteResponse getNoteResult = await _ratingManager.RetrieveUserNote(itineraryID);
            return getNoteResult;
        }

        [HttpPost]
        [Route("PostRatingCreation")]
        public async Task<ActionResult<BaseResponse>> PostRatingCreation([FromBody] UserEventRatingJSON model)
        {
            var eventID = model.eventID;
            var itineraryID = model.itineraryID;
            var userRating = model.userRating;
            BaseResponse postRatingCreationResult = await _ratingManager.CreateRating(eventID, itineraryID, userRating);
            return postRatingCreationResult;
        }

        [HttpPost]
        [Route("PostNoteCreaton")]
        public async Task<ActionResult<BaseResponse>> PostNoteCreaton([FromBody] ItineraryNoteJSON model)
        {
            int itineraryID = model.itineraryID;
            string noteContent = model.noteContent;
            BaseResponse postNoteCreationResult = await _ratingManager.CreateItineraryNote(itineraryID, noteContent);
            return postNoteCreationResult;
        }

        [HttpPut]
        [Route("PutRatingModification")]
        public async Task<ActionResult<BaseResponse>> PutRatingModification([FromBody] UserEventRatingJSON model)
        {
            var eventID = model.eventID;
            var itineraryID = model.itineraryID;
            var userRating = model.userRating;
            BaseResponse putRatingModificationResult = await _ratingManager.ModifyRating(eventID, itineraryID, userRating);
            return putRatingModificationResult;
        }

        [HttpPut]
        [Route("PutNoteModification")]
        public async Task<ActionResult<BaseResponse>> PutNoteModification([FromBody] ItineraryNoteJSON model)
        {
            int itineraryID = model.itineraryID;
            string noteContent = model.noteContent;
            BaseResponse putNoteModificationResult = await _ratingManager.ModifyItineraryNote(itineraryID, noteContent);
            return putNoteModificationResult;
        }
    }

    public class UserEventRatingJSON
    {
        public int eventID { get; set; }
        public int itineraryID { get; set; }
        public int userRating { get; set; }
    }

    public class ItineraryNoteJSON
    {
        public int itineraryID { get; set; }
        public string noteContent { get; set; }
    }
}

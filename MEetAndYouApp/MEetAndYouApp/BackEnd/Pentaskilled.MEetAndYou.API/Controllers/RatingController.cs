using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IAuthorizationManager _authzManager;
        private readonly IRatingManager _ratingManager;
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingController(IAuthorizationManager authzManager, IRatingManager _ratingManager, MEetAndYouDBContext dbcontext)
        {
            this._authzManager = authzManager;
            this._ratingManager = _ratingManager;
            _dbcontext = dbcontext;
        }

        // Web API controller to fetch the specified itinerary provided the user's ID and the itinerary's ID.
        [HttpGet]
        [Route("GetUserItinerary")]
        public async Task<ActionResult<ItineraryResponse>> GetUserItinerary(int itineraryID)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    // Input validation for the ID's
                    bool isUserIDValid = Validator.IsValidNumber(userID);
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);

                    if (isUserIDValid && isItineraryIDValid)
                    {
                        ItineraryResponse getItineraryResult = await _ratingManager.RetrieveUserItinerary(userID, itineraryID);
                        return getItineraryResult;
                    }
                    return new ItineraryResponse("The itineraries could not be fetched successfully because the given user ID or itinerary ID were invalid.", false, null);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        // Web API controller to fetch the specified user ratings provided the itinerary's ID.
        [HttpGet]
        [Route("GetUserEventRatings")]
        public async Task<ActionResult<RatingResponse>> GetUserEventRatings(int itineraryID)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    // Input validation for the ID
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);

                    if (isItineraryIDValid)
                    {
                        RatingResponse getRatingResult = await _ratingManager.RetrieveUserRatings(itineraryID);
                        return getRatingResult;
                    }
                    return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        // Web API controller to fetch the specified note provided the itinerary's ID.
        [HttpGet]
        [Route("GetUserNote")]
        public async Task<ActionResult<NoteResponse>> GetUserNote(int itineraryID)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    // Input validation for the ID
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);

                    if (isItineraryIDValid)
                    {
                        NoteResponse getNoteResult = await _ratingManager.RetrieveUserNote(itineraryID);
                        return getNoteResult;
                    }
                    return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }            
        }

        // Web API controller to post/create a user's rating given event's ID, the itinerary's ID, and the rating (1-5) given by the user.
        [HttpPost]
        [Route("PostRatingCreation")]
        public async Task<ActionResult<BaseResponse>> PostRatingCreation([FromBody] UserEventRatingJSON model)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    var eventID = model.eventID;
                    var itineraryID = model.itineraryID;
                    var userRating = model.userRating;

                    // Input validation for the ID's and user rating.
                    bool isEventIDValid = Validator.IsValidNumber(eventID);
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);
                    bool isUserRatingValid = Validator.IsValidRange(userRating, 0, 5);

                    if (isEventIDValid && isItineraryIDValid && isUserRatingValid)
                    {
                        BaseResponse postRatingCreationResult = await _ratingManager.CreateRating(eventID, itineraryID, userRating);
                        return postRatingCreationResult;
                    }
                    return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }            
        }

        // Web API controller to post/create an itinerary note given the itinerary's ID and the content of the note.
        [HttpPost]
        [Route("PostNoteCreaton")]
        public async Task<ActionResult<BaseResponse>> PostNoteCreaton([FromBody] ItineraryNoteJSON model)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    var itineraryID = model.itineraryID;
                    var noteContent = model.noteContent;

                    // Input validation for the ID and note content.
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);
                    bool isNoteContentValid = Validator.IsValueNull(noteContent);

                    if (itineraryID > 0 && noteContent != null)
                    {
                        BaseResponse postNoteCreationResult = await _ratingManager.CreateItineraryNote(itineraryID, noteContent);
                        return postNoteCreationResult;
                    }
                    return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        // Web API controller to put/update a user's rating given event's ID, the itinerary's ID, and the rating (1-5) given by the user.
        [HttpPut]
        [Route("PutRatingModification")]
        public async Task<ActionResult<BaseResponse>> PutRatingModification([FromBody] UserEventRatingJSON model)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    var eventID = model.eventID;
                    var itineraryID = model.itineraryID;
                    var userRating = model.userRating;

                    // Input validation for the ID's and user rating.
                    bool isEventIDValid = Validator.IsValidNumber(eventID);
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);
                    bool isUserRatingValid = Validator.IsValidRange(userRating, 0, 5);

                    if (isEventIDValid && isItineraryIDValid && isUserRatingValid)
                    {
                        BaseResponse putRatingModificationResult = await _ratingManager.ModifyRating(eventID, itineraryID, userRating);
                        return putRatingModificationResult;
                    }
                    return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        // Web API controller to put/update an itinerary note given the itinerary's ID and the content of the note.
        [HttpPut]
        [Route("PutNoteModification")]
        public async Task<ActionResult<BaseResponse>> PutNoteModification([FromBody] ItineraryNoteJSON model)
        {
            bool response;
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                response = _authzManager.IsAuthorized(userID, userToken, role);

                if (response)
                {
                    var itineraryID = model.itineraryID;
                    var noteContent = model.noteContent;

                    // Input validation for the ID and note content.
                    bool isItineraryIDValid = Validator.IsValidNumber(itineraryID);
                    bool isNoteContentValid = Validator.IsValueNull(noteContent);

                    if (itineraryID > 0 && noteContent != null)
                    {
                        BaseResponse putNoteModificationResult = await _ratingManager.ModifyItineraryNote(itineraryID, noteContent);
                        return putNoteModificationResult;
                    }
                    return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }
    }

    // Object to parse the JSON body for any post/put requests for UserEventRatings.
    public class UserEventRatingJSON
    {
        public int eventID { get; set; }
        public int itineraryID { get; set; }
        public int userRating { get; set; }
    }

    // Object to parse the JSON body for any post/put requests for ItineraryNotes.
    public class ItineraryNoteJSON
    {
        public int itineraryID { get; set; }
        public string noteContent { get; set; }
    }
}

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
                    if (userID > 0 && itineraryID > 0)
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
            // Input validation for the ID
            if (itineraryID > 0)
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                RatingResponse getRatingResult = await _ratingManager.RetrieveUserRatings(itineraryID);
                return getRatingResult;
            }
            return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        // Web API controller to fetch the specified note provided the itinerary's ID.
        [HttpGet]
        [Route("GetUserNote")]
        public async Task<ActionResult<NoteResponse>> GetUserNote(int itineraryID)
        {
            // Input validation for the ID
            if (itineraryID > 0)
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                NoteResponse getNoteResult = await _ratingManager.RetrieveUserNote(itineraryID);
                return getNoteResult;
            }
            return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        // Web API controller to post/create a user's rating given event's ID, the itinerary's ID, and the rating (1-5) given by the user.
        [HttpPost]
        [Route("PostRatingCreation")]
        public async Task<ActionResult<BaseResponse>> PostRatingCreation([FromBody] UserEventRatingJSON model)
        {
            var eventID = model.eventID;
            var itineraryID = model.itineraryID;
            var userRating = model.userRating;

            // Input validation for the ID's and user rating.
            if (eventID > 0 && itineraryID > 0 && (userRating >= 1 && userRating <= 5))
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                BaseResponse postRatingCreationResult = await _ratingManager.CreateRating(eventID, itineraryID, userRating);
                return postRatingCreationResult;
            }
            return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        // Web API controller to post/create an itinerary note given the itinerary's ID and the content of the note.
        [HttpPost]
        [Route("PostNoteCreaton")]
        public async Task<ActionResult<BaseResponse>> PostNoteCreaton([FromBody] ItineraryNoteJSON model)
        {
            var itineraryID = model.itineraryID;
            var noteContent = model.noteContent;

            // Input validation for the ID and note content.
            if (itineraryID > 0 && noteContent != null)
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                BaseResponse postNoteCreationResult = await _ratingManager.CreateItineraryNote(itineraryID, noteContent);
                return postNoteCreationResult;
            }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        // Web API controller to put/update a user's rating given event's ID, the itinerary's ID, and the rating (1-5) given by the user.
        [HttpPut]
        [Route("PutRatingModification")]
        public async Task<ActionResult<BaseResponse>> PutRatingModification([FromBody] UserEventRatingJSON model)
        {
            var eventID = model.eventID;
            var itineraryID = model.itineraryID;
            var userRating = model.userRating;

            // Input validation for the ID's and user rating.
            if (eventID > 0 && itineraryID > 0 && (userRating >= 1 && userRating <= 5))
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                BaseResponse putRatingModificationResult = await _ratingManager.ModifyRating(eventID, itineraryID, userRating);
                return putRatingModificationResult;
            }
            return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        // Web API controller to put/update an itinerary note given the itinerary's ID and the content of the note.
        [HttpPut]
        [Route("PutNoteModification")]
        public async Task<ActionResult<BaseResponse>> PutNoteModification([FromBody] ItineraryNoteJSON model)
        {

            var itineraryID = model.itineraryID;
            var noteContent = model.noteContent;

            // Input validation for the ID and note content.
            if (itineraryID > 0 && noteContent != null)
            {
                //string? token;
                //int userID;
                //string userToken;
                //string role;

                //token = Request.Headers["Token"];
                //if (token == null)
                //{
                //    return BadRequest("Null token");
                //}
                ////Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];
                ////Checks if the user is authorized before continuing 
                //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                //{
                //    return BadRequest("User is not authorized to use this service");
                //}

                BaseResponse putNoteModificationResult = await _ratingManager.ModifyItineraryNote(itineraryID, noteContent);
                return putNoteModificationResult;
            }
            return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
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

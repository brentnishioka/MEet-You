﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class RatingDAO : IRatingDAO
    {
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingDAO(MEetAndYouDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating)
        {
            try
            {
                _dbcontext.Entry(userRating).State = EntityState.Added;
                int addRatingResult = await _dbcontext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                return new BaseResponse("An error occurred when adding the rating to the database.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The rating could not be added.", false);
            }
            return new BaseResponse("The rating was successfully added.", true);
        }

        public async Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating)
        {
            try
            {
                _dbcontext.Entry(userRating).State = EntityState.Modified;
                int modifyRatingResult = await _dbcontext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                return new BaseResponse("An error occurred when modifying the rating in the database.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The rating could not be modified.", false);
            }
            return new BaseResponse("The rating was successfully modified.", true);
        }

        public async Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote)
        {
            try
            {
                _dbcontext.Entry(itineraryNote).State = EntityState.Added;
                int addRatingResult = await _dbcontext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                return new BaseResponse("An error occurred when adding the note to the database.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The note could not be added.", false);
            }
            return new BaseResponse("The note was successfully added.", true);
        }

        public async Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote)
        {
            try
            {
                _dbcontext.Entry(itineraryNote).State = EntityState.Modified;
                int modifyRatingResult = await _dbcontext.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                return new BaseResponse("An error occurred when modifying the note in the database.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The note could not be modified.", false);
            }
            return new BaseResponse("The note was successfully modified.", true);
        }
    }
}

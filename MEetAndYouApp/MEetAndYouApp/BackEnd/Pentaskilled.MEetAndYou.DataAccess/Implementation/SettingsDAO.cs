using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Newtonsoft.Json.Linq;


namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class SettingsDAO : ISettingsDAO
    { 
        private MEetAndYouDBContext _dbContext;

        public SettingsDAO(MEetAndYouDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        public SettingsDAO()
        {
            this._dbContext = new MEetAndYouDBContext();
        }

        public async Task<BaseResponse> updateUserEmail(int id,string email)
        {
            string message = "Email update failed";
            bool isSuccessful = false;

            try
            {
                UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                user.UserEmail = email;
                isSuccessful = true;
                _dbContext.SaveChanges();
                message = "Email was changed successfully";
            } 
            catch (SqlException e)
            {
                return new BaseResponse(message, isSuccessful);
            }
            catch(Exception e)
            {
                return new BaseResponse(message, isSuccessful);
            }
            return new BaseResponse(message, isSuccessful);
        }

        public async Task<BaseResponse> updateUserPassword(int id, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> updateUserPhone(int id, string phone)
        {
            string message = "Phone update failed";
            bool isSuccessful = false;

            try
            {
                UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                user.UserPhoneNum = phone;
                isSuccessful = true;
                _dbContext.SaveChanges();
                message = "Phone was changed successfully";
            }
            catch (SqlException e)
            {
                return new BaseResponse(message, isSuccessful);
            }
            catch (Exception e)
            {
                return new BaseResponse(message, isSuccessful);
            }
            return new BaseResponse(message, isSuccessful);
        }
    }
}

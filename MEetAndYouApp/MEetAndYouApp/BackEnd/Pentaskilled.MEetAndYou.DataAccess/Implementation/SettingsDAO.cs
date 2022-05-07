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


        /// <summary>
        /// Method to update the user email in the database.
        /// </summary>
        /// <param name="id"> id of the user</param>
        /// <param name="email"> new email to replace current email </param
        /// <returns> base response object pertaining to the status of the update</returns>
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

        /// <summary>
        /// method to update the user password in the database.
        /// </summary>
        /// <param name="id"> id of the user</param>
        /// <param name="password"> new password to replace current</param
        /// <returns> Base response object pertaining to the status of password update </returns>
        public async Task<BaseResponse> updateUserPassword(int id, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to update the user phone in the database
        /// </summary>
        /// <param name="id"> id of the user</param>
        /// <param name="phone"> new phone number to replace current </param
        /// <returns> Base response object pertaining to the status of phone update </returns>
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

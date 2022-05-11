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
        private IUMDAO _UMDAO;

        public SettingsDAO(IUMDAO umDAO,MEetAndYouDBContext dBContext)
        {
            this._dbContext = dBContext;
            this._UMDAO = umDAO;
        }

        public SettingsDAO()
        {
            this._dbContext = new MEetAndYouDBContext();
            this._UMDAO = new UMDAO();
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
            string message = "Password update failed";
            bool isSuccessful = false;

            try
            {
                isSuccessful = _UMDAO.IsUserPasswordUpdated(id, password);
                message = "Password was updated successfully";
            }
            catch (Exception e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }

            return new BaseResponse(message, isSuccessful);
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

        public async Task<BaseResponse> deleteUserAccount(int id)
        {
            string message = "Account Deletion Failed.";
            bool isSuccessful = false;

            try
            {
                UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                _dbContext.Entry(user).State = EntityState.Deleted;
                isSuccessful = true;
                _dbContext.SaveChanges();
                message = "Account was successfully deleted";
            }
            catch (SqlException e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            catch (Exception e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            return new BaseResponse(message, isSuccessful);
        }

        public async Task<BaseResponse> disableUserAccount(int id)
        {
            string message = "Account disable failure";
            bool isSuccessful = false;

            try
            {
                UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                user.Active = false;
                isSuccessful = true;
                _dbContext.SaveChanges();
                message = "Account was successfully disabled";
            }
            catch (SqlException e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            catch (Exception e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            return new BaseResponse(message, isSuccessful);
        }

        public async Task<BaseResponse> enableUserAccount(int id)
        {
            string message = "Account was not successfully enabled";
            bool isSuccessful = false;

            try
            {
                UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                user.Active = true;
                isSuccessful = true;
                _dbContext.SaveChanges();
                message = "Account was successfully enabled";
            }
            catch (SqlException e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            catch (Exception e)
            {
                return new BaseResponse(message + e.Message, isSuccessful);
            }
            return new BaseResponse(message, isSuccessful);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IHyperlinkManager
    {
        Task<HyperlinkResponse> AddUserToItineraryAsync(int userID, int itineraryID, string email, string permission);
        Task<HyperlinkResponse> RemoveUserFromItineraryAsync(int userID, int itineraryID, string email, string permission);
    }
}
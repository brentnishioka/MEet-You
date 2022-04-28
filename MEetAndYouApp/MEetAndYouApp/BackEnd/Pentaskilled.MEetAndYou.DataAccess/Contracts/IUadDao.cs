using System;
using System.Collections.Generic;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IUadDao
    {
        List<SessionViews> getMostVisitedView();
        List<SessionViews> getAvgViewTime();
        Dictionary<DateTime, int> getDailyLogin();

        Dictionary<DateTime, int> getDailyRegistration();




    }
}

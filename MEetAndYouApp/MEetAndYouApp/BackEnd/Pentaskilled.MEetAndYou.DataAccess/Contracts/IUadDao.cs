using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IUADDAO
    {
        List<SessionViews> getMostVisitedView();
        List<SessionViews> getAvgViewTime();
        Dictionary<DateTime, int> getDailyLogin();

        Dictionary<DateTime, int> getDailyRegistration();



    }
}

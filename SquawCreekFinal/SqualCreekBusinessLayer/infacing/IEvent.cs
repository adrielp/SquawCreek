using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.infacing
{
    public interface IEvent
    {
        void SendEventRequest(SqualCreekBusinessLayer.entites.SubmitedEventRequest er);

        string CalenderPopulationPublic();

    }
}

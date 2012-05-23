using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;

namespace ScheduleServiceApp
{
    // Service contract for our train schedule service
    [ServiceContract]
    public interface ScheduleService
    {
        // Operation that returns a list of defined lines
        [OperationContract]
        Line[] GetLines();

        // Operation that returns information on a specified line with schedule details
        [OperationContract]
        LineWithSchedule GetLine(string id);

        // Operation that returns a list of stations on the lines
        [OperationContract]
        Station[] GetStations();

        // Operation that returns a schedule for a specified station 
        [OperationContract]
        StationSchedule GetStationSchedule(string id);

    }
}

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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ScheduleService
    {

        [OperationContract]
        Line[] GetLines();

        [OperationContract]
        LineWithSchedule GetLine(string id);

        [OperationContract]
        Station[] GetStations();

        [OperationContract]
        StationSchedule GetStationSchedule(string id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    [DataContractAttribute]
    public class LineWithSchedule : Line
    {
        [DataMember]
        public StopTimes stopTimes = new StopTimes();

        public LineWithSchedule(Line line) : base(line.id, line.route)
        {            
        }

        public void AddStop(Station station, string time)
        {
            stopTimes.Add(station, time);
        }

    }

    [CollectionDataContract(Name = "Stops", ItemName="Stop", KeyName = "Station", ValueName = "Time")]
    public class StopTimes : Dictionary<Station, string>
    { 
    }
}

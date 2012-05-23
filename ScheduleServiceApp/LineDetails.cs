using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    // Data object for ho‌lding data of a line and its schedule
    [DataContractAttribute]
    public class LineWithSchedule : Line
    {
        [DataMember]
        public StopTimes stopTimes = new StopTimes();

        public LineWithSchedule(Line line) : base(line.id, line.route)
        {            
        }

        // convenience method for adding stops and times one by one
        public void AddStop(Station station, string time)
        {
            stopTimes.Add(station, time);
        }

    }

    // We specify names for items, keys and values to make XML look pretty and readable
    [CollectionDataContract(Name = "Stops", ItemName="Stop", KeyName = "Station", ValueName = "Time")]
    public class StopTimes : Dictionary<Station, string>
    { 
    }
}

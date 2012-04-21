using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    [DataContractAttribute]
    public class StationSchedule
    {
        [DataMember]
        public Station station;
        [DataMember]
        public Departures departures = new Departures();

        public StationSchedule(Station station) {
            this.station = station;
        }

        public void AddDeparture(Line line, string time) {
            departures.Add(time, line);
        }
    }

    [CollectionDataContract(Name = "Departures", ItemName = "Departure", KeyName = "Time", ValueName = "Line")]
    public class Departures : SortedDictionary<string, Line>
    {
    }

}

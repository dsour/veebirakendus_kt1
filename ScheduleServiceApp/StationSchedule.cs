using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    // Data object for holding schedule data for a station
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

        // Convenience method for adding departures one by one
        public void AddDeparture(Line line, string time) {
            departures.Add(time, line);
        }
    }

    // We specify names for items, keys and values to make XML look pretty and readable
    [CollectionDataContract(Name = "Departures", ItemName = "Departure", KeyName = "Time", ValueName = "Line")]
    public class Departures : SortedDictionary<string, Line>
    {
    }

}

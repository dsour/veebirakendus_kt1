using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    // Data object for holding information about a station
    [DataContractAttribute]
    public class Station
    {
        [DataMemberAttribute]
        public string id;
        [DataMemberAttribute]
        public string name;

        public Station(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

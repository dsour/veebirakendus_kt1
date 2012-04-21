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
        [DataMemberAttribute]
        public Dictionary<string, string> stopTimes = new Dictionary<string, string>();

        public LineWithSchedule(Line line) : base(line.id, line.route)
        {            
        }

        public void AddStop(string stop, string time)
        {
            stopTimes.Add(stop, time);
        }

    }
}

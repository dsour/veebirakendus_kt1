using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace ScheduleServiceApp
{

    // Data object for train line
    [DataContractAttribute]
    public class Line
    {
        [DataMemberAttribute]
        public string id;
        [DataMemberAttribute]
        public string route;

        public Line(string id, string route)
        {
            this.id = id;
            this.route = route;
        }
    }
}

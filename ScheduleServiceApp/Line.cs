using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScheduleServiceApp
{
    [DataContractAttribute]
    public class Line
    {
        [DataMemberAttribute]
        public string id;

       
       public Line(string id)
       {
           this.id = id;
       }
    }
}

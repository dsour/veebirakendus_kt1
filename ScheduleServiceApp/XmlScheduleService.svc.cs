using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Web;

namespace ScheduleServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class XmlScheduleService : ScheduleService
    {
        public LineInformation GetLines()
        {
            var lineInformation = new LineInformation();
            XPathDocument xmldoc = new XPathDocument(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soiduplaan.xml"));

            XPathNavigator navigator = xmldoc.CreateNavigator();
            
            XPathNodeIterator i;
            i = navigator.Select("/Plaan/Liinid/Liin");
            while(i.MoveNext())          
            {
                var id = i.Current.SelectSingleNode("@id").ToString();
                var line = new Line(id);
                lineInformation.AddLine(line);
            }
            return lineInformation;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

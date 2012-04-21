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
            XPathNavigator navigator = createDocNavigator();
            XPathNodeIterator i;
            i = navigator.Select("/Plaan/Liinid/Liin");
            var lineInformation = new LineInformation();
            while(i.MoveNext())          
            {
                var line = createLine(i);
                lineInformation.AddLine(line);
            }
            return lineInformation;
        }

        private static XPathNavigator createDocNavigator()
        {
            XPathDocument xmldoc = new XPathDocument(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soiduplaan.xml"));
            return xmldoc.CreateNavigator();
        }

        public LineWithSchedule GetLine(string id)
        {
            XPathNodeIterator i;
            i = createDocNavigator().Select("/Plaan/Liinid/Liin[@id = '" + id + "']");
            LineWithSchedule lineWithSchedule = null;
            while (i.MoveNext())
            {
                lineWithSchedule = new LineWithSchedule(createLine(i));
                XPathNodeIterator j = i.Current.Select("PeatusedLiinil/PeatusLiinil");
                while (j.MoveNext())
                {
                    lineWithSchedule.AddStop(
                        j.Current.SelectSingleNode("Nimetus").ToString(), 
                        j.Current.SelectSingleNode("Aeg").ToString());
                }
            }
            return lineWithSchedule;
        }

        private static Line createLine(XPathNodeIterator i)
        {
            var id = i.Current.SelectSingleNode("@id").ToString();
            var sectionNodes = i.Current.Select("Suund");
            var sections = "";
            while (sectionNodes.MoveNext())
            {
                sections += (sections.Length > 0 ? " / " : "") + sectionNodes.Current.ToString();
            }
            var line = new Line(id, sections);
            return line;
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

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
        public Line[] GetLines()
        {            
            XPathNavigator navigator = createDocNavigator();
            XPathNodeIterator i;
            i = navigator.Select("/Plaan/Liinid/Liin");
            var lines = new List<Line>();
            while(i.MoveNext())          
            {                
                lines.Add(createLine(i.Current));
            }
            return lines.ToArray();
        }

        public LineWithSchedule GetLine(string id)
        {
            XPathNavigator navigator = createDocNavigator();
            XPathNodeIterator i;
            i = navigator.Select("/Plaan/Liinid/Liin[@id = '" + id + "']");
            LineWithSchedule lineWithSchedule = null;
            while (i.MoveNext())
            {
                lineWithSchedule = new LineWithSchedule(createLine(i.Current));
                XPathNodeIterator j = i.Current.Select("PeatusedLiinil/PeatusLiinil");
                while (j.MoveNext())
                {
                    lineWithSchedule.AddStop(
                        createStation(navigator.SelectSingleNode("//Peatus[@id = '" + j.Current.SelectSingleNode("@refid") + "']")),
                        PadTime(i.Current.SelectSingleNode("Aeg").ToString()));
                }
            }
            return lineWithSchedule;
        }

        private static string PadTime(string time)
        {            
            return (time.PadLeft(5, '0'));
        }

        public Station[] GetStations() 
        {
            var navigator = createDocNavigator();
            var i = navigator.Select("/Plaan/Peatused/Peatus");
            List<Station> stations = new List<Station>();
            while (i.MoveNext())
            {
                stations.Add(createStation(i.Current));
            }
            return stations.ToArray();
        }

        public StationSchedule GetStationSchedule(string id) {
            var navigator = createDocNavigator();
            var i = navigator.Select("//PeatusLiinil[@refid = '" + id + "']");            
            var schedule = new StationSchedule(createStation(navigator.SelectSingleNode("//Peatus[@id = '" + id + "']")));
            if(schedule != null) {
                while (i.MoveNext()) {
                    var line = createLine(i.Current.SelectSingleNode("../.."));
                    schedule.AddDeparture(line, PadTime(i.Current.SelectSingleNode("Aeg").ToString()));
                }
            }
            return schedule;
        }


        private static Station createStation(XPathNavigator node)
        {
            return node == null ? null : new Station(
                                node.SelectSingleNode("@id").ToString(),
                                node.SelectSingleNode("Nimetus").ToString());
        }

        private static Line createLine(XPathNavigator node)
        {
            var id = node.SelectSingleNode("@id").ToString();
            var sectionNodes = node.Select("Suund");
            var sections = "";
            while (sectionNodes.MoveNext())
            {
                sections += (sections.Length > 0 ? " / " : "") + sectionNodes.Current.ToString();
            }
            var line = new Line(id, sections);
            return line;
        }

        private static XPathNavigator createDocNavigator()
        {
            XPathDocument xmldoc = new XPathDocument(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soiduplaan.xml"));
            return xmldoc.CreateNavigator();
        }
    }
}

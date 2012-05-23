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
using System.Web.Security;

namespace ScheduleServiceApp
{
    // Service implementation for the schedule service
    public class XmlScheduleService : ScheduleService
    {
        // select all lines with XPath and populate a collection
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

        // 
        public LineWithSchedule GetLine(string id)
        {
            XPathNavigator navigator = createDocNavigator();
            XPathNodeIterator i;
            // get the requested line by ID
            i = navigator.Select("/Plaan/Liinid/Liin[@id = '" + id + "']");
            LineWithSchedule lineWithSchedule = null;
            while (i.MoveNext())
            {
                // for each found line we iterate over all the station nodes and populate the schedule
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

        // convenience method to 0-pad times with 1-digit hour field
        private static string PadTime(string time)
        {            
            return (time.PadLeft(5, '0'));
        }

        public Station[] GetStations() 
        {
            var navigator = createDocNavigator();
            // iterate over all stations and populate a list           
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
            // find the requested station by ID
            var i = navigator.Select("//PeatusLiinil[@refid = '" + id + "']");            
            var schedule = new StationSchedule(createStation(navigator.SelectSingleNode("//Peatus[@id = '" + id + "']")));
            // for each stop in this station on any line...
            if(schedule != null) {
                while (i.MoveNext()) {
                    // ...we add a departure to the station schedule
                    var line = createLine(i.Current.SelectSingleNode("../.."));
                    schedule.AddDeparture(line, PadTime(i.Current.SelectSingleNode("Aeg").ToString()));
                }
            }
            return schedule;
        }

        // convenience method for creating a Station object from a node
        private static Station createStation(XPathNavigator node)
        {
            return node == null ? null : new Station(
                                node.SelectSingleNode("@id").ToString(),
                                node.SelectSingleNode("Nimetus").ToString());
        }

        // convenience method for creating a Line object from a node
        private static Line createLine(XPathNavigator node)
        {
            var id = node.SelectSingleNode("@id").ToString();
            var sectionNodes = node.Select("Suund");
            var sections = "";
            // join sections into a single string 
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

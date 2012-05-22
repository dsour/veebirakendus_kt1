using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScheduleClientForms
{
    /// <summary>
    /// See klientrakendus otsib ScheduleServiceApp-teenust kasutades rongisõiduplaane.
    /// Ühe dropdown-menüü kaudu antakse ette lähtejaam, teise kaudu lõppjaam (kui lähtejaam on
    /// valitud, siis lõppjaamade menüüs sedasama jaama ei kuvata). Tulemused kuvatakse listbox'is
    /// formaadis lähteaeg -> lõppaeg (00:00 -> 01:00). Tulemused järjestatakse alates käesolevast
    /// ajahetkest 24h ulatuses. Jrg päeva aegadele kirjutatakse järele "homme". Kui tulemused on
    /// kuvatud, saab otsida reise ka tagasi-suunal.
    /// </summary>

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (ServiceReference1.ScheduleServiceClient client = new ServiceReference1.ScheduleServiceClient())
            {
                //populate list of possible starting points in alphabetic order
                var stations = client.GetStations();
                List<string> namesInOrder = new List<String>();
                foreach (var a in stations)
                {
                    namesInOrder.Add(a.name);
                }
                namesInOrder.Sort();
                foreach (var stName in namesInOrder)
                    startStationBox.Items.Add(stName);

                //try to preselect Tallinn as starting point
                //if it works, we'll exclude Tallinn from destinations for the moment
                string chosenOne = "";
                startStationBox.SelectedIndex = startStationBox.FindStringExact("tallinn");
                if (startStationBox.SelectedValue != null)
                    chosenOne = startStationBox.SelectedItem.ToString();

                foreach (var stName in namesInOrder)
                {
                    if (stName != chosenOne)
                        endStationBox.Items.Add(stName);
                }
            }
        }

        private void startStationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (ServiceReference1.ScheduleServiceClient client = new ServiceReference1.ScheduleServiceClient())
            {
                //get all the stations; put them in alphabetic order
                var stations = client.GetStations();
                List<string> namesInOrder = new List<String>();
                foreach (var a in stations)
                {
                    namesInOrder.Add(a.name);
                }
                namesInOrder.Sort();

                //if a starting point is successfully chosen, it is excluded from destinations for the moment
                string selStart = startStationBox.SelectedItem.ToString();
                if (selStart != null)
                {
                    while (endStationBox.Items.Count > 0)
                        endStationBox.Items.RemoveAt(0);

                    foreach (var stName in namesInOrder)
                    {
                        if (stName != selStart)
                            endStationBox.Items.Add(stName);
                    }
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();

            using (ServiceReference1.ScheduleServiceClient client = new ServiceReference1.ScheduleServiceClient())
            {
                if (startStationBox.SelectedItem != null && endStationBox.SelectedItem != null)
                {
                    string startIndex = "";
                    string endIndex = "";

                    //identify the right start and end stations
                    var stations = client.GetStations();
                    foreach (var a in stations)
                    {
                        if (a.name == startStationBox.SelectedItem.ToString())
                            startIndex = a.id;
                        else if (a.name == endStationBox.SelectedItem.ToString())
                            endIndex = a.id;
                    }
                    
                    //if we found the stations, let's find some times
                    DateTime timeNow = DateTime.Now;                    

                    if (startIndex != "" && endIndex != "")
                    {
                        var startSchedule = client.GetStationSchedule(startIndex);
                        var endSchedule = client.GetStationSchedule(endIndex);

                        //add the departures of next 24h in order of time
                        //first the ones within the same date
                        foreach (var dept in startSchedule.departures)
                        {
                            if (DateTime.Parse(dept.Key).TimeOfDay > timeNow.TimeOfDay)
                            {
                                //check if destination is available for this departure
                                //e.g on the same line and arrival time greater than departure time
                                foreach (var arrival in endSchedule.departures)
                                {  
                                    if (dept.Value.id == arrival.Value.id &&
                                        DateTime.Parse(arrival.Key).TimeOfDay > DateTime.Parse(dept.Key).TimeOfDay)
                                    {
                                        resultsListBox.Items.Add(dept.Key + "  ->  " + arrival.Key);
                                    }
                                }
                            }
                        }
                        //then the ones in the next date
                        foreach (var dept in startSchedule.departures)
                        {
                            //check if departure already listed
                            Boolean placed = false;
                            foreach (var item in resultsListBox.Items)
                            {
                                string algus = item.ToString().Substring(0, 5);
                                if (item.ToString().Substring(0,5) == dept.Key)
                                    placed = true;                                    
                            }
                            //if not listed, then let's list it
                            if (!placed)
                            {
                                //check if destination is available for this departure
                                //e.g on the same line and arrival time greater than departure time
                                foreach (var arrival in endSchedule.departures)
                                {
                                    if (dept.Value.id == arrival.Value.id &&                                        
                                        DateTime.Parse(arrival.Key).TimeOfDay > DateTime.Parse(dept.Key).TimeOfDay)
                                    {
                                        resultsListBox.Items.Add(dept.Key + "  ->  " + arrival.Key + "  homme");
                                    }
                                }
                            }
                        }
                    }
                    if (resultsListBox.Items.Count > 0)
                    {
                        labelResults.Visible = true;
                        resultsListBox.Visible = true;
                        returnButton.Visible = true;
                    }
                }
            }
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();

            using (ServiceReference1.ScheduleServiceClient client = new ServiceReference1.ScheduleServiceClient())
            {
                if (startStationBox.SelectedItem != null && endStationBox.SelectedItem != null)
                {
                    string startIndex = "";
                    string endIndex = "";

                    //identify the right start and end stations
                    var stations = client.GetStations();
                    foreach (var a in stations)
                    {
                        if (a.name == endStationBox.SelectedItem.ToString())
                            startIndex = a.id;
                        else if (a.name == startStationBox.SelectedItem.ToString())
                            endIndex = a.id;
                    }

                    //if we found the stations, let's find some times
                    DateTime timeNow = DateTime.Now;

                    if (startIndex != "" && endIndex != "")
                    {
                        var startSchedule = client.GetStationSchedule(startIndex);
                        var endSchedule = client.GetStationSchedule(endIndex);

                        //add the departures of next 24h in order of time
                        //first the ones within the same date
                        foreach (var dept in startSchedule.departures)
                        {
                            if (DateTime.Parse(dept.Key).TimeOfDay > timeNow.TimeOfDay)
                            {
                                //check if destination is available for this departure
                                //e.g on the same line and arrival time greater than departure time
                                foreach (var arrival in endSchedule.departures)
                                {
                                    if (dept.Value.id == arrival.Value.id &&
                                        DateTime.Parse(arrival.Key).TimeOfDay > DateTime.Parse(dept.Key).TimeOfDay)
                                    {
                                        resultsListBox.Items.Add(dept.Key + "  ->  " + arrival.Key);
                                    }
                                }
                            }
                        }
                        //then the ones in the next date
                        foreach (var dept in startSchedule.departures)
                        {
                            //check if departure already listed
                            Boolean placed = false;
                            foreach (var item in resultsListBox.Items)
                            {
                                string algus = item.ToString().Substring(0, 5);
                                if (item.ToString().Substring(0, 5) == dept.Key)
                                    placed = true;
                            }
                            //if not listed, then let's list it
                            if (!placed)
                            {
                                //check if destination is available for this departure
                                //e.g on the same line and arrival time greater than departure time
                                foreach (var arrival in endSchedule.departures)
                                {
                                    if (dept.Value.id == arrival.Value.id &&
                                        DateTime.Parse(arrival.Key).TimeOfDay > DateTime.Parse(dept.Key).TimeOfDay)
                                    {
                                        resultsListBox.Items.Add(dept.Key + "  ->  " + arrival.Key + "  homme");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

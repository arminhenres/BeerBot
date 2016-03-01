using Core;
using Microsoft.Win32;
using RobotControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HilfsGUI
{
    public class ViewModel : NotifyPropertyChanged
    {
        int i;
        public ViewModel()
        {
            Coordinates = new ObservableCollection<Coordinate>();
            int i = 0;
            AddCoordinateCommand = new ActionCommand(AddCoordinate);
            SaveCoordinatesCommand = new ActionCommand(SaveCoordinates);
            LoadCoordinatesCommand = new ActionCommand(LoadCoordinates);
        }

        

        #region Properties
        public ObservableCollection<Coordinate> Coordinates
        {
            get;
            set;
        }

        public string X
        {
            get;
            set;
        }
        public string Y
        {
            get;
            set;
        }
        public string Z
        {
            get;
            set;
        }
        public string L1
        {
            get;
            set;
        }
        public string L2
        {
            get;
            set;
        }
        #endregion

        #region ActionCommands
        public ActionCommand AddCoordinateCommand
        {
            get;
            set;
        }

        public ActionCommand SaveCoordinatesCommand
        {
            get;
            set;
        }

        public ActionCommand LoadCoordinatesCommand
        {
            get;
            set;
        }
        #endregion

        #region Methods
        private void AddCoordinate()
        {
            if (X == null)
            {
                X = "0";
            }

            if (Y == null)
            {
                Y = "0";
            }

            if (Z == null)
            {
                Z = "0";
            }

            if (L1 == null)
            {
                L1 = "0";
            }

            if (L2 == null)
            {
                L2 = "0";
            }


            while(Coordinates.Any(x => Int32.Parse(x.Name.Replace("#",""))>=i))
            {
                i++;
            }
            Coordinates.Add(new Coordinate(X, Y, Z, L1, L2, "#"+i.ToString()));
            OnPropertyChanged("Coordinates");
            
        }

        public void SaveCoordinates()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "*.xml";
            save.Filter = "XML Files|*.xml";

            bool x = (bool)save.ShowDialog();
            if(x)
            {
                string filename = save.FileName;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(filename, settings);


                writer.WriteStartDocument();
                writer.WriteStartElement("Coordinates");
                int i = 0;
                foreach (Coordinate s in Coordinates)
                {
                    writer.WriteStartElement("Coordinate");
                    writer.WriteAttributeString("Name", s.Name);
                    writer.WriteAttributeString("X", s.X.ToString());
                    writer.WriteAttributeString("Y", s.Y.ToString());
                    writer.WriteAttributeString("Z", s.Z.ToString());
                    writer.WriteAttributeString("L1", s.L1.ToString());
                    writer.WriteAttributeString("L2", s.L2.ToString());
                    writer.WriteEndElement();
                    i++;
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        public void LoadCoordinates()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "*.xml";
            open.Filter = "XML Files|*.xml";
            bool success = (bool)open.ShowDialog();

            if(success)
            {
                var document = XDocument.Load(open.FileName);

                var elements = document.Descendants("Coordinate");
                Coordinates.Clear();
                foreach (var command in elements)
                {
                    Coordinates.Add(new Coordinate(command.Attribute("X").Value, command.Attribute("Y").Value, command.Attribute("Z").Value, command.Attribute("L1").Value, command.Attribute("L2").Value, command.Attribute("Name").Value));
                }
            }
        }

        #endregion
    }
}

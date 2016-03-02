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
            ResetAbsolutCommand = new ActionCommand(ResetAbsolut);
        }

        

        #region Properties
        public ObservableCollection<Coordinate> Coordinates
        {
            get;
            set;
        }

        public string XAbsolut
        {
            get;
            set;
        }

        public string YAbsolut
        {
            get;
            set;
        }

        public string ZAbsolut
        {
            get;
            set;
        }

        public string L1Absolut
        {
            get;
            set;
        }

        public string L2Absolut
        {
            get;
            set;
        }

        public string XJoint
        {
            get;
            set;
        }

        public string YJoint
        {
            get;
            set;
        }

        public string ZJoint
        {
            get;
            set;
        }

        public string L1Joint
        {
            get;
            set;
        }

        public string L2Joint
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

        public ActionCommand AddCommandJointCommand
        {
            get;
            set;
        }
        public ActionCommand AddCommandAbsolutCommand
        {
            get;
            set;
        }


        public ActionCommand ResetAbsolutCommand
        {
            get;
            set;
        }

        public ActionCommand ResetJointCommand
        {
            get;
            set;
        }

        public ActionCommand MoveInstantJointCommand
        {
            get;
            set;
        }

        public ActionCommand MoveInstantAbsolutCommand
        {
            get;
            set;
        }
        #endregion

        #region Methods
        private void AddCoordinate()
        {
            if (XAbsolut == null)
            {
                XAbsolut = "0";
            }

            if (YAbsolut == null)
            {
                YAbsolut = "0";
            }

            if (ZAbsolut == null)
            {
                ZAbsolut = "0";
            }

            if (L1Absolut == null)
            {
                L1Absolut = "0";
            }

            if (L2Absolut == null)
            {
                L2Absolut = "0";
            }


            while(Coordinates.Any(x => Int32.Parse(x.Name.Replace("#",""))>=i))
            {
                i++;
            }
            Coordinates.Add(new Coordinate(XAbsolut, YAbsolut, ZAbsolut, L1Absolut, L2Absolut, "#"+i.ToString()));
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

        public void ResetAbsolut()
        {
            XAbsolut = "0";
            YAbsolut = "0";
            ZAbsolut = "0";
            L1Absolut = "0";
            L2Absolut = "0";
        }

        public void ResetJoint()
        {
            XJoint = "0";
            YJoint = "0";
            ZJoint = "0";
            L1Joint = "0";
            L2Joint = "0";
        }

        #endregion
    }
}

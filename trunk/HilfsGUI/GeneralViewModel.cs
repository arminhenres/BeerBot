using Core;
using Microsoft.Win32;
using RobotControl;
using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace HilfsGUI
{
    public class GeneralViewModel : NotifyPropertyChanged
	{
		private MoveMasterRobot _robot;

		private int _coordinateCounter;
		
		public GeneralViewModel()
		{
			
			_coordinateCounter = 0;
			//_robot = new MoveMasterRobot();

			Coordinates = new ObservableCollection<Coordinate>();
			Commands = new ObservableCollection<RobotCommand>();
			
			AddCoordinateCommand = new ActionCommand(AddCoordinate);
			SaveCoordinatesCommand = new ActionCommand(SaveCoordinates);
			LoadCoordinatesCommand = new ActionCommand(LoadCoordinates);
			ResetAbsolutCommand = new ActionCommand(ResetAbsolut);
			ResetJointCommand = new ActionCommand(ResetJoint);
			AddCommandJointCommand = new ActionCommand(AddCommandJoint);
			AddCommandAbsolutCommand = new ActionCommand(AddCommandAbsolut);
			MoveInstantJointCommand = new ActionCommand(MoveInstantJoint);
			MoveInstantAbsolutCommand = new ActionCommand(MoveInstantAbsolut);
			SaveCommandsCommand = new ActionCommand(SaveCommands);
			LoadCommandsCommand = new ActionCommand(LoadCommands);
			WhereCommand = new ActionCommand(Where);
            AddCoordinateWhereCommand = new ActionCommand(AddCoordinateWhere);
            AddGripCloseCommand = new ActionCommand(AddGripClose);
            AddGripOpenCommand = new ActionCommand(AddGripOpen);
            GripCloseInstantCommand = new ActionCommand(GripCloseInstant);
            GripOpenInstantCommand = new ActionCommand(GripOpenInstant);
            NestCommand = new ActionCommand(Nest);
            OriginCommand = new ActionCommand(Origin);
            ResetCommand = new ActionCommand(Reset);
        }

		public GeneralViewModel(MoveMasterRobot robot) :this()
        {
            _robot = robot;
        }

		#region Properties
		public ObservableCollection<Coordinate> Coordinates
		{
			get;
			set;
		}

		public ObservableCollection<RobotCommand> Commands
		{
			get;
			set;
		}

		#region Absolutkoordinaten
		private string _xAbsolut;
		public string XAbsolut
		{
			get
			{
				if(_xAbsolut == null)
				{
					_xAbsolut = "0";
				}
				return _xAbsolut;
			}
			set
			{
				_xAbsolut = value;
			}
		}

		private string _yAbsolut;
		public string YAbsolut
		{
			get
			{
				if(_yAbsolut == null)
				{
					_yAbsolut = "0";
				}
				return _yAbsolut;
			}
			set
			{
				_yAbsolut = value;
			}
		}
		
		private string _zAbsolut;
		public string ZAbsolut
		{
			get
			{
				if(_zAbsolut == null)
				{
					_zAbsolut = "0";
				}
				return _zAbsolut;
			}
			set
			{
				_zAbsolut = value;
			}
		}

		private string _l1Absolut;
		public string L1Absolut
		{
			get
			{
				if(_l1Absolut == null)
				{
					_l1Absolut = "0";
				}
				return _l1Absolut;
			}
			set
			{
				_l1Absolut = value;
			}
		}

		private string _l2Absolut;
		public string L2Absolut
		{
			get
			{
				if(_l2Absolut == null)
				{
					_l2Absolut = "0";
				}
				return _l2Absolut;
			}
			set
			{
				_l2Absolut = value;
			}
		}
		#endregion

		#region Jointkoordinaten
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

		#region Where-Koordinaten
		public string XWhere
		{
			get;
			set;
		}

		public string YWhere
		{
			get;
			set;
		}

		public string ZWhere
		{
			get;
			set;
		}

		public string L1Where
		{
			get;
			set;
		}

		public string L2Where
		{
			get;
			set;
		}
		#endregion

		
		public string NameAbsolut
		{
			get;
			set;
		}

		public string NameJoint
		{
			get;
			set;
		}

		public string SpeedAbsolut
		{
			get;
			set;
		}

		public string SpeedJoint
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

		public ActionCommand SaveCommandsCommand
		{
			get;
			set;
		}

		public ActionCommand LoadCommandsCommand
		{
			get;
			set;
		}

		public ActionCommand WhereCommand
		{
			get;
			set;
		}

		public ActionCommand AddCoordinateWhereCommand
		{
			get;
			set;
		}
		
		public ActionCommand AddGripOpenCommand
		{
			get;
			set;
		}
		
		public ActionCommand AddGripCloseCommand
		{
			get;
			set;
		}
		
		public ActionCommand GripOpenInstantCommand
		{
			get;
			set;
		}
		
		public ActionCommand GripCloseInstantCommand
		{
			get;
			set;
		}
		
		public ActionCommand OriginCommand
		{
			get;
			set;
		}
		
		public ActionCommand NestCommand
		{
			get;
			set;
		}
		
		public ActionCommand ResetCommand
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


			Coordinates.Add(new Coordinate(XAbsolut, YAbsolut, ZAbsolut, L1Absolut, L2Absolut, NameAbsolut));
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
			OnPropertyChanged("XAbsolut");
			OnPropertyChanged("YAbsolut");
			OnPropertyChanged("ZAbsolut");
			OnPropertyChanged("L1Absolut");
			OnPropertyChanged("L2Absolut");
		}

		public void ResetJoint()
		{
			XJoint = "0";
			YJoint = "0";
			ZJoint = "0";
			L1Joint = "0";
			L2Joint = "0";
			OnPropertyChanged("XJoint");
			OnPropertyChanged("YJoint");
			OnPropertyChanged("ZJoint");
			OnPropertyChanged("L1Joint");
			OnPropertyChanged("L2Joint");
		}

		public void MoveInstantAbsolut()
		{
			var speed = Int32.Parse(SpeedAbsolut);
			Coordinate coordinate = new Coordinate(XAbsolut, YAbsolut, ZAbsolut, L1Absolut, L2Absolut, "InstantMove");
			_robot.MoveAbsolut(coordinate, speed, true);
		}

		public void MoveInstantJoint()
		{
			var speed = Int32.Parse(SpeedJoint);
			Coordinate coordinate = new Coordinate(XJoint, YJoint, ZJoint, L1Joint, L2Joint, "InstantMove");
			_robot.MoveJoint(coordinate.X, coordinate.Y, coordinate.Z, coordinate.L1, coordinate.L2, speed, true);
			
		}

		public void AddCommandAbsolut()
		{
			var speed = Int32.Parse(SpeedAbsolut);
			Coordinate coordinate = new Coordinate(XAbsolut, YAbsolut, ZAbsolut, L1Absolut, L2Absolut, NameAbsolut);
			_robot.MoveAbsolut(coordinate, speed, false);
			Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
			OnPropertyChanged("Commands");
		}

		public void AddCommandJoint()
		{
			var speed = Int32.Parse(SpeedJoint);
			Coordinate coordinate = new Coordinate(XJoint, YJoint, ZJoint, L1Joint, L2Joint, "InstantMove");
			_robot.MoveJoint(coordinate.X, coordinate.Y, coordinate.Z, coordinate.L1, coordinate.L2, speed, false);
            Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
            OnPropertyChanged("Commands");
		}

		public void SaveCommands()
		{
			SaveFileDialog save = new SaveFileDialog();
			save.DefaultExt = "*.xml";
			save.Filter = "XML Files|*.xml";

			bool x = (bool)save.ShowDialog();
			if (x)
			{
				_robot.SaveFile(save.FileName);
			}
		}

		public void LoadCommands()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.DefaultExt = "*.xml";
			open.Filter = "XML Files|*.xml";
			bool success = (bool)open.ShowDialog();

			if(success)
			{
				_robot.ReadFile(open.FileName);
				Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
				OnPropertyChanged("Commands");
			}
		}

		public void Where()
		{
			Coordinate coordinate = _robot.Where();
			string[] stringRep = coordinate.GetStringsAsCoordinate();
			XWhere = stringRep[0];
			YWhere = stringRep[1];
			YWhere = stringRep[2];
			L1Where = stringRep[3];
			L2Where = stringRep[4];

			OnPropertyChanged("XWhere");
			OnPropertyChanged("YWhere");
			OnPropertyChanged("ZWhere");
			OnPropertyChanged("L1Where");
			OnPropertyChanged("L2Where");
		}

		public void AddCoordinateWhere()
		{
			Coordinates.Add(new Coordinate(XWhere, YWhere, ZWhere, L1Where, L2Where, "Coordinate " + _coordinateCounter));
			_coordinateCounter++;
			OnPropertyChanged("Coordinates");
		}
		
		public void Origin()
		{
			_robot.Origin(true);
		}
		
		public void Nest()
		{
			_robot.Nest(true);
		}
		
		public void Reset()
		{
			_robot.Reset();
		}

        public void AddGripClose()
        {
            _robot.GripClose(false);
            Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
            OnPropertyChanged("Commands");
        }

        public void AddGripOpen()
        {
            _robot.GripOpen(false);
            Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
            OnPropertyChanged("Commands");
        }

        public void GripCloseInstant()
        {
            _robot.GripClose(true);
        }

        public void GripOpenInstant()
        {
            _robot.GripClose(true);
        }
        #endregion
    }
}

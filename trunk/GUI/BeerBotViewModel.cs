using Core;
using RobotControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI
{
    public class BeerBotViewModel : NotifyPropertyChanged
    {
        IRobot _robot;
        

        public BeerBotViewModel()
        {
            StartBeerCommand = new ActionCommand(StartBeerThread);
            
        }

        public BeerBotViewModel(IRobot robot) :this()
        {
            _robot = robot;
        }

        public ActionCommand StartBeerCommand
        {
            get;
            set;
        }

        private ObservableCollection<RobotCommand> _commands;
        public ObservableCollection<RobotCommand> Commands
        {
            get
            {
                return _commands;
            }
            set
            {
                _commands = value;
                OnPropertyChanged("Commands");
            }
        }

        private int _selectedCommand;
        public int SelectedCommand
        {
            get
            {
                return _selectedCommand;
            }
            set
            {
                _selectedCommand = value;
                OnPropertyChanged("SelectedCommand");
            }
        }

        public int Progress
        {
            get;
            set;
        }
        private bool _isBeerInitialized;
        public bool IsBeerInitialized
        {
            get
            {
                return _isBeerInitialized;
            }
            set
            {
                if(value)
                {
                    _robot.ReadFile("data\\Biereinschenkvorgang.xml");
                }
                else
                {
                    _robot.CommandsList.Clear();
                }
                Commands = new ObservableCollection<RobotCommand>(_robot.CommandsList);
                _isBeerInitialized = value;
            }
        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
            set
            {
                _isInitialized = value;
                OnPropertyChanged("IsInitialized");
            }
        }
        public void StartBeerThread()
        {
            Thread thread = new Thread(delegate() { StartBeer(); });
            thread.Start();
        }

        public void StartBeer()
        {
            _robot.commandsChanged += new CommandsChangedEventHandler(OnCommandsChanged);
            _robot.ExecuteCommands();
        }

        public void OnCommandsChanged(object o, int index)
        {
            if(index+1<Commands.Count)
            {
                SelectedCommand = index-1;
            }
            else
            {
                SelectedCommand = index;
            }
            double progress = (Convert.ToDouble(SelectedCommand+1) / Convert.ToDouble(Commands.Count)) * 100;
            Progress = (int)progress;
            OnPropertyChanged("Progress");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Backgammon;
using BackGammonLogic;

namespace BackGammonWPF
{

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    class BackGammonViewModel : BaseViewModel
    {
        private bool _arrowColor;
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        private GameManager gameLogic;
        private int turnState = 0;

        public ObservableCollection<ColumnItem> Board
        {
            get
            {
                var _board = new ObservableCollection<ColumnItem>();
                int id = 0;
                var checkersCountList = new List<int>();
                foreach (var column in gameLogic.GetBoardState())
                {
                    for (int i = 0; i < column.Count; i++)
                    {
                        checkersCountList.Add(1);
                    }
                    _board.Add(new ColumnItem(id, column.State,new List<int>(checkersCountList)));
                    id++;
                    checkersCountList.Clear();
                }
                return _board;
            } 
           private set{}
        }

        public ColumnItem SelectedColumnItem
        {
            get
            {
                return new ColumnItem(1,ColorColumState.ColorsColumState.White,new List<int>());
            }
            set
            {
                if (turnState==0)
                {
                    if (gameLogic.SetOriginColumn(value.Id.ToString()))
                    {
                        turnState++;
                    }
                }
                else
                {
                    if (gameLogic.SetTargetColumn(value.Id.ToString()))
                    {
                        turnState = 0;
                    }
                }
            }
        }

        public List<int> DiceResultList
        {
            get {return gameLogic.GetDiceResults(); }
        }

        public BackGammonViewModel()
        {
         gameLogic = new GameManager(Player1, Player2);
        }

        
    }

    internal class ColumnItem
    {
        internal ColumnItem(int _id, Column.ColorsColumState _color, List<int> _checkersCount)
        {
            Id = _id;
            Color = _color;
            CheckersCount = _checkersCount;
        }
        public int Id { get;private set; }
        public Column.ColorsColumState Color { get; private set; }
        public List<int> CheckersCount { get; private set; }
    }
}

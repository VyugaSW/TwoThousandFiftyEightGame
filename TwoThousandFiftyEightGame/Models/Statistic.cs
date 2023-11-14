using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TwoThousandFiftyEightGame.Models
{
    public class Statistic : INotifyPropertyChanged
    {
        private int _curScore;
        private int _highcore;
        
        public int CurrentScore 
        { 
            get => _curScore;
            set
            {
                _curScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentScore)));
            }
        }
        public int HighScore
        {
            get => _highcore;
            set
            {
                _highcore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighScore)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Statistic() 
        { 
            CurrentScore = 0;
            HighScore = 0;
        }
    }
}

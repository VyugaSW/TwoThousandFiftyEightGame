using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TwoThousandFiftyEightGame.Models
{
    public class Tile : INotifyPropertyChanged
    {
        private int? _value;
        
        public int? Value
        {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public int Column { get; set; }
        public int Row { get; set; }

        public Tile(int row, int column, int? value)
        {
            Row = row;
            Column = column;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

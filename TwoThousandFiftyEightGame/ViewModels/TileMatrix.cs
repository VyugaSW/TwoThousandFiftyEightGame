using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Resources;

using TwoThousandFiftyEightGame.Models;
using TwoThousandFiftyEightGame.Background_Settings;

namespace TwoThousandFiftyEightGame.ViewModels
{

    public delegate void MoveTileDelegate();
    



    public class TileMatrix
    {
        public Tile[,] Array { get; set; }

        private bool _moveFlagUp = true;
        private bool _moveFlagDown = true;
        private bool _moveFlagLeft = true;
        private bool _moveFlagRight = true;

        private MoveTileDelegate _moveTileDelegate;

        public TileMatrix()
        {
            Array = new Tile[SettingsBackground.MatrixRows, SettingsBackground.MatrixColumns];
            InitializingArray();
        }

        public Tile this[int row, int column]
        {
            get => Array[row,column];
            set => Array[row,column] = value;
        }

        private void InitializingArray()
        {
            for(int row = 0; row < Array.GetLength(0); row++)
            {
                for (int column = 0; column < Array.GetLength(1); column++)
                    Array[row, column] = new Tile(row, column, null);
            }
        }

        private int? NullToZero(int? value)
        {
            if (value == null)
                return 0;

            if (value == 0)
                return null;

            return value;
        }
        private bool CollisionTiles(Tile tile1, Tile tile2) 
        {
            if (tile1.Value != tile2.Value && tile1.Value != null && tile2.Value != null)
                return false;

            tile1.Value = NullToZero(tile1.Value);
            tile2.Value = NullToZero(tile2.Value);

            tile1.Value += tile2.Value;
            tile2.Value = null;

            tile1.Value = NullToZero(tile1.Value);

            return true;
        }
        private void MoveUp()
        {
            for(int row = Array.GetLength(0) - 1; row > -1; row--)
            {
                for(int column = Array.GetLength(1) - 1; column > -1; column--)
                {
                    if (row - 1 > -1)
                        if (CollisionTiles(Array[row - 1, column], Array[row, column]))
                            _moveFlagUp = true;
                }
            }
        }
        private void MoveDown()
        {
            for (int row = 0; row < Array.GetLength(0); row++)
            {
                for (int column = 0; column < Array.GetLength(1); column++)
                {
                    if (row + 1 < Array.GetLength(0))                  
                         if(CollisionTiles(Array[row + 1, column], Array[row, column]))
                            _moveFlagDown = true;

                }
            }
        }
        private void MoveRight()
        {
            for (int row = 0; row < Array.GetLength(0); row++)
            {
                for (int column = 0; column < Array.GetLength(1); column++)
                {
                    if (column + 1 < Array.GetLength(1))
                        if(CollisionTiles(Array[row, column + 1], Array[row, column]))
                            _moveFlagRight = true;

                }
            }
        }
        private void MoveLeft()
        {
            for (int row = Array.GetLength(0) - 1; row > -1; row--)
            {
                for (int column = Array.GetLength(1) - 1; column > -1; column--)
                {
                    if (column - 1 > -1)
                        if(CollisionTiles(Array[row, column - 1], Array[row, column]))
                            _moveFlagLeft = true;

                }
            }
        }
        public bool Move(Key key)
        {
            switch (key)
            {
                case Key.W:
                case Key.Up:
                    if(_moveFlagUp)
                        _moveTileDelegate = MoveUp;
                    break;
                case Key.S:
                case Key.Down:
                    if (_moveFlagDown)
                        _moveTileDelegate = MoveDown;
                    break;
                case Key.A:
                case Key.Left:
                    if (_moveFlagLeft)
                        _moveTileDelegate = MoveLeft;
                    break;
                case Key.D:
                case Key.Right:
                    if (_moveFlagRight)
                        _moveTileDelegate = MoveRight;
                    break;
                default:
                    return false;
            }

            for(int i = 0; i < SettingsBackground.RepeatsForFullStep; i++)
                _moveTileDelegate();
            return true;
        }

        public void GenerateNewTile()
        {
            Random random = new Random();

            int randRow = random.Next(4);
            int randColumn = random.Next(4);

            while (Array[randRow,randColumn].Value != null) 
            {
                randRow = random.Next(4);
                randColumn = random.Next(4);
            }

            int chance = random.Next(10);

            if (chance == 1)
                Array[randRow, randColumn].Value = 4;
            else
                Array[randRow, randColumn].Value = 2;
        }

        public bool ThereIs2048()
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    if (Array[i, j].Value == 2048)
                        return true;
                }
            }
            return false;
        }

        public bool IsMoving()
        {
            if (!_moveFlagDown && !_moveFlagLeft && !_moveFlagRight && !_moveFlagUp)
                return false;
            return true;

        }

    }
}

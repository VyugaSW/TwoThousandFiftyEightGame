using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Resources;

using TwoThousandFiftyEightGame.Models;

namespace TwoThousandFiftyEightGame.ViewModels
{
    public class TileMatrix
    {
        public Tile[,] Array { get; set; }

        public TileMatrix()
        {
            Array = new Tile[4, 4];
            InitializingArray();
        }

        public Tile this[int row, int column]
        {
            get => Array[row,column];
            set => Array[row,column] = value;
        }

        private int? NullToZero(int? value)
        {
            if (value == null)
                return 0;

            if (value == 0)
                return null;

            return value;
        }

        private void InitializingArray()
        {
            for(int row = 0; row < Array.GetLength(0); row++)
            {
                for (int column = 0; column < Array.GetLength(1); column++)
                    Array[row, column] = new Tile(row, column, null);
            }
        }

        private bool CollisionTiles(Tile tile1, Tile tile2) 
        {
            if (tile1.Value != tile2.Value && (tile1.Value != null && tile2.Value != null))
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
                    if(row - 1 > -1)
                        CollisionTiles(Array[row - 1, column], Array[row, column]);
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
                         CollisionTiles(Array[row + 1, column], Array[row, column]);

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
                        CollisionTiles(Array[row, column + 1], Array[row, column]);

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
                        CollisionTiles(Array[row, column - 1], Array[row, column]);

                }
            }
        }

        public bool Move(Key key)
        {
            switch (key)
            {
                case Key.W:
                case Key.Up:
                    MoveUp();
                    return true;
                case Key.S:
                case Key.Down:
                    MoveDown();
                    return true;
                case Key.A:
                case Key.Left:
                    MoveLeft();
                    return true;
                case Key.D:
                case Key.Right:
                    MoveRight();
                    return true;
                default:
                    return false;
            }
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

        private Tile[,] CopyArray(Tile[,] toArray)
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    toArray[i, j].Value = Array[i, j].Value;
                    toArray[i, j].Column = Array[i, j].Column;
                    toArray[i, j].Row = Array[i, j].Row;
                }
            }
            return toArray;
        }

        public bool IsMoving()
        {
            TileMatrix tempTileMatrix = new TileMatrix();
            tempTileMatrix.Array = CopyArray(tempTileMatrix.Array);

            tempTileMatrix.Move(Key.Up);
            tempTileMatrix.Move(Key.Down);
            tempTileMatrix.Move(Key.Left);
            tempTileMatrix.Move(Key.Right);

            if (tempTileMatrix.Array == Array)
                return false;

            return true;

        }

    }
}

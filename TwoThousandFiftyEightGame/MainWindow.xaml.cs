using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;

using TwoThousandFiftyEightGame.Models;
using TwoThousandFiftyEightGame.ViewModels;

namespace TwoThousandFiftyEightGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TileStyleChanger TileStyleChanger { get; set; }
        public TileMatrix Matrix { get; set; }
        public ScoreStatistic ScoreStatistic { get; set; }

        private DispatcherTimer _dispatcherTimer;
        private bool _flagAllow = true;
        private int _countMoves = 0;

        public MainWindow()
        {
            InitializeComponent();
            Matrix = new TileMatrix();
            TileStyleChanger = new TileStyleChanger(GameField);
            ScoreStatistic = new ScoreStatistic();

            _dispatcherTimer = new DispatcherTimer();
            
            ScoreTextBox.DataContext = ScoreStatistic.Statistic;
            HighScoreTextBox.DataContext = ScoreStatistic.Statistic;
            PreviewKeyDown += Window_PreviewKeyDown;

            InitializingTiles();
            InitializingDispatcherTimer();

        }

        public void InitializingTiles()
        {
            int row = 0;
            int column = 0;

            foreach (Label item in GameField.Children)
            {
                item.DataContext = Matrix.Array[row, column];

                if (column + 1 < Matrix.Array.GetLength(1))
                    column++;

                else
                {
                    row++;
                    column = 0;
                }

            }

            Matrix.GenerateNewTile();
            TileStyleChanger.ChangeStyle();
        }

        public void InitializingDispatcherTimer()
        {
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(0.2);
            _dispatcherTimer.Tick += (s, args) => _flagAllow = true;
            _dispatcherTimer.Start();
        }

        public void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_flagAllow)
            {
                if (Matrix.Move(e.Key))
                    _countMoves++;

                IsEnd();

                if (_countMoves % 2 == 0)
                    Matrix.GenerateNewTile();

                ScoreStatistic.UpdateScore(2);
                ScoreStatistic.CompareScore();

                TileStyleChanger.ChangeStyle();

                _flagAllow = false;
            }
        }

        public void IsEnd()
        {
            if (!Matrix.IsMoving())
            {
                MessageBox.Show("You lost!");
                UpdateGame();
            }

            else if (Matrix.ThereIs2048())
            {
                MessageBox.Show("You won!");
                UpdateGame();
            }
        }

        public void UpdateGame() 
        {
            Matrix = new TileMatrix();
            ScoreStatistic.ClearScore();
        }

    }
}

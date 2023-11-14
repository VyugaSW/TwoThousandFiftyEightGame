using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwoThousandFiftyEightGame.Models;

namespace TwoThousandFiftyEightGame.ViewModels
{
    public class ScoreStatistic
    {
        public Statistic Statistic { get; set; }

        public ScoreStatistic()
        {
            Statistic = new Statistic();

        }

        public void UpdateScore(int value)
        {
            Statistic.CurrentScore += value;
        }

        public void ClearScore()
        {
            Statistic.CurrentScore = 0;
        }

        public void CompareScore()
        {
            if( Statistic.CurrentScore > Statistic.HighScore)
                Statistic.HighScore = Statistic.CurrentScore;
        }
    }
}

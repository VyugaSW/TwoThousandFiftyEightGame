using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TwoThousandFiftyEightGame.ViewModels
{
    public class TileStyleChanger
    {
        ResourceDictionary _styles;
        List<Label> _tiles;

        public TileStyleChanger(Grid grid)
        {
            _styles = new ResourceDictionary();
            _tiles = new List<Label>();
            Initializing(grid);
        }

        private void Initializing(Grid grid)
        {
            foreach (Label item in grid.Children)
                _tiles.Add(item);
        }

        private void SwitchStyle(Label tile)
        {
            switch (Convert.ToInt32(tile.Content.ToString()))
            {
                case 2:
                    tile.Style = _styles["StyleTileLabel_2"] as Style;
                    break;
                case 4:
                    tile.Style = _styles["StyleTileLabel_4"] as Style;
                    break;
                case 8:
                    tile.Style = _styles["StyleTileLabel_8"] as Style;
                    break;
                case 16:
                    tile.Style = _styles["StyleTileLabel_16"] as Style;
                    break;
                case 32:
                    tile.Style = _styles["StyleTileLabel_32"] as Style;
                    break;
                case 64:
                    tile.Style = _styles["StyleTileLabel_64"] as Style;
                    break;
                case 128:
                    tile.Style = _styles["StyleTileLabel_128"] as Style;
                    break;
                case 256:
                    tile.Style = _styles["StyleTileLabel_256"] as Style;
                    break;
                case 512:
                    tile.Style = _styles["StyleTileLabel_512"] as Style;
                    break;
                case 1024:
                    tile.Style = _styles["StyleTileLabel_1024"] as Style;
                    break;
                case 2048:
                    tile.Style = _styles["StyleTileLabel_2048"] as Style;
                    break;
            }
        }

        public void ChangeStyle()
        {
            foreach (Label tile in _tiles)
            {
                if (tile.Content != null)
                    SwitchStyle(tile);
                else
                    tile.Style = _styles["StyleTileLabelEmpty"] as Style;

            }
        }


    }
        
}

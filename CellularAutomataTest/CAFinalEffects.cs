using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CellularAutomataTest
{
    public partial class CellularAutomata
    {
        void Smooth()
        {
            CellularAutomata changes = this.Clone();
            for(int i = 1; i < this.Grid.Count - 1; i++)
            {
                for(int j = 1; j < this.Grid[i].Count - 1; j++)
                {
                    if (Grid[i][j].State == Cell.States.Alive)
                    {
                        changes.FillRectangle(new Point(i - 2, j - 1), new Point(i + 2, j + 1), Cell.States.Alive);
                        changes.FillRectangle(new Point(i - 1, j - 2), new Point(i + 1, j + 2), Cell.States.Alive);
                    }
                }
            }
            ApplyChanges(Grid, changes.Grid);
        }

        void FillHoles()
        {
            for(int i = 1; i < this.Grid.Count - 1; i++)
            {
                for(int j = 1;  j < this.Grid[i].Count - 1; j++)
                {
                    if (AliveNeighbours(i, j) == 8)
                    {
                        Grid[i][j].SetState(Cell.States.Alive);
                    }
                }
            }
        }
    }
}

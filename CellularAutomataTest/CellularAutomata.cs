using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CellularAutomataTest
{
    public class CellularAutomata
    {
        List<List<Cell>> Grid;
        
        static class Properties
        {
            public static uint Width;
            public static uint Height;
        }

        static class Rules
        {
            public static short DieOvercrowded = 10;
            public static short DieAlone = 4;
            public static short Spawn = 5;
        }

        public class Cell
        {
            public enum States
            {
                Dead,
                Alive,
                Permadead,
                Permaalive
            }

            public static class Palette
            {
                public static readonly Color Dead = Colors.White;
                public static readonly Color Alive = Colors.Black;
            }

            public States state { get; private set; }

            public Cell() => state = States.Dead;

            public void SetState(States state) => this.state = state;
        }

        public CellularAutomata(uint width, uint height, short dieAlone, short dieOvercrowded, short spawn)
        {
            Properties.Width = width + 2;
            Properties.Height = height + 2;

            Rules.DieAlone = dieAlone;
            Rules.DieOvercrowded = dieOvercrowded;
            Rules.Spawn = spawn;

            Grid = new List<List<Cell>>();
            for (int i = 0; i < width; i++)
            {
                Grid.Add(new List<Cell>());
                for (int j = 0; j < height; j++)
                {
                    Grid[i].Add(new Cell());
                }
            }
        }

        public void Iterate()
        {
            List<List<Cell>> changes = Grid;
            for(int i = 1; i < Grid.Count - 1; i++)
            {
                for(int j = 1; j < Grid[i].Count - 1; j++)
                {
                    switch (Grid[i][j].state)
                    {
                        case Cell.States.Dead:
                            if (this.AliveNeighbours(i, j) == Rules.Spawn)
                            {
                                changes[i][j].SetState(Cell.States.Alive);
                            }
                            break;

                        case Cell.States.Alive:
                            if (this.AliveNeighbours(i, j) <= Rules.DieAlone || 
                                this.AliveNeighbours(i, j) >= Rules.DieOvercrowded) {
                                changes[i][j].SetState(Cell.States.Dead);
                            }
                            break;
                    }
                }
            }

            for (int i = 1; i < Grid.Count - 1; i++)
            {
                for (int j = 1; j < Grid[i].Count - 1; j++)
                {
                    Grid[i][j].SetState(changes[i][j].state);
                }
            }
        }

        public void Draw(WriteableBitmap writeableBitmap)
        {
            for(int i = 1; i < Grid.Count - 1; i++)
            {
                for(int j = 1; j < Grid[i].Count - 1; j++)
                {
                    switch (Grid[i][j].state)
                    {
                        case Cell.States.Alive:
                            App.Current.Dispatcher.Invoke(() => 
                            { 
                                writeableBitmap.SetPixel(i, j, Cell.Palette.Alive); 
                            });
                            break;

                        case Cell.States.Dead:
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                writeableBitmap.SetPixel(i, j, Cell.Palette.Dead);
                            });
                            break;

                        case Cell.States.Permadead:
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                writeableBitmap.SetPixel(i, j, Cell.Palette.Dead);
                            });
                            break;

                        case Cell.States.Permaalive:
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                writeableBitmap.SetPixel(i, j, Cell.Palette.Alive);
                            });
                            break;
                    }
                }
            }
        }

        public void FillRectangle(Point TL, Point RB, Cell.States state)
        {
            for(int i = (int)TL.X; i < RB.X; i++)
            {
                for(int j = (int)TL.Y; j < RB.Y; j++)
                {
                    Grid[i][j].SetState(state);
                }
            }
        }

        public void FillRandomized(Point TL, Point RB)
        {
            Random rnd = new Random();
            for (int i = (int)TL.X; i < RB.X; i++)
            {
                for (int j = (int)TL.Y; j < RB.Y; j++)
                {
                    Grid[i][j].SetState(rnd.Next() % 2 == 0 ? Cell.States.Alive : Cell.States.Dead);
                }
            }
        }

        public void Randomize()
        {
            Random rnd = new Random();
            for(int i = 1; i<Grid.Count - 1; i++)
            {
                for(int j = 1; j < Grid[i].Count - 1; j++)
                {
                    Grid[i][j].SetState(rnd.Next() % 2 == 0 ? Cell.States.Alive : Cell.States.Dead);
                }
            }
        }

        public void IterationEventHandler(object sender, EventArgs e)
        {
            Iterate();
        }

        short AliveNeighbours(int x, int y)
        {
            short count = 0;
            for(int i = x - 1; i <= x + 1; i++)
            {
                for(int j = y - 1; j <= y + 1; j++)
                {
                    if(i != x && j != y)
                    {
                        if (Grid[i][j].state == Cell.States.Alive ||
                            Grid[i][j].state == Cell.States.Permaalive) { count++; }
                    }
                }
            }
            return count;
        }
    }
}

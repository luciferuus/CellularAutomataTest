using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CellularAutomataTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DebugWriter debugger;
        CellularAutomata cellularAutomata;
        WriteableBitmap writeableBitmap;
        int iterations = 4;
        int size = 128;

        public MainWindow()
        {
            InitializeComponent();
            debugger = new DebugWriter();
            writeableBitmap = BitmapFactory.New(size, 128);
            ImageBitmap.Source = writeableBitmap;
        }

        private void On_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            debugger.Terminate();
        }

        private void Button_ForceIteration_Click(object sender, RoutedEventArgs e)
        {
            DrawRoom(Convert.ToInt32(Input_DieAloneRule.Value),
                     Convert.ToInt32(Input_DieCrowdRule.Value),
                     Convert.ToInt32(Input_Spawn.Value));
        }

        private void DrawRoom(int dieAlone, int dieOvercrowded, int spawn)
        {
            cellularAutomata = new CellularAutomata((uint)size, (uint)size, (short)dieAlone, (short)dieOvercrowded, (short)spawn);

            cellularAutomata.FillRectangle(new Point(0, 0), new Point(size, size), CellularAutomata.Cell.States.Permaalive);
            cellularAutomata.FillRandomized(new Point(20, 20), new Point(size - 20, size - 20));
            cellularAutomata.FillRectangle(new Point(30, 30), new Point(size - 30, size - 30), CellularAutomata.Cell.States.Permadead);

            for (int i = 0; i < 4; i++)
            {
                cellularAutomata.Iterate();
            }

            cellularAutomata.Draw(writeableBitmap);
        }
    }
}

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

        public MainWindow()
        {
            InitializeComponent();
            debugger = new DebugWriter();
            cellularAutomata = new CellularAutomata(100, 100, 4, 3, 1);
            writeableBitmap = BitmapFactory.New(100, 100);
            ImageBitmap.Source = writeableBitmap;
            //cellularAutomata.FillRectangle(new Point(0, 0), new Point(100, 100), CellularAutomata.Cell.States.Permaalive);
            cellularAutomata.FillRandomized(new Point(15, 15), new Point(85, 85));
            //cellularAutomata.FillRectangle(new Point(25, 25), new Point(75, 75), CellularAutomata.Cell.States.Permadead);
            cellularAutomata.Draw(writeableBitmap);
        }

        private void On_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            debugger.Terminate();
        }

        private void Button_ForceIteration_Click(object sender, RoutedEventArgs e)
        {
            cellularAutomata.Iterate();
            cellularAutomata.Draw(writeableBitmap);
        }
    }
}

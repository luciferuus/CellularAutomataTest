using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        readonly DebugWriter debugger;
        CellularAutomata cellularAutomata;
        readonly WriteableBitmap writeableBitmap;
        int iterations = 4;
        readonly int size = 128;

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
            iterations = Convert.ToInt32(Input_Iterations.Value);
            DrawRoom(GetRuleArray(StayInput), GetRuleArray(SpawnInput));
            
        }

        private void DrawRoom(short[] stay, short[] spawn)
        {
            debugger.Write(StayInput.Text);
            debugger.Write(ArrayToString(stay));
            debugger.Write(SpawnInput.Text);
            debugger.Write(ArrayToString(spawn));
            cellularAutomata = new CellularAutomata((uint)size, (uint)size, stay, spawn);

            cellularAutomata.FillRectangle(new Point(0, 0), new Point(size, size), CellularAutomata.Cell.States.Permaalive);
            cellularAutomata.FillRandomized(new Point(20, 20), new Point(size - 20, size - 20));
            cellularAutomata.FillRectangle(new Point(30, 30), new Point(size - 30, size - 30), CellularAutomata.Cell.States.Permadead);

            /*for (int i = 0; i < iterations; i++)
            {
                cellularAutomata.Iterate();
            }*/

            //cellularAutomata.Finish();

            cellularAutomata.Draw(writeableBitmap);
        }

        short[] GetRuleArray(TextBox textBox)
        {
            string temp = textBox.Text;
            temp = temp.Trim();
            string[] tarr = temp.Split(' ');
            short[] resarr = new short[tarr.Length];
            for(int i = 0; i < resarr.Length; i++)
            {
                try
                {
                    resarr[i] = Convert.ToInt16(tarr[i]);
                }
                catch { debugger.Write("Error during appending. Skipping"); }
            }
            return resarr;
        }

        string ArrayToString(short[] shorts) {
            string temp = string.Empty;
            temp += "[";
            foreach (short item in shorts)
            {
                temp += item.ToString();
                if(item != shorts[shorts.Length - 1]) {  temp += ", "; }
            }
            temp += "]";
            return temp;
        }
    }
}

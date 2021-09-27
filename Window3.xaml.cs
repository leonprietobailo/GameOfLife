using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Lógica de interacción para Window4.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        bool[] Inextstatus = new bool[9];
        bool[] Hnextstatus = new bool[9];
        bool addedVirus = false;
        string VirusName;
     
        public Window3()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Inextstatus[0] = I0.IsChecked.Value;
            Inextstatus[1] = I1.IsChecked.Value;
            Inextstatus[2] = I2.IsChecked.Value;
            Inextstatus[3] = I3.IsChecked.Value;
            Inextstatus[4] = I4.IsChecked.Value;
            Inextstatus[5] = I5.IsChecked.Value;
            Inextstatus[6] = I6.IsChecked.Value;
            Inextstatus[7] = I7.IsChecked.Value;
            Inextstatus[8] = I8.IsChecked.Value;

            Hnextstatus[0] = H0.IsChecked.Value;
            Hnextstatus[1] = H1.IsChecked.Value;
            Hnextstatus[2] = H2.IsChecked.Value;
            Hnextstatus[3] = H3.IsChecked.Value;
            Hnextstatus[4] = H4.IsChecked.Value;
            Hnextstatus[5] = H5.IsChecked.Value;
            Hnextstatus[6] = H6.IsChecked.Value;
            Hnextstatus[7] = H7.IsChecked.Value;
            Hnextstatus[8] = H8.IsChecked.Value;

            addedVirus = true;
            VirusName = virusname.Text;

            Close();
        }

        public bool[] getINextStatus()
        {
            return this.Inextstatus;
        }

        public bool[] getHNextStatus()
        {
            return this.Hnextstatus;
        }

        public bool addedvirus()
        {
            return addedVirus;
        }

        public string getvirusname()
        {
            return VirusName;
        }
    }
}

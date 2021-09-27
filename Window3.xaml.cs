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
        bool modifiedVirus = false;
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
        public bool modifiedvirus()
        {
            return modifiedVirus;
        }
        public string getvirusname()
        {
            return VirusName;
        }
        public void setvirusname(string name)
        {
            VirusName=name;
            virusname.Text = VirusName;
        }
        public void setNextStatus(bool[] INS, bool[]HNS)
        {
            Inextstatus=INS;
            Hnextstatus=HNS;

            I0.IsChecked = Inextstatus[0];
            I1.IsChecked = Inextstatus[1];
            I2.IsChecked = Inextstatus[2];
            I3.IsChecked = Inextstatus[3];
            I4.IsChecked = Inextstatus[4];
            I5.IsChecked = Inextstatus[5];
            I6.IsChecked = Inextstatus[6];
            I7.IsChecked = Inextstatus[7];
            I8.IsChecked = Inextstatus[8];

            I0B.IsChecked = !Inextstatus[0];
            I1B.IsChecked = !Inextstatus[1];
            I2B.IsChecked = !Inextstatus[2];
            I3B.IsChecked = !Inextstatus[3];
            I4B.IsChecked = !Inextstatus[4];
            I5B.IsChecked = !Inextstatus[5];
            I6B.IsChecked = !Inextstatus[6];
            I7B.IsChecked = !Inextstatus[7];
            I8B.IsChecked = !Inextstatus[8];

            H0.IsChecked = Hnextstatus[0];
            H1.IsChecked = Hnextstatus[1];
            H2.IsChecked = Hnextstatus[2];
            H3.IsChecked = Hnextstatus[3];
            H4.IsChecked = Hnextstatus[4];
            H5.IsChecked = Hnextstatus[5];
            H6.IsChecked = Hnextstatus[6];
            H7.IsChecked = Hnextstatus[7];
            H8.IsChecked = Hnextstatus[8];

            H0B.IsChecked = !Hnextstatus[0];
            H1B.IsChecked = !Hnextstatus[1];
            H2B.IsChecked = !Hnextstatus[2];
            H3B.IsChecked = !Hnextstatus[3];
            H4B.IsChecked = !Hnextstatus[4];
            H5B.IsChecked = !Hnextstatus[5];
            H6B.IsChecked = !Hnextstatus[6];
            H7B.IsChecked = !Hnextstatus[7];
            H8B.IsChecked = !Hnextstatus[8];

            modifiedVirus = true;
        }
    }
}

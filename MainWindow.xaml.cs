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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;


namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int nRows, nColumns;
        Rectangle[,] rectangles;
        Stack<Grid> history = new Stack<Grid>();
        Grid mesh;
        DispatcherTimer timer = new DispatcherTimer();
        Boolean timerStatus = false;
        long ticks;

        public MainWindow()
        {
            InitializeComponent();
            comboBox1.Items.Add("Conway");
            comboBox1.Items.Add("Covid-19");
            comboBox2.Items.Add("Dead boundaries");
            comboBox2.Items.Add("Reflective boundaries");
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(Convert.ToInt64(1/100e-9));
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            nRows = Convert.ToInt32(textBox1.Text);
            nColumns = Convert.ToInt32(textBox2.Text);
            mesh = new Grid(nRows, nColumns);
            rectangles = new Rectangle[nRows, nColumns];
            for(int i = 0; i < nRows; i++){
                for (int j = 0; j < nColumns; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = canvas1.Width / nColumns;
                    r.Height = canvas1.Height / nRows;
                    r.Fill = new SolidColorBrush(Colors.White);
                    r.StrokeThickness = 1;
                    r.Stroke = Brushes.Black;
                    canvas1.Children.Add(r);

                    Canvas.SetTop(r, i * r.Height);
                    Canvas.SetLeft(r, j * r.Width);

                    r.Tag = new Point(i, j);
                    r.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);
                    rectangles[i, j] = r;
                }
            }
            history.Push(mesh.deepCopy());
            comboBox1.SelectedIndex = 0;
            comboBox1.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;

            comboBox2.SelectedIndex = 0;
            comboBox2.Visibility = Visibility.Visible;
            label8.Visibility = Visibility.Visible;

            image1.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Visible;
        }
        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle reg = (Rectangle)sender; // Obtenemos el rectangulo sobre el que se clicó
            Point p = (Point)reg.Tag; // recuperamos el tag, que contiene las coordenadas de la casilla clicada
            //casilla.Content = "(" + p.X + "," + p.Y + ")"; // Informamos al usuario de las coordenadas
            //reg.Fill = new SolidColorBrush(Colors.Black); // Ponemos en negro la casilla
            mesh.changeCellStatus(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
            lastSelectedCellBox.Text = "(" + Convert.ToString(p.X) + "," + Convert.ToString(p.Y) + ")";
            updateMesh();
        }

        // Reflejar visualmente los cambios.
        private void updateMesh()
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    if (mesh.getCellStatus(i, j))
                    {
                        rectangles[i, j].Fill = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        rectangles[i, j].Fill = new SolidColorBrush(Colors.White);
                    }
                }
            }
            populationTextBox.Text = Convert.ToString(mesh.countInfected());
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!timerStatus)
            {
                timer.Start();
                timerStatus = true;
            }
            else
            {
                timer.Stop();
                timerStatus = false;
            }
        }

        private void nextIteration_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            history.Push(mesh.deepCopy());
            mesh.iterate();
            updateMesh();
        }

        private void previousIteration_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            mesh = history.Pop();
            updateMesh();
            
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            mesh.reset();
            history.Clear();
            updateMesh();
            
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            history.Push(mesh.deepCopy());
            mesh.iterate();
            updateMesh();
        }

        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double time = -3.0 / 400.0 * (slider.Value*10.0 - 400.0 / 3.0); //[s]
            ticks = Convert.ToInt64(time / 100e-9);
            timer.Interval = new TimeSpan(ticks);
        }

        private void saveSimulation_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            mesh.saveGrid();
        }

        private void loadSimualtion_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            mesh = new Grid(1, 1);
            mesh.loadGrid();
            int[] size = new int[2];
            size = mesh.getSize();
            textBox1.Text = Convert.ToString(size[0]);
            textBox2.Text = Convert.ToString(size[1]);
            nRows = size[0];
            nColumns = size[1];
            rectangles = new Rectangle[nRows, nColumns];
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = canvas1.Width / nColumns;
                    r.Height = canvas1.Height / nRows;
                    r.StrokeThickness = 1;
                    r.Stroke = Brushes.Black;
                    canvas1.Children.Add(r);

                    Canvas.SetTop(r, i * r.Height);
                    Canvas.SetLeft(r, j * r.Width);

                    r.Tag = new Point(i, j);
                    r.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);
                    rectangles[i, j] = r;
                }
            }
            history.Push(mesh.deepCopy());
            updateMesh();

            comboBox1.SelectedIndex = 0;
            comboBox1.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;

            comboBox2.SelectedIndex = 0;
            comboBox2.Visibility = Visibility.Visible;
            label8.Visibility = Visibility.Visible;

            image1.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Visible;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mesh.setStrategy(comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mesh.setBoundaries(comboBox2.SelectedIndex);
        }

        private void image1_Click(object sender, MouseButtonEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }
        private void image2_Click(object sender, MouseButtonEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Show();
        }
    }
}

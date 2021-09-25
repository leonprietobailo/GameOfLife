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

    public partial class MainWindow : Window
    {

        int nRows, nColumns;
        Rectangle[,] rectangles;
        Stack<Grid> history = new Stack<Grid>();
        Grid mesh, copy1;
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
            timer.Interval = new TimeSpan(Convert.ToInt64(1 / 100e-9));
            mesh = new Grid(0, 0);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text) > 1 && Convert.ToInt32(textBox1.Text) > 1)
                {
                    nRows = Convert.ToInt32(textBox1.Text);
                    nColumns = Convert.ToInt32(textBox2.Text);
                    mesh = new Grid(nRows, nColumns);
                    rectangles = new Rectangle[nRows, nColumns];
                    canvas1.Children.Clear();
                    for (int i = 0; i < nRows; i++)
                    {
                        for (int j = 0; j < nColumns; j++)
                        {
                            Rectangle r = new Rectangle();
                            r.Width = canvas1.Width / nColumns;
                            r.Height = canvas1.Height / nRows;
                            r.Fill = new SolidColorBrush(Colors.Transparent);
                            r.StrokeThickness = 1;
                            r.Stroke = Brushes.White;
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
                    comboBox2.SelectedIndex = 0;
                    mesh.setRules(comboBox1.SelectedIndex);
                    showElements();

                    if (mesh.getSize()[0] == 0 || mesh.getSize()[1] == 0)
                    {
                        label5.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    label5.Visibility = Visibility.Visible;
                }
            }
            catch (FormatException)
            {
                label5.Visibility = Visibility.Visible;
            }
            catch (OverflowException)
            {
                label5.Visibility = Visibility.Visible;
            }
        }

        private void showElements()
        {
            if (mesh.getSize()[0] != 0 && mesh.getSize()[1] != 0)
            {
                comboBox1.Visibility = Visibility.Visible;
                textStatus.Visibility = Visibility.Visible;
                label4.Visibility = Visibility.Visible;
                comboBox2.Visibility = Visibility.Visible;
                label8.Visibility = Visibility.Visible;
                image1.Visibility = Visibility.Visible;
                image2.Visibility = Visibility.Visible;
                populationLabel.Visibility = Visibility.Visible;
                populationTextBox.Visibility = Visibility.Visible;
                lastClickedCellLabel.Visibility = Visibility.Visible;
                lastSelectedCellBox.Visibility = Visibility.Visible;
                speedLabel.Visibility = Visibility.Visible;
                speedSlider.Visibility = Visibility.Visible;
                buttonStart.Visibility = Visibility.Visible;
                buttonStart.Background = Brushes.SpringGreen;
                buttonStart.BorderBrush = Brushes.White;
                buttonStart.Foreground = Brushes.White;
                label5.Visibility = Visibility.Hidden;
                textBox1.Text = Convert.ToString(mesh.getSize()[0]);
                textBox2.Text = Convert.ToString(mesh.getSize()[1]);
                restart.Visibility = Visibility.Visible;
                saveSimulation.Visibility = Visibility.Visible;
                previousIteration.Visibility = Visibility.Visible;
                nextIteration.Visibility = Visibility.Visible;
            }
        }
        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle reg = (Rectangle)sender;
            Point p = (Point)reg.Tag;
            p.X++;
            p.Y++;
            mesh.changeCellStatus(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
            lastSelectedCellBox.Text = "(" + Convert.ToString(p.Y) + "," + Convert.ToString(p.X) + ")";
            updateMesh();
        }

        // Reflejar visualmente los cambios.
        private void updateMesh()
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    if (mesh.getCellStatus(i+1, j+1))
                    {
                        if (mesh.getRules() == 0)
                        {
                            rectangles[i, j].Fill = new SolidColorBrush(Colors.Aqua);
                        }
                        else
                        {
                            rectangles[i, j].Fill = new SolidColorBrush(Colors.Red);
                        }
                    }
                    else
                    {
                        rectangles[i, j].Fill = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
            populationTextBox.Text = Convert.ToString(mesh.countInfected());

            if (mesh.isLastIteration())
            {
                timer.Stop();
                buttonStart.Content = "Start";
                buttonStart.Background = Brushes.SpringGreen;
                buttonStart.BorderBrush = Brushes.White;
                buttonStart.Foreground = Brushes.White;
                textStatus.Text = "Status: Stable";
                textStatus.Foreground = new SolidColorBrush(Colors.Green);
                timerStatus = false;
            }
            else
            {
                textStatus.Text = "Status: Unstable";
                textStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!timerStatus)
            {
                if (!mesh.isLastIteration())
                {
                    buttonStart.Content = "Stop";
                    buttonStart.Background = Brushes.Red;
                    buttonStart.BorderBrush = Brushes.White;
                    buttonStart.Foreground = Brushes.White;
                    

                    timer.Start();
                    timerStatus = true;
                }
            }
            else
            {
                buttonStart.Content = "Start";
                buttonStart.Background = Brushes.SpringGreen;
                buttonStart.BorderBrush = Brushes.White;
                buttonStart.Foreground = Brushes.White;
                timer.Stop();
                timerStatus = false;


            }
        }

        private void nextIteration_Click(object sender, RoutedEventArgs e)
        {
            if (!mesh.isLastIteration())
            {
                history.Push(mesh.deepCopy());
                mesh.iterate();
                mesh.setBoundaries(comboBox2.SelectedIndex);
                updateMesh();
            }
        }



        private void previousIteration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mesh = history.Pop();
                updateMesh();
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            history.Clear();
            timer.Stop();
            mesh.reset();
            history.Push(mesh.deepCopy());
            updateMesh();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            history.Push(mesh.deepCopy());
            mesh.iterate();
            mesh.setBoundaries(comboBox2.SelectedIndex);
            updateMesh();
        }

        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double time = -3.0 / 400.0 * (slider.Value * 10.0 - 400.0 / 3.0); //[s]
            ticks = Convert.ToInt64(time / 100e-9);
            timer.Interval = new TimeSpan(ticks);
        }

        private void saveSimulation_Click(object sender, RoutedEventArgs e)
        {
            mesh.saveGrid();
        }

        private void loadSimualtion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                copy1 = mesh.deepCopy();
                timer.Stop();
                mesh.reset();
                mesh.loadGrid();
                
                if (mesh.isClean() || mesh == null)
                {
                    mesh = copy1.deepCopy();
                }

                int[] size = new int[2];
                size = mesh.getSize();

                nRows = size[0];
                nColumns = size[1];
                rectangles = new Rectangle[nRows, nColumns];
                canvas1.Children.Clear();
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nColumns; j++)
                    {
                        Rectangle r = new Rectangle();
                        r.Width = canvas1.Width / nColumns;
                        r.Height = canvas1.Height / nRows;
                        r.StrokeThickness = 1;
                        r.Stroke = Brushes.White;
                        canvas1.Children.Add(r);

                        Canvas.SetTop(r, i * r.Height);
                        Canvas.SetLeft(r, j * r.Width);

                        r.Tag = new Point(i, j);
                        r.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);
                        rectangles[i, j] = r;
                    }
                }
                history.Clear();
                history.Push(mesh.deepCopy());
                updateMesh();
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                mesh.setRules(comboBox1.SelectedIndex);
                showElements();
            }
            catch (FileFormatException)
            {
                label5.Visibility = Visibility.Visible;
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mesh.setRules(comboBox1.SelectedIndex);
            updateMesh();
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mesh.setBoundaries(comboBox2.SelectedIndex);
        }

        private void image1_Click(object sender, MouseButtonEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.ShowDialog();
        }

        private void image2_Click(object sender, MouseButtonEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.ShowDialog();
        }
    }
}
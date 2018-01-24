using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Coords = GeometryStuff.Coords;
using Vector = GeometryStuff.Vector;
using Rectangle = GeometryStuff.Rectangle;

namespace PhoneFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string[] Labels = { "Gauč", "U školníka", "Biologie", "NoName", "V kapse", "Taška", "U ucha", "Doma", "Někde", "Má ho Anežka", "Na klavíru" };
        private int PlacesCount;
        /*readonly List<Tuple<string,List<int>>> Places = new List<Tuple<string, List<int>>>()
        {
            new Tuple<string, List<int>>("Gauč",new List<int>()
            {

            })
        }

        private enum HintIDs { Floor_3_yes, Floor_3_no, }

        readonly Dictionary<HintIDs, Tuple<string, string, bool>> Hints = new Dictionary<HintIDs, Tuple<string, string, bool>>()
        {
            { HintIDs.Floor_3_yes, new Tuple<string, string, bool>(, "Myslím, že jsem ho viděl ve druhém patře", true) },
            { HintIDs.Floor_3_no, new Tuple<string, string, bool>("Profesor") }
        }

        readonly List<string> People = new List<string>()
        {
            HintTexts.People.Person1,
            HintTexts.People.Person2,
            HintTexts.People.Person3,
        };*/

        public List<Rectangle> DeniedRegions { get; set; }
        public List<Rectangle> ButtonRegions { get; set; }
        
        private readonly Vector ButtonDimensions = new Vector(120, 50);
        private readonly Vector ButtonMargin = new Vector(20, 10);
        bool displayDeniedRegions = false;
        BackgroundWorker GenerateButtons = new BackgroundWorker();
        private readonly Random Random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            DeniedRegions = new List<Rectangle>();
            DeniedRegions.Add(new Rectangle(new Coords(100, 50), new Vector(460, 150)));
            DeniedRegions.Add(new Rectangle(new Coords(Root.Width, 0), new Vector(-65, 30),Rectangle.Corners.TopRight));
            GenerateButtons.DoWork += new DoWorkEventHandler(GenerateButtons_DoWork);
            GenerateButtons.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GenerateButtons_RunWorkerCompleted);
            GenerateButtons.WorkerReportsProgress = false;
            GenerateButtons.WorkerSupportsCancellation = false;
            GenerateButtons.RunWorkerAsync();
        }

        private void GenerateButtons_DoWork(object sender, DoWorkEventArgs e)
        {
            ButtonRegions = new List<Rectangle>();
            ButtonRegions.AddRange(DeniedRegions);
            Vector rootDimensions = Vector.Null;
            int placesCount = 0;
            Timer fadeTimer = new Timer();
            fadeTimer.Interval = 1000;
            fadeTimer.Elapsed += FadeInTimer_Elapsed;
            Dispatcher.Invoke(new Action(() =>
            {
                rootDimensions = new Vector(ActualWidth, ActualHeight);
                DoubleAnimation fade = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(500)));
                Root.BeginAnimation(OpacityProperty, fade);
                fadeTimer.Start();
                MainGrid.Children.Clear();
            }));
            Debug.WriteLine("Window dimension vector is {0}.", rootDimensions);
            int locationIndex = Random.Next(0, Labels.Length - 1);
            
            foreach (Rectangle old in DeniedRegions)
            {
                if (displayDeniedRegions)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Border border = old.GenerateBorder(rootDimensions);
                        border.BorderBrush = new SolidColorBrush(Colors.Red);
                        border.BorderThickness = new Thickness(3);
                        border.Background = new SolidColorBrush(new Color() { A = 127, R = Colors.Red.R, G = Colors.Red.G, B = Colors.Red.B });
                        border.SetValue(Panel.ZIndexProperty, 0);
                        border.Visibility = Visibility.Hidden;
                        MainGrid.Children.Add(border);
                    }));
                }
            }

            foreach (string label in Labels)
            {
                int active = Random.Next(-2, 9);

                if (active > 0 || label == Labels[locationIndex])
                {
                    placesCount++;
                    Debug.WriteLine("Generating button " + label);
                    ButtonCoords newCoords;
                    newCoords = new ButtonCoords(Random, label, rootDimensions.X - (ButtonDimensions.X + 2 * ButtonMargin.X), rootDimensions.Y - (ButtonDimensions.Y + 2 * ButtonMargin.Y), ButtonRegions, ButtonMargin, ButtonDimensions, rootDimensions);
                    ButtonRegions.Add(newCoords);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        Button button = new Button()
                        {
                            Content = new Viewbox() { Child = new TextBlock(new Run(label)) },
                            Margin = newCoords,
                            Tag = label == Labels[locationIndex],
                            Visibility = Visibility.Hidden,
                            Template = (ControlTemplate)Resources["GuessButtonStyle"],
                        };
                        button.SetValue(Panel.ZIndexProperty, 10);
                        button.Click += FindButton_Click;
                        MainGrid.Children.Add(button);
                        if (displayDeniedRegions)
                        {
                            Border inner = newCoords.Rectangle.GenerateBorder(rootDimensions);
                            inner.BorderBrush = new SolidColorBrush(Colors.Black);
                            inner.BorderThickness = new Thickness(3);
                            inner.Background = new SolidColorBrush(new Color() { A = 127, R = Colors.Black.R, G = Colors.Black.G, B = Colors.Black.B });
                            inner.SetValue(Panel.ZIndexProperty, 5);
                            inner.Visibility = Visibility.Hidden;
                            MainGrid.Children.Add(inner);

                            Border outer = newCoords.MarginRectangle.GenerateBorder(rootDimensions);
                            outer.BorderBrush = new SolidColorBrush(Colors.Green);
                            outer.BorderThickness = new Thickness(3);
                            outer.Background = new SolidColorBrush(new Color() { A = 127, R = Colors.Green.R, G = Colors.Green.G, B = Colors.Green.B });
                            outer.SetValue(Panel.ZIndexProperty, 0);
                            outer.Visibility = Visibility.Hidden;
                            MainGrid.Children.Add(outer);
                        }
                        Debug.WriteLine(string.Format("Adding button {0}.", newCoords));
                    }));
                }
                else
                {
                    Debug.WriteLine(string.Format("Button {0} skipped", label));
                }
                PlacesCount = placesCount;
            }
        }

        private void FadeInTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Stop();
            Dispatcher.Invoke(new Action(() =>
            {
                while (GenerateButtons.IsBusy) ;
                foreach (UIElement element in MainGrid.Children)
                {
                    element.Visibility = Visibility.Visible;
                }
                DoubleAnimation fade = new DoubleAnimation(1, new Duration(TimeSpan.FromMilliseconds(500)));
                Root.BeginAnimation(OpacityProperty, fade);
            }));
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)((Button)sender).Tag)
            {
                GenerateButtons.RunWorkerAsync();
                int tries = PlacesCount - MainGrid.Children.Count;
                MessageBox.Show(string.Format("You found the phone! Congrats! It took you {0} tries.", tries + 1), "Found it", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MainGrid.Children.Remove((Button)sender);
            }
        }

        private void GenerateButtons_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) throw e.Error;
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Border)
                {
                    Debug.WriteLine(string.Format("{0} with margins {1} and dimensions {2}w {3}h", element.ToString(), ((Border)element).Margin, ((Border)element).ActualWidth, ((Border)element).ActualHeight));
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (!GenerateButtons.IsBusy) GenerateButtons.RunWorkerAsync();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ContentControl_MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }

    internal class ButtonCoords : Coords
    {
        public string ButtonName { get; set; }
        public Rectangle Rectangle { get; private set; }
        public Rectangle MarginRectangle { get; private set; }
        public Vector ButtonDimensions { get; set; }
        public Vector ParentDimensions { get; private set; }

        public ButtonCoords(Random r, string buttonName, double maxX, double maxY, List<Rectangle> deniedRegions, Vector marginDimensions, Vector buttonDimensions, Vector parentDimensions) : base(maxX, maxY)
        {
            bool invalid = true;
            X = 0;
            Y = 0;
            ButtonName = buttonName;
            ButtonDimensions = buttonDimensions;
            ParentDimensions = parentDimensions;
            while (invalid)
            {
                X = r.Next((int)marginDimensions.X, (int)maxX);
                Y = r.Next((int)marginDimensions.Y, (int)maxY);
                invalid = false;
                Rectangle = new Rectangle(this, buttonDimensions);

                foreach (Rectangle rect in deniedRegions)
                {
                    if (rect.RectanglesOverlap(Rectangle))
                    {
                        invalid = true;
                    }
                }
            }
            MarginRectangle = new Rectangle(Rectangle.TopLeft, Rectangle.BottomRight);
            MarginRectangle.ResizeRectangle(Rectangle.Corners.TopLeft, marginDimensions.GetOppositeVector());
            MarginRectangle.ResizeRectangle(Rectangle.Corners.BottomRight, marginDimensions);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1};{2}]", ButtonName, X, Y);
        }

        public static implicit operator Rectangle(ButtonCoords buttonCoords)
        {
            return buttonCoords.MarginRectangle;
        }

        public static implicit operator Thickness(ButtonCoords buttonCoords)
        {
            return new Thickness(buttonCoords.X, buttonCoords.Y, buttonCoords.ParentDimensions.X - (buttonCoords.X + buttonCoords.ButtonDimensions.X), buttonCoords.ParentDimensions.Y - (buttonCoords.Y + buttonCoords.ButtonDimensions.Y));
        }
    }
}

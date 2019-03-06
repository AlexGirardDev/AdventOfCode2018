using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<List<bool>> _gridState = new List<List<bool>>();
        List<List<bool>> _newGridState = new List<List<bool>>();
        int[,] completedGrid = new int[750,750];


        private int index = 0;
        public void DoStuff()
        {


            foreach (var point in points)
            {
                //var point = new point { Id = 0, dead = false, X = 25, Y = 25 };
                if (point.dead) continue;

                if (index > 0)
                    point.dead = true;
                //    if (point.Id == 3 && i > 4)
                //{

                //}

                //X+ Y+
                for (int z = 0; z < index + 1; z++)
                {
                    int x = index - z;
                    int y = index - x;
                    DoStuff(lol, x + point.X, y + point.Y, point, index);

                }

                //X+ Y-
                for (int z = 0; z < index + 1; z++)
                {
                    int x = index - z;
                    int y = index - x;
                    DoStuff(lol, x + point.X, point.Y - y, point, index);

                }

                ////X- Y+
                for (int z = 0; z < index + 1; z++)
                {
                    int x = index - z;
                    int y = index - x;
                    DoStuff(lol, point.X - x, point.Y + y, point, index);

                }

                ////X- Y+
                for (int z = 0; z < index + 1; z++)
                {
                    int x = index - z;
                    int y = index - x;
                    DoStuff(lol, point.X - x, point.Y - y, point, index);

                }
            }

            index++;

        }

        private  void DoStuff(List<List<Dictionary<int, int>>> lol, int x, int y, point point, int i)
        {


            //if(!lol[x][y].ContainsKey(1)) lol[x][y].Add(1,1);
            // return;
            try
            {


                if (lol[x][y].ContainsKey(point.Id) || lol[x][y].Any(r => r.Value < i) ||
                    lol[x][y].Any(r => r.Value == -1)) return;
            }
            catch (Exception ex)
            {
                ButtonStartStop_OnClick(null, null);
                return;
            }


        if (lol[x][y].Any(r => r.Value > i))
            {
                lol[x][y].Clear();
            }

            var asd = lol[x][y].Where(r => r.Value != point.Id).ToList();
            if (asd.Any(v => v.Value == i) && asd.Any())
            {
                var tempDict = asd.ToDictionary(p => p.Key, p => p.Value).Keys;
                foreach (var key in tempDict)
                {
                    lol[x][y][key] = -1;
                    if (!lol[x][y].ContainsKey(point.Id))
                        lol[x][y].Add(point.Id, -1);
                    //point.dead = false;
                }
            }
            else
            {
                lol[x][y].Add(point.Id, i);
                point.dead = false;
                //if (!HadIncrements.Contains(point.Id)) HadIncrements.Add(point.Id);
            }

            if (lol[x][y].Any(z => z.Value == -1))
            {
                var keys = lol[x][y].Keys.Select(h => h).ToList();

                foreach (var key in keys)
                {
                    lol[x][y][key] = -1;
                }
            }

            if (lol[x][y].GroupBy(r => r.Value).Any(r => r.Count() > 1))
            {
                var keys = lol[x][y].Keys.Select(h => h).ToList();

                foreach (var key in keys)
                {
                    lol[x][y][key] = -1;
                }
            }
            
        }

        #region super secret code

        private int _squareSize = 3;
        private int _gridWidth = 500;
        private int _gridHeight = 500;
        private int gridSize = 500;

        List<List<Rectangle>> _rects = new List<List<Rectangle>>();
        public class point
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool dead { get; set; }
            public string color { get; set; }
        }
        List<List<Dictionary<int, int>>> lol = new List<List<Dictionary<int, int>>>();
        List<point> points = new List<point>();
        public MainWindow()
        {
            InitializeComponent();
            CreateGrid();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimerOnTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            var puzzleInput = File.ReadAllLines("day6.txt");
            var colors = File.ReadAllLines("colors.txt");
          

            
            for (int i = 0; i < gridSize; i++)
            {

                lol.Add(new List<Dictionary<int, int>>());
                for (int j = 0; j < gridSize; j++)
                {
                    lol[i].Add(new Dictionary<int, int>());
                }
            }

            int id = 0;
            foreach (var s in puzzleInput)
            {
                var split = s.Split(',');
                var point = new point
                {
                    X = int.Parse(split.First().Trim()) + 50,
                    Y = int.Parse(split.Last().Trim()),
                    Id = id,
                    dead = false,
                    color = colors[id]
                };
                points.Add(point);
                id++;
            }
        }

        public void CreateGrid()
        {
            try
            {
                //_gridWidth = int.Parse(TextBoxWidth.Text); //.Interval = new TimeSpan(0, 0, 0, 0, int.Parse(TextBoxSpeed.Text));
                //_gridHeight = int.Parse(TextBoxHeight.Text);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(this, "Use a number you bitch");
            }


            Grid.Children.Clear();
            _gridState.Clear();
            _rects.Clear();
            for (int i = 0; i < _gridWidth; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(_squareSize)});
            }
            for (int i = 0; i < _gridHeight; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(_squareSize)});
            }
            var rnd = new Random();
            for (int i = 0; i < _gridWidth; i++)
            {

                var tempList = new List<Boolean>();
                var tempRectList = new List<Rectangle>();
                for (int j = 0; j < _gridHeight; j++)
                {

                    Rectangle rect = new Rectangle();
                    var tempBool = false; //rnd.Next(10) > 4;
                    tempList.Add(tempBool);
                    rect.Fill =  onBrush ;
                    rect.SetValue(Grid.ColumnProperty, i);
                    rect.SetValue(Grid.RowProperty, j);
                    rect.Margin = new Thickness(0);
                    rect.Tag = $"{i}/{j}";
                    rect.MouseDown += Rect_MouseUp;
                    rect.MouseEnter += RectOnMouseEnter;
                    tempRectList.Add(rect);
                    Grid.Children.Add(rect);
                }
                _gridState.Add(tempList);
                _rects.Add(tempRectList);
            }
            _newGridState.Clear();
            _newGridState = _gridState.Select(x => x.ToList()).ToList();

        }

        private void RectOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            //if (App.isClicking)
            //{
            //    var getXYReturn = GetXYFromRect(sender as Rectangle);
            //    int x = getXYReturn.Item1;
            //    int y = getXYReturn.Item2;

            //    _gridState[x][y] = !_gridState[x][y];
            //    SetSquareColour(x, y, _gridState[x][y]);
            //}
        }

        private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            _newGridState.Clear();
            _newGridState = _gridState.Select(x => x.ToList()).ToList();
            DoStuff();
            UpdateGrid(_gridState);
            _gridState.Clear();
            _gridState = _newGridState.Select(n => n.ToList()).ToList();
        }

        public void UpdateGrid(List<List<bool>> list)
        {
            for (var x = 0; x < list.Count; x++)
            {
                var gridList = list[x];
                for (int y = 0; y < gridList.Count; y++)
                {
                    SetSquareColour(x, y, list[x][y]);
                }
            }
        }

        private void Rect_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var getXYReturn = GetXYFromRect(sender as Rectangle);
            int x = getXYReturn.Item1;
            int y = getXYReturn.Item2;

            _gridState[x][y] = !_gridState[x][y];
            SetSquareColour(x, y, _gridState[x][y]);


        }

        private Tuple<int, int> GetXYFromRect(Rectangle rect)
        {
            var stringTag = rect.Tag.ToString().Split('/');
            int x = int.Parse(stringTag[0]);
            int y = int.Parse(stringTag[1]);

            return new Tuple<int, int>(x, y);
        }

        private Brush onBrush = Brushes.White;
        private Brush offBrush = Brushes.Black;
        private void SetSquareColour(int x, int y, bool value)
        {
            if (completedGrid[x,y]>0)
                return;

            if (lol[x][y].Any())
            {
                completedGrid[x, y]++;
                if (lol[x][y].FirstOrDefault().Value == -1)
                {
                    _rects[x][y].Fill = Brushes.Black;
                }

                else
                {
                    _rects[x][y].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(points[lol[x][y].FirstOrDefault().Key].color)); ;
                }
               
            }

        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CreateGrid();
        }

        private void ButtonStepBase_OnClick(object sender, RoutedEventArgs e)
        {
            DoStuff();
            UpdateGrid(_newGridState);

        }

        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private void ButtonStartStop_OnClick(object sender, RoutedEventArgs e)
        {

            if (dispatcherTimer.IsEnabled)
            {
                try
                {
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, int.Parse(TextBoxSpeed.Text));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(this, "Use a number you bitch");
                }

                dispatcherTimer.Stop();
                ButtonStep.IsEnabled = true;
                ButtonStartStop.Content = "Start";

            }
            else
            {
                dispatcherTimer.Start();
                ButtonStep.IsEnabled = false;
                ButtonStartStop.Content = "Stop";
            }


        }

        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRandomize_OnClick(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();

            for (var i = 0; i < _gridState.Count; i++)
            {
                List<bool> bools = _gridState[i];
                for (var index = 0; index < bools.Count; index++)
                {
                    bools[index] = rnd.Next(10) < 5;
                }
            }
            UpdateGrid(_gridState);
        }




        #region dont open this

        #region stop

        #region stop

        #region stop

        #region stop

        #region stop

       


        #endregion


        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseUpEvent, new MouseButtonEventHandler(OnPreviewMouseUp));
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));
        }

        static void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isClicking = false;
        }

        static void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isClicking = true;
        }

        public static bool isClicking;
    }
    
}

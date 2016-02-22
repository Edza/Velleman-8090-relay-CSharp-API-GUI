using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Velleman8090;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private V8090 _relay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            this._relay = V8090.Instance;
        }

        private void Get(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(this.Index.Text.ToString());
            this.Result.Text = this._relay.Get(index).ToString();
        }

        private void On(object sender, RoutedEventArgs e)
        {
            this.Result.Text = string.Empty;
            int index = int.Parse(this.Index.Text.ToString());
            this._relay.On(index);
        }

        private void Off(object sender, RoutedEventArgs e)
        {
            this.Result.Text = string.Empty;
            int index = int.Parse(this.Index.Text.ToString());
            this._relay.Off(index);
        }

        private void Toggle(object sender, RoutedEventArgs e)
        {
            this.Result.Text = string.Empty;
            int index = int.Parse(this.Index.Text.ToString());
            this._relay.Toggle(index);
        }
    }
}

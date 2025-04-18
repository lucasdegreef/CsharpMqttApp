using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading;
using MQTTnet;
using Microsoft.UI;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FirstApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private readonly MqttService _mqttService = new MqttService();
        public MainWindow()
        {
            this.InitializeComponent();
            // Initialiseer de timer (maar start hem nog niet)
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); // Na 2 seconden terugzetten
            _timer.Tick += (s, e) =>
            {
                Knop_1.Content = "Verzend MQTT data"; // Zet de oorspronkelijke tekst terug
                _timer.Stop(); // Stop de timer
            };

            

        }
        

        private async void OnClick1(object sender, RoutedEventArgs e)
        {
            Knop_1.Content = "Start connectie";
            try
            {
                StatusText1.Text = "Wachten op verbinding...";

                await _mqttService.ConnectAsync("test.mosquitto.org", 1883);

                Debug.WriteLine("Knop geklikt");

                StatusText1.Text = "Geen fouten";

                _timer.Start();
            }
            catch (Exception ex) {
                // Foutstatus
                StatusText1.Text = $"Fout: {ex.Message}";
                StatusText1.Foreground = new SolidColorBrush(Colors.Red);
            }
            
        }

        private async void OnClick2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(MijnTextBox.Text))
                {
                    string data = MijnTextBox.Text;
                    await _mqttService.PublishAsync("Lucas/topic/App1", data);
                    StatusText2.Text = "Verzending gelukt";
                }
                else
                {
                    StatusText2.Text = "Dit veld moet ingevuld worden";
                }


            }
            catch(Exception ex)
            {
                StatusText2.Text = $"Fout: {ex.Message}";
            }

            Debug.WriteLine("Knop geklikt");

            _timer.Start();
        }

        private void TextBoxVeranderd(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MijnTextBox.Text))
            {
                StatusText2.Text = "je mag niet verzenden";
            }
            else {
                
                StatusText2.Text = "Je mag iets verzenden";
            }
            
        }

        


    }
}

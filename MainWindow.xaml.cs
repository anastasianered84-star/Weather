// Файл: MainWindow.xaml.cs (обновленная версия)
using System.Windows;
using Weather.Classes;
using Weather.Models;

namespace Weather
{
    public partial class MainWindow : Window
    {
        DataResponse response;

        public MainWindow()
        {
            InitializeComponent();
        }

        public async void Iint(string city = null)
        {
            try
            {
                Days.Items.Clear();

                if (string.IsNullOrEmpty(city))
                {
                    response = await GetWeather.GetByCoordinates(58.009671f, 56.226184f);
                }
                else
                {
                    response = await GetWeather.GetByCityName(city);
                }

                if (response?.forecasts != null)
                {
                    foreach (Forecast forecast in response.forecasts)
                        Days.Items.Add(forecast.date.ToString("dd.MM.yyyy"));

                    Create(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Create(int idForecast)
        {
            parent.Children.Clear();
            if (response?.forecasts != null && idForecast < response.forecasts.Count)
            {
                foreach (Hour hour in response.forecasts[idForecast].hours)
                {
                    parent.Children.Add(new Elements.Item(hour));
                }
            }
        }

        private void SelectDay(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Days.SelectedIndex >= 0)
                Create(Days.SelectedIndex);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text?.Trim();

            if (!string.IsNullOrEmpty(city))
            {
                Iint(city);
            }
            else
            {
                MessageBox.Show("Введите название города", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherUWP.Model;
using Windows.UI.Xaml.Controls;

namespace WeatherUWP.ViewModel
{
    class WeatherViewModel:ViewModelBase
    {
        private WeatherApiService _wApi;
        private RootObject _wCity;
        public WeatherViewModel()
        {
            _wApi = new WeatherApiService();
            
            this.WeatherSearch = new RelayCommand(FindWeather);
            IsErrorVisible = false;
        }
        public bool isRingShow { get; set; } = false;
        public bool IsErrorVisible { get; set; }
        public bool IsContentVisible { get; set; } = false;
        public string Date { get; set; }
        public string FoundCity { get; set; }
        public string CityName { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Cloudiness { get; set; }
        public string WindSpeed { get; set; }
        public string ImagePath { get; set; }
        public ICommand WeatherSearch { get; set; }

        private async void FindWeather()
        {
            isRingShow = true;
            RaisePropertyChanged("isRingShow");

            _wCity = await _wApi.GetInfoAsync(CityName);

            isRingShow = false;
            RaisePropertyChanged("isRingShow");

            IsErrorVisible = _wApi.IsError;
            RaisePropertyChanged("IsErrorVisible");

            IsContentVisible = false;
            if (IsErrorVisible==false)
            {
                IsContentVisible = true;
                UpdateView();
            }
            RaisePropertyChanged("IsContentVisible");
        }

        private void UpdateView()
        {
            Date = Day(_wCity.list.First().dt)+ " " + new DateTime(1970, 01, 01).AddSeconds(_wCity.list.First().dt).ToString("dd/MM/yy");
            RaisePropertyChanged("Date");

            Temperature = Math.Round(_wCity.list.First().temp.day,1).ToString()+" °C";
            RaisePropertyChanged("Temperature");
             
            Humidity = _wCity.list.First().humidity.ToString() + "%";
            RaisePropertyChanged("Humidity");

            WindSpeed = Math.Round(_wCity.list.First().speed,1).ToString()+ " м/c";
            RaisePropertyChanged("WindSpeed");

            Cloudiness = _wCity.list.First().clouds.ToString() + "%";
            RaisePropertyChanged("Cloudiness");

            FoundCity = "Знайдено місто: "+ _wCity.city.name + " ("+ _wCity.city.country + ")";
            RaisePropertyChanged("FoundCity");

            ImagePath = "ms-appx:///Images/" + _wCity.list.First().weather.First().icon + ".png";
            RaisePropertyChanged("ImagePath");
        }

        private string Day(int time)
        {
            var a = new DateTime(1970, 01, 01).AddSeconds(time).DayOfWeek;
            switch (a)
            {
                case DayOfWeek.Monday: return "Понеділок";
                case DayOfWeek.Tuesday: return "Вівторок";
                case DayOfWeek.Wednesday: return "Середа";
                case DayOfWeek.Thursday: return "Четверг";
                case DayOfWeek.Friday: return "Пятниця";
                case DayOfWeek.Saturday: return "Субота";
                case DayOfWeek.Sunday: return "Неділя";
                default:return "";
            }
        }
    }
}

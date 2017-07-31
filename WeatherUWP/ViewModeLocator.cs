using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherUWP.View;
using WeatherUWP.ViewModel;

namespace WeatherUWP
{
    class ViewModeLocator
    {
        public ViewModeLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = new NavigationService();
            navigationService.Configure(nameof(WeatherViewModel), typeof(WeatherView));
            SimpleIoc.Default.Unregister<INavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<WeatherViewModel>();
            
        }
        public WeatherViewModel WeatherVMInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WeatherViewModel>();
            }
        }
    }
}

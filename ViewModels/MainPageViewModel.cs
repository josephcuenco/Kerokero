using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;

        public MainPageViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

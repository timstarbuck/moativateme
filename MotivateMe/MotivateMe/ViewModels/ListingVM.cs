using MotivateMe.DataAccess;
using MotivateMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MotivateMe.Extensions;

namespace MotivateMe.ViewModels
{
    public class ListingVM : ViewModelBase
    {
        private readonly IDataAccess _dataAccess;

        private ObservableCollection<Activity> _activities;
        private Activity _selectedAtivity;

        public ListingVM(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _activities = new ObservableCollection<Activity>();
        }

        public async Task<bool> LoadActivitiesAsync()
        {
            var list = await _dataAccess.GetActivitiesRollingSevenAsync();
            Activities = list.ToObservableCollection();
            return true;
        }

        public ObservableCollection<Activity> Activities
        {
            get
            {
                return _activities;
            }
            set
            {
                _activities = value;
                RaisePropertyChanged(() => Activities);
            }
        }

        public Activity SelectedActivity
        {
            get
            {
                return _selectedAtivity;
            }
            set
            {
                _selectedAtivity = value;
                RaisePropertyChanged(() => SelectedActivity);
            }
        }

        public ContentPage Page { get; set; }


    }
}

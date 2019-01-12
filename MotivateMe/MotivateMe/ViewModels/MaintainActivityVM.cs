using MotivateMe.DataAccess;
using MotivateMe.Models;
using MotivateMe.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MotivateMe
{
    public class MaintainActivityVM : ViewModelBase
    {

        private readonly IAchievementCalculator _achievementCalculator;
        private readonly IDataAccess _dataAccess;

        private Activity _currentActivity;
        private ValidatableObject<string> _activityName;
        private ValidatableObject<string> _activityType;
        private ValidatableObject<double> _metricOne;
        private ValidatableObject<double> _metricTwo;
        private ValidatableObject<DateTime> _timestamp;
        private ValidatableObject<TimeSpan> _time;
        private ObservableCollection<string> _activityTypes;

        private bool _isValid;

        // TODO add constructor to display an existing activity
        public MaintainActivityVM(IDataAccess dataAccess, IAchievementCalculator achievementCalculator)
        {
            _achievementCalculator = achievementCalculator;
            _dataAccess = dataAccess;

            _activityName = new ValidatableObject<string>();
            _activityType = new ValidatableObject<string>();
            _metricOne = new ValidatableObject<double>();
            _metricTwo = new ValidatableObject<double>();
            _timestamp = new ValidatableObject<DateTime>(DateTime.Now);
            _time = new ValidatableObject<TimeSpan>(_timestamp.Value.TimeOfDay);
            _currentActivity = new Activity();

            Init();
        }

        private void Init()
        {
            _activityTypes = new ObservableCollection<string>(new string[] { "Run", "Bike", "Crossfit", "Yoga", "Walk" });

            AddValidations();
        }

        public ContentPage Page { get; set; }

        public ValidatableObject<string> ActivityName
        {
            get
            {
                return _activityName;
            }
            set
            {
                _activityName = value;
                RaisePropertyChanged(() => ActivityName);
            }
        }

        public ObservableCollection<string> ActivityTypes
        {
            get
            {
                return _activityTypes;
            }
            set
            {
                _activityTypes = value;
                RaisePropertyChanged(() => ActivityTypes);
            }
        }

        public ValidatableObject<string> ActivityType
        {
            get
            {
                return _activityType;
            }
            set
            {
                _activityType = value;
                RaisePropertyChanged(() => ActivityType);
            }
        }

        public ValidatableObject<double> MetricOne
        {
            get
            {
                return _metricOne;
            }
            set
            {
                _metricOne = value;
                RaisePropertyChanged(() => MetricOne);
            }
        }

        public ValidatableObject<double> MetricTwo
        {
            get
            {
                return _metricTwo;
            }
            set
            {
                _metricTwo = value;
                RaisePropertyChanged(() => MetricTwo);
            }
        }

        public ValidatableObject<DateTime> Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
                RaisePropertyChanged(() => Timestamp);
            }
        }

        public ValidatableObject<TimeSpan> Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged(() => Time);

            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }
        private Command _saveActivity;
        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command SaveActivityCommand => _saveActivity ?? (_saveActivity = new Command(async () => await ExecuteSaveActivityCommand()));

        private async Task ExecuteSaveActivityCommand()
        {
            if (IsBusy)
                return;

            if (!Validate())
            {
                IsValid = false;
            } else
            {
                IsBusy = true;
                try
                {
                    _currentActivity.Name = _activityName.Value;
                    _currentActivity.Type = _activityType.Value;
                    _currentActivity.MetricOne = _metricOne.Value;
                    _currentActivity.MetricTwo = _metricTwo.Value;
                    _currentActivity.Timestamp = _timestamp.Value;
                    _currentActivity.Time = _time.Value;

                    await _dataAccess.SaveActivityAsync(_currentActivity);

                    await _achievementCalculator.CalculateAchievementsAsync();

                    // push to listing tab - todo seems hacky
                    if (this.Page != null)
                    {
                        var masterPage = this.Page.Parent as TabbedPage;
                        masterPage.CurrentPage = masterPage.Children[1];
                    }

                }
                catch (Exception e)
                {
                    // TODO better error handling - is Application available in unit tests?
                    Console.WriteLine(e);
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to save activity.", "OK");
                }

                IsBusy = false;

            }

        }

        
        private bool Validate()
        {
            bool isActivityName = _activityName.Validate();
            bool isActivityType = _activityType.Validate();

            return isActivityName && isActivityType;
        }

        private void AddValidations()
        {
            _activityName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An activity name is required." });
            _activityType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An activity type is required." });
        }
    }
}

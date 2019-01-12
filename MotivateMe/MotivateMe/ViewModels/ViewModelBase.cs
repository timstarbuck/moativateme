using System;
using System.Collections.Generic;
using System.Text;

namespace MotivateMe
{
    public abstract class ViewModelBase: ExtendedBindableObject
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
    }
}

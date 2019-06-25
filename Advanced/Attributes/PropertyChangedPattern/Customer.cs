using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Advanced.Attributes.PropertyChangedPattern
{
    class Customer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        string name;
        internal string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value == name)
                    return;
                name = value;

                // The compiler converts the following line to :
                // `RaisePropertyChanged("CustomerName");`
                RaisePropertyChanged();
            }
        }

        string gender;
        internal string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                if (value == gender)
                    return;
                gender = value;

                // The compiler converts the following line to :
                // `RaisePropertyChanged("CustomerGender");`
                RaisePropertyChanged();
            }
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

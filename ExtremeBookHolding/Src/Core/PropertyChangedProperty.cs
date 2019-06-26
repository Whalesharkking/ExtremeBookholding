using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtremeBookHolding.Core
{
    public abstract class PropertyChangedProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
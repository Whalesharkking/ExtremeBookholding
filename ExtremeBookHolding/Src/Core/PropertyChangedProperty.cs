using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtremeBookHolding.Core
{
    public abstract class Property
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
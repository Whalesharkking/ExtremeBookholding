using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtremeBookHolding
{
    public class AccountingRecord : INotifyPropertyChanged
    {
        private Account account;
        public Account Account
        {
            get => account;
            set
            {
                if(account != value)
                {
                    account = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal value;
        public decimal Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    RaisePropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

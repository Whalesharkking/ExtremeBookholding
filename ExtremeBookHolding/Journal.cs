using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtremeBookHolding
{
    public class Journal : INotifyPropertyChanged
    {
        private Account account;
        public Account Account
        {
            get => account;
            set
            {
                if (account != value)
                {
                    account = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<AccountingRecord> sollAccountingRecords;
        public ObservableCollection<AccountingRecord> SOLLAccountingRecords
        {
            get => sollAccountingRecords;
            set
            {
                if (sollAccountingRecords != value)
                {
                    sollAccountingRecords = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<AccountingRecord> habenAccountingRecords;
        public ObservableCollection<AccountingRecord> HABENAccountingRecords
        {
            get => habenAccountingRecords;
            set
            {
                if (habenAccountingRecords != value)
                {
                    habenAccountingRecords = value;
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

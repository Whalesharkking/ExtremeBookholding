using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExtremeBookHolding.Core;

namespace ExtremeBookHolding
{
    public class Journal : INotifyPropertyChanged
    {
        private Account _account;
        public Account Account
        {
            get => _account;
            set
            {
                if (_account == value)
                    return;
                _account = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<AccountingRecord> sollAccountingRecords;
        public ObservableCollection<AccountingRecord> SOLLAccountingRecords
        {
            get => sollAccountingRecords;
            set
            {
                if (sollAccountingRecords == value)
                    return;
                sollAccountingRecords = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<AccountingRecord> habenAccountingRecords;
        public ObservableCollection<AccountingRecord> HABENAccountingRecords
        {
            get => habenAccountingRecords;
            set
            {
                if (habenAccountingRecords == value)
                    return;
                habenAccountingRecords = value;
                RaisePropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Collections.ObjectModel;

namespace ExtremeBookHolding.Core
{
    public class Journal : PropertyChangedProperty
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
    }
}
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

        private ObservableCollection<AccountingRecord> _debitAccountingRecords;

        public ObservableCollection<AccountingRecord> DebitAccountingRecords
        {
            get => _debitAccountingRecords;
            set
            {
                if (_debitAccountingRecords == value)
                    return;
                _debitAccountingRecords = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<AccountingRecord> _creditAccountingRecords;

        public ObservableCollection<AccountingRecord> CreditAccountingRecords
        {
            get => _creditAccountingRecords;
            set
            {
                if (_creditAccountingRecords == value)
                    return;
                _creditAccountingRecords = value;
                RaisePropertyChanged();
            }
        }
    }
}
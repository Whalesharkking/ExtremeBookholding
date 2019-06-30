using System.Collections.ObjectModel;
using System.Linq;

namespace ExtremeBookHolding.Core
{
    public class Journal : PropertyChangedProperty
    {
        public decimal DebitAccountingRecordsSummary => DebitAccountingRecords.Sum(x => x.Value);
        public decimal CreditAccountingRecordsSummary => CreditAccountingRecords.Sum(x => x.Value);


        private Account _account;

        public Account Account
        {
            get => _account;
            set
            {
                if (_account != value)
                {
                    _account = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<AccountingRecord> _debitAccountingRecords;

        public ObservableCollection<AccountingRecord> DebitAccountingRecords
        {
            get => _debitAccountingRecords;
            set
            {
                if (_debitAccountingRecords != value)
                {
                    _debitAccountingRecords = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(DebitAccountingRecordsSummary));
                }
            }
        }

        private ObservableCollection<AccountingRecord> _creditAccountingRecords;

        public ObservableCollection<AccountingRecord> CreditAccountingRecords
        {
            get => _creditAccountingRecords;
            set
            {
                if (_creditAccountingRecords != value)
                {
                    _creditAccountingRecords = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CreditAccountingRecordsSummary));
                }
            }
        }
    }
}
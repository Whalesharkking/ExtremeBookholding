using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ExtremeBookHolding.Core;

namespace ExtremeBookHolding.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            PrepareAccountList();
            LoadJournalExampleData();
        }

        public ObservableCollection<Journal> JournalList { get; set; } = new ObservableCollection<Journal>();


        public decimal ActivAccountingRecordsSummary => ActiveAccountingRecords.Sum(x => x.Value);

        public List<AccountingRecord> OrderedActiveAccountingRecords =>
            ActiveAccountingRecords.OrderBy(x => x.Account.Id).ToList();

        private ObservableCollection<AccountingRecord> ActiveAccountingRecords { get; } =
            new ObservableCollection<AccountingRecord>();

        public decimal PassiveAccountingRecordsSummary => PassiveAccountingRecords.Sum(x => x.Value);

        public List<AccountingRecord> OrderedPassiveAccountingRecords =>
            PassiveAccountingRecords.OrderBy(x => x.Account.Id).ToList();

        private ObservableCollection<AccountingRecord> PassiveAccountingRecords { get; } =
            new ObservableCollection<AccountingRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadJournalExampleData()
        {
            foreach (Account account in accounts.ItemsSource)
            {
                JournalList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test1Haben", Value = 11},
                    new AccountingRecord {Account = account, Text = "Test1Soll", Value = 101});
                JournalList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test2Haben", Value = 22},
                    new AccountingRecord {Account = account, Text = "Test2Soll", Value = 202});
                JournalList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test3Haben", Value = 33},
                    new AccountingRecord {Account = account, Text = "Test3Soll", Value = 303});

                //JournalList.Add(new Journal
                //{
                //    Account = account,
                //    CreditAccountingRecords = new ObservableCollection<AccountingRecord>
                //    {
                //        new AccountingRecord {Account = account, Text = "Test1Haben", Value = 11},
                //        new AccountingRecord {Account = account, Text = "Test2Haben", Value = 22},
                //        new AccountingRecord {Account = account, Text = "Test3Haben", Value = 33}
                //    },
                //    DebitAccountingRecords = new ObservableCollection<AccountingRecord>
                //    {
                //        new AccountingRecord {Account = account, Text = "Test1Soll", Value = 101},
                //        new AccountingRecord {Account = account, Text = "Test2Soll", Value = 202},
                //        new AccountingRecord {Account = account, Text = "Test3Soll", Value = 303}
                //    }
                //});
            }
        }


        private void PrepareAccountList()
        {
            accounts.ItemsSource = new List<Account>
            {
                new Account(AccountName.Kasse),
                new Account(AccountName.Post),
                new Account(AccountName.Bank),
                new Account(AccountName.Fll),
                new Account(AccountName.Warenbestand),
                new Account(AccountName.Mobilien),
                new Account(AccountName.Immobilien),
                new Account(AccountName.VLL),
                new Account(AccountName.Darlehensschuld),
                new Account(AccountName.Hypotheken),
                new Account(AccountName.Eigenkapital)
            };
            accounts.DisplayMemberPath = nameof(accounts.Name);
        }

        private void OnEnterButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!accountValue.Value.HasValue || accountValue.Value == 0)
                return;
            if (accounts.SelectedItem == null || !(accounts.SelectedItem is Account selectedAccount))
                return;
            switch (selectedAccount.Type)
            {
                case AccountType.Active:
                    Active(selectedAccount);
                    break;
                case AccountType.Passive:
                    Passive(selectedAccount);
                    break;
                case AccountType.Both:
                    if (accountValue.Value > 0)
                    {
                        Active(selectedAccount);
                    }
                    else
                    {
                        Passive(selectedAccount);
                    }

                    break;
            }
        }

        private void Active(Account selectedAccount)
        {
            InsertAccountRecordWithAccountValueToAccountRecordList(ActiveAccountingRecords,
                selectedAccount);
            RaisePropertyChanged(nameof(OrderedActiveAccountingRecords));
            RaisePropertyChanged(nameof(ActivAccountingRecordsSummary));
        }

        private void Passive(Account selectedAccount)
        {
            InsertAccountRecordWithAccountValueToAccountRecordList(PassiveAccountingRecords,
                selectedAccount);
            RaisePropertyChanged(nameof(OrderedPassiveAccountingRecords));
            RaisePropertyChanged(nameof(PassiveAccountingRecordsSummary));
        }

        private void InsertAccountRecordWithAccountValueToAccountRecordList(
            ObservableCollection<AccountingRecord> accountingRecordList, Account account)
        {
            var accountingRecord = accountingRecordList.FirstOrDefault(x => x.Account == account);
            if (accountingRecord != null)
            {
                if (accountValue.Value != null)
                    accountingRecord.Value += (decimal) accountValue.Value;
            }
            else
            {
                if (accountValue.Value != null)
                    accountingRecordList.Add(new AccountingRecord
                        {Account = account, Value = (decimal) accountValue.Value, Text = "Anfangsbilanz"});
            }
        }
    }
}
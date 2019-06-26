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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Journal> JournalList { get; set; } = new ObservableCollection<Journal>();


        public decimal ActivAccountingRecordsSummary => ActiveAccountingRecords.Sum(x => x.Value);

        public List<AccountingRecord> OrderedActiveAccountingRecords =>
            ActiveAccountingRecords.OrderBy(x => x.Account.Id).ToList();

        private ObservableCollection<AccountingRecord> ActiveAccountingRecords { get; set; } =
            new ObservableCollection<AccountingRecord>();

        public decimal PassiveAccountingRecordsSummary => PassiveAccountingRecords.Sum(x => x.Value);

        public List<AccountingRecord> OrderedPassiveAccountingRecords =>
            PassiveAccountingRecords.OrderBy(x => x.Account.Id).ToList();

        private ObservableCollection<AccountingRecord> PassiveAccountingRecords { get; set; } =
            new ObservableCollection<AccountingRecord>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            PrepareAccountList();
            LoadJournalExampleData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadJournalExampleData()
        {
            foreach (Account account in accounts.ItemsSource)
            {
                JournalList.Add(new Journal()
                {
                    Account = account,
                    CreditAccountingRecords = new ObservableCollection<AccountingRecord>
                    {
                        new AccountingRecord() {Account = account, Text = "Test1Haben", Value = 11},
                        new AccountingRecord() {Account = account, Text = "Test2Haben", Value = 22},
                        new AccountingRecord() {Account = account, Text = "Test2Haben", Value = 33},
                    },
                    DebitAccountingRecords = new ObservableCollection<AccountingRecord>
                    {
                        new AccountingRecord() {Account = account, Text = "Test1Soll", Value = 101},
                        new AccountingRecord() {Account = account, Text = "Test2Soll", Value = 202},
                        new AccountingRecord() {Account = account, Text = "Test2Soll", Value = 303},
                    }
                });
            }
        }


        private void PrepareAccountList()
        {
            accounts.ItemsSource = new List<Account>()
            {
                new Account() {Name = "Kasse", Type = AccountType.Active},
                new Account() {Name = "Post", Type = AccountType.Active},
                new Account() {Name = "Bank", Type = AccountType.Both},
                new Account() {Name = "FLL", Type = AccountType.Active},
                new Account() {Name = "Warenbestand", Type = AccountType.Active},
                new Account() {Name = "Mobilien", Type = AccountType.Active},
                new Account() {Name = "Immobilien", Type = AccountType.Active},
                new Account() {Name = "VLL", Type = AccountType.Passive},
                new Account() {Name = "Darlehensschuld", Type = AccountType.Passive},
                new Account() {Name = "Hypotheken", Type = AccountType.Passive},
                new Account() {Name = "Eigenkapital", Type = AccountType.Passive}
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
                    InsertAccountRecordWithAccountValueToAccountRecordList(ActiveAccountingRecords,
                        selectedAccount);
                    RaisePropertyChanged(nameof(OrderedActiveAccountingRecords));
                    RaisePropertyChanged(nameof(ActivAccountingRecordsSummary));
                    break;
                case AccountType.Passive:
                    InsertAccountRecordWithAccountValueToAccountRecordList(PassiveAccountingRecords,
                        selectedAccount);
                    RaisePropertyChanged(nameof(OrderedPassiveAccountingRecords));
                    RaisePropertyChanged(nameof(PassiveAccountingRecordsSummary));
                    break;
                default:
                    //TODO: Type Both noch umsetzen (z.B Als Typ Both gilt Bank, da es aktiv und passiv sein kann)
                    break;
            }
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
                    accountingRecordList.Add(new AccountingRecord()
                        {Account = account, Value = (decimal) accountValue.Value, Text = "Anfangsbilanz"});
            }
        }
    }
}
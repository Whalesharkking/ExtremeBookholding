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
            LoadJournalExampleData();
        }

        public List<LedgerAccount> LedgerAccountList => LedgerAccountHelper.LedgerAccountList.OrderBy(x => x.Account.Id).ToList();
        public ObservableCollection<Journal> JournalList { get; set; } = new ObservableCollection<Journal>();


        public List<LedgerAccount> OrderedActiveSchlussbestandRecords
        {
            get
            {
                var list = new List<LedgerAccount>(LedgerAccountHelper.LedgerAccountList.Where(x => x.DebitAccountingRecords!= null && x.DebitAccountingRecords.Any(y => y.ID == 999)));
                foreach (var item in list)
                {
                    var sbRecord = item.DebitAccountingRecords.First(y => y.ID == 999);
                    item.DebitAccountingRecords = new ObservableCollection<AccountingRecord>();
                    item.DebitAccountingRecords.Add(sbRecord);
                }
                return list.OrderBy(x => x.Account.Id).ToList();
            }
        }
        public List<LedgerAccount> OrderedPassivSchlussbestandRecords
        {
            get
            {
                var list = new List<LedgerAccount>(LedgerAccountHelper.LedgerAccountList.Where( x => x.CreditAccountingRecords != null && x.CreditAccountingRecords.Any(y => y.ID == 999)));
                foreach (var item in list)
                {
                    var sbRecord = item.CreditAccountingRecords.First(y => y.ID == 999);
                    item.CreditAccountingRecords = new ObservableCollection<AccountingRecord>();
                    item.CreditAccountingRecords.Add(sbRecord);
                }
                return list.OrderBy(x => x.Account.Id).ToList();
            }
        }

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
            foreach (Account account in AccountList)
            {
                //LedgerAccountList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test1Haben", Value = 11},
                //    new AccountingRecord {Account = account, Text = "Test1Soll", Value = 101});
                //LedgerAccountList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test2Haben", Value = 22},
                //    new AccountingRecord {Account = account, Text = "Test2Soll", Value = 202});
                //LedgerAccountList.AddBuchungssatz(new AccountingRecord {Account = account, Text = "Test3Haben", Value = 33},
                //    new AccountingRecord {Account = account, Text = "Test3Soll", Value = 303});

                JournalList.Add(new Journal { Text = "Die Rechnung von 100Fr. wird per Bank bezahlt" });
            }
        }

        public List<Account> AccountList => new List<Account>()
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
                selectedAccount, false);
            RaisePropertyChanged(nameof(OrderedPassiveAccountingRecords));
            RaisePropertyChanged(nameof(PassiveAccountingRecordsSummary));
        }

        private void InsertAccountRecordWithAccountValueToAccountRecordList(
            ObservableCollection<AccountingRecord> accountingRecordList, Account account, bool isActiv = true)
        {
            var accountingRecord = accountingRecordList.FirstOrDefault(x => x.Account == account);
            if (accountingRecord != null)
            {
                if (accountValue.Value != null)
                {
                    accountingRecord.Value += (decimal)accountValue.Value;
                    var ledgerCreditRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == account.Id && x.CreditAccountingRecords != null && x.CreditAccountingRecords.Any(y => y.ID == 0))?.CreditAccountingRecords.First(y => y.ID == 0);
                    var ledgerDebitRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account == account && x.DebitAccountingRecords != null && x.DebitAccountingRecords.Any(y => y.ID == 0))?.DebitAccountingRecords.First(y => y.ID == 0);
                    if (ledgerCreditRecord != null)
                    {
                        ledgerCreditRecord.Value = accountingRecord.Value;
                    }
                    else if (ledgerDebitRecord != null)
                    {
                        ledgerDebitRecord.Value = accountingRecord.Value;
                    }
                    else
                    {
                        if (!isActiv)
                        {
                            LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(new Journal(0) { CreditAccount = account.Id, Text = ledgerDebitRecord.Text, Value = accountingRecord.Value });
                        }
                        else
                        {
                            LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(new Journal(0) { DebitAccount = account.Id, Text = ledgerDebitRecord.Text, Value = accountingRecord.Value });
                        }
                    }
                }
            }
            else
            {
                if (accountValue.Value != null)
                {
                    var newRecord = new AccountingRecord
                    { Account = account, Value = (decimal)accountValue.Value, Text = "Anfangsbilanz" };
                    accountingRecordList.Add(newRecord);
                    if (!isActiv)
                    {
                        LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(new Journal(0) { CreditAccount = account.Id, Text = newRecord.Text, Value = newRecord.Value });
                    }
                    else
                    {
                        LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(new Journal(0) { DebitAccount = account.Id, Text = newRecord.Text, Value = newRecord.Value });
                    }
                }
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JournalList.Add(new Journal());
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as System.Windows.Controls.TabControl).SelectedIndex == 2)
            {
                LedgerAccountHelper.LedgerAccountList.LoadSchlussbestand();
                RaisePropertyChanged(nameof(LedgerAccountList));
                RaisePropertyChanged(nameof(OrderedActiveSchlussbestandRecords));
                RaisePropertyChanged(nameof(OrderedPassivSchlussbestandRecords));
            }
        }

        private void DeleteLast_Click(object sender, RoutedEventArgs e)
        {
            if (JournalList.Any())
            {
                var existingCreditAccount = LedgerAccountList.FirstOrDefault(x => x.CreditAccountingRecords != null && x.CreditAccountingRecords.Any(y => JournalList.LastOrDefault().ID == y.ID));
                if (existingCreditAccount != null)
                {
                    existingCreditAccount.CreditAccountingRecords.Remove(existingCreditAccount.CreditAccountingRecords.First(y => JournalList.Last().ID == y.ID));
                }

                var existingDebitAccount = LedgerAccountList.FirstOrDefault(x => x.DebitAccountingRecords != null && x.DebitAccountingRecords.Any(y => JournalList.LastOrDefault().ID == y.ID));
                if (existingDebitAccount != null)
                {
                    existingDebitAccount.CreditAccountingRecords.Remove(existingDebitAccount.DebitAccountingRecords.First(y => JournalList.Last().ID == y.ID));
                }
                JournalList.Remove(JournalList.Last());
            }
        }
    }
}
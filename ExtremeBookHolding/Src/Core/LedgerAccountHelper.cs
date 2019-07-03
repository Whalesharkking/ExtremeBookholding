using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding.Core
{
    public static class LedgerAccountHelper
    {
        public static ObservableCollection<LedgerAccount> LedgerAccountList { get; set; } = new ObservableCollection<LedgerAccount>();



        public static void LoadSchlussbestand(this ObservableCollection<LedgerAccount> ledgerAccounts)
        {
            foreach (var ledgerAccount in ledgerAccounts)
            {
                if (ledgerAccount.DebitAccountingRecords != null && ledgerAccount.DebitAccountingRecords.Any(x => x.ID == 999))
                {
                    ledgerAccount.DebitAccountingRecords.Remove(ledgerAccount.DebitAccountingRecords.First(x => x.ID == 999));
                }
                if (ledgerAccount.CreditAccountingRecords != null && ledgerAccount.CreditAccountingRecords.Any(x => x.ID == 999))
                {
                    ledgerAccount.CreditAccountingRecords.Remove(ledgerAccount.CreditAccountingRecords.First(x => x.ID == 999));
                }

                if (ledgerAccount.CreditAccountingRecords == null)
                {
                    ledgerAccount.CreditAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                if (ledgerAccount.DebitAccountingRecords == null)
                {
                    ledgerAccount.DebitAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                var debitSum = ledgerAccount.DebitAccountingRecords.Sum(x => x.Value);
                var creditSum = ledgerAccount.CreditAccountingRecords.Sum(x => x.Value);

                if (debitSum > creditSum)
                {
                    ledgerAccount.CreditAccountingRecords.Add(new AccountingRecord() { Account = ledgerAccount.Account, ID = 999, Text = "Schlussbestand", Value = debitSum - creditSum });
                }
                else
                {
                    ledgerAccount.DebitAccountingRecords.Add(new AccountingRecord() { Account = ledgerAccount.Account, ID = 999, Text = "Schlussbestand", Value = creditSum - debitSum });
                }
            }
        }

        public static void AddBuchungssatz(this ObservableCollection<LedgerAccount> ledgerAccounts, Journal journal)
        {
            LedgerAccount debitLedgerAccount = null;
            LedgerAccount creditLedgerAccount = null;

            if (journal.DebitAccount != 0 && !ledgerAccounts.Where(x => x.Account.Id == journal.DebitAccount).Any(x=>  x.DebitAccountingRecords != null && x.DebitAccountingRecords.Any(y => y.ID == journal.ID)))
            {
                debitLedgerAccount = ledgerAccounts.FirstOrDefault(x => x.Account.Id == journal.DebitAccount);
                if (debitLedgerAccount == null)
                {
                    debitLedgerAccount = new LedgerAccount() { Account = new Account((AccountName)journal.DebitAccount) };
                    ledgerAccounts.Add(debitLedgerAccount);
                }
                if (debitLedgerAccount.DebitAccountingRecords == null)
                {
                    debitLedgerAccount.DebitAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                debitLedgerAccount.DebitAccountingRecords.Add(new AccountingRecord() { Account = debitLedgerAccount.Account, ID = journal.ID, Text = journal.Text, Value = journal.Value });
            }


            if (journal.CreditAccount != 0 && !ledgerAccounts.Where(x=> x.Account.Id == journal.CreditAccount).Any(x => x.CreditAccountingRecords != null && x.CreditAccountingRecords.Any(y => y.ID == journal.ID)))
            {
                creditLedgerAccount = ledgerAccounts.FirstOrDefault(x => x.Account.Id == journal.CreditAccount);
                if (creditLedgerAccount == null)
                {
                    creditLedgerAccount = new LedgerAccount() { Account = new Account((AccountName)journal.CreditAccount) };
                    ledgerAccounts.Add(creditLedgerAccount);
                }
                if (creditLedgerAccount.CreditAccountingRecords == null)
                {
                    creditLedgerAccount.CreditAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                creditLedgerAccount.CreditAccountingRecords.Add(new AccountingRecord() { Account = creditLedgerAccount.Account, ID = journal.ID, Text = journal.Text, Value = journal.Value });

            }
        }
    }

}

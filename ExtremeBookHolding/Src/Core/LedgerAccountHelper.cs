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


        private static int lastID = 0;

        public static void AddBuchungssatz(this ObservableCollection<LedgerAccount> ledgerAccounts,
            AccountingRecord accountingRecord, bool addToCredit, bool keepCurrentLastID = false)
        {
            var ledgerAccount = ledgerAccounts.FirstOrDefault(x => x.Account == accountingRecord.Account);
            if (ledgerAccount == null)
            {
                ledgerAccount = new LedgerAccount() {Account = accountingRecord.Account};
                ledgerAccounts.Add(ledgerAccount);
            }

            if (keepCurrentLastID)
            {
                accountingRecord.ID = lastID;
            }
            else
            {
                accountingRecord.ID = ++lastID;
            }

            if (addToCredit)
            {
                if (ledgerAccount.CreditAccountingRecords == null)
                {
                    ledgerAccount.CreditAccountingRecords = new ObservableCollection<AccountingRecord>();
                }

                ledgerAccount.CreditAccountingRecords.Add(accountingRecord);
            }
            else
            {
                if (ledgerAccount.DebitAccountingRecords == null)
                {
                    ledgerAccount.DebitAccountingRecords = new ObservableCollection<AccountingRecord>();
                }

                ledgerAccount.DebitAccountingRecords.Add(accountingRecord);
            }
        }

        public static void AddBuchungssatz(this ObservableCollection<LedgerAccount> ledgerAccounts, AccountingRecord debitAccount,
            AccountingRecord creditAccount)
        {
            AddBuchungssatz(ledgerAccounts, debitAccount, false);
            AddBuchungssatz(ledgerAccounts, creditAccount, true, true);
        }

    }
}
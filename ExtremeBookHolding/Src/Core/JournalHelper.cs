using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding.Core
{
    public static class JournalHelper
    {
        private static int lastID = 0;

        public static void AddBuchungssatz(this ObservableCollection<Journal> journals, AccountingRecord accountingRecord, bool addToCredit,bool keepCurrentLastID = false)
        {
            var journal = journals.FirstOrDefault(x => x.Account == accountingRecord.Account);
            if (journal == null)
            {
                journal = new Journal() { Account = accountingRecord.Account };
                journals.Add(journal);
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
                if(journal.CreditAccountingRecords == null)
                {
                    journal.CreditAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                journal.CreditAccountingRecords.Add(accountingRecord);
            }
            else
            {
                if (journal.DebitAccountingRecords == null)
                {
                    journal.DebitAccountingRecords = new ObservableCollection<AccountingRecord>();
                }
                journal.DebitAccountingRecords.Add(accountingRecord);
            }
            
        }

        public static void AddBuchungssatz(this ObservableCollection<Journal> journals, AccountingRecord debitAccount, AccountingRecord creditAccount)
        {
            AddBuchungssatz(journals, debitAccount, false);
            AddBuchungssatz(journals, creditAccount, true,true);
        }
    }
}

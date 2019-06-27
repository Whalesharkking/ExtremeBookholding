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
        public static void AddBuchungssatz(this ObservableCollection<Journal> journals, decimal value, Account account, string text, bool addToCredit)
        {
            var journal = journals.FirstOrDefault(x => x.Account == account);
            if (journal == null)
            {
                journal = new Journal() { Account = account };
            }
            if (addToCredit)
            {
                journal.CreditAccountingRecords.Add(new AccountingRecord() { Account = account, Text = text, Value = value });
            }
            else
            {
                journal.DebitAccountingRecords.Add(new AccountingRecord() { Account = account, Text = text, Value = value });
            }
        }
    }
}

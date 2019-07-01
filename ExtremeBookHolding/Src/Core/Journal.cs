using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding.Core
{
    public class Journal : PropertyChangedProperty
    {
        private static int lastId = 0;

        public Journal()
        {
            ID = ++lastId;
        }
        
        public Journal(int id_)
        {
            ID = id_;
        }

        private int id;

        public int ID
        {
            get => id;
            private set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _debitAccount;

        public int DebitAccount
        {
            get => _debitAccount;
            set
            {
                if (_debitAccount != value)
                {
                    var existingRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == _debitAccount && x.DebitAccountingRecords != null)?.DebitAccountingRecords.FirstOrDefault(y => y.ID == ID);

                    if (existingRecord != null)
                    {
                        LedgerAccountHelper.LedgerAccountList.First(x => x.Account.Id == _debitAccount).DebitAccountingRecords.Remove(existingRecord);
                    }
                    _debitAccount = value;
                    LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(this);
                    RaisePropertyChanged();
                }
            }
        }

        private int _creditAccount;

        public int CreditAccount
        {
            get => _creditAccount;
            set
            {
                if (_creditAccount != value)
                {
                    var existingRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == _creditAccount && x.CreditAccountingRecords!= null )?.CreditAccountingRecords.FirstOrDefault(y => y.ID == ID);

                    if (existingRecord != null)
                    {
                        LedgerAccountHelper.LedgerAccountList.First(x => x.Account.Id == _creditAccount).CreditAccountingRecords.Remove(existingRecord);
                    } 
                    _creditAccount = value;
                    LedgerAccountHelper.LedgerAccountList.AddBuchungssatz(this);
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _value;

        public decimal Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged();
                    var existingDebitRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == DebitAccount && x.DebitAccountingRecords != null)?.DebitAccountingRecords.FirstOrDefault(y => y.ID == ID);
                    if (existingDebitRecord != null)
                    {
                        existingDebitRecord.Value = value;
                    }

                    var existingCreditRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == CreditAccount && x.CreditAccountingRecords != null)?.CreditAccountingRecords.FirstOrDefault(y => y.ID == ID);
                    if (existingCreditRecord != null)
                    {
                        existingCreditRecord.Value = value;
                    }
                    
                }
            }
        }

        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged();
                    var existingDebitRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == DebitAccount && x.DebitAccountingRecords != null )?.DebitAccountingRecords.FirstOrDefault(y => y.ID == ID);
                    if (existingDebitRecord != null)
                    {
                        existingDebitRecord.Text = value;
                    }

                    var existingCreditRecord = LedgerAccountHelper.LedgerAccountList.FirstOrDefault(x => x.Account.Id == CreditAccount && x.CreditAccountingRecords != null)?.CreditAccountingRecords.FirstOrDefault(y => y.ID == ID);
                    if (existingCreditRecord != null)
                    {
                        existingCreditRecord.Text = value;
                    }
                }
            }
        }
    }
}

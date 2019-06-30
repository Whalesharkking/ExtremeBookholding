using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding.Core
{
    public class Journal:PropertyChangedProperty
    {
        private static int lastId = 0;

        public Journal()
        {
            ID = ++lastId;
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

        private Account _debitAccount;

        public Account DebitAccount
        {
            get => _debitAccount;
            set
            {
                if (_debitAccount != value)
                {
                    _debitAccount = value;
                    RaisePropertyChanged();
                    //TODO LedgerAccountHelper.AddBuchungssatz()
                }
            }
        }

        private Account _creditAccount;

        public Account CreditAccount
        {
            get => _creditAccount;
            set
            {
                if (_creditAccount != value)
                {
                    _creditAccount = value;
                    RaisePropertyChanged();
                    //TODO LedgerAccountHelper.AddBuchungssatz()
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
                }
            }
        }
    }
}

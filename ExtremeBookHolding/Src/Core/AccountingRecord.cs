namespace ExtremeBookHolding.Core
{
    public class AccountingRecord : PropertyChangedProperty
    {
        //private static int lastId;
        //public AccountingRecord()
        //{
            //ID = lastId++;
        //}

        private int id;
        public int ID
        {
            get => id; set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged();
                }
            }
        }



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
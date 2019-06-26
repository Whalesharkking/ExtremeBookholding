namespace ExtremeBookHolding.Core
{
    public class AccountingRecord : PropertyChangedProperty
    {
        private Account _account;

        public Account Account
        {
            get => _account;
            set
            {
                if (_account == value)
                    return;
                _account = value;
                RaisePropertyChanged();
            }
        }

        private decimal _value;

        public decimal Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                RaisePropertyChanged();
            }
        }

        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;
                _text = value;
                RaisePropertyChanged();
            }
        }
    }
}
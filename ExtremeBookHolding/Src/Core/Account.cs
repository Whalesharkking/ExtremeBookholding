using System;

namespace ExtremeBookHolding.Core
{
    public enum AccountType
    {
        Active = 1,
        Passive = 3,
        Both = 5
    }

    public enum AccountName
    {
        Kasse = AccountType.Active + 1,
        Post = AccountType.Active + 2,
        Bank = AccountType.Both + 4,
        Fll = AccountType.Active + 8,
        Warenbestand = AccountType.Active + 16,
        Mobilien = AccountType.Active + 32,
        Immobilien = AccountType.Active + 64,
        VLL = AccountType.Passive + 128,
        Darlehensschuld = AccountType.Passive + 256,
        Hypotheken = AccountType.Passive + 512,
        Eigenkapital = AccountType.Passive + 1024
    }

    public class Account
    {
        public readonly AccountName AccountName;

        public Account(AccountName accountName)
        {
            Id = (int) AccountName;
            AccountName = accountName;
            Type = (AccountType) (int) (Id - Math.Pow(2, (int) Math.Sqrt(Id)));
        }

        public int Id { get; }
        public string Name => AccountName.ToString();
        public AccountType Type { get; set; }
    }
}
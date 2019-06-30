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
        Kasse = AccountType.Active + 2,
        Post = AccountType.Active + 4,
        Bank = AccountType.Both + 8,
        Fll = AccountType.Active + 16,
        Warenbestand = AccountType.Active + 32,
        Mobilien = AccountType.Active + 64,
        Immobilien = AccountType.Active + 128,
        VLL = AccountType.Passive + 256,
        Darlehensschuld = AccountType.Passive + 512,
        Hypotheken = AccountType.Passive + 1024,
        Eigenkapital = AccountType.Passive + 2048
    }

    public class Account
    {
        public readonly AccountName AccountName;

        public Account(AccountName accountName)
        {
            Id = (int) accountName;
            AccountName = accountName;
            Type = (AccountType) (int) (Id - Math.Pow(2, (int) Math.Log(Id, 2)));
        }

        public int Id { get; }
        public string Name => AccountName.ToString();
        public AccountType Type { get; set; }
    }
}
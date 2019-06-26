namespace ExtremeBookHolding
{
    public enum AccountType { Active, Passive, Both}

    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
    }
}

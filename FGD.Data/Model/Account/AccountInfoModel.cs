namespace FGD.Data
{
    public class AccountInfoModel<TKey>
    {
        public TKey Id { get; set; }

        public TKey AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public AccountModel<TKey> Account { get; set; }

    }
}

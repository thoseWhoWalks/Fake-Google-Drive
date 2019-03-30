namespace FGD.Data
{
    public class AccountCryptoModel<TKey>
    {
        public TKey Id { get; set; }

        public string TokenKeySecondPart { get; set; }

        public string AESKeySecondPart { get; set; }
          
        #region Navigation props
        public TKey AccountId { get; set; }

        public AccountModel<TKey> Account { get; set; }
        #endregion
    }
}

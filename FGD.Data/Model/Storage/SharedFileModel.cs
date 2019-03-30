namespace FGD.Data
{
    public class SharedFileModel<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFileId { get; set; }

        public TKey AccountId { get; set; }

        #region nav props
        public StoredFileModel<TKey> StoredFile { get; set; }

        public AccountModel<TKey> Account { get; set; }
        #endregion
    }
}

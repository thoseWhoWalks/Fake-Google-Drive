namespace FGD.Data
{
    public class SharedFolderModel<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFolderId { get; set; }

        public TKey AccountId { get; set; }

        #region nav props
        public StoredFolderModel<TKey> StoredFolder { get; set; }

        public AccountModel<TKey> Account { get; set; }
        #endregion
    }
}

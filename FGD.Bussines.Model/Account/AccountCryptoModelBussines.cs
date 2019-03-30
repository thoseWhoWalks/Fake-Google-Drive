using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model 
{
    public class AccountCryptoModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public TKey AccountId { get; set; }

        public String TokenKeySecondPart { get; set; }

        public String AESKeySecondPart { get; set; }
    }
}

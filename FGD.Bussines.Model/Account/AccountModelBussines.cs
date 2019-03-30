using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{
    public class AccountModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public int Age { get; set; }

        public String Password { get; set; }

        public String Role { get; set; }  

        public bool IsDelted { get; set; }
    }
}

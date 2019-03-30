using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class AccountModelApi<T>
    {
        public T Id { get; set; }

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public int Age { get; set; }

        public String Password { get; set; }

        public bool IsDelted { get; set; }

    }
}

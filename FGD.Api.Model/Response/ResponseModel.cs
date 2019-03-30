using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class ResponseModel<T>
    {
        public ResponseModel() {
            this.Errors = new List<Error>(5);
        }

        public ResponseModel(T responseItem)
        {
            this.Item = responseItem;

            this.Errors = new List<Error>(5);
        }

        public T Item { get; set; }

        public bool Ok { get; set; } = true;

        public IList<Error> Errors;

        public ResponseModel<T> AddError(Error error)
        {
            Ok = false;

            Errors.Add(error);

            return this;
        }

    }

    public class Error
    {
        public Error(String message)
        {
            this.Message = message;
        }

        public String Message { get; set; }
    }
}

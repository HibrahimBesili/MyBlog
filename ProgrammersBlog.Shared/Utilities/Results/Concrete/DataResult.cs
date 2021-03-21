using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        
        public DataResult(T data, ResultStatus resultStatus)
        {
            Data = data;
            ResultStatus = resultStatus;
        }

        public DataResult(T data, ResultStatus resultStatus,string message)
        {
            Data = data;
            ResultStatus = resultStatus;
            Message = message;
        }

        public DataResult(T data, ResultStatus resultStatus, string message,Exception exception)
        {
            Data = data;
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }

         public T Data { get;}

         public ResultStatus ResultStatus { get; }

         public string Message { get; }

         public Exception Exception { get; }
    }
}

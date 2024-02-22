using JinjiProject.Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
        public bool IsSuccess { get; }

        public string Message { get; }
        
        public Result(bool isSuccess) : this() => IsSuccess = isSuccess;
        public Result(bool isSuccess, string message) : this(isSuccess) => Message = message;
    }
}

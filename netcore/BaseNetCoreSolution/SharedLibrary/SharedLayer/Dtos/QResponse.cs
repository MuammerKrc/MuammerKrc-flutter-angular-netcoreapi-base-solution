using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class QResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public T Model { get; private set; }
        public int StatusCode { get; private set; } = 200;
        public List<string> Error { get; private set; } = new List<string>();

        public static QResponse<T> SuccessResponse(T model, int statusCode = 200)
        {
            return new QResponse<T>() { IsSuccess = true, Model = model, StatusCode = statusCode };
        }
        public static QResponse<T> SuccessResponse(int statusCode = 200)
        {
            return new QResponse<T>() { IsSuccess = true, StatusCode = statusCode, };
        }
        public static QResponse<T> ErrorResponse(List<string> errors, int statusCode = 500)
        {
            return new QResponse<T>() { Error=errors, StatusCode = statusCode, IsSuccess = false };
        }

        public static QResponse<T> ErrorResponse(string error, int statusCode = 500)
        {
            return new QResponse<T>() { Error = new List<string>(){error}, StatusCode = statusCode, IsSuccess = false };
        }
    }
}

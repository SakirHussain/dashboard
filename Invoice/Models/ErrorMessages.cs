using Invoice.WebModels;
using System;
using System.Data;

namespace Invoice.Models
{
    public class ErrorMessages
    {
        public static Error GetErrorMessage(Error er, Exception exception)
        {

            switch (exception)
            {
                case FormatException _:
                    er.errorCode = StatusCodes.Status403Forbidden;
                    er.errorMessage =  "Invalid input format. Please check your data.";
                    break;
                case UnauthorizedAccessException _:
                    er.errorCode = StatusCodes.Status406NotAcceptable;
                    er.errorMessage = "Invalid Credentials, Please check";
                    break;
                case DataException _:
                    er.errorCode = StatusCodes.Status412PreconditionFailed;
                    er.errorMessage =  "Please enter all necessary information";
                    break;
                default:
                    er.errorCode = 0;
                    er.errorMessage = "An unexpected error occurred. Please try again later.";
                    break;              
            }

            return er;
        }
    }
}

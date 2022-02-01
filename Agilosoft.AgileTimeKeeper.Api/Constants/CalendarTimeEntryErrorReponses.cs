using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Constants
{
    public class CalendarTimeEntryErrorReponses
    {
        public const string NOT_FOUND = "Time Entry record not found";
        public const string UNABLE_TO_INSERT = "Error occurred when Inserting record. Some fields may be missing or are incorrect";
        public const string UNABLE_TO_UPDATE = "Error occurred when Updating record. Some fields may be missing or are incorrect";
        public const string UNABLE_TO_DELETE = "Error occurred when Deleting record. Some fields may be missing or are incorrect ";
    }
}

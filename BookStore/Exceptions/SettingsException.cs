using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Exceptions
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {
        }
    }
}
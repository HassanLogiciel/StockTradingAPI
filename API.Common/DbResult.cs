using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common
{
    public class DbResult
    {
        public HashSet<string> Errors { get; set; } = new HashSet<string>();
        public bool IsSuccess
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}

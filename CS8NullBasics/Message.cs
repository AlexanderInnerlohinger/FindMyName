using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS8NullBasics
{
    class Message
    {
        public string From { get; set; }
        public string Text { get; set; }

        public string ToUpperFrom()
        {
            return From.ToUpperInvariant();
        }
    }
}

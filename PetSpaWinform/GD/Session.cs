using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD
{
    public static class Session
    {
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static string Token { get; set; }
    }
}

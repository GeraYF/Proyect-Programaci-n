using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.ViewModel
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
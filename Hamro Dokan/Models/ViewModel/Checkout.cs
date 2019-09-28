using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hamro_dokan.Models.ViewModel
{
    public class Checkout
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public int Telephone { get; set; }


    }
}
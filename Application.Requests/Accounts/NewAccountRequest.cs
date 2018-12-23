using Application.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Accounts
{
    public class NewAccountRequest
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public AccountType AccountType { get; set; }

        public DateTime BornDate { get; set; }

        public MusicalGenders PreferedMusicalGender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public string PhoneNumber { get; set; }

        public float ExpectedHirePrice { get; set; }
    }
}

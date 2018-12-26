﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestCustomerConsole.Model
{
    class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }

        

        public Customer(int id, string firstName, string lastName, int year)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
        }

        public Customer()
        {

        }

        public override string ToString()
        {
            return $"Fornavn:{FirstName} Efternavn:{LastName} År:{Year}";
        }
    }
}

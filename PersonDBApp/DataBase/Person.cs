﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp.DataBase
{
    public class Person
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Gender { get; set; }

    }
}

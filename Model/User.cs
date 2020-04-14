using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

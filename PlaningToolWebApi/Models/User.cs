﻿using PlaningToolWebApi.Models;
namespace PlaningToolWebApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public UserRole Role{ get; set;}
        public string Password { get; set; }

    }
}
    
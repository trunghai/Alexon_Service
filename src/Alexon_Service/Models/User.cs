﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class User
    {
        private int _id;
        private String _username;
        private String _session;
        private String _fullname;
        private String _email;
        private String _address;
        private String _phone;
        private String _role;
        private int _roleId;
        private String _salution;
        private String _password;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String username
        {
            get { return _username; }
            set { _username = value; }
        }  

        public String fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
        public String email
        {
            get { return _email; }
            set { _email = value; }
        }
        public String session
        {
            get { return _session; }
            set { _session = value; }
        }
        public String address
        {
            get { return _address; }
            set { _address = value; }
        }
        public String phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public String role
        {
            get { return _role; }
            set { _role = value; }
        }
        public int roleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        public String salution
        {
            get { return _salution; }
            set { _salution = value; }
        }
        public String password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}

﻿using IDI.Core.Common;

namespace IDI.Central.Models.SCM
{
    public class SignInForm : IForm
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
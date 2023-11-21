﻿using ApplicationCore_BilgeAdam.Entities.Abstract;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete
{
    public class AppUser : IdentityUser, IBaseUser
    {
        private Status _status = Status.Active;
        private DateTime _createdDate = DateTime.Now;

        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get => _status; set => _status = value; }
    }
}

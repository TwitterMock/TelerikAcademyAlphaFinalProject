﻿using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwittersService
    {
        void StoreTwitterByUserId(string userId, Twitter twitter);
    }
}

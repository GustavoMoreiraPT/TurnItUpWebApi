﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.ResponseModels
{
    public class ApiValidationError
    {
        public int Code { get; set; }

        public string Reason { get; set; }
    }
}

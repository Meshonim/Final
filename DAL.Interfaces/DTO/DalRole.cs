﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalRole: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

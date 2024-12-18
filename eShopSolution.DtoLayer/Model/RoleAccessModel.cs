﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class RoleAccessModel
    {
        public int ID { get; set; }
        [Required]
        public string RoleID { get; set; }
        [Required]
        public int MenuPermissionID { get; set; }
    }
}

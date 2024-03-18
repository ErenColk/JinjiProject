﻿using JinjiProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Subscribers
{
    public class DeletedSubscriberListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StatusName { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}

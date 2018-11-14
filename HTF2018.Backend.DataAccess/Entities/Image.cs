﻿using System;

namespace HTF2018.Backend.DataAccess.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public String Checksum { get; set; }
        public Byte[] Data { get; set; }
    }
}
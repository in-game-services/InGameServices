﻿namespace InGameServices.Infrastructure.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int ExpiresInHours { get; set; }
    }
}

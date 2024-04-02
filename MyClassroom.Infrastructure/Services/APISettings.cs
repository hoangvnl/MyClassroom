﻿namespace MyClassroom.Infrastructure.Services
{
    public class APISettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
    }
}

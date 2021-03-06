﻿using System;
using Microsoft.Extensions.Options;

namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// Validate Basic Authentication Configuration Options
    /// </summary>
    public class BasicAuthenticationPostConfigureOptions : IPostConfigureOptions<BasicAuthenticationOptions>
    {
        public void PostConfigure(string name, BasicAuthenticationOptions options)
        {
            if (string.IsNullOrEmpty(options.Realm))
            {
                throw new InvalidOperationException("Realm must be provided in options");
            }
        }
    }
}
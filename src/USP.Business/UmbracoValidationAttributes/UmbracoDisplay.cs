﻿using System;
using System.ComponentModel;

namespace USP.Business.UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UmbracoDisplayName : DisplayNameAttribute
    {
        private readonly string _dictionaryKey;

        // This is a positional argument
        public UmbracoDisplayName(string dictionaryKey)
        {
            _dictionaryKey = dictionaryKey;
        }

        public override string DisplayName => UmbracoValidationHelper.UmbracoHelper.GetDictionaryValue(_dictionaryKey);
    }
}

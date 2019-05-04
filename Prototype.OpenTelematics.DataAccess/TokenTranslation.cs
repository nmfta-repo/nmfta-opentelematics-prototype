using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class TokenTranslation
    {
        [Key]
        public string msgid { get; set; }
        public string origin { get; set; }
        public string msgstr { get; set; }
    }

    public class TokenTranslationsModel
    {
        public List<TokenTranslation> data { get; set; }
    }
}

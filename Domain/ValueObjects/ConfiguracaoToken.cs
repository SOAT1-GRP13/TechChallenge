using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class ConfiguracaoToken
    {
        public ConfiguracaoToken()
        {
            ClientSecret = string.Empty;
            PreSalt = string.Empty;
            PosSalt = string.Empty;
        }

        public const string Configuration = "ConfiguracaoToken";
        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
    }
}

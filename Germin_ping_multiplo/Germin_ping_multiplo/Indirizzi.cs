using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Germin_ping_multiplo
{
     class Indirizzi
     {
        public string Ip { get; set; }
        public string Risposta { get; set; }
        public string Tempo { get; set; }

        public Indirizzi() { }

        public Indirizzi(string Ip,string Risposta, string Tempo)
        {
            this.Ip = Ip;
            this.Risposta = Risposta;
            this.Tempo = Tempo;
        }
     }
}

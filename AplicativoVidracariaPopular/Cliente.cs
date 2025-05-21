using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AplicativoVidracariaPopular
{
    public  class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        public override string ToString()
        {
            return Nome + "-" + Endereco;
        }
    }
}

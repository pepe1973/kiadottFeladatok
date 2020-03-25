using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemia
{
    class KemiaiElem
    {
        public string FelfedezesEve { get; set; }
        public string ElemNeve { get; set; }
        public string Vegyjel { get; set; }
        public int Rendszam { get; set; }
        public string Felfedezo { get; set; }

        public KemiaiElem(string fe, string en, string vj, int rsz, string fel)
        {
            this.FelfedezesEve = fe;
            this.ElemNeve = en;
            this.Vegyjel = vj;
            this.Rendszam = rsz;
            this.Felfedezo = fel;
        }
    }
}

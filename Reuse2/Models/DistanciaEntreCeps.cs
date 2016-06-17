using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reuse2.Models
{
    public class DistanciaEntreCeps
    {
        public int distanciaEntreCepsID { get; set; }
        public string cep1 { get; set; }
        public string cep2 { get; set; }
        public string distancia { get; set; }
        public string duracao { get; set; }
        public double distanciaCalc { get; set; }
        public double duracaoCalc { get; set; }
    }
}
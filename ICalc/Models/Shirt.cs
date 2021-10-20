using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICalc.Models
{
    class Shirt : ITextil
    {
        public int Id { get; set; }
        public string Farbe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Größe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Fütterung { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Positionen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

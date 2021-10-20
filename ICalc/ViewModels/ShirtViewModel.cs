using ICalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICalc.ViewModels
{
    class ShirtViewModel
    {
        public int Id { get; set; }
        public Shirt Shirt { get; set; }
        public int Stückzahl { get; set; }
        public List<Logo> LogoListe { get; set; }
        public List<Platzierung> PlatzierungsListe { get; set; }

    }
}

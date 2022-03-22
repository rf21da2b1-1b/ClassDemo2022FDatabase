using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilLib.model
{
    public  class Car2
    {

        private String _regNr;
        private String _colour;
        private bool _el;

        // association to the owner
        private CarOwner _ejer;

        public Car2()
        {
        }

        public Car2(string regNr, string colour, bool el, CarOwner ejer)
        {
            _regNr = regNr;
            _colour = colour;
            _el = el;
            _ejer = ejer;
        }

        public string RegNr
        {
            get => _regNr;
            set => _regNr = value;
        }

        public string Colour
        {
            get => _colour;
            set => _colour = value;
        }

        public bool El
        {
            get => _el;
            set => _el = value;
        }

        public CarOwner Ejer
        {
            get => _ejer;
            set => _ejer = value;
        }

        public override string ToString()
        {
            return $"{nameof(RegNr)}: {RegNr}, {nameof(Colour)}: {Colour}, {nameof(El)}: {El}, {nameof(Ejer)}: {Ejer}";
        }
    }
}

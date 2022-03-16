using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilLib.model
{
    public class Car
    {
        private String _regNr;
        private String _colour;
        private bool _el;

        // Hack - egentlig en objekt reference
        private String _ejer;

        public Car()
        {
        }

        public Car(string regNr, string colour, bool el, string ejer)
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

        public string Ejer
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

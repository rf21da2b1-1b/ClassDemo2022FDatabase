using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilLib.model
{
    public  class CarOwner
    {
        private string _phone;
        private string _name;

        public CarOwner()
        {
        }

        public CarOwner(string phone, string name)
        {
            _phone = phone;
            _name = name;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public override string ToString()
        {
            return $"{nameof(Phone)}: {Phone}, {nameof(Name)}: {Name}";
        }

        protected bool Equals(CarOwner other)
        {
            return _phone == other._phone && _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CarOwner)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_phone, _name);
        }
    }
}

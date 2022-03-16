using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilLib.model;

namespace BilDatabaseApp
{
    public interface IBilService
    {
        List<Car> GetAll();
        Car GetByRegNr(string regNr);
        Car Create(Car newCar);
        Car Delete(string regNr);
        Car Modify(string regNr, Car modifiedCar);
    }
}

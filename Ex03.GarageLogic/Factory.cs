using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        public enum eTypeOfVehicles
        {
            ElectricCar = 1,
            GasCar,
            ElectricMotorcycle,
            GasMotorCycle,
            Truck
        }

        public static Vehicle InitializeNewVehicle(string i_RegistrationNumber, eTypeOfVehicles i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case (eTypeOfVehicles.ElectricCar):
                    newVehicle = new ElectricCar(i_RegistrationNumber);
                    break;
                case (eTypeOfVehicles.GasCar):
                    newVehicle = new GasCar(i_RegistrationNumber);
                    break;
                case (eTypeOfVehicles.ElectricMotorcycle):
                    newVehicle = new ElectricMotorcycle(i_RegistrationNumber);
                    break;
                case (eTypeOfVehicles.GasMotorCycle):
                    newVehicle = new GasMotorcycle(i_RegistrationNumber);
                    break;
                case (eTypeOfVehicles.Truck):
                    newVehicle = new Truck(i_RegistrationNumber);
                    break;
            }

            return newVehicle;
        }
    }
}

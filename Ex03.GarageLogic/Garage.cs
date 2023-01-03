using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> m_ListOfVehicles;

        public Garage()
        {
            m_ListOfVehicles = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> ListOfVehicles
        {
            get { return m_ListOfVehicles; }
        }

        public List<string> GetListOfVehicels(eVehicleState i_ListFilterCondition)
        {
            List<string> listOfVehiclesIDs = new List<string>();

            foreach (Vehicle vehicle in ListOfVehicles.Values)
            {
                if (vehicle.VehicleState == i_ListFilterCondition)
                {
                    listOfVehiclesIDs.Add(vehicle.RegistrationNumber);
                }
            }

            return listOfVehiclesIDs;
        }

        public List<string> GetListOfVehicles()
        {
            List<string> listOfVehiclesIDs = new List<string>();

            foreach (string vehicleKey in ListOfVehicles.Keys)
            {
                listOfVehiclesIDs.Add(vehicleKey);
            }

            return listOfVehiclesIDs;
        }

        public void ChangeVehicleState(string i_RegistrationNumber, eVehicleState i_NewVehicleState)
        {
            ListOfVehicles[i_RegistrationNumber].VehicleState = i_NewVehicleState;
        }

        public void FillVihecleEnergy(string i_RegistrationNumber, float i_ValueToAdd, eFuelType i_FuelType = eFuelType.Octan96)
        {
            ListOfVehicles[i_RegistrationNumber].Fill(i_ValueToAdd, i_FuelType);
        }

        public void PumpVehicleWheels(string i_RegistrationNumber)
        {
            ListOfVehicles[i_RegistrationNumber].PumpWheels();
        }

        public string GetVehicleDetails(string i_RegistrationNumber)
        {
            return ListOfVehicles[i_RegistrationNumber].ToString();
        }

        public void AddNewVehicle(Vehicle i_NewVehicle)
        {
            ListOfVehicles.Add(i_NewVehicle.RegistrationNumber, i_NewVehicle);
        }

        public bool IsVehicleExist(string i_RegistrationNumber)
        {
            return ListOfVehicles.ContainsKey(i_RegistrationNumber);
        }
    }
}

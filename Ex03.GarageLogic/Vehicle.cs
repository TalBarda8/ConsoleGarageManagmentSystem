using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_RegistrationNumber;
        private string m_ModelName;
        private float m_EnergyPercent;
        private readonly List<Wheel> m_ListOfWheels;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleState m_VehicleState;

        public Vehicle(string i_RegistrationNumber)
        {
            m_RegistrationNumber = i_RegistrationNumber;
            m_ModelName = string.Empty;
            m_EnergyPercent = 0;
            m_ListOfWheels = new List<Wheel>();
            m_OwnerName = string.Empty;
            m_OwnerPhoneNumber = string.Empty;
            m_VehicleState = eVehicleState.InProgress;
        }

        protected string ModelName 
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string RegistrationNumber
        {
            get { return m_RegistrationNumber; }
        }

        protected float EnergyPercent
        {
            get { return m_EnergyPercent; }
            set { m_EnergyPercent = value; }
        }

        protected List<Wheel> WheelsList
        {
            get { return m_ListOfWheels; }
        }

        protected string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        protected string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();
            vehicleData.AppendLine("Vehicle data:");
            vehicleData.AppendLine(string.Format($"Registration Number: {RegistrationNumber}"));
            vehicleData.AppendLine(string.Format($"Vehicle type: {this.GetType().Name}"));
            vehicleData.AppendLine(string.Format($"Model name: {ModelName}"));
            vehicleData.AppendLine(string.Format($"Owner name: {OwnerName}"));
            vehicleData.AppendLine(string.Format($"Vehicle state: {VehicleState}"));
            vehicleData.AppendLine(string.Format($"Wheels list state: {string.Join(", ", WheelsList)}"));

            AddSpecificDataToString(ref vehicleData);

            return vehicleData.ToString();
        }

        public void PumpWheels()
        {
            foreach (Wheel wheel in WheelsList)
            {
                float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.PumpWheel(airToAdd);
            }
        }

        public virtual Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string, Type> myParameters = new Dictionary<string, Type>();
            myParameters.Add("Owner name", typeof(string));
            myParameters.Add("Owner phone number", typeof(string));
            myParameters.Add("Model name", typeof(string));
            myParameters.Add("Wheels Manufacturer", typeof(string));
            myParameters.Add("Wheels Current air pressure", typeof(float));

            return myParameters;
        }

        public virtual void SetInitializedParameters(Dictionary<string,Object> i_ParametersValues)
        {
            OwnerName = (string)i_ParametersValues["Owner name"];
            OwnerPhoneNumber = (string)i_ParametersValues["Owner phone number"];
            ModelName = (string)i_ParametersValues["Model name"];
        }

        protected abstract void AddSpecificDataToString(ref StringBuilder io_CurrentData);

        public abstract void Fill(float i_ValueToAdd, eFuelType i_FuelType);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GasMotorcycle : GasVehicle
    {
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public GasMotorcycle(string i_RegistrationNumber)
            : base(i_RegistrationNumber, eFuelType.Octan98, 6.2f)
        {
        }

        private eMotorcycleLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        private int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        protected override void AddSpecificDataToString(ref StringBuilder io_CurrentData)
        {
            io_CurrentData.AppendLine(string.Format($"Fuel type is: {FuelType}"));
            io_CurrentData.AppendLine(string.Format($"Fuel state: {CurrentFuelInLiters} liters left, out of {FuelTankSizeInLiters} liters"));
            io_CurrentData.AppendLine(string.Format($"License type of this motorcycle is: {LicenseType}"));
            io_CurrentData.AppendLine(string.Format($"Engine capacity of this motorcycle is: {EngineCapacity}"));
        }

        public override Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string, Type> myParameters = base.GetParametersForm();
            myParameters.Add("License type", typeof(eMotorcycleLicenseType));
            myParameters.Add("Engine capacity", typeof(int));

            return myParameters;
        }

        public override void SetInitializedParameters(Dictionary<string, Object> i_ParametersValues)
        {
            base.SetInitializedParameters(i_ParametersValues);

            LicenseType = (eMotorcycleLicenseType)i_ParametersValues["License type"];
            EngineCapacity = (int)i_ParametersValues["Engine capacity"];

            for (int i = 0; i < 2; i++)
            {
                Wheel wheel = new Wheel((string)i_ParametersValues["Wheels Manufacturer"], 31.0f);
                wheel.PumpWheel((float)i_ParametersValues["Wheels Current air pressure"]);
                WheelsList.Add(wheel);
            }
        }
    }
}

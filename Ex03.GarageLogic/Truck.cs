using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : GasVehicle
    {
        private bool m_ContainsCoolCargo;
        private float m_TrunkCapacity;

        public Truck(string i_RegistrationNumber)
            : base(i_RegistrationNumber, eFuelType.Soler, 120.0f)
        {
        }

        private bool ContainsCoolCargo
        {
            get { return m_ContainsCoolCargo; }
            set { m_ContainsCoolCargo = value; }
        }

        private float TrunkCapacity
        {
            get { return m_TrunkCapacity; }
            set { m_TrunkCapacity = value; }
        }

        protected override void AddSpecificDataToString(ref StringBuilder io_CurrentData)
        {
            io_CurrentData.AppendLine(string.Format($"Fuel type is: {FuelType}"));
            io_CurrentData.AppendLine(string.Format($"Fuel state: {CurrentFuelInLiters} liters left, out of {FuelTankSizeInLiters} liters"));
            if (ContainsCoolCargo)
            {
                io_CurrentData.AppendLine(string.Format($"this truck has cooling stock"));
            }
            else
            {
                io_CurrentData.AppendLine(string.Format($"this truck does not have cooling stock"));
            }

            io_CurrentData.AppendLine(string.Format($"Trunk capacity of this truck is: {TrunkCapacity}"));
        }

        public override Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string, Type> myParameters = base.GetParametersForm();
            myParameters.Add("Is Contains Cool Cargo", typeof(bool));
            myParameters.Add("Trunk capacity", typeof(float));

            return myParameters;
        }

        public override void SetInitializedParameters(Dictionary<string, Object> i_ParametersValues)
        {
            base.SetInitializedParameters(i_ParametersValues);

            ContainsCoolCargo = (bool)i_ParametersValues["Is Contains Cool Cargo"];
            TrunkCapacity = (float)i_ParametersValues["Trunk capacity"];

            for (int i = 0; i < 16; i++)
            {
                Wheel wheel = new Wheel((string)i_ParametersValues["Wheels Manufacturer"], 24.0f);
                wheel.PumpWheel((float)i_ParametersValues["Wheels Current air pressure"]);
                WheelsList.Add(wheel);
            }
        }
    }
}

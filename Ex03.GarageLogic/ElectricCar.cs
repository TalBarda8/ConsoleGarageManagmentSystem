using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private eCarColor m_Color;
        private eCarDoorsCount m_DoorsCount;

        public ElectricCar(string i_RegistrationNumber)
            : base(i_RegistrationNumber, 3.3f)
        {
        }

        private eCarColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        private eCarDoorsCount DoorsCount
        {
            get { return m_DoorsCount; }
            set { m_DoorsCount = value; }
        }

        protected override void AddSpecificDataToString(ref StringBuilder io_CurrentData)
        {
            io_CurrentData.AppendLine(string.Format($"Battery state: {CurrentBatteryTime} houres of battery left, out of {MaxBatteryTime} houres"));
            io_CurrentData.AppendLine(string.Format($"The color of this car is: {Color}"));
            io_CurrentData.AppendLine(string.Format($"The number of doors of this car is: {DoorsCount}"));
        }

        public override Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string, Type> myParameters = base.GetParametersForm();
            myParameters.Add("Car color", typeof(eCarColor));
            myParameters.Add("Number of doors", typeof(eCarDoorsCount));

            return myParameters;
        }

        public override void SetInitializedParameters(Dictionary<string, Object> i_ParametersValues)
        {
            base.SetInitializedParameters(i_ParametersValues);

            Color = (eCarColor)i_ParametersValues["Car color"];
            DoorsCount = (eCarDoorsCount)i_ParametersValues["Number of doors"];

            for (int i = 0; i < 4; i++)
            {
                Wheel wheel = new Wheel((string)i_ParametersValues["Wheels Manufacturer"], 29.0f);
                wheel.PumpWheel((float)i_ParametersValues["Wheels Current air pressure"]);
                WheelsList.Add(wheel);
            }
        }
    }
}

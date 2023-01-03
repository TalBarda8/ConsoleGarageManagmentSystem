using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private readonly float m_MaxBatteryTime;
        private float m_CurrentBatteryTime;

        public ElectricVehicle(string i_RegistrationNumber, float i_MaxBatteryTime)
            : base(i_RegistrationNumber)
        {
            m_MaxBatteryTime = i_MaxBatteryTime;
            m_CurrentBatteryTime = 0;
        }

        protected float MaxBatteryTime
        {
            get { return m_MaxBatteryTime; }
        }

        protected float CurrentBatteryTime
        {
            get { return m_CurrentBatteryTime; }
            set { m_CurrentBatteryTime = value; }
        }

        public override void Fill(float i_ValueToAdd, eFuelType i_FuelType)
        {
            float chargeTimeByHoures = i_ValueToAdd / 60;
            if ((float)(CurrentBatteryTime + chargeTimeByHoures) > MaxBatteryTime || chargeTimeByHoures < 0)
            {
                throw new ValueOutOfRagneException(null, 0, (MaxBatteryTime - CurrentBatteryTime) * 60,
                    string.Format($"An error occured while trying to charge vehicle battery with the value {i_ValueToAdd},\nsince the max possible value is {(MaxBatteryTime - CurrentBatteryTime) * 60}, and the min value possible is 0."));
            }
            else
            {
                CurrentBatteryTime += chargeTimeByHoures;
                EnergyPercent = CurrentBatteryTime / MaxBatteryTime;
            }
        }

        public override Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string, Type> myParameters = base.GetParametersForm();
            myParameters.Add("Current battery time in minuts", typeof(float));

            return myParameters;
        }

        public override void SetInitializedParameters(Dictionary<string, Object> i_ParametersValues)
        {
            base.SetInitializedParameters(i_ParametersValues);
            float value = (float)i_ParametersValues["Current battery time in minuts"];
            Fill(value, eFuelType.Octan96);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class GasVehicle : Vehicle
    {
        private readonly eFuelType m_FuelType;
        private readonly float m_FuelTankSizeInLiters;
        private float m_CurrentFuelInLiters;

        public GasVehicle(string i_RegistrationNumber, eFuelType i_FuelType, float i_FuelTankSizeInLiters)
            : base(i_RegistrationNumber)
        {
            m_FuelType = i_FuelType;
            m_FuelTankSizeInLiters = i_FuelTankSizeInLiters;
            m_CurrentFuelInLiters = 0;
        }

        protected eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        protected float FuelTankSizeInLiters
        {
            get { return m_FuelTankSizeInLiters; }
        }

        protected float CurrentFuelInLiters
        {
            get { return m_CurrentFuelInLiters; }
            set { m_CurrentFuelInLiters = value; }
        }

        public override void Fill(float i_ValueToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException(string.Format($"Fuel type inserted does not match this vehicle fuel type.\nfuel type inserted: {i_FuelType}.\nfuel type required: {FuelType}."));
            }

            if (((float)(CurrentFuelInLiters + i_ValueToAdd) > FuelTankSizeInLiters) || i_ValueToAdd < 0)
            {
                throw new ValueOutOfRagneException(null, 0, FuelTankSizeInLiters - CurrentFuelInLiters,
                    string.Format($"An error occured while trying to fill vehicle gas tank with the value {i_ValueToAdd},\nsince the max possible value is {(float)(FuelTankSizeInLiters - CurrentFuelInLiters)}, and the min value possible is 0."));
            }
            else
            {
                CurrentFuelInLiters += i_ValueToAdd;
                EnergyPercent = CurrentFuelInLiters / FuelTankSizeInLiters;
            }
        }

        public override Dictionary<string, Type> GetParametersForm()
        {
            Dictionary<string,Type> myParameters = base.GetParametersForm();
            myParameters.Add("Current fuel in liters", typeof(float));

            return myParameters;
        }

        public override void SetInitializedParameters(Dictionary<string, Object> i_ParametersValues)
        {
            base.SetInitializedParameters(i_ParametersValues);
            float value = (float)i_ParametersValues["Current fuel in liters"];
            Fill(value, FuelType);
        }
    }
}

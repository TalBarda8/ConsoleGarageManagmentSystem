using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string m_Manufacturer;
        private readonly float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0;
        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public void PumpWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd > m_MaxAirPressure || i_AirPressureToAdd < 0)
            {
                throw new ValueOutOfRagneException(null, 0, m_MaxAirPressure - m_CurrentAirPressure,
                    string.Format($"An error occured while trying to pump vehicle wheel with the value {i_AirPressureToAdd},\nsince the max possible value is {m_MaxAirPressure - m_CurrentAirPressure}, and the min value possible is 0."));
            }
            else
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
        }

        public override string ToString()
        {
            return string.Format($"Wheel: [Current air pressure: {CurrentAirPressure}, Manufacturer: {Manufacturer}]");
        }
    }
}

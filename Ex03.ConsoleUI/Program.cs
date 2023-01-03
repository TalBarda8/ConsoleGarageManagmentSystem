using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartGarageSystem();
        }

        public static void StartGarageSystem()
        {
            Console.WriteLine("Welcome to Roee's & Tal's garage!");
            string newServiceRequest = string.Empty;
            string registrationNumberForServie = string.Empty;
            Garage theGarage = new Garage();
            bool v_StayInGarage = true;
            bool v_State = true;
            string getOutCheck = string.Empty;

            while (v_StayInGarage)
            {
                Thread.Sleep(1000);
                Console.Clear();

                try
                {
                    chooseGarageService(out newServiceRequest);
                    if (!(newServiceRequest.Equals("1") || newServiceRequest.Equals("2")))
                    {
                        chooseVehicleForService(theGarage, out registrationNumberForServie);
                    }

                    runSelectedService(theGarage, newServiceRequest, registrationNumberForServie);

                    Console.WriteLine("operation done!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    v_State = true;
                    Thread.Sleep(500);
                    Console.WriteLine(string.Empty);
                    Console.WriteLine("Would you like to get any other services from the garage? (y/n)");
                    while (v_State)
                    {
                        getOutCheck = Console.ReadLine();

                        if (getOutCheck.Equals("y"))
                        {
                            v_State = false;
                        }
                        else if (getOutCheck.Equals("n"))
                        {
                            v_State = false;
                            v_StayInGarage = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid answer, please tey again.");
                        }
                    }
                }
            }

            Console.WriteLine("Thank you for visiting, see you soon!");
            Console.ReadLine();
        }

        private static void chooseGarageService(out string i_newServiceRequest)
        {
            Console.WriteLine("These are the services we can suggest you:");
            Console.WriteLine(string.Empty);
            Console.WriteLine("In order to add new vehicle to the garage, please press \"1\".");
            Console.WriteLine("In order to see the vehicles that are currently in the garage, please press \"2\".");
            Console.WriteLine("In order to change the state of a car in the garage, please press \"3\".");
            Console.WriteLine("In order to fill the wheels of a car in the garage, please press \"4\".");
            Console.WriteLine("In order to refuel a gas vehicle / charge an electric vehicle that in the garage, please press \"5\".");
            Console.WriteLine("In order to see the full data of a vehice that in the garage, please press \"6\".");

            i_newServiceRequest = Console.ReadLine();
            checksIfValidServiceInput(i_newServiceRequest);
        }

        private static void checksIfValidServiceInput(string i_newServiceRequest)
        {
            int service;

            if (int.TryParse(i_newServiceRequest, out service) && service > 0 && service < 7)
            {
                return;
            }
            else
            {
                throw new ArgumentException("Error. this service does not exsist");
            }
        }

        private static void chooseVehicleForService(Garage i_theGarage, out string o_RegistrationNumberForServie)
        {
            bool v_State = true;
            string inputRegistrationNumber = string.Empty;
            o_RegistrationNumberForServie = string.Empty;

            Console.WriteLine("Please write the registration number of the vehicle you would like to take the service for:");

            while (v_State)
            {
                inputRegistrationNumber = Console.ReadLine();

                if (i_theGarage.IsVehicleExist(inputRegistrationNumber))
                {
                    o_RegistrationNumberForServie = inputRegistrationNumber;
                    v_State = false;
                }
                else
                {
                    Console.WriteLine("The car with this registration number is not in the garage, please enter valid input.");
                }
            }
        }

        private static void runSelectedService(Garage i_theGarage, string i_newServiceRequest, string i_RegistrationNumberForServie)
        {
            switch (i_newServiceRequest)
            {
                case "1":
                    tryAddNewVehicle(i_theGarage);
                    break;
                case "2":
                    getListOfVehicles(i_theGarage);
                    break;
                case "3":
                    changeVehicleState(i_theGarage, i_RegistrationNumberForServie);
                    break;
                case "4":
                    vehiclePumpWheels(i_theGarage, i_RegistrationNumberForServie);
                    break;
                case "5":
                    fillEnergyInVehicle(i_theGarage, i_RegistrationNumberForServie);
                    break;
                case "6":
                    Console.WriteLine(i_theGarage.GetVehicleDetails(i_RegistrationNumberForServie));
                    break;
            }
        }

        private static void getListOfVehicles(Garage i_theGarage)
        {
            bool v_State = true;
            string toFilterList = string.Empty;
            Object filter;
            List<string> listOfRegistrationNumbers = new List<string>();

            Console.WriteLine("Would you like to filter the list with one of the car states? (y/n)");

            while (v_State)
            {
                toFilterList = Console.ReadLine();

                if (toFilterList.Equals("y"))
                {
                    filter = chooseEnumParameter(typeof(eVehicleState));
                    listOfRegistrationNumbers = i_theGarage.GetListOfVehicels((eVehicleState)filter);
                    v_State = false;
                }
                else if (toFilterList.Equals("n"))
                {
                    listOfRegistrationNumbers = i_theGarage.GetListOfVehicles();
                    v_State = false;
                }
                else
                {
                    Console.WriteLine("This is not a valid answer. Please try again.");
                }
            }

            Console.WriteLine(string.Join(", ", listOfRegistrationNumbers));
        }

        private static void changeVehicleState(Garage i_theGarage, string i_RegistrationNumberForServie)
        {
            bool v_State = true;
            string stateToChageInput = string.Empty;
            Object changeToState;
            Console.WriteLine("Please choose a state to change to");
            changeToState = chooseEnumParameter(typeof(eVehicleState));

            i_theGarage.ChangeVehicleState(i_RegistrationNumberForServie, (eVehicleState)changeToState);
        }

        private static void vehiclePumpWheels(Garage i_theGarage, string i_RegistrationNumberForServie)
        {
            i_theGarage.PumpVehicleWheels(i_RegistrationNumberForServie);
        }

        private static void fillEnergyInVehicle(Garage i_theGarage, string i_RegistrationNumberForServie)
        {
            string input = string.Empty;
            float valueToAdd = 0;
            Object fuelType = null;
            bool v_State = true;

            if (i_theGarage.ListOfVehicles[i_RegistrationNumberForServie] is GasVehicle)
            {
                Console.WriteLine("Your vihecle is fuel vihecle!");
                Console.WriteLine("please enter fuel type");
                fuelType = chooseEnumParameter(typeof(eFuelType));
                Console.WriteLine("please enter fuel amount to fill (in liters)");
            }
            else
            {
                Console.WriteLine("Your vihecle is electric vihecle!");
                Console.WriteLine("please enter charge time wished (in minuts)");
            }

            while (v_State)
            {
                input = Console.ReadLine();

                if (!float.TryParse(input, out valueToAdd))
                {
                    Console.WriteLine("invalid input. please enter a float number");
                    continue;
                }

                v_State = false;
            }

            if (fuelType == null)
            {
                i_theGarage.FillVihecleEnergy(i_RegistrationNumberForServie, valueToAdd);
            }
            else
            {
                i_theGarage.FillVihecleEnergy(i_RegistrationNumberForServie, valueToAdd, (eFuelType)fuelType);
            }
        }

        private static void tryAddNewVehicle(Garage i_theGarage)
        {
            Console.WriteLine("please enter new vehicle registration number");
            string registrationNumber = Console.ReadLine();
            if (i_theGarage.IsVehicleExist(registrationNumber))
            {
                Console.WriteLine("Vehicle already exsist. changing state to \"in progress\"");
                i_theGarage.ChangeVehicleState(registrationNumber, eVehicleState.InProgress);
            }
            else
            {
                Console.WriteLine("Adding new vehicle..");
                addNewVehicle(i_theGarage, registrationNumber);
            }
        }

        private static void addNewVehicle(Garage i_theGarage, string i_RegistrationNumber)
        {
            Console.WriteLine("Enter vehicle type wished");
            Factory.eTypeOfVehicles vehicleType = (Factory.eTypeOfVehicles)chooseEnumParameter(typeof(Factory.eTypeOfVehicles));
            Vehicle newVehicle = Factory.InitializeNewVehicle(i_RegistrationNumber, vehicleType);
            Dictionary<string, Object> settedParameters = new Dictionary<string, Object>();
            Dictionary<string, Type> parametersForm = newVehicle.GetParametersForm();
            bool v_state = true;
            Console.WriteLine("start collecting relevant parameters");
            Console.WriteLine(string.Empty);

            foreach (string parameterName in parametersForm.Keys)
            {
                v_state = true;

                while (v_state)
                {
                    try
                    {
                        if (parametersForm[parameterName].IsEnum)
                        {
                            addEnumParameterData(parameterName, parametersForm[parameterName], ref settedParameters);
                        }
                        else
                        {
                            addNonEnumParameterData(parameterName, parametersForm[parameterName], ref settedParameters);
                        }

                        v_state = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format($"{ex.Message} try again"));
                    }
                }
            }

            newVehicle.SetInitializedParameters(settedParameters);
            i_theGarage.AddNewVehicle(newVehicle);
            Console.WriteLine($"new vehicle {i_RegistrationNumber} has been added to the garage");
        }

        private static Object chooseEnumParameter(Type i_EnumType)
        {
            string input = string.Empty;
            bool v_state = true;
            int chosenNumber;
            Object chosenEnum = null;

            while (v_state)
            {
                int counter = 1;
                Console.WriteLine("choose one of the following options (type the number corresponds to your wished option)");
                foreach (Object eNumValue in Enum.GetValues(i_EnumType))
                {
                    Console.WriteLine($"{counter++}) {eNumValue}");
                }

                input = Console.ReadLine();

                if (!int.TryParse(input, out chosenNumber))
                {
                    Console.WriteLine("invalid input. please enter a number.");
                    continue;
                }

                if (!Enum.IsDefined(i_EnumType, chosenNumber))
                {
                    Console.WriteLine("invalid input. please choose one of the options below.");
                    continue;
                }

                chosenEnum = Enum.Parse(i_EnumType, input);
                v_state = false;
            }

            return chosenEnum;
        }

        private static void addNonEnumParameterData(string i_ParameterName, Type i_ParameterType, ref Dictionary<string, Object> o_ResultPatametersData)
        {
            try
            {
                Console.WriteLine(string.Format($"enter {i_ParameterName} ({ i_ParameterType.Name})"));
                string userInput = Console.ReadLine();

                Object data = Convert.ChangeType(userInput, i_ParameterType);
                o_ResultPatametersData.Add(i_ParameterName, data);
            }
            catch (Exception ex)
            {
                throw new FormatException(string.Format($"invalid input type. input should be {i_ParameterType.Name} type."));
            }
        }

        private static void addEnumParameterData(string i_ParameterName, Type i_ParameterType, ref Dictionary<string, Object> o_ResultPatametersData)
        {
            Console.WriteLine(string.Format($"enter {i_ParameterName}"));

            Object data = chooseEnumParameter(i_ParameterType);
            o_ResultPatametersData.Add(i_ParameterName, data);
        }
    }
}

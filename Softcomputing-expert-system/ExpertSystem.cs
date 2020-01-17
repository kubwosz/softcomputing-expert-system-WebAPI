using Newtonsoft.Json;
using Softcomputing_expert_system.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Softcomputing_expert_system
{
    static public class ExpertSystem
    {
        const string CITY_CAR_POWER_CAR = "You've chosen city car type and a very powerful engine. These requirements are very hard to meet. Think about changing it.";
        const string POWER_CAR_ECONOMICAL_CAR = "You've chosen a very powerful engine and very economical car. These requirements are nearly impossible to meet. Make sure these are correct.";
        const string SPORT_CAR_LONG_DISTANCES = "You've chosen sport car type and long distances for the main routes. These answers can be unintentional. Make sure these are correct.";
        const string BUDGET_CAR_NEW_CAR = "You've chosen low budget for the car and just new car option. These requirements are very hard to meet. New car prices begins about 30 000.";
        static public Result ProceedExpertSystem(Question[] questionnaire) {

            List<Car> carsExamples = GetCarExamples();

            int[] carPoints = new int[carsExamples.Count + 1];

            int carTypeWeight = 10;
            int powerWeight = 5;
            int fuelConsumptionWeight = 7;
            int facilitiesWeight = 4;
            int gearBoxWeight = 4;
            int interiorTrimWeight = 4;
            int routeWeight = 5;
            int safetyWeight = 5;
            int budgetWeight = 10;
            int newCarWeight = 7;
            int driveTypeWeight = 3;

            string cityCarPowerCar = "";
            string powerCarEconomicalCar = "";
            string sportCarLongDistances = "";
            string budgetCarNewCar = "";

            //Caluclate total number of checked answers
            int answersCounter = 0;
            foreach(var q in questionnaire)
            {
                foreach (var a in q.answers)
                {
                    answersCounter++;
                }
            }

            //Check if the questionnaire answers are not incosistend
            //
            if (questionnaire[0].answers.Count() == 1 && questionnaire[0].answers[0] == 4 && questionnaire[1].answers.Count() == 1 && questionnaire[1].answers[0] == 3)
            {
                cityCarPowerCar = CITY_CAR_POWER_CAR;
            }
            if (questionnaire[1].answers.Count() == 1 && questionnaire[1].answers[0] == 3 && questionnaire[2].answers.Count() == 1 && questionnaire[2].answers[0] == 1)
            {
                powerCarEconomicalCar = POWER_CAR_ECONOMICAL_CAR;
            }
            if (questionnaire[0].answers.Count() == 1 && questionnaire[0].answers[0] == 2 && questionnaire[6].answers.Count() == 1 && questionnaire[6].answers[0] == 1)
            {
                sportCarLongDistances = SPORT_CAR_LONG_DISTANCES;
            }
            if ((questionnaire[8].answers.Contains(1) || questionnaire[8].answers.Contains(2)) && questionnaire[9].answers.Contains(1))
            {
                budgetCarNewCar = BUDGET_CAR_NEW_CAR;
            }

            if(cityCarPowerCar != "" || powerCarEconomicalCar != "" || sportCarLongDistances != "" || budgetCarNewCar != "")
            {
                if(cityCarPowerCar != "")
                {
                    return new Result
                    {
                        Message = cityCarPowerCar
                    };
                }
                else if (powerCarEconomicalCar != "")
                {
                    return new Result
                    {
                        Message = powerCarEconomicalCar
                    };
                }
                else if (sportCarLongDistances != "")
                {
                    return new Result
                    {
                        Message = sportCarLongDistances
                    };
                }
                else if (budgetCarNewCar != "")
                {
                    return new Result
                    {
                        Message = budgetCarNewCar
                    };
                }
            }
            else if (answersCounter > 15)
            {
                return new Result
                {
                    Message = "Too much answers"
                };
            }
            else
            {
            foreach (var c in carsExamples)
            {
                int tmpVar = -1;
                //Question 1
                for (int i = 0; i < c.carType.Length; i++)
                {
                    if (questionnaire[0].answers.Contains(c.carType[i]))
                    {
                        if(tmpVar == -1)
                        {
                            carPoints[c.Id] += carTypeWeight;
                        tmpVar = 1;
                        }
                    }
                }
                tmpVar = -1;
                //Question 2
                //if (questionnaire[1].answers.Contains(1)) {
                //    tmpVar = 
                //}
                if (questionnaire[1].answers.Contains(c.power))
                {
                        carPoints[c.Id] += powerWeight;
                }

                //Question 3
                if (questionnaire[2].answers.Contains(c.fuelConsumption))
                {
                        carPoints[c.Id] += fuelConsumptionWeight;
                }

                //Question 4
                if (questionnaire[3].answers.Contains(c.facilities))
                {
                    carPoints[c.Id]+=facilitiesWeight;
                }

                //Question 5
                for (int i = 0; i < c.gearBox.Length; i++)
                {
                    if (questionnaire[4].answers.Contains(c.gearBox[i]))
                    {
                        if (tmpVar == -1)
                        {
                            carPoints[c.Id] += gearBoxWeight;
                            tmpVar = 1;
                        }
                        else
                        {
                            carPoints[c.Id]++;
                        }
                    }
                }
                tmpVar = -1;
                //Question 6
                for (int i = 0; i < c.interiorTrim.Length; i++)
                {
                    if (questionnaire[5].answers.Contains(c.interiorTrim[i]))
                    {
                        if (tmpVar == -1)
                        {
                            carPoints[c.Id] += interiorTrimWeight;
                            tmpVar = 1;
                        }
                        else
                        {
                            carPoints[c.Id]++;
                        }
                    }
                }
                tmpVar = -1;

                //Question 7
                if (questionnaire[6].answers.Contains(c.route))
                {
                    carPoints[c.Id]+=routeWeight;
                }

                //Question 8
                if (questionnaire[7].answers.Contains(c.safety))
                {
                    carPoints[c.Id]+=safetyWeight;
                }

                //Question 9
                if(questionnaire[8].answers.Contains(1) && c.budget < 15000)
                {
                    if (tmpVar == -1)
                    {
                        carPoints[c.Id]+=budgetWeight;
                        tmpVar = 1;
                    }
                }
                else if (questionnaire[8].answers.Contains(2) && c.budget > 15000 && c.budget < 30000)
                {
                    if (tmpVar == -1)
                    {
                        carPoints[c.Id] += budgetWeight;
                        tmpVar = 1;
                    }
                }
                else if (questionnaire[8].answers.Contains(3) && c.budget > 30000 && c.budget < 50000)
                {
                    if (tmpVar == -1)
                    {
                        carPoints[c.Id] += budgetWeight;
                        tmpVar = 1;
                    }
                }
                else if (questionnaire[8].answers.Contains(4) && c.budget > 50000 && c.budget < 100000)
                {
                    if (tmpVar == -1)
                    {
                        carPoints[c.Id] += budgetWeight;
                        tmpVar = 1;
                    }
                }
                else if (questionnaire[8].answers.Contains(5) && c.budget > 100000)
                {
                    if (tmpVar == -1)
                    {
                        carPoints[c.Id] += budgetWeight;
                        tmpVar = 1;
                    }
                }

                //Question 10
                if (questionnaire[9].answers.Contains(c.newCar))
                {
                    carPoints[c.Id]+=newCarWeight;
                }
                if (questionnaire[9].answers.Contains(3))
                {
                    carPoints[c.Id]++;
                }

                //Question 11
                for (int i = 0; i < c.driveType.Length; i++)
                {
                    if (questionnaire[10].answers.Contains(c.driveType[i]))
                    {
                        carPoints[c.Id]+=driveTypeWeight;
                    }
                }
            }
            }


            CarScore[] carPointsObjects = new CarScore[carsExamples.Count + 1];

            int bestCarid = 0;
            for (int i = 0; i < carPoints.Length; i++)
            {
                if(carPoints[i] > carPoints[bestCarid])
                {
                    bestCarid = i;
                }
                carPointsObjects[i] = new CarScore
                {
                    Id = i,
                    Points = carPoints[i]
                };
            }
            var sortedArray = carPointsObjects.OrderBy(s => s.Points).ToArray();

            return new Result
            {
                Message = "",
                CarScore = sortedArray
            };
        }

        static List<Car> GetCarExamples()
        {
            using (StreamReader r = new StreamReader("Data/expertSystemCarExamples.json"))
            {
                string json = r.ReadToEnd();
                List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(json);

                return cars;
            }
        }

    }
}

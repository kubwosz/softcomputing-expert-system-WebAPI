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
        static public int ProceedExpertSystem(Question[] questionnaire) {

            List<Car> carsExamples = GetCarExamples();

            int[] carPoints = new int[carsExamples.Count + 1];

            foreach(var c in carsExamples)
            {
                for (int i = 0; i < c.carType.Length; i++)
                {
                    if (questionnaire[0].answers.Contains(c.carType[i]))
                    {
                        carPoints[c.Id]++;
                    }
                }

                if (questionnaire[1].answers.Contains(c.power))
                {
                    carPoints[c.Id]++;
                }

                if (questionnaire[2].answers.Contains(c.fuelConsumption))
                {
                    carPoints[c.Id]++;
                }

                if (questionnaire[3].answers.Contains(c.facilities))
                {
                    carPoints[c.Id]++;
                }

                for (int i = 0; i < c.gearBox.Length; i++)
                {
                    if (questionnaire[4].answers.Contains(c.gearBox[i]))
                    {
                        carPoints[c.Id]++;
                    }
                }

                for (int i = 0; i < c.interiorTrim.Length; i++)
                {
                    if (questionnaire[5].answers.Contains(c.interiorTrim[i]))
                    {
                        carPoints[c.Id]++;
                    }
                }

                if (questionnaire[6].answers.Contains(c.route))
                {
                    carPoints[c.Id]++;
                }

                if (questionnaire[7].answers.Contains(c.safety))
                {
                    carPoints[c.Id]++;
                }

                if (questionnaire[8].answers.Contains(c.budget))
                {
                    carPoints[c.Id]++;
                }

                if (questionnaire[9].answers.Contains(c.newCar))
                {
                    carPoints[c.Id]++;
                }

                for (int i = 0; i < c.driveType.Length; i++)
                {
                    if (questionnaire[10].answers.Contains(c.driveType[i]))
                    {
                        carPoints[c.Id]++;
                    }
                }
            }

            int bestCarid = -1;
            for (int i = 0; i < carPoints.Length; i++)
            {
                if(carPoints[i] > bestCarid)
                {
                    bestCarid = i;
                }
            }

            return bestCarid;
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

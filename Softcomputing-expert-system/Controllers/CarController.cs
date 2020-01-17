using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softcomputing_expert_system.Model;

namespace Softcomputing_expert_system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {

        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Post([FromBody] QuestionViewModel[] questionViewModel)
        {
            Question[] questions = new Question[questionViewModel.Length];

            for (int i = 0; i < questionViewModel.Length; i++)
            {
                int[] answersTmp = new int[questionViewModel[i].answers.Length];
                for (int j = 0; j < questionViewModel[i].answers.Length; j++)
                {
                    answersTmp[j] = int.Parse(questionViewModel[i].answers[j]);
                }

                questions[i] = new Question
                {
                    questionID = questionViewModel[i].questionID,
                    answers = answersTmp
                };

            }
            var result = ExpertSystem.ProceedExpertSystem(questions);

            return Ok(result);
        }
    }
}

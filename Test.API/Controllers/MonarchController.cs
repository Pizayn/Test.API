using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vienna.Test.API.Entites;
using Vienna.Test.API.Repositories;
using Vienna.Test.API.Services;

namespace Vienna.Test.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MonarchController : ControllerBase
    {
        // Repository to interact with Monarch data.
        private readonly IMonarchRepository _monarchRepository;

        // Constructor injecting the MonarchRepository.
        public MonarchController(IMonarchRepository monarchRepository)
        {
            _monarchRepository = monarchRepository;
        }

        // HTTP GET method to retrieve all monarchs.
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Monarch>), (int)HttpStatusCode.OK)] // Indicates the response type and status code.
        public async Task<ActionResult<IEnumerable<Monarch>>> GetAllMonarchList()
        {
            var list = await _monarchRepository.GetAllAsync();
            return Ok(list); // Returns the list of monarchs with an OK status.
        }

        // HTTP GET method to retrieve the total number of monarchs.
        [HttpGet("stats/total")]
        public async Task<ActionResult<int>> GetTotalMonarchs()
        {
            var list = await _monarchRepository.GetAllAsync();
            return Ok(list.Count); // Returns the count of monarchs.
        }

        // HTTP GET method to find the monarch with the longest reign.
        [HttpGet("stats/longestRulingMonarch")]
        public async Task<ActionResult<string>> GetLongestRulingMonarch()
        {
            var list = await _monarchRepository.GetAllAsync();
            var longestRuler = list.OrderByDescending(m => m.ReignLength).FirstOrDefault(); // Finds the monarch with the longest reign.
            return Ok($"{longestRuler?.Nm} - {longestRuler?.ReignLength} years"); // Returns the name and reign length.
        }

        // HTTP GET method to find the most common first name among monarchs.
        [HttpGet("stats/mostCommonFirstName")]
        public async Task<ActionResult<string>> GetMostCommonFirstName()
        {
            var list = await _monarchRepository.GetAllAsync();
            var firstName = list.GroupBy(m => m.Nm.Split(' ')[0]) // Groups monarchs by first name.
                                .OrderByDescending(g => g.Count()) // Orders by the number of occurrences.
                                .Select(g => g.Key) // Selects the most common first name.
                                .FirstOrDefault();
            return Ok(firstName); // Returns the most common first name.
        }

        // HTTP GET method to find the house with the longest cumulative reign.
        [HttpGet("stats/longestRulingHouse")]
        public async Task<ActionResult<string>> GetLongestRulingHouse()
        {
            var list = await _monarchRepository.GetAllAsync();
            var house = list.GroupBy(m => m.Hse) // Groups monarchs by house.
                            .Select(group => new { House = group.Key, TotalYears = group.Sum(m => m.ReignLength) }) // Calculates total years for each house.
                            .OrderByDescending(g => g.TotalYears) // Orders by total years.
                            .FirstOrDefault();
            return Ok($"{house?.House} - {house?.TotalYears} years"); // Returns the house and total years.
        }
    }
}

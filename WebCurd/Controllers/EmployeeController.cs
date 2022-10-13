using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebCrud.Models;

namespace WebCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        public EmployeeController(IConfiguration config)
        {
            _config=config; 
        }
        public IActionResult Index()
        {
            List<EmployeeWebModel> employeeWebModels = new List<EmployeeWebModel>();
            HttpClient client = new HttpClient();   
            client.BaseAddress= new Uri(_config.GetValue<string>("BaseApiUrl")+"Get");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(_config.GetValue<string>("BaseApiUrl") + "Get").Result;
            employeeWebModels=JsonConvert.DeserializeObject<List<EmployeeWebModel>>(response.Content.ReadAsStringAsync().Result);
            return View(employeeWebModels);
        }
    }
}

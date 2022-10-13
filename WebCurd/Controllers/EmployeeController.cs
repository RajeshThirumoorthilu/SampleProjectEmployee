using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
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
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult PushEmployee(EmployeeWebModel employeeWebModel)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(employeeWebModel);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_config.GetValue<string>("BaseApiUrl") + "Insert");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync(_config.GetValue<string>("BaseApiUrl") + "Insert", requestContent).Result;
            var result =response.Content.ReadAsStringAsync().Result;
            return Json("Success");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
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
            EmployeeWebModel employeeWebModel = new EmployeeWebModel();
            employeeWebModel.Mode = "Add";
            return View(employeeWebModel);
        }
        public IActionResult PushEmployee(EmployeeWebModel employeeWebModel)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(employeeWebModel);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            string temp = employeeWebModel.Mode == "Add" ? "Insert" : "Update";
            client.BaseAddress =new Uri(_config.GetValue<string>("BaseApiUrl") + temp);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response=new HttpResponseMessage();
            if (employeeWebModel.Mode == "Add")
            {
                response = client.PostAsync(_config.GetValue<string>("BaseApiUrl") + "Insert", requestContent).Result;
            }
            else if (employeeWebModel.Mode == "Edit")
            {
                response = client.PutAsync(_config.GetValue<string>("BaseApiUrl") + "Update", requestContent).Result;
            }
            var result =response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(temp=="Insert"? "Record inserted successfully" : "Record updated successfully");
            }
            else
            {
                return Json(temp == "Insert" ? "Record insertion failed" : "Record updation failed");
            }
        }
        public IActionResult Edit(int Id)
        {
            EmployeeWebModel employeeWebModel = new EmployeeWebModel();
            employeeWebModel.Id = Id;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_config.GetValue<string>("BaseApiUrl") + "GetById?id=" + Id);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(_config.GetValue<string>("BaseApiUrl") + "GetById?id=" + Id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                employeeWebModel = JsonConvert.DeserializeObject<EmployeeWebModel>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                employeeWebModel = new EmployeeWebModel();
            }
            employeeWebModel.Mode = "Edit";
            return View("Add", employeeWebModel);
        }
        public IActionResult Delete(int Id)
        {
            EmployeeWebModel employeeWebModel = new EmployeeWebModel();
            employeeWebModel.Id = Id;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_config.GetValue<string>("BaseApiUrl") + "Delete?id=" + Id);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.DeleteAsync(_config.GetValue<string>("BaseApiUrl") + "Delete?id=" + Id).Result;
            return Json("");
        }
    }
}

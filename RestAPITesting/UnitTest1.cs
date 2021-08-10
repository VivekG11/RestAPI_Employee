using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestAPI_EmployeDetails;
using System;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RestAPITesting
{
    [TestClass]
    public class RestAPIEmpPayroll
    {
        RestClient client = null;
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }

        public IRestResponse GetEmployeesData()
        {
            //Define method Type
            RestRequest request = new RestRequest("/Employees", Method.GET);
            //Eexcute request
            IRestResponse response = client.Execute(request);
            //Return the response
            return response;
        }
        [TestMethod]
        public void MethodToGetEmployeesData()
        {
            IRestResponse response = GetEmployeesData();
            //Deserialize json object to List
            var jsonObject = JsonConvert.DeserializeObject<List<EmployeeData>>(response.Content);
            foreach (var element in jsonObject)
            {
                Console.WriteLine($" first_name: {element.firstName} || last_name:{element.lastName} || email :{element.email} || id: {element.id} ||");
            }
            //Check by count 
            Assert.AreEqual(6, jsonObject.Count);
            //Check by status code 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

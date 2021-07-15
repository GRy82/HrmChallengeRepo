using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;
        private static Compensation _compensation;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();

            _compensation = new Compensation()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 65000,
                EffectiveDate = DateTime.Now
            }; 
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }


        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = _compensation;
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.EmployeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void CreateCompensation_Returns_BadRequest()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = null, Salary = _compensation.Salary, EffectiveDate = _compensation.EffectiveDate
            };
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNull(newCompensation);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var compensation = _compensation;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var retrievedCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.EmployeeId, retrievedCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, retrievedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, retrievedCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationById_Returns_NotFound()
        {
            //Arrange 
            var novelGuid = Guid.NewGuid().ToString();

            //Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{novelGuid}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var retrievedCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNull(retrievedCompensation);
        }
    }
}
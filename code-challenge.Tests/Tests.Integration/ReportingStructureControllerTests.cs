using challenge.Repositories;
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
using System.Collections.Generic;
using challenge.Services;
using static challenge.Services.ReportingStructureService;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NestedEmployees()
        {
            //Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var getResponse = getRequestTask.Result;
            var reportingStructure = getResponse.DeserializeContent<Reportable>();
            //Assert
            Assert.IsNotNull(reportingStructure);
            Assert.IsInstanceOfType(reportingStructure, typeof(Reportable));
            Assert.AreEqual(4, reportingStructure.numberOfReports);
            Assert.AreEqual(employeeId, reportingStructure.employeeId);
            Assert.AreEqual(2, reportingStructure.employee.DirectReports.Count);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NotFound()
        {
            //Arrange
            string employeeId = "16a59";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var getResponse = getRequestTask.Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}

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

       //[TestMethod]
       //public void GetReportingStructure_Returns_NestedEmployees()
       // {
       //     //Arrange
       //     Employee employee = new Employee()
       //     {
       //         FirstName = "Billy",
       //         LastName = "Preston",
       //         Department = "HR",
       //         Position = "Talent Acquisition",
       //         DirectReports = new List<Employee>()
       //         {
       //             new Employee(){    
       //                 FirstName = "Tom", 
       //                 DirectReports = null 
       //             },
       //             new Employee(){
       //                 FirstName = "Bill",
       //                 DirectReports = new List<Employee>()
       //                 {
       //                     new Employee(){ FirstName = "Todd", DirectReports = null }
       //                 }
       //             }
       //         }
       //     };

       //     var requestContent = new JsonSerialization().ToJson(employee);
       //     var postRequestTask = _httpClient.PostAsync("api/employee",
       //        new StringContent(requestContent, Encoding.UTF8, "application/json"));
       //     var postResponse = postRequestTask.Result;

       //     var retrievedEmployee = postResponse.DeserializeContent<Employee>();

       //     Assert.IsInstanceOfType(retrievedEmployee.EmployeeId, typeof(String));
       //     Assert.IsNotNull(retrievedEmployee.EmployeeId);

       //     //Act
       //     var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{retrievedEmployee.EmployeeId}");
       //     var getResponse = getRequestTask.Result;
       //     var reportingStructure = getResponse.DeserializeContent<Employee>();
       //     //Assert
       //     Assert.IsNotNull(getResponse);
       //     Assert.IsNotNull(reportingStructure);
       //     Assert.AreEqual(2, reportingStructure.DirectReports.Count);
       //     Assert.AreEqual("Todd", reportingStructure.DirectReports[1].DirectReports[0].FirstName);
       // }

        [TestMethod]
        public void GetReportingStructure_Returns_NestedEmployees()
        {
            //Arrange
            Employee employee = new Employee()
            {
                FirstName = "Billy",
                LastName = "Preston",
                Department = "HR",
                Position = "Talent Acquisition",
                DirectReports = new List<Employee>()
                {
                    new Employee(){
                        FirstName = "Tom",
                        DirectReports = null
                    },
                    new Employee(){
                        FirstName = "Bill",
                        DirectReports = new List<Employee>()
                        {
                            new Employee(){ FirstName = "Todd", DirectReports = null }
                        }
                    }
                }
            };

            var requestContent = new JsonSerialization().ToJson(employee);
            var postRequestTask = _httpClient.PostAsync("api/employee",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;

            var retrievedEmployee = postResponse.DeserializeContent<Employee>();

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{retrievedEmployee.EmployeeId}");
            var getResponse = getRequestTask.Result;
            var reportingStructure = getResponse.DeserializeContent<List<Employee>>();
            //Assert
            Assert.AreEqual(2, reportingStructure.Count);
        }

    }
}

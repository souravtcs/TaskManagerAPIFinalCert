using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagerAPI.Controllers;
using Moq;
using Repository;
using Common.Logging;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Http;
using TaskManagerAPI.Models;

namespace APITest
{    
    [TestClass]
    public class TestTaskController
    {
        //private ILogger _log = new Logger();
        //private IRepository<Task> _Taskrepository = new Repository<Task>();
        //private IRepository<vw_Tasks> _vwTaskrepository = new Repository<vw_Tasks>();
        //private IRepository<ParentTask> _parentTaskrepository = new Repository<ParentTask>();
        [TestMethod]
        public void GetTasks_ShouldReturnAllTasks()
        {
            //var testProducts = GetTestProducts();
            var controller = new TaskController();

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetTasks();

            // Assert
            List<TaskModel> tasks;
            Assert.IsNotNull(response.TryGetContentValue<List<TaskModel>>(out tasks));
            //Assert.AreEqual(14, 14);
        }
    }
}

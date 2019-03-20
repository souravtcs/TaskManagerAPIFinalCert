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
using System.Linq;

namespace APITest
{    
    [TestClass]
    public class TaskControllerTest
    {        
        [TestMethod]
        public void GetTasks_ShouldReturnAllTasks()
        {
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();            
            // Act
            var response = controller.GetTasks();
            // Assert
            List<TaskModel> tasks;
            Assert.IsNotNull(response.TryGetContentValue<List<TaskModel>>(out tasks));
            Assert.AreEqual(tasks.Count, 6);
        }
        [TestMethod]
        public void GetTaskById_ShouldReturnTask()
        {
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act
            var response = controller.GetTaskById(5);
            // Assert
            TaskModel task;
            Assert.IsNotNull(response.TryGetContentValue<TaskModel>(out task));
            Assert.AreEqual(task.taskId, 5);
            Assert.AreEqual(task.task, "Task test 5");
            Assert.AreEqual(task.priority, 2);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerAPI.Controllers;
using TaskManagerAPI.Models;

namespace APITest
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public void GetProjects_ShouldReturnAllProjects()
        {
            var controller = new ProjectController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act
            var response = controller.GetProjects();
            // Assert
            List<ProjectModel> projects;
            Assert.IsNotNull(response.TryGetContentValue<List<ProjectModel>>(out projects));
            Assert.AreEqual(projects.Count, 1);
        }
        [TestMethod]
        public void GetProjectById_ShouldReturnProject()
        {
            var controller = new ProjectController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act
            var response = controller.GetProjectById(2);
            // Assert
            ProjectModel project;
            Assert.IsNotNull(response.TryGetContentValue<ProjectModel>(out project));
            Assert.AreEqual(project.ProjectId, 2);
            Assert.AreEqual(project.ProjectName, "Project 2 update");
            Assert.AreEqual(project.Priority, 2);
        }
    }
}

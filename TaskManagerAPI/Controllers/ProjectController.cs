using Common.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4201", headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        private ILogger _log;
        private IRepository<Project> _Projectrepository { get; set; }        
        public ProjectController()
        {
            this._Projectrepository = new Repository<Project>();
            this._log = new Logger();
        }        
        [Route("api/Project/GetProjects")]
        [HttpGet]
        public HttpResponseMessage GetProjects()
        {
            List<ProjectModel> result = new List<ProjectModel>();
            HttpResponseMessage response = null;
            try
            {
                result = _Projectrepository.GetAll()
                    .Where(s=>s.isActive.GetValueOrDefault() == true)
                    .Select(s => new ProjectModel
                    {
                        ProjectId = s.ProjectId,
                        ProjectName = s.ProjectName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        Priority = s.Priority,
                        ProjectStatus = 1
                    }).ToList();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
                //var c = 0;
                //var d = (1 / c);
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
            //return result;
        }
        [Route("api/Project/GetProjectById/{ProjectId:int}")]
        [HttpGet]
        public HttpResponseMessage GetProjectById([FromUri]int ProjectId)
        {
            HttpResponseMessage response = null;
            try
            {
                var s = _Projectrepository.GetById(ProjectId);
                ProjectModel data = new ProjectModel
                {
                    ProjectId = s.ProjectId,
                    ProjectName = s.ProjectName,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Priority = s.Priority,
                    ProjectStatus = 1
                };
                response = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Project/CreateProject")]
        [HttpPost]
        public HttpResponseMessage CreateProject([FromBody]ProjectModel ProjectDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                //long? parentId = null;                
                Project data = new Project
                {
                    ProjectId = ProjectDetails.ProjectId,
                    ProjectName = ProjectDetails.ProjectName,
                    StartDate = ProjectDetails.StartDate,
                    EndDate = ProjectDetails.EndDate,
                    Priority = ProjectDetails.Priority,
                    isActive = true
                };
                var result = _Projectrepository.Insert(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Project/UpdateProject")]
        [HttpPost]
        public HttpResponseMessage UpdateProject([FromBody]ProjectModel ProjectDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                Project data = new Project
                {
                    ProjectId = ProjectDetails.ProjectId,
                    ProjectName = ProjectDetails.ProjectName,
                    StartDate = ProjectDetails.StartDate,
                    EndDate = ProjectDetails.EndDate,
                    Priority = ProjectDetails.Priority,
                    isActive = true
                };
                var result = _Projectrepository.Update(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Project/DeleteProject/{ProjectId:int}")]
        [HttpPost]
        public HttpResponseMessage DeleteProject([FromUri]int ProjectId)
        {
            HttpResponseMessage response = null;
            try
            {
                _Projectrepository.Delete(ProjectId);
                response = Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Project/updateProjectStatus/{ProjectId:int}")]
        [HttpDelete]
        public HttpResponseMessage updateProjectStatus([FromUri]int ProjectId)
        {
            HttpResponseMessage response = null;
            try
            {
                var task = _Projectrepository.GetById(ProjectId);
                task.isActive = false;
                var res = _Projectrepository.Update(task);
                response = Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
    }
}

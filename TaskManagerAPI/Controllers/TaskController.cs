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
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        private ILogger _log;
        private IRepository<Task> _Taskrepository { get; set; }
        private IRepository<vw_Tasks> _vwTaskrepository { get; set; }
        private IRepository<ParentTask> _parentTaskrepository { get; set; }
        public TaskController()
        {
            this._Taskrepository = new Repository<Task>();
            this._vwTaskrepository = new Repository<vw_Tasks>();
            this._parentTaskrepository = new Repository<ParentTask>();
            this._log = new Logger();
        }
        //public TaskController(IRepository<Task> mockTaskrepository, IRepository<vw_Tasks> mockvwTaskrepository, IRepository<ParentTask> parentTaskrepository, ILogger mocklog)
        //{
        //    this._Taskrepository = mockTaskrepository;
        //    this._vwTaskrepository = mockvwTaskrepository;
        //    this._parentTaskrepository = parentTaskrepository;
        //    this._log = mocklog;
        //}
        [Route("api/Task/GetTasks")]
        [HttpGet]
        public HttpResponseMessage GetTasks()
        {
            List<TaskModel> result = new List<TaskModel>();
            HttpResponseMessage response = null;
            try
            {
                result = _vwTaskrepository.GetAll().Select(s=>new TaskModel
                {
                    taskId = s.TaskId,
                    task = s.TaskName,
                    parentTask = s.Parent_Task_Name,
                    priority = s.Priority,
                    stEndDate = s.EndDate,
                    stStartDate = s.StartDate,
                    taskStatus = 1
                }).ToList();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
                //var c = 0;
                //var d = (1 / c);
            }
            
            catch(Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
            //return result;
        }
        [Route("api/Task/GetTaskById/{TaskId:int}")]
        [HttpGet]
        public HttpResponseMessage GetTaskById([FromUri]int TaskId)
        {
            HttpResponseMessage response=null;
            try
            {
                var result = _vwTaskrepository.GetById(TaskId);
                TaskModel data = new TaskModel
                {
                    taskId = result.TaskId,
                    task = result.TaskName,
                    parentTask = result.Parent_Task_Name,
                    priority = result.Priority,
                    stEndDate = result.EndDate,
                    stStartDate = result.StartDate,
                    taskStatus = 1
                };
                response = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Task/CreateTask")]
        [HttpPost]
        public HttpResponseMessage CreateTask([FromBody]TaskModel TaskDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                //long? parentId = null;
                var parentId = _parentTaskrepository.GetAll().Where(s => s.Parent_Task_Name.Trim().ToUpper() == TaskDetails.parentTask.Trim().ToUpper()).Select(s => s.Parent_Task_Id).FirstOrDefault();
                Task data = new Task
                {
                    TaskId = TaskDetails.taskId,
                    TaskName = TaskDetails.task,
                    ParentId = parentId,
                    Priority = TaskDetails.priority,
                    EndDate = TaskDetails.stEndDate,
                    StartDate = TaskDetails.stStartDate,
                    isActive = true
                };
                var result = _Taskrepository.Insert(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Task/UpdateTask")]
        [HttpPost]
        public HttpResponseMessage UpdateTask([FromBody]TaskModel TaskDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                var parentId = _parentTaskrepository.GetAll().Where(s => s.Parent_Task_Name.Trim().ToUpper() == TaskDetails.parentTask.Trim().ToUpper()).Select(s => s.Parent_Task_Id).FirstOrDefault();
                Task data = new Task
                {
                    TaskId = TaskDetails.taskId,
                    TaskName = TaskDetails.task,
                    ParentId = parentId,
                    Priority = TaskDetails.priority,
                    EndDate = TaskDetails.stEndDate,
                    StartDate = TaskDetails.stStartDate,
                    isActive = true
                };
                var result = _Taskrepository.Update(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Task/DeleteTask/{TaskId:int}")]
        [HttpPost]
        public HttpResponseMessage DeleteTask([FromUri]int TaskId)
        {
            HttpResponseMessage response = null;
            try
            {
                _Taskrepository.Delete(TaskId);
                response = Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/Task/updateTaskStatus/{TaskId:int}")]
        [HttpDelete]
        public HttpResponseMessage updateTaskStatus([FromUri]int TaskId)
        {
            HttpResponseMessage response = null;
            try
            {
                var task = _Taskrepository.GetById(TaskId);
                task.isActive = false;
                var res = _Taskrepository.Update(task);
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

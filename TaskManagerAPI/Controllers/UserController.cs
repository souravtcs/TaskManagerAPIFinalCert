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
    public class UserController : ApiController
    {
        private ILogger _log;
        private IRepository<User> _Userrepository { get; set; }
        private IRepository<vw_Users> _vwUsersrepository { get; set; }
        private IRepository<Project> _Projectrepository { get; set; }
        private IRepository<Task> _Taskrepository { get; set; }
        public UserController()
        {
            this._Userrepository = new Repository<User>();
            this._vwUsersrepository = new Repository<vw_Users>();
            this._Projectrepository = new Repository<Project>();
            this._Taskrepository = new Repository<Task>();
            this._log = new Logger();
        }
        //public TaskController(IRepository<Task> mockTaskrepository, IRepository<vw_Tasks> mockvwTaskrepository, IRepository<ParentTask> parentTaskrepository, ILogger mocklog)
        //{
        //    this._Taskrepository = mockTaskrepository;
        //    this._vwTaskrepository = mockvwTaskrepository;
        //    this._parentTaskrepository = parentTaskrepository;
        //    this._log = mocklog;
        //}
        [Route("api/User/GetUsers")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            List<UserModel> result = new List<UserModel>();
            HttpResponseMessage response = null;
            try
            {
                result = _vwUsersrepository.GetAll().Select(s => new UserModel
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    EmployeeId = s.EmployeeId,
                    ProjectName = s.ProjectName,
                    TaskName = s.TaskName,
                    UserStatus = 1
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
        }
        [Route("api/User/GetUserById/{UserId:int}")]
        [HttpGet]
        public HttpResponseMessage GetUserById([FromUri]int UserId)
        {
            HttpResponseMessage response = null;
            try
            {
                var s = _vwUsersrepository.GetById(UserId);
                UserModel data = new UserModel
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    EmployeeId = s.EmployeeId,
                    ProjectName = s.ProjectName,
                    TaskName = s.TaskName,
                    UserStatus = 1
                };
                response = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/User/CreateUser")]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]UserModel UserDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                long ProjectId = _Projectrepository.GetAll()
                    .Where(s => s.ProjectName.ToUpper() == UserDetails.ProjectName.ToUpper())
                    .Select(s => s.ProjectId).FirstOrDefault();
                long TaskId = _Taskrepository.GetAll()
                    .Where(s => s.TaskName.ToUpper() == UserDetails.TaskName.ToUpper())
                    .Select(s => s.TaskId).FirstOrDefault();
                //long? parentId = null;                
                User data = new User
                {
                    UserId = UserDetails.UserId,
                    Name = UserDetails.Name,
                    EmployeeId = UserDetails.EmployeeId,
                    ProjectId = ProjectId,
                    TaskId = TaskId,
                    isActive = true
                };
                var result = _Userrepository.Insert(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/User/UpdateUser")]
        [HttpPost]
        public HttpResponseMessage UpdateUser([FromBody]UserModel UserDetails)
        {
            HttpResponseMessage response = null;
            try
            {
                long ProjectId = _Projectrepository.GetAll()
                    .Where(s => s.ProjectName.ToUpper() == UserDetails.ProjectName.ToUpper())
                    .Select(s => s.ProjectId).FirstOrDefault();
                long TaskId = _Taskrepository.GetAll()
                    .Where(s => s.TaskName.ToUpper() == UserDetails.TaskName.ToUpper())
                    .Select(s => s.TaskId).FirstOrDefault();
                User data = new User
                {
                    UserId = UserDetails.UserId,
                    Name = UserDetails.Name,
                    EmployeeId = UserDetails.EmployeeId,
                    ProjectId = ProjectId,
                    TaskId = TaskId,
                    isActive = true
                };
                var result = _Userrepository.Update(data);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/User/DeleteUser/{UserId:int}")]
        [HttpPost]
        public HttpResponseMessage DeleteUser([FromUri]int UserId)
        {
            HttpResponseMessage response = null;
            try
            {
                _Userrepository.Delete(UserId);
                response = Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return response;
        }
        [Route("api/User/updateUserStatus/{UserId:int}")]
        [HttpDelete]
        public HttpResponseMessage updateUserStatus([FromUri]int UserId)
        {
            HttpResponseMessage response = null;
            try
            {
                var user = _Userrepository.GetById(UserId);
                user.isActive = false;
                var res = _Userrepository.Update(user);
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

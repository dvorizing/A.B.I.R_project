using AbirCarsProject.Data;
using AbirCarsProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace AbirCarsProject.Controllers
{
    public class UserController : Controller
    {
         const int PAGE_SIZE = 5;
        private HashSet<string> UserCollectionPassword;
        private readonly CarsDbContext _carsDbContext;
        public UserController(CarsDbContext carsDbContext)
        {
            _carsDbContext = carsDbContext;
            UserCollectionPassword = new HashSet<string>();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {
                var sqlQuery = "INSERT INTO Users (UserId,UserName, Password) VALUES ({0}, {1},{2})";
                var parameterValueUserName = user.Username;
                var parameterValueId = user.UserId;
                String parameterValuePassword = user.Password;      
                if (!UserCollectionPassword.TryGetValue(parameterValuePassword, out _))
                {
                  UserCollectionPassword.Add(parameterValuePassword);
                } 
               else return RedirectToAction("Erorr");
                String hashedPassword = BCrypt.Net.BCrypt.HashPassword(parameterValuePassword);
                _carsDbContext.Database.ExecuteSqlRaw(sqlQuery, parameterValueId, parameterValueUserName, hashedPassword);              
                return RedirectToAction("Sucsses",user);
            }
            return View(user);
        }      
        [HttpGet]
        public ActionResult GetById()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetByIdAsync(User u)
        {
            var parameterValueID = u.UserId;
             var user = _carsDbContext.Users.FromSql($"SELECT * FROM Users WHERE UserId = {parameterValueID}").FirstOrDefault();
           if(user!=null)
                return RedirectToAction("Sucsses", user);
            return View(user);
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Update(User user, int id)
        {

            if (ModelState.IsValid)
            {
                var sqlQuery = "UPDATE Users SET Username = @username, Password = @password WHERE UserId = @id";
                var parameterValueID = id; 
                var parameterValueUserName = user.Username; 
                String parameterValuePassword = user.Password;
                String hashedPassword = BCrypt.Net.BCrypt.HashPassword(parameterValuePassword);
                _carsDbContext.Database.ExecuteSqlRaw(sqlQuery, new SqlParameter("@id", parameterValueID), new SqlParameter("@username", parameterValueUserName), new SqlParameter("@password", hashedPassword));               
                return RedirectToAction("Sucsses");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var sqlQuery = "DELETE FROM Users WHERE UserId = @id";
            var parameterValueId = id; 
            _carsDbContext.Database.ExecuteSqlRaw(sqlQuery, new SqlParameter("@id", parameterValueId));
            return RedirectToAction("GetUsers");
        }      
       public async Task<ActionResult> GetUsers(BaseRequest request)
        {
            if (request == null)
                request.CurrentPage = 0;
            //var sqlQuery = "SELECT * FROM Users ";
            var users = _carsDbContext.Users.FromSql($"SELECT * FROM Users ").ToList();
            var usersRequest = users.Skip((request.CurrentPage - 1) * PAGE_SIZE);
            return View (usersRequest);
        }
        public ActionResult Sucsses(User user)
        {
            return View(user);
        }
        public ActionResult Erorr()
        {
            return View();
        }
        public ActionResult NextUsers(BaseRequest req)
        {
            if (req.CurrentPage<=(_carsDbContext.Users.Count()/5))
                req.CurrentPage++;
            return RedirectToAction("GetUsers", req);
        }
        public ActionResult PrevUsers(BaseRequest req)
        {
            if(req.CurrentPage > 0)
            req.CurrentPage--;
            return RedirectToAction("GetUsers", req);
        }
    }


}


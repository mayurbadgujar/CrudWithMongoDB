using System;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using CrudWithMongoDB.Models;
using System.Configuration;
using System.Web.Http;

namespace CrudWithMongoDB.Controllers
{
    [System.Web.Http.RoutePrefix("Api/Emp")]
    public class EmpController : ApiController
    {

        // GET: Emp
        [System.Web.Http.Route("GetAllEmployee")]
        [System.Web.Http.HttpGet]
        public object GetAllEmployee()
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var client = new MongoClient(constr);
            var DB = client.GetDatabase("Employee");
            var collection = DB.GetCollection<Employee>("EmployeeDetails").Find(new BsonDocument()).ToList();
            return Json(collection);
        }

        [System.Web.Http.Route("GetEmployeeById")]
        [System.Web.Http.HttpGet]
        public object GetEmployeeById(string id)
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var client = new MongoClient(constr);
            var DB = client.GetDatabase("Employee");
            var collection = DB.GetCollection<Employee>("EmployeeDetails");
            var emp = collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault();
            return Json(emp);
        }

        [System.Web.Http.Route("AddEmployee")]
        [System.Web.Http.HttpPost]
        public object AddEmployee(Employee employee)
        {
            try
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var client = new MongoClient(constr);
                var DB = client.GetDatabase("Employee");
                var collection = DB.GetCollection<Employee>("EmployeeDetails");

                if (employee.Id == null)
                {
                    collection.InsertOne(employee);
                    return new Status
                    {
                        Result = "Success",
                        Message = "Employee Details Inserted Successfully!"
                    };
                }
                else
                {
                    var update = collection.FindOneAndUpdateAsync(Builders<Employee>.Filter.Eq("Id", employee.Id),
                                 Builders<Employee>.Update.Set("Name", employee.Name).Set("Department", employee.Department).Set("Address", employee.Address).Set("City", employee.City).Set("Country", employee.Country));
                    return new Status
                    {
                        Result = "Success",
                        Message = "Employee details updated successfulyy!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Status
                {
                    Result = "Error",
                    Message = ex.Message.ToString()
                };
            }
        }

        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpGet]
        public object Delete(string id)
        {
            try
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var client = new MongoClient(constr);
                var DB = client.GetDatabase("Employee");
                var collection = DB.GetCollection<Employee>("EmployeeDetails");
                var DeleteRecord = collection.DeleteOneAsync(Builders<Employee>.Filter.Eq("Id", id));
                return new Status
                {
                    Result = "Success",
                    Message = "Employee details deleted successfully!"
                };
            }
            catch (Exception ex)
            {
                return new Status
                {
                    Result = "Error",
                    Message = ex.Message.ToString()
                };
            }
        }
    }
}
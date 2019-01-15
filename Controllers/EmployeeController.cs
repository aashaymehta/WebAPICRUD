using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using WebApiCRUDDemo.Models;

namespace WebApiCRUDDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET api/employee
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            try
            {
                using (var empDbContext = new EmployeeDB())
                {
                    var employeeList = empDbContext.emps.ToList();
                    List<EmployeeModel> employees = AdaptToEmployeeModel(employeeList);
                    return employees;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET api/employee/5
        public EmployeeModel GetEmployee(int id)
        {
            try
            {
                using (var empDbContext = new EmployeeDB())
                {
                    var employeeList = empDbContext.emps.ToList();
                    List<EmployeeModel> employees = AdaptToEmployeeModel(employeeList);
                    if (employees != null)
                    {
                        return employees.Find(x => x.EmpId == id);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // POST api/employee
        public void Post([FromBody]EmployeeModel employee)
        {
            try
            {
                using (var empDbContext = new EmployeeDB())
                {
                    emp emp = AdaptToDBEntity(employee);
                    empDbContext.emps.Add(emp);
                    empDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody]EmployeeModel employee)
        {
            try
            {
                using (var empDbContext = new EmployeeDB())
                {
                    var employeeList = empDbContext.emps.ToList();
                    var employeeToBeUpdated = employeeList.Find(x => x.empno == id);

                    emp emp = AdaptToDBEntity(employee);
                    employeeToBeUpdated.ename = emp.ename;
                    employeeToBeUpdated.hiredate = emp.hiredate;
                    employeeToBeUpdated.job = emp.job;
                    employeeToBeUpdated.sal = emp.sal;

                    empDbContext.emps.AddOrUpdate(employeeToBeUpdated);
                    empDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
            try
            {
                using (var empDbContext = new EmployeeDB())
                {
                    var employeeList = empDbContext.emps.ToList();
                    var emp = employeeList.Find(x => x.empno == id);
                    empDbContext.emps.Remove(emp);
                    empDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Adapters
        private List<EmployeeModel> AdaptToEmployeeModel(List<emp> employeeList)
        {
            if (employeeList == null)
                return null;

            var employees = new List<EmployeeModel>();
            foreach (var emp in employeeList)
            {
                employees.Add(new EmployeeModel
                {
                    EmpId = emp.empno,
                    EmpName = emp.ename,
                    HireDate = emp.hiredate,
                    Job = emp.job,
                    Salary = emp.sal
                });
            }
            return employees;
        }

        private emp AdaptToDBEntity(EmployeeModel employee)
        {
            if (employee == null)
                return null;

            var emp = new emp
            {
                empno = employee.EmpId,
                ename = employee.EmpName,
                job = employee.Job,
                sal = employee.Salary,
                hiredate = employee.HireDate
            };
            return emp;
        } 
        #endregion
    }
}
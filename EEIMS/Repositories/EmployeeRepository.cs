﻿using EEIMS.Functionalities;
using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web;

namespace EEIMS.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _context;

        public EmployeeRepository()
        {

        }

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }

        public ApplicationDbContext Context
        {
            get 
            {
                return _context ?? new ApplicationDbContext(); 
            }
            private set 
            { 
                _context = value; 
            }
        }

        void IEmployeeRepository.AddOnce(FirstTimeAddEmployeeViewModel employee)
        {
            var temp = Context.Employees.Where(e => e.Id == employee.Id).FirstOrDefault();
            
            temp.FirstName = employee.FirstName;
            temp.LastName = employee.LastName;
            temp.Designation = employee.Designation;
            temp.Department = employee.Department;
            temp.PhoneNumber = employee.PhoneNumber;
            temp.Organization = employee.Organization;
           
            SaveChanges();
        }

        void IEmployeeRepository.Update(UpdateEmployeeViewModel model)
        {
            try
            {
                var temp = Context.Employees.Where(e => e.EmployeeId == model.EmployeeId).FirstOrDefault();

                temp.FirstName = model.FirstName;
                temp.LastName = model.LastName;
                temp.Designation = model.Designation;
                temp.Department = model.Department;
                temp.PhoneNumber = model.PhoneNumber;
                temp.Email = model.Email;
                temp.Organization = model.Organization;

                SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Updation failed, Try again later.....");
                throw e;    
            }
        }



        void IEmployeeRepository.Delete(Expression<Func<Employee, bool>> where)
        {
            Context.Employees.Remove(Context.Employees.Where(where).FirstOrDefault());
            SaveChanges();
        }

        void IEmployeeRepository.DeleteById(int id)
        {
            Context.Employees.Remove(Context.Employees.Where(e => e.EmployeeId == id).FirstOrDefault());
            SaveChanges();
        }

        //
        // Summary: For getting employee by Id (string Type)
        Employee IEmployeeRepository.GetById(string id)
        {
            return Context.Employees.Where(e => e.Id == id).FirstOrDefault();
        }
        Employee IEmployeeRepository.Get(Expression<Func<Employee, bool>> where)
        {
            return Context.Employees.Where(where).FirstOrDefault();
        }


        IEnumerable<Employee> IEmployeeRepository.GetAllEmployee()
        {
            return Context.Employees.ToList();
        }

        IEnumerable<Employee> IEmployeeRepository.GetMany(Expression<Func<Employee, bool>> where)
        {
            return Context.Employees.Where(where).ToList();
        }


        void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
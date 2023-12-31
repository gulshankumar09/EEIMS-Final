﻿using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EEIMS.Functionalities
{
    public interface IEmployeeRepository
    {
        void AddOnce(FirstTimeAddEmployeeViewModel employee);

        void Update(UpdateEmployeeViewModel model);

        void DeleteById(int id);

        Employee GetById(string id);
        Employee Get(Expression<Func<Employee, bool>> where);

        IEnumerable<Employee> GetAllVerifiedEmployee();

        IEnumerable<Employee> GetNonAllVerifiedEmployee();
    }
}


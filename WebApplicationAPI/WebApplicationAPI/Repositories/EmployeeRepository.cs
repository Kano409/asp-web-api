using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.DataContext;

namespace WebApplicationAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;

        public EmployeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var result = await _Context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if(result != null)
            {
                _Context.Employees.Remove(result);
                await _Context.SaveChangesAsync();
                return result;

            }
            return null;
        }
 
        public async Task<Employee> GetEmployee(int Id)
        {
            var result = await _Context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var result = await _Context.Employees.ToListAsync();
            return result;
        }


        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _Context.Employees.Where(a => a.Id == employee.Id).FirstOrDefaultAsync();
            if (result!=null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
            
        }

        
        public async Task<IEnumerable<Employee>> Search(string Name)
        {
            IQueryable <Employee> query = _Context.Employees;
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(a => a.Name.Contains(Name));
            }
            return await query.ToListAsync();
        }
    }
}

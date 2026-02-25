using DWQueueAPI.Data;
using DWQueueAPI.Data.Entities;

namespace DWQueueAPI.Sevices
{
    public class EmployeeService
    {
        private readonly DWQueueContext _context;

        public EmployeeService(DWQueueContext context)
        {
            _context = context;
        }


        public void UpdateEmployee(Employees employee)
        {
            var existingEmployee = _context.Employeess.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.DepartmentID = employee.DepartmentID;
                _context.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employeess.FirstOrDefault(e => e.EmployeeID == id);
            if (employee != null)
            {
                _context.Employeess.Remove(employee);
                _context.SaveChanges();
            }
        }

        public void AddEmployee(Employees employee)
        {
            _context.Employeess.Add(employee);
            _context.SaveChanges();
        }

        public Employees GetEmployeeByID(int id)
        {
            return _context.Employeess.FirstOrDefault(e => e.EmployeeID == id);
        }

        public List<Employees> GetAllEmployees()
        {
            return _context.Employeess.ToList();
        }
    }
}

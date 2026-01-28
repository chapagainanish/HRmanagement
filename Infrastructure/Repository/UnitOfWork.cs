using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private EmployeeRepository _employeeRepo;
        private OrganizationRepository _organizationRepo;
        private AttendenceRepository _attendenceRepo;
        private PayrollRepository _payrollRepo;
        private TravelExpenseRepository _travelExpenseRepo;
        private UserRepository _userRepo;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employeeRepo == null)
                {
                    _employeeRepo = new EmployeeRepository(_context);
                }
                return _employeeRepo;
            }
        }

        public IOrganizationRepository Organizations
        {
            get
            {
                if (_organizationRepo == null)
                {
                    _organizationRepo = new OrganizationRepository(_context);
                }
                return _organizationRepo;
            }
        }

        public IAttendenceRepository Attendences
        {
            get
            {
                if (_attendenceRepo == null)
                {
                    _attendenceRepo = new AttendenceRepository(_context);
                }
                return _attendenceRepo;
            }
        }

        public IPayrollRepository Payrolls
        {
            get
            {
                if (_payrollRepo == null)
                {
                    _payrollRepo = new PayrollRepository(_context);
                }
                return _payrollRepo;
            }
        }

        public ITravelExpenseRepository TravelExpences
        {
            get
            {
                if (_travelExpenseRepo == null)
                {
                    _travelExpenseRepo = new TravelExpenseRepository(_context);
                }
                return _travelExpenseRepo;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_context);
                }
                return _userRepo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}

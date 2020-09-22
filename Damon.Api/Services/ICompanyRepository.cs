using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Damon.Api.Entities;

namespace Damon.Api.Services
{
    public interface ICompanyRepository
    {
        void AddCompany(Company company);
        void AddEmployee(Guid companyId, Employee employee);
        Task<bool> CompanyExistsAsync(Guid companyId);
        void DeleteCompany(Company company);
        void DeleteEmployee(Employee employee);
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId);

        Task<bool> SaveAsync();
        void UpdateCompany(Company company);
        void UpdateEmployee(Employee employee);
    }
}
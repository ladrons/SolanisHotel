using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class EmployeeManager : BaseManager<Employee>, IEmployeeManager
    {
        readonly IEmployeeRepository _eRep;

        public EmployeeManager(IEmployeeRepository eRep) : base(eRep)
        {
            _eRep = eRep;
        }
    }
}
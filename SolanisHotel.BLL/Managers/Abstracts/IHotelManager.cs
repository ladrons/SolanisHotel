using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IHotelManager : IManager<Hotel>
    {
        Task<Hotel> FindHotelById(int hotelId);
    }
}
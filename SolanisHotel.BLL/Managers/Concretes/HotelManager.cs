using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class HotelManager : BaseManager<Hotel>, IHotelManager
    {
        readonly IHotelRepository _hRep;

        public HotelManager(IHotelRepository hRep) : base(hRep)
        {
            _hRep = hRep;
        }

        //-----//

        /// <summary>
        /// Belirtilen otel kimliğine sahip oteli bulur.
        /// </summary>
        /// <param name="hotelId">Otel kimliği</param>
        /// <returns>Belirtilen otel kimliğine sahip otel, eğer bulunamazsa null</returns>
        public async Task<Hotel> FindHotelById(int hotelId) => await _hRep.FirstOrDefaultAsync(x => x.Id == hotelId);
    }
}
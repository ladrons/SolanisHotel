using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Enums
{
    public enum ReservationStatus
    {
        Pending = 1, Confirmed = 2, Cancelled = 3,

        /*
            Pending => Rezervasyon bekleme sürecinde.
            Confirmed => Rezervasyon onaylandı.
            Cancelled => Rezervasyon iptal edildi.
         */

    }
}
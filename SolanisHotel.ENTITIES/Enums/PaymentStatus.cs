using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.ENTITIES.Enums
{
    public enum PaymentStatus
    {        
        Complete = 1, Cancelled = 2, Refunded = 3, Failed = 4

        /*
            Complete => Tamamlanmış
            Cancelled => İptal edilmiş / Başarısız.

            Refunded => İade Edilmiş/Edilecek
            Failed => Başarısız
        */
    }
}
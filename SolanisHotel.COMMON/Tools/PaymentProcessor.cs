using System.Text;
using System.Text.Json;

#nullable disable

namespace SolanisHotel.COMMON.Tools
{
    public class PaymentProcessor
    {
        readonly HttpClient _client;

        public PaymentProcessor(HttpClient client)
        {
            _client = client;
        }

        //----------//----------//

        public async Task<PaymentResult> SendPaymentRequest(PaymentInformation paymentInfo)
        {
            string jsonPayment = JsonSerializer.Serialize(paymentInfo);
            StringContent content = new StringContent(jsonPayment, Encoding.UTF8, "application/json");

            try
            {
                using HttpResponseMessage response = await _client.PostAsync("https://localhost:7069/api/Payment/ReceivePayment", content);

                string responseData = await response.Content.ReadAsStringAsync();
                PaymentResult paymentResult = JsonSerializer.Deserialize<PaymentResult>(responseData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return paymentResult;
            }
            catch (Exception)
            {
                return new PaymentResult
                {
                    Result = false,
                    TransactionCode = 0, //İlgili değer 0 ise banka tarafına veri hiç gitmemiş demektir.
                    PaymentDate = DateTime.Now,
                };
            }
        }
    }


    public class PaymentInformation
    {
        public string CardUsername { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecurityNumber { get; set; }

        public decimal Amount { get; set; }
    }

    public class PaymentResult
    {
        public PaymentResult()
        {

        }

        public PaymentResult(bool result)
        {
            Result = result;
        }

        public bool Result { get; set; }
        public int TransactionCode { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

//Read Information
/*

    Bu sınıf ödeme işlemi için geçici 'API' görevi görmektedir. API hizmeti aktif edildiğinde bazı kısımları kaldırılacaktır!

        PaymentInfo: Kullanıcının ödeme ekranlarında girdiği bilgileri temsil eder. Bu bilgileri API tarafına aktarır.

        PaymentProcessor: API hizmetini temsil eder. (API aktif edildiğinde kaldırılacak)

        PaymentResult: API tarafından gelen sonuçları temsil eden sınıfır. Bu bilgileri gerekli yerlere aktarır.

 */
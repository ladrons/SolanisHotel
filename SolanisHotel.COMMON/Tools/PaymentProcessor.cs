#nullable disable

using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Results;

namespace SolanisHotel.COMMON.Tools
{
    public class PaymentProcessor //API TARAFINI TEMSİL EDER. (ileride API aktif olduğunda bu sınıf kaldırılacak.)
    {
        private Dictionary<string, CardInfo> _registeredCards;

        public PaymentProcessor()
        {
            _registeredCards = new Dictionary<string, CardInfo>();

            AddRegisteredCard("1234567812345670", "John Doe", "12", "25", "123", 5000.00m);
        }

        public PaymentResult ProcessPayment(CardInfoDTO info, decimal amount)
        {
            PaymentStatus paymentResult = CheckRegisteredCard(info, amount);
            if (paymentResult == PaymentStatus.Success)
            {
                return new PaymentResult
                {
                    IsSuccessful = true,
                    TransactionCode = GenerateTransactionCode(),
                    PaymentDate = DateTime.Now,
                };
            }
            else
            {
                return new PaymentResult
                {
                    IsSuccessful = false,
                    TransactionCode = GenerateTransactionCode(),
                    PaymentDate = DateTime.Now,
                };
            }
        } //API

        private PaymentStatus CheckRegisteredCard(CardInfoDTO info, decimal amount)
        {
            if (_registeredCards.ContainsKey(info.CardNumber))
            {
                CardInfo registeredCard = _registeredCards[info.CardNumber];

                if (registeredCard.CardUsername == info.CardUsername
                    && registeredCard.ExpirationMonth == info.ExpirationMonth
                    && registeredCard.ExpirationYear == info.ExpirationYear
                    && registeredCard.SecurityNumber == info.CVV)
                {
                    if (registeredCard.Balance >= amount)
                    {
                        return PaymentStatus.Success;
                    }
                    else
                    {
                        //Kart bakiyesi yetersiz.
                        return PaymentStatus.InsufficientBalance;
                    }
                }
                else
                {
                    //Kart bilgileri yanlış.
                    return PaymentStatus.InvalidCard;
                }
            }
            else
            {
                //Kayıtlı kart bulunamadı.
                return PaymentStatus.CardNotFound;
            }
        } //API

        private int GenerateTransactionCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        } //API
        private void AddRegisteredCard(string cardNumber, string cardUsername, string expirationMonth, string expirationYear, string cvv, decimal balance)
        {
            _registeredCards.Add(cardNumber, new CardInfo(cardUsername, expirationMonth, expirationYear, cvv, balance)); //API
        } //API

        //-----/-----//

        private class CardInfo
        {
            public CardInfo(string cardUsername, string expirationMonth, string expirationYear, string ccv, decimal balance)
            {
                CardUsername = cardUsername;
                ExpirationMonth = expirationMonth;
                ExpirationYear = expirationYear;
                SecurityNumber = ccv;
                Balance = balance;
            }

            public string CardUsername { get; set; }

            public string CardNumber { get; set; }
            public string ExpirationMonth { get; set; }
            public string ExpirationYear { get; set; }
            public string SecurityNumber { get; set; }

            public decimal Balance { get; set; }
        } //DB'de bulunan kart bilgilerini temsil eder. //API
        private enum PaymentStatus
        {
            Success = 1, InvalidCard = 7, InsufficientBalance = 8, CardNotFound = 0
        } //API
    }
}

//Read Information
/*

    Bu sınıf ödeme işlemi için geçici 'API' görevi görmektedir. API hizmeti aktif edildiğinde bazı kısımları kaldırılacaktır!

        PaymentInfo: Kullanıcının ödeme ekranlarında girdiği bilgileri temsil eder. Bu bilgileri API tarafına aktarır.

        PaymentProcessor: API hizmetini temsil eder. (API aktif edildiğinde kaldırılacak)

        PaymentResult: API tarafından gelen sonuçları temsil eden sınıfır. Bu bilgileri gerekli yerlere aktarır.

 */
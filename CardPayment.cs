public class CardPayment : PaymentMethodBase
{
    private readonly string _cardNumber;

    public CardPayment(string cardNumber)
    {
        _cardNumber = cardNumber;
    }

    protected override void PrintDetails()
    {
        string lastFourDigits = _cardNumber[^4..];

        Console.WriteLine("=== Thanh toán bằng thẻ ===");
        Console.WriteLine($"Số thẻ: ****{lastFourDigits}");
    }
}
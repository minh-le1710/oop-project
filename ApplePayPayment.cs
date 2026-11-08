public class ApplePayPayment : PaymentMethodBase
{
    protected override void PrintDetails()
    {
        Console.WriteLine("=== Thanh toán bằng Apple Pay ===");
        Console.WriteLine("Tài khoản Apple Pay: demo@applepay.test");
    }
}

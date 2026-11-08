public class TransferPayment : PaymentMethodBase
{
    protected override void PrintDetails()
    {
        Console.WriteLine("=== Thanh toán bằng chuyển khoản ===");
        Console.WriteLine("Số tài khoản: 1234");
        Console.WriteLine("Chủ tài khoản: NGUYEN VAN A");
        Console.WriteLine("Nội dung chuyển khoản: ĐƠN HÀNG 1234");
    }
}
public class QRPayment : PaymentMethodBase
{
    protected override void PrintDetails()
    {
        Console.WriteLine("QR Code: QR_CODE_TEST");
        Console.WriteLine("Chủ tài khoản: NGUYEN VAN A");
        Console.WriteLine("Nội dung chuyển khoản: ĐƠN HÀNG 1234");
    }
}

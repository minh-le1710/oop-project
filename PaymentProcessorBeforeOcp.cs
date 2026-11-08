public class PaymentProcessorBeforeOcp
{
    public void PrintPaymentDetails(string paymentType)
    {
        if (paymentType == "QR")
        {
            Console.WriteLine("=== Thanh toán bằng QR ===");
            Console.WriteLine("QR Code: QR_CODE_TEST");
        }
        else if (paymentType == "Transfer")
        {
            Console.WriteLine("=== Thanh toán bằng chuyển khoản ===");
            Console.WriteLine("Số tài khoản: 1234");
        }
        else if (paymentType == "Card")
        {
            Console.WriteLine("=== Thanh toán bằng thẻ ===");
            Console.WriteLine("Số thẻ: ****1234");
        }
        else
        {
            Console.WriteLine($"Không hỗ trợ cách thanh toán: {paymentType}");
        }
    }
}

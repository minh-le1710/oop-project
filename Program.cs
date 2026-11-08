Console.WriteLine("=== Trước OCP: dùng if/else ===");

PaymentProcessorBeforeOcp beforeOcp = new PaymentProcessorBeforeOcp();
beforeOcp.PrintPaymentDetails("ApplePay");

Console.WriteLine();
Console.WriteLine("=== Sau OCP: dùng đa hình ===");

IPaymentMethod[] paymentMethods =
{
    new QRPayment(),
    new TransferPayment(),
    new CardPayment("0000 0000 0000 1234"),
};

foreach (IPaymentMethod paymentMethod in paymentMethods)
{
    Order order = new Order(200);

    paymentMethod.Pay(order);
    Console.WriteLine();
}

Console.WriteLine("=== Kiểm tra thanh toán ===");

Order repeatedOrder = new Order(200);
IPaymentMethod repeatedPayment = new QRPayment();

repeatedPayment.Pay(repeatedOrder);
repeatedPayment.Pay(repeatedOrder);



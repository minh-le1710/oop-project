public abstract class PaymentMethodBase : IPaymentMethod
{
    public bool Pay(Order order)
    {
        if (order.IsPaid == true)
        {
            Console.WriteLine("Thanh toán thất bại");
            return false;
        }
        PrintDetails();
        Console.WriteLine($"Số tiền: {order.Price}");
        order.Pay();
        Console.WriteLine("Thanh toán thành công");
        return true;
    }
    protected abstract void PrintDetails();
}
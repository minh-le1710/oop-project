public class Order
{
    public decimal Price { get; }
    public bool IsPaid { get; private set; }
    
    public Order(decimal price)
    {
        Price = price;
    }
    public bool Pay()
    {
        if (IsPaid == true)
        {
            return false;
        }

        IsPaid = true;
        return true;
    }
}


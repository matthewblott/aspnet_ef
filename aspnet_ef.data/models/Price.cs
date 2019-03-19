namespace aspnet_ef.data.models
{
  public class Price
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    
  }
  
}
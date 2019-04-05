namespace aspnet_ef.web.models
{
  public class PriceViewModel
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
  }
}
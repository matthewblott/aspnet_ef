namespace aspnet_ef.services
{
  public interface IAddUpdate<in T> where T : class
  {
    void Add(T entity);
    void Update(T entity);
    
  }
  
}
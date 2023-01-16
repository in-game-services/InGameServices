namespace InGameServices.Models.Service.Messages.Request;

public class UpdateServiceRequest
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string MainPictureUrl { get; set; }
  public decimal Price { get; set; }
}
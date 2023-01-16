namespace InGameServices.Models;

public class ServiceDto
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string MainPictureUrl { get; set; }
  public decimal Price { get; set; }

  public static explicit operator ServiceDto(Data.Entities.Service service)
  {
    return new ServiceDto()
    {
      Id = service.Id,
      UserId = service.UserId,
      Title = service.Title,
      Description = service.Description,
      MainPictureUrl = service.MainPictureUrl,
      Price = service.Price
    };
  }
}
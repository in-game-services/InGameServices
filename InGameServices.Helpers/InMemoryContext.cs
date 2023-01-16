using InGameServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InGameServices.Helpers;

public static class InMemoryContext
{
  public static InGameServicesDbContext CreateContext()
  {
    var options = new DbContextOptionsBuilder<InGameServicesDbContext>()
        .UseInMemoryDatabase("InGameServices")
        .ConfigureWarnings(c => c.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;

    return new InGameServicesDbContext(options);
  }
}
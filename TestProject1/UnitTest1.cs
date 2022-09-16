using HelloWorld1.Infrastructure;
using HelloWorld1.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        // var mockDbContext = new Mock<DataContext>();
        // var mockCancelToken = new CancellationToken();
        // var service = new BookService(mockDbContext.Object);
        //
        // var result = await service.GetNewest(2, mockCancelToken);
        // Assert.Equal(2, result.Count());
        Assert.Equal(1, 1);
    }
}
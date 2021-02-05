using System.Collections.Generic;
using Xunit;
using StoreApi.Domain.Models;

namespace StoreApi.Testing
{
  public class ArticleTest
  {
    [Fact]
    public void Test_ArticleExists()
    {

      // arrange
      var sut = new Article(); // inference

      // act
      var actual = sut;

      // assert
      Assert.IsType<Article>(actual);
      Assert.NotNull(actual);

    }
  }
}

using System.Collections.Generic;
using Xunit;
using StoreApi.Domain.Models;

namespace StoreApi.Testing
{
  public class AuthorTest
  {
    [Fact]
    public void Test_AuthorExists()
    {

      // arrange
      var sut = new Author(); // inference


      // act
      var actual = sut;

      // assert
      Assert.IsType<Author>(actual);
      Assert.NotNull(actual);

    }
  }
}

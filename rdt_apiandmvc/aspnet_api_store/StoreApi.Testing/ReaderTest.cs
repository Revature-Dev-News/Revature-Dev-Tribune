using System.Collections.Generic;
using Xunit;
using StoreApi.Domain.Models;

namespace StoreApi.Testing
{
  public class ReaderTest
  {
    [Fact]
    public void Test_ReaderExists()
    {

      // arrange
      var sut = new Reader(); // inference


      // act
      var actual = sut;

      // assert
      Assert.IsType<Reader>(actual);
      Assert.NotNull(actual);

    }
  }
}

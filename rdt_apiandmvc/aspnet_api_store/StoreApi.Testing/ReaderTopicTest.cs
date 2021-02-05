using System.Collections.Generic;
using Xunit;
using StoreApi.Domain.Models;

namespace StoreApi.Testing
{
  public class ReaderTopicTest
  {
    [Fact]
    public void Test_ReaderTopicExists()
    {

      // arrange
      var sut = new ReaderTopic(); // inference


      // act
      var actual = sut;

      // assert
      Assert.IsType<ReaderTopic>(actual);
      Assert.NotNull(actual);

    }
  }
}

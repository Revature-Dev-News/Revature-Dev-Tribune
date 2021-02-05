using System.Collections.Generic;
using Xunit;
using StoreApi.Domain.Models;

namespace StoreApi.Testing
{
  public class TopicTest
  {
    [Fact]
    public void Test_TopicExists()
    {

      // arrange
      var sut = new Topic(); // inference


      // act
      var actual = sut;

      // assert
      Assert.IsType<Topic>(actual);
      Assert.NotNull(actual);

    }
  }
}

using Xunit;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Microsoft.Extensions.Logging;
using Voting.Application.Services;

namespace Voting.API.Controllers.Tests
{
    public class ParedaoControllerTests
    {
        //private readonly ParedaoController _paredaoController;
        //private readonly Mock<IVoteService> _voteServiceMock;
        //private readonly Mock<ILogger<ParedaoController>> _loggerMock;
        //private readonly Mock<IDistributedCache> _cacheMock;

        //public ParedaoControllerTests()
        //{
        //    _voteServiceMock = new Mock<IVoteService>();
        //    _loggerMock = new Mock<ILogger<ParedaoController>>();
        //    _cacheMock = new Mock<IDistributedCache>();
        //    _paredaoController = new ParedaoController(_voteServiceMock.Object, _loggerMock.Object, _cacheMock.Object);
        //}

        //[Fact]
        //public async Task CanVoteAsync_UserWithinLimit_ReturnsTrue()
        //{
        //    // Arrange
        //    string userId = "user123";
        //    string cacheKey = $"VoteCount_{userId}";
        //    _cacheMock.Setup(c => c.GetStringAsync(cacheKey, It.IsAny<CancellationToken>()))
        //               .ReturnsAsync("0"); // Usuário ainda não votou

        //    // Act
        //    var result = await _paredaoController.CanVoteAsync(userId);

        //    // Assert
        //    Assert.True(result);
        //    _cacheMock.Verify(c => c.SetStringAsync(cacheKey, "1", It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task CanVoteAsync_UserExceedsLimit_ReturnsFalse()
        //{
        //    // Arrange
        //    string userId = "user123";
        //    string cacheKey = $"VoteCount_{userId}";
        //    _cacheMock.Setup(c => c.GetStringAsync(cacheKey, It.IsAny<CancellationToken>()))
        //               .ReturnsAsync("5"); // Simulando que o usuário já votou 5 vezes

        //    // Act
        //    var result = await _paredaoController.CanVoteAsync(userId);

        //    // Assert
        //    Assert.False(result);
        //    _cacheMock.Verify(c => c.SetStringAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        //}
    }
}
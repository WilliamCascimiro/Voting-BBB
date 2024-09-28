using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using Voting.API.Voting.Infra.Redis.Cache;
using Xunit;

namespace Voting.API.Test.Cache
{
    public class VoteCacheTests
    {
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly VoteCache _voteCache;

        public VoteCacheTests()
        {
            _cacheMock = new Mock<IDistributedCache>();
            _voteCache = new VoteCache(_cacheMock.Object);
        }

        [Fact]
        public async Task CanVoteAsync_UserWithinLimit_ReturnsTrue()
        {
            // Arrange
            string userId = "user123";
            string cacheKey = $"VoteCount_{userId}";
            byte[] initialVoteCount = Encoding.UTF8.GetBytes("0");

            _cacheMock.Setup(c => c.GetAsync(cacheKey, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(initialVoteCount); // Usuário ainda não votou

            // Act
            var result = await _voteCache.CanVoteAsync(userId);

            // Assert
            Assert.True(result);
            _cacheMock.Verify(c => c.SetAsync(
                cacheKey,
                It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "1"),
                It.IsAny<DistributedCacheEntryOptions>(), 
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact]
        public async Task CanVoteAsync_UserExceedsLimit_ReturnsFalse()
        {
            // Arrange
            string userId = "user123";
            string cacheKey = $"VoteCount_{userId}";
            byte[] initialVoteCount = Encoding.UTF8.GetBytes("5");

            _cacheMock.Setup(c => c.GetAsync(cacheKey, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(initialVoteCount); // Usuário já votou 5 vezes, que é o limite

            // Act
            var result = await _voteCache.CanVoteAsync(userId);

            // Assert
            Assert.False(result);
            _cacheMock.Verify(c => c.SetAsync(
                cacheKey,
                It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "6"),
                It.IsAny<DistributedCacheEntryOptions>(), 
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }

        [Fact]
        public async Task CanVoteAsync_CacheIsEmpty_ReturnsTrue()
        {
            // Arrange
            string userId = "user123";
            string cacheKey = $"VoteCount_{userId}";

            _cacheMock.Setup(c => c.GetAsync(cacheKey, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((byte[])null); // Não há contagem de votos ainda

            // Act
            var result = await _voteCache.CanVoteAsync(userId);

            // Assert
            Assert.True(result);
            _cacheMock.Verify(c => c.SetAsync(
                cacheKey,
                It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "1"),
                It.IsAny<DistributedCacheEntryOptions>(), 
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

    }
}

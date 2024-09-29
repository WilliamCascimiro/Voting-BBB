using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.API.Voting.Infra.Redis.Cache;
using Voting.Application.Services;
using Voting.Domain.Interfaces;
using Voting.Infra.Queue;
using Xunit;

namespace Voting.API.Test.Application.Services
{
    public class VoteServiceTests
    {
        private readonly Mock<IVoteRepository> _voteRepositoryMock;
        private readonly Mock<IVoteCache> _voteCacheMock;
        private readonly Mock<ILogger<VoteService>> _loggerMock;
        private readonly Mock<IRabbitMqProducer> _rabbitMqProducerMock;

        private readonly VoteService _voteService;

        public VoteServiceTests()
        {
            _voteRepositoryMock = new Mock<IVoteRepository>();
            _voteCacheMock = new Mock<IVoteCache>();
            _loggerMock = new Mock<ILogger<VoteService>>();
            _rabbitMqProducerMock = new Mock<IRabbitMqProducer>();
            _voteService = new VoteService(_voteRepositoryMock.Object, _loggerMock.Object, _rabbitMqProducerMock.Object, _voteCacheMock.Object);
        }

        [Fact]
        public async Task CanVoteAsync_ReturnsTrue_WhenUserCanVote()
        {
            // Arrange
            var userId = "test-user";
            _voteCacheMock.Setup(v => v.CanVoteAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _voteService.CanVoteAsync(userId);

            // Assert
            Assert.True(result);
            _voteCacheMock.Verify(v => v.CanVoteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task CanVoteAsync_ReturnsFalse_WhenUserCannotVote()
        {
            // Arrange
            var userId = "test-user";
            _voteCacheMock.Setup(v => v.CanVoteAsync(userId)).ReturnsAsync(false);

            // Act
            var result = await _voteService.CanVoteAsync(userId);

            // Assert
            Assert.False(result);
            _voteCacheMock.Verify(v => v.CanVoteAsync(userId), Times.Once);
        }

        //[Fact]
        //public async Task CanVoteAsync_ThrowsArgumentNullException_WhenUserIdIsNull()
        //{
        //    // Act & Assert
        //    await Assert.ThrowsAsync<ArgumentNullException>(() => _voteService.CanVoteAsync(null));
        //}


    }
}

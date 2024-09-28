using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Voting.API.Voting.Infra.DataBase.Context;

namespace Voting.API.Voting.Infra.Redis.Cache
{
    public interface IVoteCache
    {
        Task<bool> CanVoteAsync(string message);
    }

    public class VoteCache : IVoteCache
    {
        private readonly IDistributedCache _cache;
        private const int VoteLimitPerMinute = 5; // Limite de votos por minuto

        public VoteCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> CanVoteAsync(string userId)
        {
            string cacheKey = $"VoteCount_{userId}";

            var voteCountString = await _cache.GetAsync(cacheKey);
            int voteCount = voteCountString != null ? int.Parse(voteCountString) : 0;

            if (voteCount >= VoteLimitPerMinute)
            {
                return false;
            }

            voteCount++;

            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(voteCount.ToString()), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            return true;
        }
    }
}

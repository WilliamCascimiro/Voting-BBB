using Voting.Application.DTOs.Booking;
using Voting.Domain.Interfaces;
using Voting.Infra.Queue;
using Newtonsoft.Json;
using Voting.API.Domain.Entities;

namespace Voting.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly ILogger<VoteService> _logger;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public VoteService(IVoteRepository voteRepository, ILogger<VoteService> logger, IRabbitMqProducer rabbitMqProducer)
        {
            _voteRepository = voteRepository;
            _logger = logger;
            _rabbitMqProducer = rabbitMqProducer;
        }

        public async Task<bool> CreateAsync(CreateVoteRequest createVoteRequest)
        {
            try
            {
                if (createVoteRequest == null)
                    return false;

                Vote vote = new Vote(createVoteRequest.userId, createVoteRequest.participantId, createVoteRequest.paredaoId);

                await _voteRepository.AdcionarAsync(vote);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR BBB");
                return false;
            }
        }

        public bool Create(CreateVoteRequest createVoteRequest)
        {
            try
            {
                if (createVoteRequest == null)
                    return false;

                Vote vote = new Vote(createVoteRequest.userId, createVoteRequest.participantId, createVoteRequest.paredaoId);

                _voteRepository.Adcionar(vote);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR BBB");
                return false;
            }
        }

        public bool Queue(CreateVoteRequest createVoteRequest)
        {
            try
            {
                Vote vote = new Vote(createVoteRequest.userId, createVoteRequest.participantId, createVoteRequest.paredaoId);
                var voteJson = JsonConvert.SerializeObject(vote);

                _rabbitMqProducer.Publica(voteJson);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR VOTING");
                return false;
            }
        }

        public async Task<bool> QueueAsync(CreateVoteRequest createVoteRequest)
        {
            try
            {
                Vote vote = new Vote(createVoteRequest.userId, createVoteRequest.participantId, createVoteRequest.paredaoId);
                var voteJson = JsonConvert.SerializeObject(vote);

                await _rabbitMqProducer.PublicaAsync(voteJson);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR VOTING");
                return false;
            }
        }

    }
}

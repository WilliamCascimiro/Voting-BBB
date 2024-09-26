using Voting.Application.DTOs.Booking;
using Voting.Domain.Interfaces;

namespace Voting.Application.Services
{
    public interface IVoteService
    {
        Task<bool> CreateAsync(CreateVoteRequest reserva);
        bool Create(CreateVoteRequest reserva);
        bool Queue(CreateVoteRequest reserva);
        Task<bool> QueueAsync(CreateVoteRequest reserva);
    }
}

using AutoMapper;
using Voting.API.Domain.Entities;
using Voting.Application.DTOs.Booking;

namespace Voting.Application.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Vote, CreateVoteRequest>().ReverseMap();
        }
    }
}

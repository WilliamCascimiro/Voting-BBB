using Voting.Application.DTOs.Booking;
using Voting.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;

namespace Voting.API.Controllers
{
    [ApiController]
    [Route("Paredao")]
    [AllowAnonymous]
    public class ParedaoController : ControllerBase
    {
        protected readonly IVoteService _voteService;
        private readonly ILogger<ParedaoController> _logger;
        private readonly IDistributedCache _cache;
        private const int VoteLimitPerMinute = 5; // Limite de votos por minuto

        public ParedaoController(IVoteService bookingService, ILogger<ParedaoController> logger, IDistributedCache cache)
        {
            _voteService = bookingService;
            _logger = logger;
            _cache = cache;
        }

        [HttpPost("Votos-BD-Async")]
        public async Task<IActionResult> CreateBDAsync([FromBody] CreateVoteRequest createVoteRequest)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var idRastreio = Guid.NewGuid();
                _logger.LogInformation(idRastreio + " - inicio processo");

                var sucess = await _voteService.CreateAsync(createVoteRequest);
                _logger.LogInformation(idRastreio + " - fim processo");
                _logger.LogInformation($"{idRastreio} Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");

                if (!sucess)
                    return BadRequest();
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }

        [HttpPost("Votos-BD-Sync")]
        public IActionResult Create([FromBody] CreateVoteRequest createVoteRequest)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var idRastreio = Guid.NewGuid();
                _logger.LogInformation(idRastreio + " - inicio processo");
                var sucess = _voteService.Create(createVoteRequest);
                _logger.LogInformation(idRastreio + " - fim processo");
                _logger.LogInformation($"{idRastreio} Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");

                if (!sucess)
                    return BadRequest();
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }

        [HttpPost("Votos-Queue-Async")]
        public async Task<IActionResult> CreateQueueAsync([FromBody] CreateVoteRequest createVoteRequest)
        {
            bool sucess;
            string messagemResult;
            var idRastreio = Guid.NewGuid();
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var clientIpOriginal = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {    
                _logger.LogError($"[INFO] - Inicio processo {idRastreio}");

                var canVote = await CanVoteAsync(createVoteRequest.userId.ToString());

                if (canVote)
                {
                    sucess = await _voteService.QueueAsync(createVoteRequest);
                }
                else
                {
                    sucess = false;
                    ModelState.AddModelError("Não pode votar", "Usuário excedeu o limite de votações");
                    _logger.LogInformation($"[INFO] - Não pode votar {idRastreio}");
                }

                _logger.LogError($"[INFO] - Fim processo {idRastreio}");

                if (!sucess)
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERRO] - {ex.Message}");
            }
            finally
            {
                _logger.LogInformation($"[INFO] - {idRastreio} Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");
            }

            return Ok();
        }

        [HttpPost("Votos-Queue-Sync")]
        public IActionResult CreateQueue([FromBody] CreateVoteRequest createVoteRequest)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var idRastreio = Guid.NewGuid();
                _logger.LogInformation(idRastreio + " - inicio processo");
                var sucess = _voteService.Queue(createVoteRequest);
                _logger.LogInformation(idRastreio + " - fim processo");
                _logger.LogInformation($"{idRastreio} Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");

                if (!sucess)
                    return BadRequest();
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }



        public async Task<bool> CanVoteAsync(string userId)
        {
            string cacheKey = $"VoteCount_{userId}";

            var voteCountString = await _cache.GetStringAsync(cacheKey);
            int voteCount = voteCountString != null ? int.Parse(voteCountString) : 0;

            if (voteCount >= VoteLimitPerMinute)
            {
                return false;
            }

            voteCount++;

            // Atualize o cache com a nova contagem e defina o tempo de expiração para 1 minuto
            await _cache.SetStringAsync(cacheKey, voteCount.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            return true;
        }

    }
}

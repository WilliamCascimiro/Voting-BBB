//using BookingRoom.Application.DTOs.Booking;
//using BookingRoom.Application.Services;
//using BookingRoom.Domain.Entities;
//using BookingRoom.Domain.Interfaces;
//using BookingRoom.Infra.Data.Context;
//using BookingRoom.Infra.Data.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using Moq;
//using System.Linq.Expressions;

//namespace BookingRoom.Tests
//{
//    public class BookingServiceTests_Real : IClassFixture<DatabaseFixture>
//    {
//        private readonly BookingService _bookingService;
//        private readonly IRoomTimeSlotRepository _timeSlotRepository;
//        private readonly BBBDbContext _context;
//        private IDbContextTransaction _transaction;
//        private readonly Mock<IUnitOfWork> _mockUnitOfMork;
//        private readonly Mock<IRoomTimeSlotRepository> _timeSlotRepositoryMock;

//        public BookingServiceTests_Real(DatabaseFixture fixture)
//        {
//            _mockUnitOfMork = new Mock<IUnitOfWork>();
//            _timeSlotRepositoryMock = new Mock<IRoomTimeSlotRepository>();

//            _context = fixture.DbContext;
//            _timeSlotRepository = new RoomTimeSlotRepository(_context);
//            _bookingService = new BookingService(
//                new VoteRepository(_context),
//                new RoomTimeSlotRepository(_context),
//                new UnitOfWork(_context)
//            );

//            //_transaction = _context.Database.BeginTransaction();
//        }

//        [Fact]
//        public async Task Update_SimultaneousBooking_ShouldHandleConcurrency()
//        {
//            var bookingId = Guid.NewGuid();
//            var timeSlotId = Guid.NewGuid();

//            timeSlotId = new Guid("E144CE7C-DD8A-418D-B243-005554C21C18");
//            var bookingId1 = new Guid("68416CF5-C392-4E0D-BD85-E3095A4AE22C");
//            var bookingId2 = new Guid("9D690038-D935-46B7-BB31-E59AB4A59050");
//            //timeSlotId = "FB7CDF8D-9E7D-4B0D-BFEA-053DE76BAC38";

//            var timeSlots = await _timeSlotRepository.Buscar(x => !x.IsBooked);
//            var timeSlotToAdd = timeSlots.FirstOrDefault();
//            var listTimeSlot = new List<TimeSlot>();
//            listTimeSlot.Add(timeSlotToAdd);

//            //var timeSlotToAdd = new RoomTimeSlot
//            //{
//            //    Id = timeSlotId,
//            //    IsBooked = true,
//            //    BookingId = bookingId,
//            //    RoomId = new Guid("DE9338CA-7D90-4735-9F43-E51C4BBEB7DB"),
//            //};

//            var timeSlotToRemove = new TimeSlot();
//            //{
//            //    Id = timeSlotId,
//            //    IsBooked = false,
//            //    BookingId = null
//            //};

//            var bookings = await _bookingService.ListAllBookings();
//            //var booking = bookings.FirstOrDefault();
//            //var booking1 = bookings.Where(x => x.Id == bookingId1).FirstOrDefault();
//            var booking2 = bookings.Where(x => x.Id == bookingId2).FirstOrDefault();

//            var booking1 = new Booking(new Guid(), new Guid());

//            //Task<int> valorSaveChanges = 1;
//            _mockUnitOfMork.Setup(x => x.BeginTransactionAsync());
//            _mockUnitOfMork.Setup(x => x.SaveChangesAndCommitAsync()).Returns(It.IsAny<Task<int>>());
//            _timeSlotRepositoryMock.Setup(x => x.Update(timeSlotToRemove));

//            // Simulate two parallel update requests
//            var task1 = await _bookingService.UpdateTransactional(listTimeSlot, null, booking1);
//            Thread.Sleep(10000);
//            //var task2 = _bookingService.UpdateTransactional(listTimeSlot, null, booking2);

//            // Act
//            //var results = await Task.WhenAll(task1, task2);

//            // Assert
//            //var successfulUpdates = results.Count(result => result.IsSuccess);
//            //var failedUpdates = results.Count(result => !result.IsSuccess);

//            //Assert.Equal(1, successfulUpdates); // Only one update should succeed
//            //Assert.Equal(1, failedUpdates);     // The other should fail
//            Assert.True(true);
//        }

//        public void Dispose()
//        {
//            // Rollback transaction
//            //_transaction.Rollback();
//            //_transaction.Dispose();
//        }

//    }
//}

using API.Contracts;
using API.DTOs.Booking;
using API.Models;
using API.Utilities.Enums;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }


        public IEnumerable<BookingDetailDto>? BookingsDetail()
        {
            var bookingsDetail = _bookingRepository.GetAll();

            if (bookingsDetail == null)
            {
                return null;
            }

            var employeesDetail = _employeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            var detailBookings = (from booking in bookingsDetail
                                  join employee in employeesDetail on booking.EmployeeGuid equals employee.Guid
                                  join room in rooms on booking.RoomGuid equals room.Guid
                                  select new BookingDetailDto
                                  {
                                      Guid = booking.Guid,
                                      BookedNik = employee.Nik,
                                      BookedBy = employee.FirstName + "" + employee.LastName,
                                      StartDate = DateTime.Now,
                                      EndDate = booking.EndDate,
                                      RoomName = room.Name,
                                      Status = booking.Status,
                                  }).ToList();
            if (!detailBookings.Any())
            {
                return null;
            }

            return detailBookings;
        }

       
        public BookingDetailDto? BookingDetailByGuid(Guid guid)
        {
            var booking = BookingsDetail();

            var bookingByGuid = booking!.FirstOrDefault(booking => booking.Guid == guid);

            return bookingByGuid;
        }


        public IEnumerable<GetBookingDto>? GetBooking()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return null;
            }

            var toDto = bookings.Select(booking => new GetBookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Remarks = booking.Remarks,
                Status = booking.Status,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            }).ToList();

            return toDto;
        }


        public GetBookingDto? GetBooking(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);

            if (booking is null)
            {
                return null;
            }

            var toDto = new GetBookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Remarks = booking.Remarks,
                Status = booking.Status,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            };

            return toDto;
        }


        public IEnumerable<GetBookedByDto> GetRoomsInUseToday()
        {
            var bookings = _bookingRepository.GetAll();
            if (bookings == null)
            {
                return null; 
            }

            var employees = _employeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            var detailBookings = (
                from booking in bookings
                join employee in employees on booking.EmployeeGuid equals employee.Guid
                join room in rooms on booking.RoomGuid equals room.Guid
                where booking.StartDate <= DateTime.Now && booking.EndDate >= DateTime.Now
                select new GetBookedByDto
                {
                    BookingGuid = booking.Guid,
                    RoomName = room.Name,
                    Status = booking.Status,
                    Floor = room.Floor,
                    BookedBy = employee.FirstName + " " + employee.LastName,
                }).ToList();

            if (!detailBookings.Any())
            {
                return null; 
            }


            var toDto = detailBookings.Select(detailBookings => new GetBookedByDto
            {
                BookingGuid = detailBookings.BookingGuid,
                RoomName = detailBookings.RoomName,
                Status = detailBookings.Status,
                Floor = detailBookings.Floor,
                BookedBy = detailBookings.BookedBy
            });
            return toDto;
        }


        public GetBookingDto? CreateBooking(NewBookingDto newBookingDto)
        {
            var booking = new Booking // Membuat objek dari sebuah kelas yang miliki properti guid dll.
            {
                Guid = new Guid(),
                StartDate = newBookingDto.StartDate,
                EndDate = newBookingDto.EndDate,
                Remarks = newBookingDto.Remarks,
                Status = newBookingDto.Status,
                RoomGuid = newBookingDto.RoomGuid,
                EmployeeGuid = newBookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdBooking = _bookingRepository.Create(booking);
            if (createdBooking is null)
            {
                return null;
            }

            var toDto = new GetBookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Remarks = booking.Remarks,
                Status = booking.Status,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            };

            return toDto;
        }


        public int UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var isExist = _bookingRepository.IsExist(updateBookingDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getBooking = _bookingRepository.GetByGuid(updateBookingDto.Guid);

            var booking = new Booking
            {

                Guid = updateBookingDto.Guid,
                StartDate = updateBookingDto.StartDate,
                EndDate = updateBookingDto.EndDate,
                Remarks = updateBookingDto.Remarks,
                Status = updateBookingDto.Status,
                RoomGuid = updateBookingDto.RoomGuid,
                EmployeeGuid = updateBookingDto.EmployeeGuid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getBooking!.CreatedDate
            };

            var isUpdate = _bookingRepository.Update(booking);
            if (!isUpdate)
            {
                return 0;
            }

            return 1;
        }


        public int DeleteBooking(Guid guid)
        {
            var isExist = _bookingRepository.IsExist(guid);
            if (!isExist)
            {
                return -1;
            }

            var booking = _bookingRepository.GetByGuid(guid);
            var isDelete = _bookingRepository.Delete(booking!);
            if (!isDelete)
            {
                return 0;
            }

            return 1;
        }
    }
}

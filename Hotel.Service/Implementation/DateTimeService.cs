using Hotel.Service.Contract;
using System;

namespace Hotel.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Hotel.Domain.Entities;
using Hotel.Persistence;

namespace Hotel.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertHotelIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var hotel = new Domain.Entities.Hotel();
            context.Hotels.Add(hotel);
            Assert.AreEqual(EntityState.Added, context.Entry(hotel).State);
        }


        [Test]
        public void CanInsertRoomIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var room = new Domain.Entities.Room();
            context.Rooms.Add(room);
            Assert.AreEqual(EntityState.Added, context.Entry(room).State);
        }

        [Test]
        public void CanInsertReviewIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var review = new Domain.Entities.Review();
            context.Reviews.Add(review);
            Assert.AreEqual(EntityState.Added, context.Entry(review).State);
        }
    }
}

using Beeater.Contracts;
using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beeater.Persistence.Repositories
{
    public class SeatRepository : RepositoryBase<Seat>, ISeatRepository
    {
        public SeatRepository(beeaterContext context)
            : base(context)
        {

        }

        public void CreateSeats(int[] rows, int theaterId)
        {
            var seats = new List<Seat>();
            int row = 0;
            foreach (var seatCount in rows)
            {
                row++;
                for (int i = 1; i <= seatCount; i++)
                {
                    seats.Add(new Seat() { Number = i, Row = row, TheaterId = theaterId });
                }
            }
            Create(seats);
        }
    }
}

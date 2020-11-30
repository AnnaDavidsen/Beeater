using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Movie
    {
        public Movie()
        {
            Ratings = new HashSet<Rating>();
            Shows = new HashSet<Show>();
            Trailers = new HashSet<Trailer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? AgeRating { get; set; }
        public int GenreId { get; set; }
        public string MoviePoster { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}

using Music.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Music.Services.Models
{
    public class SongModel
    {
        public static Expression<Func<Song, SongModel>> FromSong
        {
            get
            {
                return s => new SongModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArtistId = s.ArtistId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int ArtistId { get; set; }
    }
}
using Music.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Music.Services.Models
{
    public class ArtistModel
    {
        public static Expression<Func<Artist, ArtistModel>> FromArtist
        {
            get
            {
                return a => new ArtistModel
                {
                    Id = a.Id,
                    Name = a.Name
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
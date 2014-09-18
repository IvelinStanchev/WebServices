using Music.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Music.Services.Models
{
    public class AlbumModel
    {
        public static Expression<Func<Album, AlbumModel>> FromAlbum
        {
            get
            {
                return a => new AlbumModel
                {
                    Id = a.Id,
                    Title = a.Title
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
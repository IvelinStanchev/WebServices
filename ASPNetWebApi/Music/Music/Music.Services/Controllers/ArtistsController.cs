using Music.Data;
using Music.Models;
using Music.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Music.Services.Controllers
{
    public class ArtistsController : ApiController
    {
        private IMusicData data;

        public ArtistsController()
            : this(new MusicData())
        {
        }

        public ArtistsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.data.Artists.All()
                .Select(ArtistModel.FromArtist);

            return Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var artists = this.data.Artists
                .All()
                .Where(a => a.Id == id)
                .Select(ArtistModel.FromArtist)
                .FirstOrDefault();

            if (artists == null)
            {
                return BadRequest("Artist does not exist!");
            }

            return Ok(artists);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArtist = new Artist
            {
                Name = artist.Name
            };

            this.data.Artists.Add(newArtist);
            this.data.SaveChanges();

            artist.Id = newArtist.Id;

            return Ok(artist);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingArtist = this.data.Artists.All().First(a => a.Id == id);
            if (existingArtist == null)
            {
                return BadRequest("Such artist does not exist!");
            }

            existingArtist.Name = artist.Name;
            this.data.SaveChanges();

            artist.Id = id;

            return Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingArtist = this.data.Artists.All().First(a => a.Id == id);
            if (existingArtist == null)
            {
                return BadRequest("Such artist does not exist!");
            }

            this.data.Artists.Delete(existingArtist);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddSong(int id, int songId)
        {
            var artist = this.data.Artists.All().FirstOrDefault(a => a.Id == id);
            if (artist == null)
            {
                return BadRequest("Such artist does not exist!");
            }

            var song = this.data.Songs.All().FirstOrDefault(s => s.Id == songId);
            if (song == null)
            {
                return BadRequest("Such song does not exist!");
            }

            artist.Songs.Add(song);
            this.data.SaveChanges();

            return Ok();
        }
    }
}

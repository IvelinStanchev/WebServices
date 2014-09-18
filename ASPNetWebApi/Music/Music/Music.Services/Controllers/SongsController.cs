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
    public class SongsController : ApiController
    {
        private IMusicData data;

        public SongsController()
            : this(new MusicData())
        {
        }

        public SongsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.data.Songs.All()
                .Select(SongModel.FromSong);

            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var songs = this.data.Songs
                .All()
                .Where(s => s.Id == id)
                .Select(SongModel.FromSong)
                .FirstOrDefault();

            if (songs == null)
            {
                return BadRequest("Song does not exist!");
            }

            return Ok(songs);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = this.data.Artists.All().First(a => a.Id == song.ArtistId);

            if (artist == null)
            {
                return BadRequest("Such artist does not exist!");
            }

            var newSong = new Song
            {
                Title = song.Title,
                ArtistId = song.ArtistId
            };

            this.data.Songs.Add(newSong);
            this.data.SaveChanges();

            song.Id = newSong.Id;

            return Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSong = this.data.Songs.All().First(s => s.Id == id);
            if (existingSong == null)
            {
                return BadRequest("Such song does not exist!");
            }

            existingSong.Title = song.Title;
            existingSong.ArtistId = song.ArtistId;
            this.data.SaveChanges();

            song.Id = id;

            return Ok(song);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingSong = this.data.Songs.All().First(s => s.Id == id);
            if (existingSong == null)
            {
                return BadRequest("Such song does not exist!");
            }

            this.data.Songs.Delete(existingSong);
            this.data.SaveChanges();

            return Ok();
        }
    }
}

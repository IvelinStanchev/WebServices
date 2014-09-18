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
    public class AlbumsController : ApiController
    {
        private IMusicData data;

        public AlbumsController()
            : this(new MusicData())
        {
        }

        public AlbumsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.data.Albums.All()
                .Select(AlbumModel.FromAlbum);

            return Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var albums = this.data.Albums
                .All()
                .Where(a => a.Id == id)
                .Select(AlbumModel.FromAlbum)
                .FirstOrDefault();

            if (albums == null)
            {
                return BadRequest("Album does not exist!");
            }

            return Ok(albums);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAlbum = new Album
            {
                Title = album.Title
            };

            this.data.Albums.Add(newAlbum);
            this.data.SaveChanges();

            album.Id = newAlbum.Id;

            return Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAlbum = this.data.Albums.All().First(a => a.Id == id);
            if (existingAlbum == null)
            {
                return BadRequest("Such album does not exist!");
            }

            existingAlbum.Title = album.Title;
            this.data.SaveChanges();

            album.Id = id;

            return Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingAlbum = this.data.Albums.All().First(a => a.Id == id);
            if (existingAlbum == null)
            {
                return BadRequest("Such album does not exist!");
            }

            this.data.Albums.Delete(existingAlbum);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddArtist(int id, int artistId)
        {
            var album = this.data.Albums.All().FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return BadRequest("Such album does not exist!");
            }

            var artist = this.data.Artists.All().FirstOrDefault(a => a.Id == artistId);
            if (artist == null)
            {
                return BadRequest("Such artist does not exist!");
            }

            album.Artists.Add(artist);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddSong(int id, int songId)
        {
            var album = this.data.Albums.All().FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return BadRequest("Such album does not exist!");
            }

            var song = this.data.Songs.All().FirstOrDefault(s => s.Id == songId);
            if (song == null)
            {
                return BadRequest("Such song does not exist!");
            }

            album.Songs.Add(song);
            this.data.SaveChanges();

            return Ok();
        }
    }
}

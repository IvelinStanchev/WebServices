namespace Music.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Artist
    {
        private ICollection<Song> songs;

        public Artist()
        {
            this.songs = new HashSet<Song>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<Song> Songs
        {
            get
            {
                return this.songs;
            }
            set
            {
                this.songs = value;
            }
        }
    }
}

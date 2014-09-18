namespace Music.Data
{
    using Music.Data.Repositories;
    using Music.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMusicData
    {
        IRepository<Album> Albums { get; }

        IRepository<Artist> Artists { get; }

        IRepository<Song> Songs { get; }

        void SaveChanges();
    }
}

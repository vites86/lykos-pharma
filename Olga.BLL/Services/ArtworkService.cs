using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
public class ArtworkService: IArtwork
    {

        IUnitOfWorkGeneral Database { get; set; }
        //private ArtworkRepository Database { get; set; }

        public ArtworkService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ArtworkDTO artworkDto)
        {
            var ArtworkDto = Mapper.Map<ArtworkDTO, Artwork>(artworkDto);
            Database.Artworks.Create(ArtworkDto);
        }

        public ArtworkDTO GetItem(int id)
        {
            return Mapper.Map<Artwork, ArtworkDTO>(Database.Artworks.Get(id));
        }

        public IEnumerable<ArtworkDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Artwork>, IEnumerable<ArtworkDTO>>(Database.Artworks.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.Artworks.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Artworks.Commit();
        }

    }
}

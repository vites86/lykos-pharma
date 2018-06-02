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
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
public class ArtworkService: IArtwork
    {

        private ArtworkRepository Database { get; set; }

        public ArtworkService(ProductContext context)
        {
            Database = new ArtworkRepository(context);
        }

        public void AddItem(ArtworkDTO artworkDto)
        {
            var ArtworkDto = Mapper.Map<ArtworkDTO, Artwork>(artworkDto);
            Database.Create(ArtworkDto);
        }

        public ArtworkDTO GetItem(int id)
        {
            return Mapper.Map<Artwork, ArtworkDTO>(Database.Get(id));
        }

        public IEnumerable<ArtworkDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Artwork>, IEnumerable<ArtworkDTO>>(Database.GetAll().ToList());
        }

        public void DeleteItem(int id)
        {
            Database.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
        public void Commit()
        {
            Database.Commit();
        }

    }
}

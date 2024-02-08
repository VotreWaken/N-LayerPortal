using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class ImageService : IImageService
    {
        IUnitOfWorks Database { get; set; }

        public ImageService(IUnitOfWorks unitOfWork)
        {
            Database = unitOfWork;
        }
        public async Task<List<ImageDTO>> GetAll()
        {
            var images = await Database.Image.GetAll();
            return images.Select(image => new ImageDTO
            {
                Id = image.Id,
                Path = image.Path
            }).ToList();
        }
        public async Task<ImageDTO> GetById(int id)
        {
            var imageEntity = await Database.Image.GetById(id);

            if (imageEntity == null)
            {
                throw new Exception($"Image with id {id} not found.");
            }

            return new ImageDTO
            {
                Id = imageEntity.Id,
                Path = imageEntity.Path
            };
        }

        public async Task<ImageDTO> Create(ImageDTO image)
        {
            var imageEntity = new Image
            {
                Path = image.Path
            };
			var createdImage = await Database.Image.Create(imageEntity);

			return new ImageDTO
			{
				Id = createdImage.Id,
				Path = createdImage.Path
			};
		}

        public async Task Update(ImageDTO image)
        {
            var imageEntity = await Database.Image.GetById(image.Id);
            if (imageEntity == null)
            {
                throw new Exception($"Image with id {image.Id} not found.");
            }
            imageEntity.Path = image.Path;
            await Database.Image.Update(imageEntity);
        }

        public async Task Delete(int id)
        {
            await Database.Image.Delete(id);
        }
    }
}

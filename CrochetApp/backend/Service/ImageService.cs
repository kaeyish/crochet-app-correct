using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class ImageService
    {
        private IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public Image GetImageById(int id)
        {
            return _imageRepository.GetImageById(id);
        }

        public Image GetImageByURL(string url)
        {
            return _imageRepository.GetImageByURL(url);
        }

        public List<Image> GetAllImages()
        {
            return _imageRepository.GetAllImages();
        }

        public void AddImage(string url)
        {
            _imageRepository.AddImage(url);
        }

        public void UpdateImage(int id, string url)
        {
            _imageRepository.UpdateImage(id, url);
        }

        public Image DeleteImage(int id)
        {
            return _imageRepository.DeleteImage(id);
        }


    }
}

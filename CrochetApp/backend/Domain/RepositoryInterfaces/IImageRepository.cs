using System;
using CrochetApp.backend.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Image = CrochetApp.backend.Domain.Model.Image;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        Image GetImageById(int id);
        Image GetImageByURL(string url );

        List<Image> GetAllImages();

        void AddImage(string url);

        void UpdateImage(int id, string url);

        Image DeleteImage(int id);
    }
}

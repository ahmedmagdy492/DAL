using DAL;
using DAL.Models;
using DAL.Repository;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageManager : ImageService
    {
        private readonly IRepository<Image> _imageRepo;
        private readonly UnitOfWork _unitOfWork;

        public ImageManager()
        {
            _unitOfWork = new UnitOfWork(DBContextCreator.DbContext);
            _imageRepo = _unitOfWork.CreateImageRepo();
        }

        public Image Add(Image model)
        {
            var image = _imageRepo.Add(model);
            _unitOfWork.Commit();
            return image;
        }

        public void Delete(Image model)
        {
            _imageRepo.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<Image> GetAll()
        {
            return _imageRepo.GetAll();
        }

        public Image GetById(int id)
        {
            return _imageRepo.GetById(id);
        }

        public Image Update(Image model)
        {
            var updatedImage = _imageRepo.Update(model);
            _unitOfWork.Commit();
            return updatedImage;
        }
    }
}

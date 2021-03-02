using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage, IFormFile formFile)
        {
            var imagecount = _carImageDal.GetAll(i=>i.CarId==carImage.CarId).Count;
            if (imagecount>=5)
            {
                return new ErrorResult(Messages.CheckIfImageLimit);
            }
            carImage.ImagePath = ImageFileHelper.Add(formFile);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Delete(CarImage carImage ,IFormFile formFile)
        {
            var image = _carImageDal.Get(c => c.Id==carImage.Id);
            if (image == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }

            ImageFileHelper.Delete(image.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.İmageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll(int id)
        {
            
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetById(int Id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckCarImageExists(Id),Messages.ListedByCarId);
        }

        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            var uimage = (_carImageDal.Get(i=>i.Id==carImage.Id).ImagePath);
            carImage.ImagePath = ImageFileHelper.Update(uimage,formFile); 
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }
        private List<CarImage> CheckCarImageExists(int carId)
        {
            var result = _carImageDal.GetAll(x => x.CarId == carId).Any();
            string path = @"\wwwroot\Images\default.jpg";
            if (!result)
            {
                List<CarImage> carImages = new List<CarImage>()
                {
                   new CarImage{CarId = carId,ImagePath =path}
                };
                return carImages;
            }
            return _carImageDal.GetAll(x => x.CarId == carId);
        }

    }
}

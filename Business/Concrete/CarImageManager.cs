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

        public IResult Delete(CarImage carImage ,int id)
        {
            var image = _carImageDal.Get(i=>i.Id==id);
            _carImageDal.Delete(image);
            carImage.ImagePath = ImageFileHelper.Delete();
            return new SuccessResult("Resim başarıyla silinmiştir");
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<CarImage> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            var image = _carImageDal.Get(i=>i.Id==carImage.Id);
            if (image==null)
            {
                return new ErrorResult();
            }
            carImage.ImagePath = ImageFileHelper.Update(formFile);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }


    }
}

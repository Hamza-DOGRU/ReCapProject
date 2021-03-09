using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;

        public CarManager(ICarDal carDal,IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(CarValidator))] //Add metodunu doğrula CarValidator kullanarak
        public IResult Add(Car car)
        {
            IResult result=BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId),CheckIfCarModelYear(car.ModelYear),CheckIfBrandLimit());
            if (result!=null)
            {
                return result;
            } 
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        //[CacheAspect] 
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 14)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == carId), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id), Messages.CarsListed);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }





        //Sistemde bir markaya ait en fazla 15 araç bulunabilir.
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c=>c.BrandId==brandId).Count;
            if (result>=15)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }
        //Sisteme yeni kayıt olacak araçların model yılının 2020'den yüksek olması gerekiyor.
        private IResult CheckIfCarModelYear(decimal modelYear)
        {
            if (modelYear <=2020)
            {
                return new ErrorResult(Messages.CheckIfCarModelYearError);
            }
            return new SuccessResult();
        }
        //Sistemde kayıtlı 20 farklı marka var ise yeni kayıt alınmaz.
        private IResult CheckIfBrandLimit()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count>20)
            {
                return new ErrorResult(Messages.CheckIfBrandLimit);
            }
            return new SuccessResult();
        }
    }
}

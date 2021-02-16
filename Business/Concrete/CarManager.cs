using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.ModelYear>2020)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
                return new ErrorResult(Messages.CarModelYearInvalid);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==15)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>( _carDal.Get(p=>p.Id==carId),Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour==19)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>( _carDal.GetCarDetails(),Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(p => p.BrandId == id),Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {

            return new SuccessDataResult<List<Car>>( _carDal.GetAll(p => p.ColorId == id),Messages.CarsListed);
        }
    }
}

using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll(int id);
        IDataResult<List<CarImage>> GetById(int Id);
        IResult Add(CarImage carImage,IFormFile formFile);
        IResult Delete(CarImage carImage,IFormFile formFile);
        IResult Update(CarImage carImage,IFormFile formFile);
    }
}

using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //bir validator type verilir
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir");//Gönderilen Type IValidator değilse hata fırlat
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);  //Reflection--Çalışma anında CarValidatorün bir instance oluştur
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  //CarValidatorün çalışma tipini bul veri tipini
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  //İlgili methodun parametrelerini bul
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); //ValidationTool kullanarak doğrula
            }
        }
    }
}

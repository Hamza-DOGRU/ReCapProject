using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araç sisteme eklendi";
        public static string CarModelYearInvalid = "Araç model yılı 2020'den büyük olmalı";
        public static string MaintenanceTime="Sistem bakımda";
        public static string CarsListed="Araçlar listelendi";
        public static string BrandsListed = "Markalar listelendi";
        public static string ColorsListed = "Renkler listelendi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserUpdated = "Kullanıcı bilgileri güncellendi";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string RentalNotAdded = "Kiralama yapılamadı. Araç başka bir kullanıcıda";
        public static string RentalAdded = "Kiralama başarıyla eklendi";
    }
}

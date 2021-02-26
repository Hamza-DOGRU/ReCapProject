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
        public static string CarDeleted = "Araç sistemden silindi";
        public static string CarUpdated = "Araç bilgileri sistemde güncellendi";
        public static string BrandAdded = "Marka eklendi";
        public static string BrandDeleted = "Marka sistemden silindi";
        public static string BrandUpdated = "Marka bilgileri güncellendi";
        public static string CustomerListed = "Müşteriler listelendi";
        public static string CustomerUpdated = "Müşteri bilgileri güncellendi";
        public static string UserListed = "Kullanıcılar Listelendi";
        public static string RentalDeleted = "Kiralama kayıtı başarıyla silindi";
        public static string RentalUpdated = "Kiralama kayıtı başarıyla güncellendi";
        public static string RentalListed = "Kiralama kayıtları başarıyla listelendi";
        public static string CarCountOfBrandError="Bu markada araç limitinizi doldurdunuz";
        public static string CheckIfCarModelYearError="Girilen aracın model yılı 2020'den büyük olmalı";
        public static string CheckIfBrandLimit="Sistemde kayıtlı marka sayısı 20'dir yeni kayıt eklenemez";
    }
}

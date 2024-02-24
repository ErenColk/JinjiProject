using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Constants
{
    public class Messages
    {
        public const string CreateAdminSuccess = "Admin başarıyla oluşturuldu.";
        public const string CreateAdminError = "Admin oluşturulamadı.";
        public const string CreateAdminRepoError = "Admin veritabanına eklenirken hata oluştu.";
        public const string AdminNotFound = "Admin bulunamadı.";
        public const string AdminFoundSuccess = "Admin başarıyla bulundu.";
        public const string AdminListedSuccess = "Admin başarıyla listelendi.";
        public const string AdminFilteredError = "Admin bulunamadı.";
        public const string AdminFilteredSuccess = "Admin başarıyla bulundu.";
        public const string AdminDeletedSuccess = "Admin başarıyla silindi.";
        public const string AdminDeletedRepoError = "Admin veritabanı sebebiyle silinemedi.";
        public const string UpdateAdminError = "Admin güncellenemedi.";
        public const string UpdateAdminSuccess = "Admin başarıyla güncellendi.";
        public const string UpdateAdminRepoError = "Admin veritabanı sebebiyle güncellenemedi.";
    }
}

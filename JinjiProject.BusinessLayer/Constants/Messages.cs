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


        public const string CreateBrandSuccess = "Marka başarıyla oluşturuldu.";
        public const string CreateBrandError = "Marka oluşturulamadı.";
        public const string CreateBrandRepoError = "Marka veritabanına eklenirken hata oluştu.";
        public const string BrandNotFound = "Marka bulunamadı.";
        public const string BrandFoundSuccess = "Marka başarıyla bulundu.";
        public const string BrandListedSuccess = "Marka başarıyla listelendi.";
        public const string BrandFilteredError = "Marka bulunamadı.";
        public const string BrandFilteredSuccess = "Marka başarıyla bulundu.";
        public const string BrandDeletedSuccess = "Marka başarıyla silindi.";
        public const string BrandDeletedRepoError = "Marka veritabanı sebebiyle silinemedi.";
        public const string UpdateBrandError = "Marka güncellenemedi.";
        public const string UpdateBrandSuccess = "Marka başarıyla güncellendi.";
        public const string UpdateBrandRepoError = "Marka veritabanı sebebiyle güncellenemedi.";

        public const string CreateMaterialSuccess = "Malzeme başarıyla oluşturuldu.";
        public const string CreateMaterialError = "Malzeme oluşturulamadı.";
        public const string CreateMaterialRepoError = "Malzeme veritabanına eklenirken hata oluştu.";
        public const string MaterialNotFound = "Malzeme bulunamadı.";
        public const string MaterialFoundSuccess = "Malzeme başarıyla bulundu.";
        public const string MaterialListedSuccess = "Malzeme başarıyla listelendi.";
        public const string MaterialFilteredError = "Malzeme bulunamadı.";
        public const string MaterialFilteredSuccess = "Malzeme başarıyla bulundu.";
        public const string MaterialDeletedSuccess = "Malzeme başarıyla silindi.";
        public const string MaterialDeletedRepoError = "Malzeme veritabanı sebebiyle silinemedi.";
        public const string UpdateMaterialError = "Malzeme güncellenemedi.";
        public const string UpdateMaterialSuccess = "Malzeme başarıyla güncellendi.";
        public const string UpdateMaterialRepoError = "Malzeme veritabanı sebebiyle güncellenemedi.";
        public const string CreateCategoryError = "Kategori yüklenirken hata oluştu.";
       
        public const string CategoryFilteredError = "Kategori bulunamadı.";
        public const string CategoryFilteredSuccess = "Kategori başarıyla bulundu.";
        public const string CategoryDeletedSuccess = "Kategori silme işlemi başarılı.";
        public const string CategoryDeletedRepoError = "Kategori silme işlemi başarısız.";
        public const string CategoryHardDeletedSuccess = "Kategori tamamen silinmiştir.";
        public const string CategoryHardDeletedRepoError = "Kategori silme işlemi başarısız.";
        public const string UpdateCategoryError = "Kategori güncelleme işlemi başarısız.";


        public const string UpdateCategorySuccess = "Kategori güncelleme işlemi başarılı.";
        public const string UpdateCategoryRepoError = "Kategori güncellerken sunucu tarafında hata oluştu.";

        public const string UpdateListCategorySuccess = "Kategorilerden bazıları ana sayfaya eklendi";
        public const string UpdateListCategoryRepoError = "Kategorilerden bazıları ana sayfaya eklenemedi.";
        public const string NoCategoriesToUpdateError = "Güncellenecek kategori bulunamadı.";


        public const string CreateProductError = "Ürün yüklenirken hata oluştu.";
        public const string CreateProductSuccess = "Ürün başarıyla oluşturuldu";
        public const string CreateProductRepoError = "Ürün oluştururken sunucu tarafında hata oluştu.";
        public const string ProductNotFound = "Ürün bulunamadı";
        public const string ProductFoundSuccess = "Ürün başarıyla bulundu.";
        public const string ProductFilteredError = "Ürün bulunamadı.";
        public const string ProductFilteredSuccess = "Ürün başarıyla bulundu.";
        public const string ProductDeletedSuccess = "Ürün silme işlemi başarılı.";
        public const string ProductDeletedRepoError = "Ürün silme işlemi başarısız.";
        public const string UpdateProductError = "Ürün güncelleme işlemi başarısız.";
        public const string UpdateProductSuccess = "Ürün güncelleme işlemi başarılı.";
        public const string UpdateProductRepoError = "Ürün güncellerken sunucu tarafında hata oluştu.";


        public const string ProductListedEmpty = "Ürün Listesi Boş";
        public const string ProductListedSuccess = "Ürün listesi başarıyla yüklendi.";


        public const string CreateCategorySuccess = "Kategori başarıyla oluşturuldu";
        public const string CreateCategoryRepoError = "Kategori oluştururken sunucu tarafında hata oluştu.";
        public const string CategoryListedSuccess = "Kategoriler Listelendi";
        public const string CategoryListedEmpty= "Kategori Listesi Boş";
		public const string CategoryNotFound = "Kategori bulunamadı";
		public const string CategoryFoundSuccess = "Kategori başarıyla bulundu.";


        public const string CreateGenreSuccess = "Kategori türü başarıyla oluşturuldu.";
        public const string CreateGenreError = "Kategori türü oluşturulamadı.";
        public const string CreateGenreRepoError = "Kategori türü veritabanına eklenirken hata oluştu.";
        public const string GenreNotFound = "Kategori türü bulunamadı.";
        public const string GenreFoundSuccess = "Kategori türü başarıyla bulundu.";
        public const string GenreListedSuccess = "Kategori türü başarıyla listelendi.";
        public const string GenreFilteredError = "Kategori türü bulunamadı.";
        public const string GenreFilteredSuccess = "Kategori türü başarıyla bulundu.";
        public const string GenreDeletedSuccess = "Kategori türü başarıyla silindi.";
        public const string GenreDeletedRepoError = "Kategori türü veritabanı sebebiyle silinemedi.";
        public const string UpdateGenreError = "Kategori türü güncellenemedi.";
        public const string UpdateGenreSuccess = "Kategori türü başarıyla güncellendi.";
        public const string UpdateGenreRepoError = "Kategori türü veritabanı sebebiyle güncellenemedi.";
        public const string NoGenresToUpdateError = "Güncellenecek kategori türü bulunamadı.";
        public const string UpdateListGenreSuccess = "Kategori türlerinden bazıları ana sayfaya eklendi";
        public const string UpdateListGenreRepoError = "Kategori türlerinden bazıları ana sayfaya eklenemedi.";


        public const string CreateSubscriberSuccess = "Abone olma başarılı.";
        public const string CreateSubscriberError = "Abone olunamadı.";
        public const string CreateSubscriberRepoError = "Abone olunurken hata oluştu.Tekrar deneyiniz.";
        public const string SubscriberNotFound = "Abone bulunamadı.";
        public const string SubscriberFoundSuccess = "Abone başarıyla bulundu.";
        public const string SubscriberListedSuccess = "Abone başarıyla listelendi.";
        public const string SubscriberFilteredError = "Abone bulunamadı.";
        public const string SubscriberFilteredSuccess = "Abone başarıyla bulundu.";
        public const string SubscriberDeletedSuccess = "Abone başarıyla silindi.";
        public const string SubscriberDeletedRepoError = "Abone veritabanı sebebiyle silinemedi.";
        public const string UpdateSubscriberError = "Abone güncellenemedi.";
        public const string UpdateSubscriberSuccess = "Abone başarıyla güncellendi.";
        public const string UpdateSubscriberRepoError = "Abone veritabanı sebebiyle güncellenemedi.";

    }
}

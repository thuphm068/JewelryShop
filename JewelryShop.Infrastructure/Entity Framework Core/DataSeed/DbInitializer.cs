using JewelryShop.Domain.Entities;

namespace JewelryShop.Infrastructure.Entity_Framework_Core.DataSeed
{
    public class DbInitializer
    {

        public static void Initialize(JewelryShopDBContext context)
        {
            context.Database.EnsureCreated();




            if (!context._warrantys.Any())
            {
                var warranty1 = new Warranty { Period = 6, Description = "Bảo hành 6 tháng đầu với mọi sai hỏng do lỗi kỹ thuật chế tác (KHÔNG áp dụng cho sản phẩm giảm giá và sản phẩm gia công theo yêu cầu; KHÔNG áp dụng cho nguyên nhân như khách hàng bị ứng bạc, đổi ý, té ngã móp méo)" };
                var warranty2 = new Warranty { Period = 12, Description = "Bảo hành 12 tháng đầu với mọi sai hỏng do lỗi kỹ thuật chế tác (KHÔNG áp dụng cho sản phẩm giảm giá và sản phẩm gia công theo yêu cầu; KHÔNG áp dụng cho nguyên nhân như khách hàng bị ứng bạc, đổi ý, té ngã móp méo)" };
                var warrantys = new Warranty[] { warranty1, warranty2 };
                foreach (Warranty s in warrantys)
                {
                    context._warrantys.Add(s);
                }


                if (!context._categories.Any())
                {
                    var cate1 = new Category { Name = "Nhẫn bạc" };
                    var cate2 = new Category { Name = "Khuyên tai" };
                    var cate3 = new Category { Name = "Dây chuyền" };
                    var cate4 = new Category { Name = "Vòng tay" };
                    var categories = new Category[]
                    {
                    cate1 , cate2, cate3, cate4
                    };


                    context._categories.AddRange(categories);

                    
                    //context.SaveChanges();

                    var subcate1 = new SubCategory { CategoryId = cate1.Id, Name = "Nhẫn nữ" };
                    var subcate2 = new SubCategory { CategoryId = cate1.Id, Name = "Nhẫn nam" };
                    var subcate3 = new SubCategory { CategoryId = cate1.Id, Name = "Nhãn đôi (Set)" };
                    var subcate4 = new SubCategory { CategoryId = cate2.Id, Name = "Khuyên tai" };
                    var subcate5 = new SubCategory { CategoryId = cate2.Id, Name = "Khuyên vị trí khác" };
                    var subcate6 = new SubCategory { CategoryId = cate4.Id, Name = "Vòng - Lắc tay" };
                    var subcate7 = new SubCategory { CategoryId = cate4.Id, Name = "Vòng - Lắc chân" };
                    context.SaveChanges();

                    if (!context._subcategories.Any())
                    {
                        var subcategories = new SubCategory[] { subcate1, subcate2, subcate3, subcate4, subcate5, subcate6, subcate7 };

                        context._subcategories.AddRange(subcategories);

                        context.SaveChanges();

                    }

                    if (!context._products.Any())
                    {
                        var products = new Product[]
                    {
                        //Nhẫn nữ
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Bạc Nữ Lá Nhỏ Freesize", Description = "Nhẫn Bạc Nữ Lá Nhỏ Freesize", Material = "Bạc 925", Price = 250000, SubCategoryId = subcate1.Id, Image = "n-lanho"},
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Bạc Nữ 2 Viên Đá Emeral Freesize", Description = "Nhẫn Bạc Nữ 2 Viên Đá Emeral Freesize", Material = "Bạc 925", Price = 280000, SubCategoryId = subcate1.Id, Image = "n-2vienda"},
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Bạc Cánh Bướm", Description = "Nhẫn Bạc Cánh Bướm", Material = "Bạc 925", Price = 350000, SubCategoryId = subcate1.Id, Image = "n-canhbuom"},   
                        //Nhẫn nam
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Nam Đôi Cánh Đại Bàng", Description = "Nhẫn Nam Đôi Cánh Đại Bàng", Material = "Bạc 925", Price = 450000, SubCategoryId = subcate2.Id, Image = "n-daibang"},
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Nam Hình Móng Rồng", Description = "Nhẫn Nam Hình Móng Rồng", Material = "Bạc 925", Price = 800000, SubCategoryId = subcate2.Id, Image = "n-mongrong"},
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Nam Đá Cz Cao Cấp", Description = "Nhẫn Nam Đá Cz Cao Cấp", Material = "Bạc 925", Price = 450000, SubCategoryId = subcate2.Id, Image = "n-dacz"},
                        //Nhẫn đôi 
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Cặp Bạc Tình Yêu Cao Cấp", Description = "Nhẫn Cặp Bạc Tình Yêu Cao Cấp", Material = "Bạc 925", Price = 650000, SubCategoryId = subcate3.Id, Image = "nd-tinhyeu"},
                        new Product() {WarrantyId = warranty2.Id, Name = "Nhẫn Cặp Bạc Tình Yêu Cao Cấp", Description = "Nhẫn Cặp Bạc Tình Yêu Cao Cấp", Material = "Bạc 925", Price = 650000, SubCategoryId = subcate3.Id, Image = "nd-tinhyeu"},

                    };
                        foreach (Product s in products)
                        {
                            context._products.Add(s);
                        }
                    }

                }


                //
            }

            context.SaveChanges();

        }
    }
}

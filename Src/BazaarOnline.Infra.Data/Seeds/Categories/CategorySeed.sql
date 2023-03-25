USE BazaarOnline_Database;
 
SET IDENTITY_INSERT [dbo].[Categories] ON
--------------------------------------------------------------------
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES
  (1,N'املاک', null, 'HouseOutlined'),
  (2,N'وسایل نقلیه', null, 'DirectionsCarFilledOutlined'),
  (3,N'کالای دیجیتال', null, 'PhoneIphoneOutlined'),
  (4,N'خانه و آشپزخانه', null, 'BlenderOutlined'),
  (5,N'خدمات', null, 'FormatPaintOutlined'),
  (6,N'وسایل شخصی', null, 'WatchOutlined'),
  (7,N'سرگرمی و فراغت', null, 'CasinoOutlined'),
  (8,N'اجتماعی', null, 'PeopleOutlined'),
  (9,N'تجهیزات و صنعتی', null, 'HomeRepairServiceOutlined'),
  (10,N'استخدام و کاریابی', null, 'WorkOutline'),
  (11,N'فروش مسکونی', 1, ''),
  (12,N'اجاره مسکونی', 1, ''),
  (17,N'آپارتمان', 11, ''),
  (18,N'خانه و ویلا', 11, ''),
  (20,N'خودرو', 2, ''),
  (21,N'قطعات یدکی و لوازم جانبی خودرو', 2, ''),
  (22,N'موتورسیکلت و لوازم جانبی', 2, ''),
  (24,N'سواری و وانت', 20, ''),
  (25,N'کلاسیک', 20, ''),
  (26,N'اجاره‌ای', 20, ''),
  (27,N'سنگین', 20, ''),
  (28,N'موبایل و تبلت', 3, ''),
  (29,N'رایانه', 3, ''),
  (30,N'کنسول، بازی‌ ویدئویی و آنلاین', 3, ''),
  (31,N'صوتی و تصویری', 3, ''),
  (32,N'تلفن رومیزی', 3, ''),
  (33,N'گوشی موبایل', 28, ''),
  (34,N'تبلت', 28, ''),
  (35,N'لوازم جانبی موبایل و تبلت', 28, ''),
  (36,N'سیم کارت', 28, '')
--------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Categories] OFF

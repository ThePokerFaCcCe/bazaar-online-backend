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
  (10,N'استخدام و کاریابی', null, 'WorkOutline')
--------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Categories] OFF

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/real-estate.efdfc654.png' WHERE Id = 1

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/vehicles.e236aaef.png' WHERE Id = 2

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/electronic-devices.a8f529dd.png' WHERE Id = 3

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/home-kitchen.db87dd2e.png' WHERE Id = 4

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/services.ab181eb2.png' WHERE Id = 5

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/personal-goods.9d865d33.png' WHERE Id = 6

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/entertainment.2ee67eb3.png' WHERE Id = 7

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/social-services.fecccf57.png' WHERE Id = 8

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/tools-materials-equipment.a5381fdd.png' WHERE Id = 9

UPDATE [dbo].[Categories] SET ImageUrl='https://s100.divarcdn.com/statics/2023/09/jobs.b7dec5b8.png' WHERE Id = 10

-- Complete Categories
DECLARE @RowId INT = 10;
DECLARE @ParentRowId INT = 1;


SET IDENTITY_INSERT [dbo].[Categories] ON
--------------------------------------------------------------------
  -- Extracted Category List For ParentId {1}
SET @RowId = @RowId + 1;

SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فروش مسکونی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آپارتمان', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خانه و ویلا', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'زمین و کلنگی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اجاره مسکونی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آپارتمان', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خانه و ویلا', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فروش اداری و تجاری', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دفتر کار، اتاق اداری و مطب', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مغازه و غرفه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صنعتی،‌ کشاورزی و تجاری', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اجاره اداری و تجاری', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دفتر کار، اتاق اداری و مطب', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مغازه و غرفه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صنعتی،‌ کشاورزی و تجاری', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اجاره کوتاه مدت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آپارتمان و سوئیت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ویلا و باغ', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دفتر کار و فضای آموزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پروژه‌های ساخت و ساز', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مشارکت در ساخت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پیش‌فروش', @ParentRowId, '');
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {2}
SET @ParentRowId = 2;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خودرو', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سواری و وانت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کلاسیک', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اجاره‌ای', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سنگین', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 2;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'موتورسیکلت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 2;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'قطعات یدکی و لوازم جانبی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 2;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'قایق و سایر وسایل نقلیه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {3}
SET @ParentRowId = 3;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'موبایل و تبلت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گوشی موبایل', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تبلت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم جانبی موبایل و تبلت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سیم کارت', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 3;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رایانه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رایانه همراه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رایانه رومیزی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'قطعات و لوازم جانبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مودم و تجهیزات شبکه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پرینتر/اسکنر/کپی/فکس', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 3;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کنسول، بازی‌ ویدئویی و آنلاین', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 3;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صوتی و تصویری', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فیلم و موسیقی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دوربین عکاسی و فیلم‌برداری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پخش‌کننده همراه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سیستم صوتی خانگی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ویدئو و پخش کننده DVD', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تلویزیون و پروژکتور', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دوربین مداربسته', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 3;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تلفن رومیزی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {4}
SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم خانگی برقی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'یخچال و فریزر', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آب‌سردکن و تصفیه آب', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ماشین لباسشویی و خشک‌کن لباس', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ماشین ظرفشویی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'جاروبرقی، جاروشارژی و بخارشو', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اتو و لوازم اتو', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آبمیوه و آب‌مرکبات‌گیر', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خردکن، آسیاب و غذاساز', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سماور، چای‌ساز و قهوه‌ساز', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اجاق گاز و لوازم برقی پخت‌وپز', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'هود', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سایر لوازم برقی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ظروف و لوازم آشپزخانه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سفره، حوله و دستمال آشپزخانه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آب‌چکان و نظم‌دهنده ظروف', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'قوری، کتری و قهوه‌ساز دستی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ظروف سرو و پذیرایی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ظروف نگهدارنده، پلاستیکی و یکبارمصرف', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ظروف پخت‌وپز', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خوردنی و آشامیدنی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خیاطی و بافتنی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'چرخ خیاطی و ریسندگی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم خیاطی و بافتنی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مبلمان و صنایع چوب', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مبلمان خانگی و میزعسلی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'میز و صندلی غذاخوری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بوفه، ویترین و کنسول', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کتابخانه، شلف و قفسه‌های دیواری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'جاکفشی، کمد و دراور', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تخت و سرویس خواب', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'میز تلفن', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'میز تلویزیون', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'میز تحریر و کامپیوتر', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مبلمان اداری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صندلی و نیمکت', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'نور و روشنایی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوستر و چراغ آویز', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'چراغ خواب و آباژور', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ریسه و چراغ تزئینی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لامپ و چراغ', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فرش، گلیم و موکت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فرش', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تابلو فرش', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'روفرشی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پادری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'موکت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گلیم، جاجیم و گبه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پشتی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تشک، روتختی و رختخواب', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رختخواب، بالش و پتو', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تشک تختخواب', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سرویس روتختی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم دکوری و تزئینی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پرده، رانر و رومیزی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آینه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ساعت دیواری و تزئینی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تابلو، نقاشی و عکس', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مجسمه، تندیس و ماکت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گل مصنوعی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گل و گیاه طبیعی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صنایع دستی و سایر لوازم تزئینی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تهویه، سرمایش و گرمایش', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آبگرمکن، پکیج و شوفاژ', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بخاری، هیتر و شومینه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کولر آبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کولر گازی و فن‌کوئل', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پنکه و تصفیه‌کنندهٔ هوا', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'شست‌وشو و نظافت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مواد شوینده و دستمال کاغذی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم نظافت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بندرخت و رخت‌آویز', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 4;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حمام و سرویس بهداشتی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم حمام', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم سرویس بهداشتی', @ParentRowId, '');
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {5}
SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'موتور و ماشین', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پذیرایی/مراسم', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خدمات رایانه‌ای و موبایل', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مالی/حسابداری/بیمه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حمل و نقل', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پیشه و مهارت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آرایشگری و زیبایی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'نظافت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'باغبانی و درختکاری', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 5;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آموزشی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {6}
SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کیف، کفش و لباس', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کیف/کفش/کمربند', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لباس', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'زیورآلات و اکسسوری', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ساعت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'جواهرات', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بدلیجات', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آرایشی، بهداشتی و درمانی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کفش و لباس بچه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'وسایل بچه و اسباب بازی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اسباب بازی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کالسکه و لوازم جانبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صندلی بچه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اسباب و اثاث بچه', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 6;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم التحریر', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {7}
SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بلیط', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کنسرت', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تئاتر و سینما', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کارت هدیه و تخفیف', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اماکن و مسابقات ورزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ورزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اتوبوس، مترو و قطار', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تور و چارتر', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کتاب و مجله', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آموزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ادبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تاریخی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مذهبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مجلات', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دوچرخه/اسکیت/اسکوتر', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حیوانات', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گربه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'موش و خرگوش', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خزنده', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پرنده', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ماهی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'لوازم جانبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حیوانات مزرعه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سگ', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کلکسیون و سرگرمی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سکه، تمبر و اسکناس', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اشیای عتیقه', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آلات موسیقی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گیتار، بیس و امپلیفایر', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سازهای بادی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پیانو/کیبورد/آکاردئون', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سنتی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'درام و پرکاشن', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ویولن', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ورزش و تناسب اندام', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ورزش‌های توپی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کوهنوردی و کمپینگ', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'غواصی و ورزش‌های آبی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ماهیگیری', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تجهیزات ورزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ورزش‌های زمستانی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اسب و تجهیزات اسب سواری', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 7;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اسباب‌ بازی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {8}
SET @ParentRowId = 8;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رویداد', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حراج', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گردهمایی و همایش', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ورزشی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 8;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'داوطلبانه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تحقیقاتی', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 8;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'گم‌شده‌ها', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حیوانات', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اشیا', @ParentRowId, '');
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {9}
SET @ParentRowId = 9;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مصالح و تجهیزات ساختمان', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 9;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ابزارآلات', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 9;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'ماشین‌آلات صنعتی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 9;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'تجهیزات کسب‌وکار', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'پزشکی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'فروشگاه و مغازه', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'کافی‌شاپ و رستوران', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آرایشگاه و سالن های زیبایی', @ParentRowId, '');
SET @RowId = @RowId + 1;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'دفتر کار', @ParentRowId, '');
SET @RowId = @RowId + 1;


SET @ParentRowId = 9;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'عمده فروشی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {10}
SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'اداری و مدیریت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'سرایداری و نظافت', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'معماری ،عمران و ساختمانی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'خدمات فروشگاه و رستوران', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'رایانه و فناوری اطلاعات', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'مالی و حسابداری و حقوقی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'بازاریابی و فروش', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'صنعتی و فنی و مهندسی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'آموزشی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'حمل و نقل', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'درمانی و زیبایی و بهداشتی', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


SET @ParentRowId = 10;
INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'هنری و رسانه', @ParentRowId, '');
SET @ParentRowId = @RowId;
SET @RowId = @RowId + 1;


--------------------------------------


--------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Categories] OFF

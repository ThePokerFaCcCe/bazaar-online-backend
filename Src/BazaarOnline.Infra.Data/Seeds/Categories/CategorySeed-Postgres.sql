--------------------------------------------------------------------
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES
  (1,N'املاک', null, 'HouseOutlined', ''),
  (2,N'وسایل نقلیه', null, 'DirectionsCarFilledOutlined', ''),
  (3,N'کالای دیجیتال', null, 'PhoneIphoneOutlined', ''),
  (4,N'خانه و آشپزخانه', null, 'BlenderOutlined', ''),
  (5,N'خدمات', null, 'FormatPaintOutlined', ''),
  (6,N'وسایل شخصی', null, 'WatchOutlined', ''),
  (7,N'سرگرمی و فراغت', null, 'CasinoOutlined', ''),
  (8,N'اجتماعی', null, 'PeopleOutlined', ''),
  (9,N'تجهیزات و صنعتی', null, 'HomeRepairServiceOutlined', ''),
  (10,N'استخدام و کاریابی', null, 'WorkOutline', '');
--------------------------------------------------------------------

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/real-estate.efdfc654.png' WHERE "Id" = 1;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/vehicles.e236aaef.png' WHERE "Id" = 2;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/electronic-devices.a8f529dd.png' WHERE "Id" = 3;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/home-kitchen.db87dd2e.png' WHERE "Id" = 4;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/services.ab181eb2.png' WHERE "Id" = 5;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/personal-goods.9d865d33.png' WHERE "Id" = 6;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/entertainment.2ee67eb3.png' WHERE "Id" = 7;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/social-services.fecccf57.png' WHERE "Id" = 8;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/tools-materials-equipment.a5381fdd.png' WHERE "Id" = 9;

UPDATE public."Categories" SET "ImageUrl"='https://s100.divarcdn.com/statics/2023/09/jobs.b7dec5b8.png' WHERE "Id" = 10;

-- Complete Categories
DO $$
DECLARE RowId INTEGER;
DECLARE ParentRowId INTEGER;
BEGIN

RowId := 11;

--------------------------------------------------------------------

  -- Extracted Category List For ParentId {1}

ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فروش مسکونی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آپارتمان', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خانه و ویلا', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'زمین و کلنگی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اجاره مسکونی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آپارتمان', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خانه و ویلا', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فروش اداری و تجاری', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دفتر کار، اتاق اداری و مطب', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مغازه و غرفه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صنعتی،‌ کشاورزی و تجاری', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اجاره اداری و تجاری', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دفتر کار، اتاق اداری و مطب', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مغازه و غرفه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صنعتی،‌ کشاورزی و تجاری', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اجاره کوتاه مدت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آپارتمان و سوئیت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ویلا و باغ', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دفتر کار و فضای آموزشی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پروژه‌های ساخت و ساز', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مشارکت در ساخت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پیش‌فروش', ParentRowId, '', '');
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {2}
ParentRowId := 2;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خودرو', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سواری و وانت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کلاسیک', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اجاره‌ای', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سنگین', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 2;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'موتورسیکلت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 2;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'قطعات یدکی و لوازم جانبی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 2;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'قایق و سایر وسایل نقلیه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {3}
ParentRowId := 3;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'موبایل و تبلت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گوشی موبایل', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تبلت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم جانبی موبایل و تبلت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سیم کارت', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 3;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رایانه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رایانه همراه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رایانه رومیزی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'قطعات و لوازم جانبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مودم و تجهیزات شبکه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پرینتر/اسکنر/کپی/فکس', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 3;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کنسول، بازی‌ ویدئویی و آنلاین', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 3;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صوتی و تصویری', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فیلم و موسیقی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دوربین عکاسی و فیلم‌برداری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پخش‌کننده همراه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سیستم صوتی خانگی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ویدئو و پخش کننده DVD', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تلویزیون و پروژکتور', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دوربین مداربسته', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 3;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تلفن رومیزی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {4}
ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم خانگی برقی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'یخچال و فریزر', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آب‌سردکن و تصفیه آب', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ماشین لباسشویی و خشک‌کن لباس', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ماشین ظرفشویی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'جاروبرقی، جاروشارژی و بخارشو', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اتو و لوازم اتو', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آبمیوه و آب‌مرکبات‌گیر', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خردکن، آسیاب و غذاساز', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سماور، چای‌ساز و قهوه‌ساز', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اجاق گاز و لوازم برقی پخت‌وپز', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'هود', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سایر لوازم برقی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ظروف و لوازم آشپزخانه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سفره، حوله و دستمال آشپزخانه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آب‌چکان و نظم‌دهنده ظروف', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'قوری، کتری و قهوه‌ساز دستی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ظروف سرو و پذیرایی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ظروف نگهدارنده، پلاستیکی و یکبارمصرف', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ظروف پخت‌وپز', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خوردنی و آشامیدنی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خیاطی و بافتنی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'چرخ خیاطی و ریسندگی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم خیاطی و بافتنی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مبلمان و صنایع چوب', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مبلمان خانگی و میزعسلی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'میز و صندلی غذاخوری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بوفه، ویترین و کنسول', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کتابخانه، شلف و قفسه‌های دیواری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'جاکفشی، کمد و دراور', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تخت و سرویس خواب', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'میز تلفن', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'میز تلویزیون', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'میز تحریر و کامپیوتر', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مبلمان اداری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صندلی و نیمکت', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'نور و روشنایی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوستر و چراغ آویز', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'چراغ خواب و آباژور', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ریسه و چراغ تزئینی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لامپ و چراغ', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فرش، گلیم و موکت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فرش', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تابلو فرش', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'روفرشی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پادری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'موکت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گلیم، جاجیم و گبه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پشتی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تشک، روتختی و رختخواب', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رختخواب، بالش و پتو', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تشک تختخواب', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سرویس روتختی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم دکوری و تزئینی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پرده، رانر و رومیزی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آینه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ساعت دیواری و تزئینی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تابلو، نقاشی و عکس', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مجسمه، تندیس و ماکت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گل مصنوعی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گل و گیاه طبیعی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صنایع دستی و سایر لوازم تزئینی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تهویه، سرمایش و گرمایش', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آبگرمکن، پکیج و شوفاژ', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بخاری، هیتر و شومینه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کولر آبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کولر گازی و فن‌کوئل', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پنکه و تصفیه‌کنندهٔ هوا', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'شست‌وشو و نظافت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مواد شوینده و دستمال کاغذی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم نظافت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بندرخت و رخت‌آویز', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 4;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حمام و سرویس بهداشتی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم حمام', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم سرویس بهداشتی', ParentRowId, '', '');
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {5}
ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'موتور و ماشین', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پذیرایی/مراسم', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خدمات رایانه‌ای و موبایل', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مالی/حسابداری/بیمه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حمل و نقل', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پیشه و مهارت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آرایشگری و زیبایی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'نظافت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'باغبانی و درختکاری', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 5;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آموزشی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {6}
ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کیف، کفش و لباس', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کیف/کفش/کمربند', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لباس', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'زیورآلات و اکسسوری', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ساعت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'جواهرات', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بدلیجات', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آرایشی، بهداشتی و درمانی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کفش و لباس بچه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'وسایل بچه و اسباب بازی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اسباب بازی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کالسکه و لوازم جانبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صندلی بچه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اسباب و اثاث بچه', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 6;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم التحریر', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {7}
ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بلیط', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کنسرت', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تئاتر و سینما', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کارت هدیه و تخفیف', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اماکن و مسابقات ورزشی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ورزشی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اتوبوس، مترو و قطار', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تور و چارتر', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کتاب و مجله', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آموزشی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ادبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تاریخی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مذهبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مجلات', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دوچرخه/اسکیت/اسکوتر', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حیوانات', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گربه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'موش و خرگوش', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خزنده', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پرنده', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ماهی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'لوازم جانبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حیوانات مزرعه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سگ', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کلکسیون و سرگرمی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سکه، تمبر و اسکناس', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اشیای عتیقه', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آلات موسیقی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گیتار، بیس و امپلیفایر', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سازهای بادی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پیانو/کیبورد/آکاردئون', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سنتی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'درام و پرکاشن', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ویولن', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ورزش و تناسب اندام', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ورزش‌های توپی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کوهنوردی و کمپینگ', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'غواصی و ورزش‌های آبی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ماهیگیری', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تجهیزات ورزشی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ورزش‌های زمستانی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اسب و تجهیزات اسب سواری', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 7;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اسباب‌ بازی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {8}
ParentRowId := 8;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رویداد', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حراج', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گردهمایی و همایش', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ورزشی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 8;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'داوطلبانه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تحقیقاتی', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 8;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'گم‌شده‌ها', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حیوانات', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اشیا', ParentRowId, '', '');
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {9}
ParentRowId := 9;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مصالح و تجهیزات ساختمان', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 9;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ابزارآلات', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 9;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'ماشین‌آلات صنعتی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 9;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'تجهیزات کسب‌وکار', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'پزشکی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'فروشگاه و مغازه', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'کافی‌شاپ و رستوران', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آرایشگاه و سالن های زیبایی', ParentRowId, '', '');
RowId := RowId + 1;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'دفتر کار', ParentRowId, '', '');
RowId := RowId + 1;


ParentRowId := 9;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'عمده فروشی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------

-- Extracted Category List For ParentId {10}
ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'اداری و مدیریت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'سرایداری و نظافت', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'معماری ،عمران و ساختمانی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'خدمات فروشگاه و رستوران', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'رایانه و فناوری اطلاعات', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'مالی و حسابداری و حقوقی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'بازاریابی و فروش', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'صنعتی و فنی و مهندسی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'آموزشی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'حمل و نقل', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'درمانی و زیبایی و بهداشتی', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


ParentRowId := 10;
INSERT INTO public."Categories"("Id", "Title", "ParentCategoryId", "Icon", "ImageUrl") VALUES (RowId,N'هنری و رسانه', ParentRowId, '', '');
ParentRowId := RowId;
RowId := RowId + 1;


--------------------------------------


--------------------------------------------------------------------


END $$;
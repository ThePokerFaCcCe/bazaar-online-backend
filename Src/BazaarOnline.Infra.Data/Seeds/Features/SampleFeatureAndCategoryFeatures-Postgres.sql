INSERT INTO public."FeatureIntegerTypes" ("Id", "Minimum", "Maximum", "Placeholder") VALUES (1, 1300, 1401, N'سال شمسی ...');

INSERT INTO public."FeatureIntegerTypes" ("Id", "Minimum", "Maximum", "Placeholder") VALUES (2, 0, 2000000, N'کیلومتر ...');

-----------------------

-----------------------
INSERT INTO public."FeatureSelectTypes" ("Id", "Options") VALUES (1, N'سایپا,پژو,سمند,زامیاد');

INSERT INTO public."FeatureSelectTypes" ("Id", "Options") VALUES (2, N'بنزینی,دوگانه شرکتی,دوگانه شخصی,برقی');

INSERT INTO public."FeatureSelectTypes" ("Id", "Options") VALUES (3, N'اتوماتیک,دستی');

INSERT INTO public."FeatureSelectTypes" ("Id", "Options") VALUES (4, N'دارد,ندارد');

-----------------------

-----------------------
INSERT INTO public."FeatureStringTypes" ("Id", "MinLength", "MaxLength", "Regex", "Placeholder") VALUES (1, 2, 10, NULL, N'خاکستری');

INSERT INTO public."FeatureStringTypes" ("Id", "MinLength", "MaxLength", "Regex", "Placeholder") VALUES (2, 4, 20, NULL, N'سالم - دارای خط - تمام رنگ ...');

-----------------------

-----------------------
INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (1, N'مدل', N'سال تولید خودرو', 1, 2, NULL, 1, NULL);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (2, N'کارکرد', N'مسافت طی شده توسط خودرو ', 1, 2, NULL, 2, NULL);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (3, N'رنگ', N'رنگ خودرو', 1, 1, 1, NULL, NULL);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (4, N'برند', N'شرکت سازنده خودرو', 2, 3, NULL, NULL, 1);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (5, N'نوع سوخت', N'نوع مصرف سوخت خودرو', 2, 3, NULL, NULL, 2);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (6, N'وضعیت بدنه', N'وضعیت سلامت بدنه', 2, 1, 2, NULL, NULL);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (7, N'گیربکس', N'نوع گیربکس', 2, 3, NULL, NULL, 3);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (8, N'شیشه دودی', N'برچسب شیشه دودی دارد', 3, 3, NULL, NULL, 4);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (13, N'بدنه اسپرت', N'بدنه اسپرت دارد', 3, 3, NULL, NULL, 4);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (16, N'روکش صندلی', N'روکش صندلی دارد', 3, 3, NULL, NULL, 4);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (17, N'چراغ خطر شیشه عقب', N'چراغ خطر شیشه عقب دارد', 4, 3, NULL, NULL, 4);

INSERT INTO public."Features" ("Id", "Name", "Description", "Position", "Type", "StringTypeId", "IntegerTypeId", "SelectTypeId") VALUES (18, N'چراغ عقب اسپرت', N'چراغ عقب اسپرت دارد', 4, 3, NULL, NULL, 4);

-----------------------

-----------------------
INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (1, 1, true, true, false, 2, 1);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (2, 2, true, true, true, 2, 2);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (3, 3, true, false, false, 2, 3);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (4, 5, true, true, false, 33, 4);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (5, 6, true, true, false, 33, 5);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (6, 7, false, false, false, 33, 6);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (7, 8, true, false, false, 33, 7);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (8, 9, true, false, false, 34, 8);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (9, 10, true, false, false, 34, 13);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (10, 11, true, false, false, 34, 16);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (11, 12, false, false, false, 34, 17);

INSERT INTO public."CategoryFeatures" ("Id", "SortNumber", "IsRequired", "IsFilterable", "IsShownInList", "CategoryId", "FeatureId") VALUES (12, 13, false, false, false, 34, 18);

-----------------------
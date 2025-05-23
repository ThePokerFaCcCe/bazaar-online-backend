USE [BazaarOnline_Database]
GO

SET IDENTITY_INSERT [bazaar].[FeatureIntegerTypes] ON 
GO
INSERT [bazaar].[FeatureIntegerTypes] ([Id], [Minimum], [Maximum], [Placeholder]) VALUES (1, 1300, 1401, N'سال شمسی ...')
GO
INSERT [bazaar].[FeatureIntegerTypes] ([Id], [Minimum], [Maximum], [Placeholder]) VALUES (2, 0, 2000000, N'کیلومتر ...')
GO
SET IDENTITY_INSERT [bazaar].[FeatureIntegerTypes] OFF
GO
SET IDENTITY_INSERT [bazaar].[FeatureSelectTypes] ON 
GO
INSERT [bazaar].[FeatureSelectTypes] ([Id], [Options]) VALUES (1, N'سایپا,پژو,سمند,زامیاد')
GO
INSERT [bazaar].[FeatureSelectTypes] ([Id], [Options]) VALUES (2, N'بنزینی,دوگانه شرکتی,دوگانه شخصی,برقی')
GO
INSERT [bazaar].[FeatureSelectTypes] ([Id], [Options]) VALUES (3, N'اتوماتیک,دستی')
GO
INSERT [bazaar].[FeatureSelectTypes] ([Id], [Options]) VALUES (4, N'دارد,ندارد')
GO
SET IDENTITY_INSERT [bazaar].[FeatureSelectTypes] OFF
GO
SET IDENTITY_INSERT [bazaar].[FeatureStringTypes] ON 
GO
INSERT [bazaar].[FeatureStringTypes] ([Id], [MinLength], [MaxLength], [Regex], [Placeholder]) VALUES (1, 2, 10, NULL, N'خاکستری')
GO
INSERT [bazaar].[FeatureStringTypes] ([Id], [MinLength], [MaxLength], [Regex], [Placeholder]) VALUES (2, 4, 20, NULL, N'سالم - دارای خط - تمام رنگ ...')
GO
SET IDENTITY_INSERT [bazaar].[FeatureStringTypes] OFF
GO
SET IDENTITY_INSERT [bazaar].[Features] ON 
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (1, N'مدل', N'سال تولید خودرو', 1, 2, NULL, 1, NULL)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (2, N'کارکرد', N'مسافت طی شده توسط خودرو ', 1, 2, NULL, 2, NULL)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (3, N'رنگ', N'رنگ خودرو', 1, 1, 1, NULL, NULL)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (4, N'برند', N'شرکت سازنده خودرو', 2, 3, NULL, NULL, 1)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (5, N'نوع سوخت', N'نوع مصرف سوخت خودرو', 2, 3, NULL, NULL, 2)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (6, N'وضعیت بدنه', N'وضعیت سلامت بدنه', 2, 1, 2, NULL, NULL)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (7, N'گیربکس', N'نوع گیربکس', 2, 3, NULL, NULL, 3)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (8, N'شیشه دودی', N'برچسب شیشه دودی دارد', 3, 3, NULL, NULL, 4)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (13, N'بدنه اسپرت', N'بدنه اسپرت دارد', 3, 3, NULL, NULL, 4)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (16, N'روکش صندلی', N'روکش صندلی دارد', 3, 3, NULL, NULL, 4)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (17, N'چراغ خطر شیشه عقب', N'چراغ خطر شیشه عقب دارد', 4, 3, NULL, NULL, 4)
GO
INSERT [bazaar].[Features] ([Id], [Name], [Description], [Position], [Type], [StringTypeId], [IntegerTypeId], [SelectTypeId]) VALUES (18, N'چراغ عقب اسپرت', N'چراغ عقب اسپرت دارد', 4, 3, NULL, NULL, 4)
GO
SET IDENTITY_INSERT [bazaar].[Features] OFF
GO
SET IDENTITY_INSERT [bazaar].[CategoryFeatures] ON 
GO 
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (1, 1, 1, 1, 0, 2, 1)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (2, 2, 1, 1, 1, 2, 2)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (3, 3, 1, 0, 0, 2, 3)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (4, 5, 1, 1, 0, 33, 4)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (5, 6, 1, 1, 0, 33, 5)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (6, 7, 0, 0, 0, 33, 6)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (7, 8, 1, 0, 0, 33, 7)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (8, 9, 1, 0, 0, 34, 8)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (9, 10, 1, 0, 0, 34, 13)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (10, 11, 1, 0, 0, 34, 16)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (11, 12, 0, 0, 0, 34, 17)
GO
INSERT [bazaar].[CategoryFeatures] ([Id], [SortNumber], [IsRequired], [IsFilterable], [IsShownInList], [CategoryId], [FeatureId]) VALUES (12, 13, 0, 0, 0, 34, 18)
GO
SET IDENTITY_INSERT [bazaar].[CategoryFeatures] OFF
GO

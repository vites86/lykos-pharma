USE [Olga]
GO
/****** Object:  Schema [info]    Script Date: 08.02.2018 10:24:23 ******/
CREATE SCHEMA [info]
GO
/****** Object:  Schema [product]    Script Date: 08.02.2018 10:24:23 ******/
CREATE SCHEMA [product]
GO
/****** Object:  Table [info].[ApprDocsTypes]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[ApprDocsTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApprType] [nvarchar](150) NULL,
 CONSTRAINT [PK_ApprDocsTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[Artworks]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[Artworks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Artwork] [nvarchar](150) NULL,
 CONSTRAINT [PK_Artworks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[Countries]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[Manufacturers]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[Manufacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[MarketingAuthorizHolders]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[MarketingAuthorizHolders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_MarketingAuthorHolder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[MarketingAuthorizNumbers]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[MarketingAuthorizNumbers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](250) NULL,
 CONSTRAINT [PK_MarketingAuthorizNumbers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[PackSizes]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[PackSizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Size] [nvarchar](250) NULL,
 CONSTRAINT [PK_PackSizes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[PharmaceuticalForms]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[PharmaceuticalForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PharmaForm] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_info.PharmaceuticalForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[ProductCodes]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[ProductCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_ProductCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[ProductNames]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[ProductNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_ProductNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [info].[Strength]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [info].[Strength](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Strength] [nvarchar](250) NULL,
 CONSTRAINT [PK_info.Strength] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [product].[Products]    Script Date: 08.02.2018 10:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [product].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[ProductNameId] [int] NOT NULL,
	[PharmaceuticalFormId] [int] NOT NULL,
	[StrengthId] [int] NOT NULL,
	[ProductCodeId] [int] NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[MarketingAuthorizHolderId] [int] NOT NULL,
	[MarketingAuthorizNumberId] [int] NOT NULL,
	[IssuedDate] [date] NULL,
	[ExpiredDate] [date] NULL,
	[PackSizeId] [int] NOT NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [info].[ApprDocsTypes] ON 

INSERT [info].[ApprDocsTypes] ([Id], [ApprType]) VALUES (3, N'ND / MQC')
INSERT [info].[ApprDocsTypes] ([Id], [ApprType]) VALUES (4, N'Pack materials / Labelling')
INSERT [info].[ApprDocsTypes] ([Id], [ApprType]) VALUES (2, N'PIL')
INSERT [info].[ApprDocsTypes] ([Id], [ApprType]) VALUES (1, N'Registration certificate')
SET IDENTITY_INSERT [info].[ApprDocsTypes] OFF
SET IDENTITY_INSERT [info].[Artworks] ON 

INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (4, N'Blister')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (7, N'Foil')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (3, N'Label')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (2, N'Leaflet')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (6, N'Sachet')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (5, N'Tube')
INSERT [info].[Artworks] ([Id], [Artwork]) VALUES (1, N'Сarton')
SET IDENTITY_INSERT [info].[Artworks] OFF
SET IDENTITY_INSERT [info].[Countries] ON 

INSERT [info].[Countries] ([Id], [Name]) VALUES (1, N'Ukraine')
INSERT [info].[Countries] ([Id], [Name]) VALUES (2, N'Russia')
INSERT [info].[Countries] ([Id], [Name]) VALUES (3, N'Armenia')
INSERT [info].[Countries] ([Id], [Name]) VALUES (4, N'Azerbaijan')
INSERT [info].[Countries] ([Id], [Name]) VALUES (5, N'Moldova')
INSERT [info].[Countries] ([Id], [Name]) VALUES (6, N'Georgia')
INSERT [info].[Countries] ([Id], [Name]) VALUES (7, N'Kazakhstan')
INSERT [info].[Countries] ([Id], [Name]) VALUES (8, N'Kyrgystan')
INSERT [info].[Countries] ([Id], [Name]) VALUES (9, N'Turkmenistan')
INSERT [info].[Countries] ([Id], [Name]) VALUES (10, N'Uzbekistan')
INSERT [info].[Countries] ([Id], [Name]) VALUES (11, N'Lithuania')
INSERT [info].[Countries] ([Id], [Name]) VALUES (12, N'Latvia')
INSERT [info].[Countries] ([Id], [Name]) VALUES (13, N'Estonia')
INSERT [info].[Countries] ([Id], [Name]) VALUES (14, N'Belarus')
SET IDENTITY_INSERT [info].[Countries] OFF
SET IDENTITY_INSERT [info].[Manufacturers] ON 

INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (8, N'Aziende Chimiche Riunite Angelini Francesco ACRAF SPA, Italy')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (6, N'Doppel Farmaceutici S.r. L.Italy')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (2, N'Famar Italia S.p.A., Italy')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (5, N'Fidia Pharmaceutici S. p. A.,  Italy')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (4, N'Italfarmaco S.p.A., Italy')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (7, N'Laboratoires Expanscience, France')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (3, N'Medicom International s.r.o., Czech Republic')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (1, N'OSO Biopharm+F2+F3:F15+F3:F16+F2+F3:F15+F3:F18+F3:F17+F+F3:F17')
INSERT [info].[Manufacturers] ([Id], [Name]) VALUES (9, N'Фаркотерм С.р.л., Віа Абеле Мерлі, 1 - Кузано Міланіно (МІ), Італія')
SET IDENTITY_INSERT [info].[Manufacturers] OFF
SET IDENTITY_INSERT [info].[MarketingAuthorizHolders] ON 

INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (7, N'Aziende Chimiche Riunite Angelini Francesco A.C.R.A.F. S.p.A., Italy')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (4, N'Fidia Pharmaceutici S. p. A., Italy')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (1, N'Helsinn Birex Pharmaceuticals Ltd.,Ireland')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (3, N'Italfarmaco S.p.А., Italy')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (6, N'Laboratoires Expanscience, France')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (5, N'Poliсhеm S.r.l.,Italy')
INSERT [info].[MarketingAuthorizHolders] ([Id], [Name]) VALUES (2, N'UAB MRA, Republic of Lithuania')
SET IDENTITY_INSERT [info].[MarketingAuthorizHolders] OFF
SET IDENTITY_INSERT [info].[MarketingAuthorizNumbers] ON 

INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (10, N'No. UA/13173/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (18, N'No. UA/15577/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (19, N'No. UA/15577/01/02')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (4, N'No. UA/2196/02/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (8, N'No. UA/3934/02/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (12, N'No. UA/4012/02/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (9, N'No. UA/5045/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (16, N'No. UA/9939/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (17, N'No. UA/9939/01/02')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (2, N'No. UА/14088/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (3, N'No. UА/14153/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (15, N'No. UА/3920/02/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (7, N'No. UА/3934/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (5, N'№ UА/2196/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (14, N'№ UА/3920/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (13, N'№ UА/3920/03/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (11, N'№ UА/4012/01/01')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (6, N'№13805/2014')
INSERT [info].[MarketingAuthorizNumbers] ([Id], [Number]) VALUES (1, N'№UA/16037/01/01')
SET IDENTITY_INSERT [info].[MarketingAuthorizNumbers] OFF
SET IDENTITY_INSERT [info].[PackSizes] ON 

INSERT [info].[PackSizes] ([Id], [Size]) VALUES (1, N'1 vial contains 5 ml of solution; one vial with solution per cardboard box ')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (3, N'1 vial with powder with a measuring container in a cardboard box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (2, N'10 capsules in a blister; 2 or 6 blisters in a cardboard box ')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (13, N'10 lozenges in stick; 2 or 3 sticks together into the cardboard box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (11, N'10 sachets in a cardboard box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (18, N'10 tablets in a blister; 1 or 2 or 3 blisters in a carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (9, N'10 tablets in a blister; 2 blisters in a carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (17, N'10 tablets per blister; 2 blisters per carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (16, N'10 tablets per blister; 3 blisters per carton box or 15 tablets per blister; 2 blisters per carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (14, N'120 mL of solution in a bottle with a measuring cup with labeling in Ukrainian; 1 bottle per cardboard box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (4, N'14 capsules per blister; 1 blister per box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (12, N'140 ml of solution in bottle with cannule and closure cap; 5 bottles in cardboard box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (10, N'15 capsules in a blister; 1 blisters in a carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (6, N'2 pre-filled syringes')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (7, N'30 g in a tube; one tube assembly with a graduated syringe in a carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (15, N'30 mL in a vial with a nebulizer, with labeling in Ukrainian; one vial per carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (5, N'4 ml per ampoule, 3 ampoules per plastic container, 1 container placed in a cardboard box ')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (19, N'7 tablets in a blister; 2 or 4 blisters in a carton box')
INSERT [info].[PackSizes] ([Id], [Size]) VALUES (8, N'8 suppositories in a blister, 1 blister in a carton box')
SET IDENTITY_INSERT [info].[PackSizes] OFF
SET IDENTITY_INSERT [info].[PharmaceuticalForms] ON 

INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (3, N'Capsules ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (8, N'Coated tablets ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (18, N'Drops')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (10, N'Granules for vaginal solution ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (12, N'Lozenges with mint flavor  3 mg')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (13, N'Oromucosal solution')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (14, N'Oromucosal spray')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (4, N'Powder for oral suspension  ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (16, N'Prolonged-release film-coated tablets ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (15, N'Prolonged-release tablets')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (2, N'Solution for injection')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (5, N'Sterile non-pyrogenic hydrogel, for intra-articular injection')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (19, N'Tablets')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (17, N'Vaginal capsules')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (6, N'Vaginal cream ')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (11, N'Vaginal solution 0.1 %')
INSERT [info].[PharmaceuticalForms] ([Id], [PharmaForm]) VALUES (7, N'Сapsules vaginal soft ')
SET IDENTITY_INSERT [info].[PharmaceuticalForms] OFF
SET IDENTITY_INSERT [info].[ProductCodes] ON 

INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (1, N'PFLGLI14CPUA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (2, N'PFLIN0014UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (3, N'60005236UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (4, N'60005222UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (5, N'60005195UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (6, N'8700433 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (7, N'129467 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (8, N'129466 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (9, N'129074 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (10, N'129464 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (11, N'129463 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (12, N'129878 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (13, N'129670 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (14, N'129966 UA', 1)
INSERT [info].[ProductCodes] ([Id], [Code], [CountryId]) VALUES (15, N'129967 UA', 1)
SET IDENTITY_INSERT [info].[ProductCodes] OFF
SET IDENTITY_INSERT [info].[ProductNames] ON 

INSERT [info].[ProductNames] ([Id], [Name]) VALUES (1, N'Aloxi')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (16, N'Dalifast')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (2, N'Ermucin')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (14, N'FEMINELLA')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (3, N'GLIATILIN®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (4, N'HYMOVIS® HYADD®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (6, N'MACMIROR')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (5, N'MACMIROR COMPLEX®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (7, N'PIASCLEDINE®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (8, N'TANTUM ROSA®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (9, N'TANTUM VERDE®')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (15, N'TECHNOFER')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (11, N'TRITTICO 150 mg')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (10, N'TRITTICO 75 mg')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (12, N'TRITTICO XR 150 mg')
INSERT [info].[ProductNames] ([Id], [Name]) VALUES (13, N'TRITTICO XR 300 mg')
SET IDENTITY_INSERT [info].[ProductNames] OFF
SET IDENTITY_INSERT [info].[Strength] ON 

INSERT [info].[Strength] ([Id], [Strength]) VALUES (14, N'1.5 mg/ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (7, N'10 g/ 4 000000 I.U')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (5, N'1000 mg /4 ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (12, N'140 ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (17, N'150 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (3, N'175 mg/5 ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (9, N'200 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (6, N'24 mg/3 ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (1, N'250 mkg/5 ml')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (13, N'3 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (2, N'300 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (4, N'400 mg ')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (11, N'500 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (8, N'500 mg + 200 000 I.U')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (21, N'600 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (16, N'75 mg')
INSERT [info].[Strength] ([Id], [Strength]) VALUES (20, N'not have')
SET IDENTITY_INSERT [info].[Strength] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__ApprDocs__17A8798761EED909]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[ApprDocsTypes] ADD UNIQUE NONCLUSTERED 
(
	[ApprType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Artworks__08FB8D3291F52C44]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[Artworks] ADD UNIQUE NONCLUSTERED 
(
	[Artwork] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Manufact__72E12F1B79BF83A0]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[Manufacturers] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Marketin__737584F6B8F9F3A9]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[MarketingAuthorizHolders] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Marketin__78A1A19D31212E29]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[MarketingAuthorizNumbers] ADD UNIQUE NONCLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__PackSize__A3250D060DDF3E4D]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[PackSizes] ADD UNIQUE NONCLUSTERED 
(
	[Size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [AK_PharmaceuticalForms]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[PharmaceuticalForms] ADD  CONSTRAINT [AK_PharmaceuticalForms] UNIQUE NONCLUSTERED 
(
	[PharmaForm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__ProductN__737584F6ADA95718]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[ProductNames] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Strength__72E12F1B164C05D9]    Script Date: 08.02.2018 10:24:23 ******/
ALTER TABLE [info].[Strength] ADD UNIQUE NONCLUSTERED 
(
	[Strength] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [info].[ProductCodes]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [info].[Countries] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [info].[Countries] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([ManufacturerId])
REFERENCES [info].[Manufacturers] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([MarketingAuthorizHolderId])
REFERENCES [info].[MarketingAuthorizHolders] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([MarketingAuthorizNumberId])
REFERENCES [info].[MarketingAuthorizNumbers] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([PackSizeId])
REFERENCES [info].[PackSizes] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([PharmaceuticalFormId])
REFERENCES [info].[PharmaceuticalForms] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([ProductNameId])
REFERENCES [info].[ProductNames] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([ProductCodeId])
REFERENCES [info].[ProductCodes] ([Id])
GO
ALTER TABLE [product].[Products]  WITH CHECK ADD FOREIGN KEY([StrengthId])
REFERENCES [info].[Strength] ([Id])
GO

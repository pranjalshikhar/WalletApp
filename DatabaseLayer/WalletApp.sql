--CREATE DATABASE WalletApp
--go

USE WalletApp
GO


IF OBJECT_ID('UserCard') IS NOT NULL
	DROP TABLE UserCard
GO

IF OBJECT_ID('Bank') IS NOT NULL
	DROP TABLE Bank
GO

IF OBJECT_ID('UserTransaction') IS NOT NULL
	DROP TABLE UserTransaction
GO

IF OBJECT_ID('MerchantTransaction') IS NOT NULL
	DROP TABLE MerchantTransaction
GO


IF OBJECT_ID('PaymentType') IS NOT NULL
	DROP TABLE PaymentType
GO



IF OBJECT_ID('MerchantServiceMapping') IS NOT NULL
	DROP TABLE MerchantServiceMapping
GO

IF OBJECT_ID('MerchantServiceType') IS NOT NULL
	DROP TABLE MerchantServiceType
GO

IF OBJECT_ID('Merchant') IS NOT NULL
	DROP TABLE Merchant
GO

IF OBJECT_ID('OTP') IS NOT NULL
	DROP TABLE OTP
GO

IF OBJECT_ID('OTPPurpose') IS NOT NULL
	DROP TABLE OTPPurpose
GO

IF OBJECT_ID('PaymentType') IS NOT NULL
	DROP TABLE PaymentType
GO


IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE [User]
GO



IF OBJECT_ID('Status') IS NOT NULL
	DROP TABLE Status
GO

/****** Object:  Table [dbo].[Bank]    Script Date: 3/5/2018 6:18:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bank](
	[BankId] [tinyint] IDENTITY(1,1) NOT NULL,
	[BankName] [varchar](50) NOT NULL,
 CONSTRAINT [pk_Bank_BankId] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Merchant]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Merchant](
	[MerchantId] [smallint] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](255) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[MobileNumber] [varchar](10) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [df_Merchant_IsActive]  DEFAULT ((1)),
	[CreatedTimestamp] [datetime2](7) NOT NULL CONSTRAINT [df_Merchant_CreatedTimestamp]  DEFAULT (getdate()),
	[ModifiedTimestamp] [datetime2](7) NULL,
 CONSTRAINT [pk_Merchant_MerchantId] PRIMARY KEY CLUSTERED 
(
	[MerchantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MerchantServiceMapping]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MerchantServiceMapping](
	[MerchantServiceMappingId] [smallint] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](255) NOT NULL,
	[ServiceId] [tinyint] NOT NULL,
	[DiscountPercent] [decimal](5, 2) NULL CONSTRAINT [df_MerchantServices_Discount]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [df_MerchantServices_IsActive]  DEFAULT ((1)),
	[CreatedTimestamp] [datetime2](7) NOT NULL CONSTRAINT [df_MerchantServices_CreatedTimestamp]  DEFAULT (getdate()),
	[ModifiedTimestamp] [datetime2](7) NULL,
 CONSTRAINT [uk_MerchantServicesMapping_EmailIdServicesId] UNIQUE CLUSTERED 
(
	[EmailId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MerchantServiceType]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MerchantServiceType](
	[ServiceId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ServiceType] [varchar](50) NOT NULL,
 CONSTRAINT [pk_MerchantServiceType_ServiceId] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MerchantTransaction]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MerchantTransaction](
	[TransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](255) NOT NULL,
	[Amount] [money] NOT NULL,
	[TransactionDateTime] [datetime] NOT NULL,
	[PaymentTypeId] [tinyint] NOT NULL,
	[Remarks] [varchar](50) NULL,
	[Info] [varchar](100) NOT NULL,
	[StatusId] [tinyint] NOT NULL,
 CONSTRAINT [pk_MerchantTransaction_TransactionId] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OTP]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OTP](
	[OTPId] [int] IDENTITY(1,1) NOT NULL,
	[OTPValue] [varchar](6) NOT NULL,
	[ReferenceId] [varchar](255) NOT NULL,
	[ExpiryDateTime] [datetime] NOT NULL,
	[OTPPurposeId] [tinyint] NOT NULL,
	[IsValid] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[OTPId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OTPPurpose]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OTPPurpose](
	[OTPPurposeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[OTPPurpose] [varchar](30) NOT NULL,
 CONSTRAINT [pk_OTPPurpose_OTPPurposeId] PRIMARY KEY CLUSTERED 
(
	[OTPPurposeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentType]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentType](
	[PaymentTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[PaymentFrom] [char](1) NOT NULL,
	[PaymentTo] [char](1) NOT NULL,
	[PaymentType] [bit] NOT NULL,
 CONSTRAINT [pk_PaymentType_PaymentTypeId] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [tinyint] IDENTITY(1,1) NOT NULL,
	[StatusValue] [varchar](20) NOT NULL,
 CONSTRAINT [pk_Status_StatusId] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmailID] [varchar](255) NOT NULL,
	[MobileNumber] [varchar](10) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[StatusId] [tinyint] NOT NULL,
	[CreatedTimestamp] [datetime2](7) NOT NULL CONSTRAINT [df_User_CreatedTimestamp]  DEFAULT (getdate()),
	[ModifiedTimestamp] [datetime2](7) NULL,
 CONSTRAINT [pk_User_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCard]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserCard](
	[UserCardMappingId] [int] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](255) NOT NULL,
	[CardNumber] [varchar](16) NOT NULL,
	[BankId] [tinyint] NOT NULL,
	[ExpiryDate] [smalldatetime] NOT NULL,
	[StatusId] [tinyint] NOT NULL,
	[CreatedTimestamp] [datetime2](7) NOT NULL,
	[ModifiedTimestamp] [datetime2](7) NULL,
 CONSTRAINT [pk_UserCardMappingId] PRIMARY KEY CLUSTERED 
(
	[UserCardMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserTransaction]    Script Date: 3/5/2018 6:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserTransaction](
	[UserTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](255) NOT NULL,
	[Amount] [money] NOT NULL,
	[TransactionDateTime] [datetime] NOT NULL,
	[PaymentTypeId] [tinyint] NOT NULL,
	[Remarks] [varchar](50) NULL,
	[Info] [varchar](100) NOT NULL,
	[StatusId] [tinyint] NOT NULL,
	[PointsEarned] [smallint] NULL,
	[IsRedeemed] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Bank] ON 

INSERT [dbo].[Bank] ([BankId], [BankName]) VALUES (1, N'EDU Bank')
SET IDENTITY_INSERT [dbo].[Bank] OFF
SET IDENTITY_INSERT [dbo].[Merchant] ON 

INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (1, N'et@Quisque.com', N'Hyatt Buckner', N'Mer!123', N'1107421849', 1, CAST(N'2017-08-10 17:24:50.4130000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (2, N'ut.nisi@Aliquam.co.uk', N'Fuller Walter', N'Mer!1234', N'6540516415', 1, CAST(N'2017-08-10 17:24:50.4200000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (3, N'lectus@velitegetlaoreet.co.uk', N'Jerome Howe', N'Mer!1234', N'6080325011', 1, CAST(N'2017-08-10 17:24:50.4230000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (4, N'sed.facilisis@parturientmontesnascetur.co.uk', N'Galvin Watkins', N'Mer!1234', N'409602481', 1, CAST(N'2017-08-10 17:24:50.4300000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (5, N'et.malesuada.fames@molestiearcuSed.co.uk', N'Garrison Guthrie', N'Mer!1234', N'7410521854', 1, CAST(N'2017-08-10 17:24:50.4370000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (6, N'auctor.quis@elementum.ca', N'Dane Wolf', N'Mer!1234', N'7498504051', 1, CAST(N'2017-08-10 17:24:50.4430000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (7, N'nunc.est@Uttincidunt.edu', N'Rashad Beasley', N'Mer!1234', N'4333526913', 1, CAST(N'2017-08-10 17:24:50.4500000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (8, N'mus.Proin@semmolestie.com', N'Lucian Sutton', N'Mer!1234', N'2840866813', 1, CAST(N'2017-08-10 17:24:50.4530000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (9, N'ornare@Phasellusdolor.org', N'Travis Mccall', N'Mer!1234', N'9831559083', 1, CAST(N'2017-08-10 17:24:50.4600000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (10, N'aliquet.lobortis@non.net', N'Armand Guerra', N'Mer!1234', N'7098054097', 1, CAST(N'2017-08-10 17:24:50.4670000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (11, N'Nam@ullamcorpervelit.org', N'Orson Baldwin', N'Mer!1234', N'5000606004', 1, CAST(N'2017-08-10 17:24:50.4730000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (12, N'nunc.nulla.vulputate@egestasblandit.co.uk', N'Mark Welch', N'Mer!1234', N'9681940840', 1, CAST(N'2017-08-10 17:24:50.4800000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (13, N'quis.pede@euismodurna.edu', N'Vance Clements', N'Mer!1234', N'716132876', 1, CAST(N'2017-08-10 17:24:50.4900000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (14, N'ante@Suspendissenonleo.co.uk', N'Gray Daniels', N'Mer!1234', N'1325580939', 1, CAST(N'2017-08-10 17:24:50.4930000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (15, N'Integer.vulputate.risus@sagittisplacerat.ca', N'Macaulay Ferrell', N'Mer!1234', N'518788904', 1, CAST(N'2017-08-10 17:24:50.5000000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (16, N'ligula.Aenean.gravida@sitamet.com', N'Dale Durham', N'Mer!1234', N'8819570490', 1, CAST(N'2017-08-10 17:24:50.5070000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (17, N'egestas.rhoncus.Proin@vitaealiquameros.ca', N'Cedric Cooke', N'Mer!1234', N'9262793567', 1, CAST(N'2017-08-10 17:24:50.5130000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (18, N'orci@Etiam.net', N'Price Hodges', N'Mer!1234', N'6950607489', 1, CAST(N'2017-08-10 17:24:50.5200000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (19, N'mi.enim@mattis.ca', N'Valentine Mitchell', N'Mer!1234', N'4854029680', 1, CAST(N'2017-08-10 17:24:50.5230000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (20, N'est.mollis@nonloremvitae.ca', N'Tucker Ellison', N'Mer!1234', N'3344734617', 1, CAST(N'2017-08-10 17:24:50.5300000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (21, N'montes@enim.org', N'Derek Ayers', N'Mer!1234', N'6082724156', 1, CAST(N'2017-08-10 17:24:50.5370000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (22, N'tincidunt@Duis.net', N'Xander Vance', N'Mer!1234', N'6204631403', 1, CAST(N'2017-08-10 17:24:50.5430000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (23, N'parturient@Pellentesquehabitantmorbi.co.uk', N'Bevis Sandoval', N'Mer!1234', N'2646024208', 1, CAST(N'2017-08-10 17:24:50.5500000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (24, N'ut.nulla@mienimcondimentum.net', N'Quentin Garcia', N'Mer!1234', N'4449627229', 1, CAST(N'2017-08-10 17:24:50.5530000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (25, N'penatibus.et@placeratorcilacus.com', N'Brady Goff', N'Mer!1234', N'6488664165', 1, CAST(N'2017-08-10 17:24:50.5600000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (26, N'vel@bibendumfermentummetus.edu', N'Driscoll Estrada', N'Mer!1234', N'7779042431', 1, CAST(N'2017-08-10 17:24:50.5670000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (27, N'tortor@tristiquepharetra.com', N'Hammett Herrera', N'Mer!1234', N'5956311472', 1, CAST(N'2017-08-10 17:24:50.5730000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (28, N'bibendum.fermentum@accumsanconvallis.net', N'Arthur Mcfarland', N'Mer!1234', N'8377249817', 1, CAST(N'2017-08-10 17:24:50.5800000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (29, N'non.lacinia@tristique.co.uk', N'Zahir Potter', N'Mer!1234', N'1603296925', 1, CAST(N'2017-08-10 17:24:50.5830000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (30, N'sit.amet@nuncid.org', N'Wade Hansen', N'Mer!1234', N'5942455065', 1, CAST(N'2017-08-10 17:24:50.5900000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (31, N'ad.litora.torquent@Duisacarcu.net', N'Zane Brown', N'Mer!1234', N'2411214504', 1, CAST(N'2017-08-10 17:24:50.5970000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (32, N'orci@necante.org', N'Brennan Henson', N'Mer!1234', N'4155834362', 1, CAST(N'2017-08-10 17:24:50.6030000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (33, N'Quisque.fringilla.euismod@eunibhvulputate.com', N'Dorian Lloyd', N'Mer!1234', N'5487813749', 1, CAST(N'2017-08-10 17:24:50.6100000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (34, N'arcu.Aliquam@magnaseddui.com', N'Joseph Cline', N'Mer!1234', N'2256900220', 1, CAST(N'2017-08-10 17:24:50.6130000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (35, N'tellus.id.nunc@posuerevulputatelacus.edu', N'Chester Mullins', N'Mer!1234', N'340678756', 1, CAST(N'2017-08-10 17:24:50.6200000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (36, N'non@eunulla.net', N'Nolan Waller', N'Mer!1234', N'2361587210', 1, CAST(N'2017-08-10 17:24:50.6270000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (37, N'metus.urna.convallis@molestieSed.org', N'Paki Mccarty', N'Mer!1234', N'9167605670', 1, CAST(N'2017-08-10 17:24:50.6330000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (38, N'malesuada.id@fermentummetus.co.uk', N'Cade Mueller', N'Mer!1234', N'4449203782', 1, CAST(N'2017-08-10 17:24:50.6400000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (39, N'Praesent@aaliquet.edu', N'Dolan Santos', N'Mer!1234', N'5136645609', 1, CAST(N'2017-08-10 17:24:50.6430000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (40, N'eu.lacus@amet.com', N'Yardley Love', N'Mer!1234', N'5851837302', 1, CAST(N'2017-08-10 17:24:50.6500000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (41, N'Maecenas.mi.felis@quis.net', N'Oleg Schroeder', N'Mer!1234', N'7626034053', 1, CAST(N'2017-08-10 17:24:50.6570000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (42, N'amet.dapibus@urna.net', N'Alfonso Rasmussen', N'Mer!1234', N'4365653323', 1, CAST(N'2017-08-10 17:24:50.6630000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (43, N'libero@dolortempusnon.com', N'Marvin Buckner', N'Mer!1234', N'3438621350', 1, CAST(N'2017-08-10 17:24:50.6700000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (44, N'pede.nec@tellusPhaselluselit.edu', N'Callum Savage', N'Mer!1234', N'3369354138', 1, CAST(N'2017-08-10 17:24:50.6730000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (45, N'aliquet@augue.co.uk', N'Xander Drake', N'Mer!1234', N'7434992598', 1, CAST(N'2017-08-10 17:24:50.6800000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (46, N'Donec@Duiselementumdui.edu', N'Xanthus Mccall', N'Mer!1234', N'6820019033', 1, CAST(N'2017-08-10 17:24:50.6870000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (47, N'Nunc@adipiscingligula.org', N'Len Goff', N'Mer!1234', N'6507681488', 1, CAST(N'2017-08-10 17:24:50.6930000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (48, N'In.nec.orci@Donecsollicitudinadipiscing.edu', N'Kareem Little', N'Mer!1234', N'6477566741', 1, CAST(N'2017-08-10 17:24:50.7000000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (49, N'amet.dapibus@Phasellusin.com', N'Tarik Church', N'Mer!1234', N'8199309870', 1, CAST(N'2017-08-10 17:24:50.7030000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (50, N'Pellentesque.tincidunt.tempus@diam.net', N'Caleb Gray', N'Mer!1234', N'5110812930', 1, CAST(N'2017-08-10 17:24:50.7100000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (51, N'pede@Crasdolordolor.ca', N'Joseph Carson', N'Mer!1234', N'3031922694', 1, CAST(N'2017-08-10 17:24:50.7170000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (52, N'magna.Sed@conubianostra.com', N'Wyatt Haley', N'Mer!1234', N'7929129875', 1, CAST(N'2017-08-10 17:24:50.7230000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (53, N'Duis@tempor.com', N'Clarke Spears', N'Mer!1234', N'6206759951', 1, CAST(N'2017-08-10 17:24:50.7370000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (54, N'vitae@metusIn.edu', N'Noble Yang', N'Mer!1234', N'4353554598', 1, CAST(N'2017-08-10 17:24:50.7470000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (55, N'augue@quis.ca', N'Joseph Small', N'Mer!1234', N'6948253606', 1, CAST(N'2017-08-10 17:24:50.7530000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (56, N'mauris@Proinvelnisl.net', N'Hamilton Frank', N'Mer!1234', N'3449395236', 1, CAST(N'2017-08-10 17:24:50.7600000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (57, N'et.magnis.dis@euerat.com', N'Jarrod Watkins', N'Mer!1234', N'1020153668', 1, CAST(N'2017-08-10 17:24:50.7670000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (58, N'urna.convallis.erat@SuspendisseduiFusce.net', N'Hall Stanton', N'Mer!1234', N'355224329', 1, CAST(N'2017-08-10 17:24:50.7700000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (59, N'diam.dictum.sapien@semperpretiumneque.edu', N'Samson Hickman', N'Mer!1234', N'5235742298', 1, CAST(N'2017-08-10 17:24:50.7770000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (60, N'rutrum.justo@anteMaecenas.ca', N'Hammett Rosa', N'Mer!1234', N'6407814604', 1, CAST(N'2017-08-10 17:24:50.7830000' AS DateTime2), NULL)
INSERT [dbo].[Merchant] ([MerchantId], [EmailId], [Name], [Password], [MobileNumber], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (61, N'ut.sem@egetmagna.edu', N'Dillon Madden', N'Mer!1234', N'1964459826', 1, CAST(N'2017-08-10 17:24:50.7900000' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Merchant] OFF
SET IDENTITY_INSERT [dbo].[MerchantServiceMapping] ON 

INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (6, N'auctor.quis@elementum.ca', 2, CAST(9.00 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2970000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (5, N'et.malesuada.fames@molestiearcuSed.co.uk', 1, CAST(7.95 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2900000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (1, N'et@Quisque.com', 2, CAST(7.75 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2700000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (3, N'lectus@velitegetlaoreet.co.uk', 3, CAST(3.15 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2770000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (8, N'mus.Proin@semmolestie.com', 2, CAST(10.25 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.3370000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (7, N'nunc.est@Uttincidunt.edu', 1, CAST(3.25 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.3030000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (4, N'sed.facilisis@parturientmontesnascetur.co.uk', 3, CAST(6.35 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2830000' AS DateTime2), NULL)
INSERT [dbo].[MerchantServiceMapping] ([MerchantServiceMappingId], [EmailId], [ServiceId], [DiscountPercent], [IsActive], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (2, N'ut.nisi@Aliquam.co.uk', 1, CAST(5.25 AS Decimal(5, 2)), 1, CAST(N'2017-08-10 17:24:51.2700000' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[MerchantServiceMapping] OFF
SET IDENTITY_INSERT [dbo].[MerchantServiceType] ON 

INSERT [dbo].[MerchantServiceType] ([ServiceId], [ServiceType]) VALUES (1, N'Broadband')
INSERT [dbo].[MerchantServiceType] ([ServiceId], [ServiceType]) VALUES (3, N'DTH')
INSERT [dbo].[MerchantServiceType] ([ServiceId], [ServiceType]) VALUES (2, N'Mobile')
SET IDENTITY_INSERT [dbo].[MerchantServiceType] OFF
SET IDENTITY_INSERT [dbo].[OTPPurpose] ON 

INSERT [dbo].[OTPPurpose] ([OTPPurposeId], [OTPPurpose]) VALUES (3, N'Add Card')
INSERT [dbo].[OTPPurpose] ([OTPPurposeId], [OTPPurpose]) VALUES (1, N'Register')
INSERT [dbo].[OTPPurpose] ([OTPPurposeId], [OTPPurpose]) VALUES (2, N'Transaction')
SET IDENTITY_INSERT [dbo].[OTPPurpose] OFF
SET IDENTITY_INSERT [dbo].[PaymentType] ON 

INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (1, N'W', N'W', 0)
INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (2, N'W', N'W', 1)
INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (3, N'B', N'W', 1)
INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (4, N'W', N'B', 0)
INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (5, N'W', N'M', 0)
INSERT [dbo].[PaymentType] ([PaymentTypeId], [PaymentFrom], [PaymentTo], [PaymentType]) VALUES (6, N'R', N'W', 1)
SET IDENTITY_INSERT [dbo].[PaymentType] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (1, N'Success')
INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (2, N'Failure')
INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (3, N'Pending')
INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (4, N'Approved')
INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (5, N'Denied')
INSERT [dbo].[Status] ([StatusId], [StatusValue]) VALUES (6, N'InActive')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (1, N'mvanyashkin0@fastcompany.com', N'1235641573', N'Melanie Vanyashkin', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.1900000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (2, N'rstraun1@nydailynews.com', N'7733360609', N'Ranique Straun', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2000000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (3, N'pkahler2@ameblo.jp', N'4124831055', N'Patten Kahler', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2200000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (4, N'fhorsewood3@reverbnation.com', N'1552743880', N'Florenza Horsewood', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2270000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (5, N'tgeerdts4@arizona.edu', N'1722889999', N'Trescha Geerdts', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2370000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (6, N'tvacher5@odnoklassniki.ru', N'4772803411', N'Tyne Vacher', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2500000' AS DateTime2), NULL)
INSERT [dbo].[User] ([UserId], [EmailID], [MobileNumber], [Name], [Password], [StatusId], [CreatedTimestamp], [ModifiedTimestamp]) VALUES (7, N'gyarnley6@uiuc.edu', N'3594010701', N'Granville Yarnley', N'User@1234', 1, CAST(N'2017-08-10 17:24:49.2570000' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [uk_Merchant_EmailId]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[Merchant] ADD  CONSTRAINT [uk_Merchant_EmailId] UNIQUE NONCLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pk_MerchantServicesMapping_MerchantServiceMMapping]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[MerchantServiceMapping] ADD  CONSTRAINT [pk_MerchantServicesMapping_MerchantServiceMMapping] PRIMARY KEY NONCLUSTERED 
(
	[MerchantServiceMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [uk_MerchantServiceType_ServiceType]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[MerchantServiceType] ADD  CONSTRAINT [uk_MerchantServiceType_ServiceType] UNIQUE NONCLUSTERED 
(
	[ServiceType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [pk_OTP_OTPReferenceIdExpiryDateTime]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[OTP] ADD  CONSTRAINT [pk_OTP_OTPReferenceIdExpiryDateTime] UNIQUE NONCLUSTERED 
(
	[OTPValue] ASC,
	[ReferenceId] ASC,
	[ExpiryDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [uk_OTPPurpose_OTPPurpose]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[OTPPurpose] ADD  CONSTRAINT [uk_OTPPurpose_OTPPurpose] UNIQUE NONCLUSTERED 
(
	[OTPPurpose] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [uk_User_EmailID]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [uk_User_EmailID] UNIQUE NONCLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [uk_User_MobileNumber]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [uk_User_MobileNumber] UNIQUE NONCLUSTERED 
(
	[MobileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [pk_UserCard_EmailId_CardNumber]    Script Date: 3/5/2018 6:18:48 PM ******/
ALTER TABLE [dbo].[UserCard] ADD  CONSTRAINT [pk_UserCard_EmailId_CardNumber] UNIQUE NONCLUSTERED 
(
	[EmailId] ASC,
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MerchantTransaction] ADD  DEFAULT (getdate()) FOR [TransactionDateTime]
GO
ALTER TABLE [dbo].[OTP] ADD  CONSTRAINT [df_OTPs_IsValid]  DEFAULT ((1)) FOR [IsValid]
GO
ALTER TABLE [dbo].[UserCard] ADD  CONSTRAINT [df_UserCard_CreatedTimestamp]  DEFAULT (getdate()) FOR [CreatedTimestamp]
GO
ALTER TABLE [dbo].[UserTransaction] ADD  DEFAULT (getdate()) FOR [TransactionDateTime]
GO
ALTER TABLE [dbo].[UserTransaction] ADD  CONSTRAINT [df_UserTransaction_PointsEarned]  DEFAULT ((0)) FOR [PointsEarned]
GO
ALTER TABLE [dbo].[UserTransaction] ADD  CONSTRAINT [df_UserTransaction_IsRedeemed]  DEFAULT ((0)) FOR [IsRedeemed]
GO
ALTER TABLE [dbo].[MerchantServiceMapping]  WITH CHECK ADD  CONSTRAINT [fk_MerchantServicesMapping_EmailId] FOREIGN KEY([EmailId])
REFERENCES [dbo].[Merchant] ([EmailId])
GO
ALTER TABLE [dbo].[MerchantServiceMapping] CHECK CONSTRAINT [fk_MerchantServicesMapping_EmailId]
GO
ALTER TABLE [dbo].[MerchantServiceMapping]  WITH CHECK ADD  CONSTRAINT [fk_MerchantServicesMapping_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[MerchantServiceType] ([ServiceId])
GO
ALTER TABLE [dbo].[MerchantServiceMapping] CHECK CONSTRAINT [fk_MerchantServicesMapping_ServiceId]
GO
ALTER TABLE [dbo].[MerchantTransaction]  WITH CHECK ADD  CONSTRAINT [fk_MerchantTransaction_EmailId] FOREIGN KEY([EmailId])
REFERENCES [dbo].[Merchant] ([EmailId])
GO
ALTER TABLE [dbo].[MerchantTransaction] CHECK CONSTRAINT [fk_MerchantTransaction_EmailId]
GO
ALTER TABLE [dbo].[MerchantTransaction]  WITH CHECK ADD  CONSTRAINT [fk_MerchantTransaction_PaymentTypeId] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([PaymentTypeId])
GO
ALTER TABLE [dbo].[MerchantTransaction] CHECK CONSTRAINT [fk_MerchantTransaction_PaymentTypeId]
GO
ALTER TABLE [dbo].[MerchantTransaction]  WITH CHECK ADD  CONSTRAINT [fk_MerchantTransaction_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[MerchantTransaction] CHECK CONSTRAINT [fk_MerchantTransaction_Status]
GO
ALTER TABLE [dbo].[OTP]  WITH CHECK ADD  CONSTRAINT [fk_OTP_OTPPurposeId] FOREIGN KEY([OTPPurposeId])
REFERENCES [dbo].[OTPPurpose] ([OTPPurposeId])
GO
ALTER TABLE [dbo].[OTP] CHECK CONSTRAINT [fk_OTP_OTPPurposeId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [chk_User_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [chk_User_Status]
GO
ALTER TABLE [dbo].[UserCard]  WITH CHECK ADD  CONSTRAINT [fk_UserCard_BankId] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([BankId])
GO
ALTER TABLE [dbo].[UserCard] CHECK CONSTRAINT [fk_UserCard_BankId]
GO
ALTER TABLE [dbo].[UserCard]  WITH CHECK ADD  CONSTRAINT [fk_UserCard_EmailId] FOREIGN KEY([EmailId])
REFERENCES [dbo].[User] ([EmailID])
GO
ALTER TABLE [dbo].[UserCard] CHECK CONSTRAINT [fk_UserCard_EmailId]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [fk_UserTransaction_EmailId] FOREIGN KEY([EmailId])
REFERENCES [dbo].[User] ([EmailID])
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [fk_UserTransaction_EmailId]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [fk_UserTransaction_PaymentTypeId] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([PaymentTypeId])
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [fk_UserTransaction_PaymentTypeId]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [fk_UserTransaction_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [fk_UserTransaction_StatusId]
GO
ALTER TABLE [dbo].[UserTransaction] ADD CONSTRAINT [pk_UserTransaction_UserTransactionId] PRIMARY KEY (UserTransactionId)
GO

ALTER TABLE [dbo].[MerchantTransaction]  WITH CHECK ADD  CONSTRAINT [chk_MerchantTransaction_Amount] CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[MerchantTransaction] CHECK CONSTRAINT [chk_MerchantTransaction_Amount]
GO
ALTER TABLE [dbo].[PaymentType]  WITH CHECK ADD  CONSTRAINT [chk_PaymentType_PaymentFrom] CHECK  (([PaymentFrom]='W' OR [PaymentFrom]='M' OR [PaymentFrom]='B' OR [PaymentFrom]='R'))
GO
ALTER TABLE [dbo].[PaymentType] CHECK CONSTRAINT [chk_PaymentType_PaymentFrom]
GO
ALTER TABLE [dbo].[PaymentType]  WITH CHECK ADD  CONSTRAINT [chk_PaymentType_PaymentTo] CHECK  (([PaymentTo]='W' OR [PaymentTo]='M' OR [PaymentTo]='B'))
GO
ALTER TABLE [dbo].[PaymentType] CHECK CONSTRAINT [chk_PaymentType_PaymentTo]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [chk_UserTransaction_Amount] CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [chk_UserTransaction_Amount]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [chk_UserTransaction_PointsEarned] CHECK  (([PointsEarned]>=(0)))
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [chk_UserTransaction_PointsEarned]
GO

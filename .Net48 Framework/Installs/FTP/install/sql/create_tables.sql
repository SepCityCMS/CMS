/****** Object:  Table [dbo].[AccessClasses]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessClasses](
	[ClassName] [nvarchar](50) NULL,
	[KeyIDs] varchar(max) NULL,
	[LoggedDays] [int] NULL,
	[LoggedSwitchTo] varchar(40) NULL,
	[InDays] [int] NULL,
	[PrivateClass] [bit] NULL,
	[Description] [nvarchar](max) NULL,
	[SortGroup] [int] NULL,
	[InSwitchTo] varchar(40) NULL,
	[PortalIDs] [varchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[ClassID] [float] NOT NULL,
 CONSTRAINT [PK_ClassID_AccessClasses] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccessKeys]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessKeys](
	[KeyName] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[KeyID] [float] NOT NULL,
 CONSTRAINT [PK_KeyID_AccessKeys] PRIMARY KEY CLUSTERED 
(
	[KeyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Activities]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[ActivityID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[ActType] [nvarchar](50) NULL,
	[IPAddress] [varchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ModuleID] [int] NULL,
	[UniqueID] [float] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_ActivityID_Activities] PRIMARY KEY CLUSTERED 
(
	[ActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Advertisements]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertisements](
	[AdID] [float] NOT NULL,
	[MaxClicks] [int] NULL,
	[MaxExposures] [int] NULL,
	[TotalClicks] [int] NULL,
	[TotalExposures] [int] NULL,
	[ImageURL] [varchar](255) NULL,
	[SiteURL] [varchar](255) NULL,
	[UserID] [varchar](40) NULL,
	[CatIDs] varchar(max) NULL,
	[PortalIDs] [varchar](max) NULL,
	[PageIDs] varchar(max) NULL,
	[Description] [nvarchar](max) NULL,
	[UseHTML] [bit] NULL,
	[Status] [int] NULL,
	[HTMLCode] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ZoneID] [float] NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageType] [varchar](25) NULL,
	[DateDeleted] [datetime] NULL,
	[Country] [varchar](2) NULL,
	[State] [varchar](50) NULL,
 CONSTRAINT [PK_Advertisements_1_10] PRIMARY KEY CLUSTERED 
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AffiliatePaid]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AffiliatePaid](
	[PaidID] [float] NOT NULL,
	[InvoiceID] [float] NOT NULL,
	[AffiliateID] [float] NOT NULL,
	[DatePaid] [datetime] NULL,
	[AmountPaid] [decimal](18, 4) NULL,
 CONSTRAINT [PK_PaidID_1_10] PRIMARY KEY CLUSTERED 
(
	[PaidID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ApprovalEmails]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalEmails](
	[EmailID] [float] NOT NULL,
	[ApproveID] [varchar](40) NULL,
	[FullName] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[Weight] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_EmailID_1_10] PRIMARY KEY CLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Approvals]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Approvals](
	[ApproveID] [float] NOT NULL,
	[ChainName] [nvarchar](100) NULL,
	[ModuleIDs] [varchar](max) NULL,
	[PortalIDs] [varchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_ApproveID_1_10] PRIMARY KEY CLUSTERED 
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApprovalXML]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalXML](
	[XMLID] [float] NOT NULL,
	[ApproveID] [varchar](40) NULL,
	[ModuleID] [int] NULL,
	[UniqueID] [varchar](40) NULL,
	[XMLData] [nvarchar](max) NULL,
	[ApproveXML] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_XMLID_1_10] PRIMARY KEY CLUSTERED 
(
	[XMLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Articles]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleID] [float] NOT NULL,
	[Headline] [nvarchar](100) NULL,
	[Author] [nvarchar](100) NULL,
	[Headline_Date] [datetime] NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Summary] [nvarchar](max) NULL,
	[Full_Article] [nvarchar](max) NULL,
	[UserID] [varchar](40) NULL,
	[CatID] [float] NULL,
	[Source] [varchar](255) NULL,
	[Article_URL] [varchar](255) NULL,
	[Meta_Description] [nvarchar](max) NULL,
	[Meta_Keywords] [nvarchar](max) NULL,
	[Related_Articles] [varchar](max) NULL,
	[Visits] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_Articles_1_10] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Associations]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Associations](
	[AssociationID] [float] NOT NULL,
	[UniqueID] [float] NULL,
	[ModuleID] [int] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_AssociationID_1_10] PRIMARY KEY CLUSTERED 
(
	[AssociationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AuctionAds]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuctionAds](
	[AdID] [float] NOT NULL,
	[CatID] [float] NULL,
	[LinkID] [float] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[StartBid] [decimal](18, 4) NULL,
	[CurrentBid] [decimal](18, 4) NULL,
	[MaxBid] [decimal](18, 4) NULL,
	[BidUserID] [varchar](40) NULL,
	[BidIncrease] [decimal](18, 4) NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TotalBids] [int] NULL,
	[Visits] [int] NULL,
	[OldAd] [bit] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_AuctionAds_1_10] PRIMARY KEY CLUSTERED 
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AuctionFeedback]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuctionFeedback](
	[FeedbackID] [float] NOT NULL,
	[AdID] [float] NOT NULL,
	[Message] [nvarchar](255) NULL,
	[ToUserID] [varchar](40) NULL,
	[FromUserID] [varchar](40) NULL,
	[BORS] [varchar](1) NULL,
	[Rating] [int] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_FeedbackID_AuctionFeedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BG_Emails]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BG_Emails](
	[ProcessID] [float] NOT NULL,
	[To_Email_Address] [varchar](100) NULL,
	[From_Email_Address] [varchar](100) NULL,
	[Email_Subject] [varchar](100) NULL,
	[Email_Body] [nvarchar](max) NULL,
	[Email_Attachment] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BG_Processes]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BG_Processes](
	[ProcessID] [float] NOT NULL,
	[ProcessName] [varchar](20) NULL,
	[DateStarted] [datetime] NULL,
	[DateEnded] [datetime] NULL,
	[IntervalSeconds] [int] NULL,
	[RecurringDays] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [pk_ProcessID_BG_Processes] PRIMARY KEY CLUSTERED 
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BG_SMS]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BG_SMS](
	[ProcessID] [float] NOT NULL,
	[To_Phone] [varchar](50) NULL,
	[From_Phone] [varchar](50) NULL,
	[Message_Body] [varchar](200) NULL,
	[Send_Date] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogID] [float] NOT NULL,
	[CatID] [float] NULL,
	[BlogName] [nvarchar](50) NULL,
	[UserID] [varchar](40) NULL,
	[Message] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[Comments] [bit] NULL,
	[Hits] [int] NULL,
	[EmailReply] [bit] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_Blog_1_10] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BusinessListings]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BusinessListings](
	[BusinessID] [float] NOT NULL,
	[CatID] [float] NULL,
	[BusinessName] [nvarchar](100) NULL,
	[SiteURL] [varchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[FullDescription] [nvarchar](max) NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[FaxNumber] [nvarchar](30) NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](15) NULL,
	[Country] [nvarchar](3) NULL,
	[DatePosted] [datetime] NULL,
	[Visits] [int] NULL,
	[LinkID] [float] NULL,
	[AlbumID] [float] NULL,
	[ClaimID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[TwitterLink] [varchar](255) NULL,
	[FacebookLink] [varchar](255) NULL,
	[LinkedInLink] [varchar](255) NULL,
	[OfficeHours] [varchar](255) NULL,
	[IncludeProfile] [bit] NULL,
	[IncludeMap] [bit] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_BusinessID_1_10] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [float] NOT NULL,
	[ListUnder] [float] NULL,
	[CategoryName] [nvarchar](100) NULL,
	[CatType] [varchar](12) NULL,
	[Moderator] [nvarchar](25) NULL,
	[Description] [nvarchar](max) NULL,
	[Keywords] [nvarchar](max) NULL,
	[SEODescription] [nvarchar](max) NULL,
	[SEOPageTitle] [nvarchar](150) NULL,
	[AccessKeys] [varchar](4000) NULL,
	[WriteKeys] [varchar](4000) NULL,
	[ManageKeys] [varchar](4000) NULL,
	[AccessHide] [bit] NULL,
	[WriteHide] [bit] NULL,
	[ExcPortalSecurity] [bit] NULL,
	[Weight] [int] NULL,
	[ShowList] [bit] NULL,
	[Sharing] [bit] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageType] [varchar](25) NULL,
	[FileName] [varchar](25) NULL,
	[FeedID] [float] NULL,
 CONSTRAINT [PK_CatID_Categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoriesModules]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesModules](
	[UniqueID] [float] NOT NULL,
	[CatID] [float] NOT NULL,
	[ModuleID] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_UniqueID_8_10] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoriesPages]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesPages](
	[UniqueID] [float] NOT NULL,
	[CatID] [float] NOT NULL,
	[ModuleID] [int] NULL,
	[PageText] [nvarchar](max) NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_UniqueID_7_10] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoriesPortals]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesPortals](
	[UniqueID] [float] NOT NULL,
	[CatID] [float] NOT NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_UniqueID_9_10] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClassifiedsAds]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassifiedsAds](
	[AdID] [float] NOT NULL,
	[CatID] [float] NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 4) NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Visits] [int] NULL,
	[LinkID] [float] NULL,
	[SoldUserID] [varchar](40) NULL,
	[SoldDate] [datetime] NULL,
	[Soldout] [bit] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_AdID_1_10] PRIMARY KEY CLUSTERED 
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClassifiedsFeedback]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassifiedsFeedback](
	[FeedbackID] [float] NOT NULL,
	[AdID] [float] NULL,
	[Message] [nvarchar](255) NULL,
	[ToUserID] [varchar](40) NULL,
	[FromUserID] [varchar](40) NULL,
	[BORS] [varchar](1) NULL,
	[Rating] [int] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_FeedbackID_1_10] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[UniqueID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[FullName] [nvarchar](150) NULL,
	[ModuleID] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[IPAddress] [varchar](50) NULL,
	[DatePosted] [datetime] NULL,
	[UserLikes] [int] NULL,
	[UserDislikes] [int] NULL,
	[CommentID] [float] NOT NULL,
	[ReplyID] [float] NOT NULL,
 CONSTRAINT [PK_CommentID_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommentsLikes]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CommentsLikes](
	[LikeID] [float] NOT NULL,
	[CommentID] [float] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[UserLike] [int] NOT NULL,
	[UserDislike] [int] NOT NULL,
	[IPAddress] [varchar](20) NOT NULL,
	[UserID] [varchar](40) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [pk_LikeID_CommentsLikes] PRIMARY KEY CLUSTERED 
(
	[LikeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContentRotator]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentRotator](
	[ContentID] [float] NOT NULL,
	[ZoneID] [float] NULL,
	[CatIDs] [varchar](max) NULL,
	[PortalIDs] [varchar](max) NULL,
	[PageIDs] [varchar](max) NULL,
	[HTMLContent] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_ContentRotator_1_10] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomFieldOptions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldOptions](
	[OptionID] [float] NOT NULL,
	[FieldID] [float] NOT NULL,
	[OptionName] [nvarchar](100) NULL,
	[OptionValue] [nvarchar](100) NULL,
	[Price] [decimal](18, 4) NULL,
	[RecurringPrice] [decimal](18, 4) NULL,
	[Weight] [int] NULL,
 CONSTRAINT [PK_OptionID_4_10] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomFields]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFields](
	[FieldID] [float] NOT NULL,
	[SectionID] [float] NULL,
	[FieldName] [nvarchar](100) NULL,
	[AnswerType] [varchar](25) NULL,
	[FieldType] [varchar](2) NULL,
	[ListUnder] [varchar](12) NULL,
	[Required] [bit] NULL,
	[ModuleIDs] [varchar](max) NULL,
	[PortalIDs] [varchar](max) NULL,
	[UniqueIDs] [varchar](max) NULL,
	[Searchable] [int] NULL,
	[Weight] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_FieldID_3_10] PRIMARY KEY CLUSTERED 
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomFieldUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldUsers](
	[UserFieldID] [float] NOT NULL,
	[UniqueID] [varchar](40) NULL,
	[FieldID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[ModuleID] [int] NULL,
	[FieldValue] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_UserFieldID_3_10] PRIMARY KEY CLUSTERED 
(
	[UserFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomSections]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomSections](
	[SectionID] [float] NOT NULL,
	[SectionName] [nvarchar](100) NULL,
	[SectionText] [nvarchar](max) NULL,
	[Weight] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_SectionID_CustomSections] PRIMARY KEY CLUSTERED 
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiscountSystem]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscountSystem](
	[DiscountID] [float] NOT NULL,
	[DiscountCode] [nvarchar](50) NULL,
	[PriceOff] [varchar](15) NULL,
	[Quantity] [int] NULL,
	[ExpireDate] [datetime] NULL,
	[CatID] [float] NULL,
	[Disclaimer] [nvarchar](max) NULL,
	[ShowWeb] [bit] NULL,
	[LabelText] [nvarchar](100) NULL,
	[CompanyName] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[PostalCode] [varchar](10) NULL,
	[Country] [nvarchar](2) NULL,
	[Visits] [int] NULL,
	[PriceType] [int] NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_DiscountID_1_10] PRIMARY KEY CLUSTERED 
(
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ELearnCourses]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnCourses](
	[CourseID] [float] NOT NULL,
	[CatID] [float] NULL,
	[CourseName] [nvarchar](100) NULL,
	[Instructor] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Credits] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_CourseID_1_10] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnExamGrades]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnExamGrades](
	[GradeID] [float] NOT NULL,
	[ExamID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[Grade] [nvarchar](50) NULL,
	[UserNotes] [nvarchar](max) NULL,
	[Approved] [bit] NULL,
 CONSTRAINT [PK_GradeID_1_10] PRIMARY KEY CLUSTERED 
(
	[GradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnExamQuestions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnExamQuestions](
	[QuestionID] [float] NOT NULL,
	[ExamID] [float] NULL,
	[QuestionNo] [int] NULL,
	[QuestionHeader] [nvarchar](255) NULL,
	[QuestionFooter] [nvarchar](255) NULL,
	[QuestionType] [varchar](50) NULL,
	[RightAnswer] [nvarchar](50) NULL,
	[Answer1] [nvarchar](50) NULL,
	[Answer2] [nvarchar](50) NULL,
	[Answer3] [nvarchar](50) NULL,
	[Answer4] [nvarchar](50) NULL,
	[Answer5] [nvarchar](50) NULL,
 CONSTRAINT [PK_QuestionID_1_10] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnExams]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnExams](
	[ExamID] [float] NOT NULL,
	[CourseID] [float] NULL,
	[ExamName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_ExamID_1_10] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnExamUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnExamUsers](
	[EUserID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[ExamID] [float] NULL,
	[QuestionID] [float] NULL,
	[Answer] [nvarchar](max) NULL,
	[CorrectAnswer] [int] NULL,
	[Graded] [bit] NULL,
 CONSTRAINT [PK_EUserID_1_10] PRIMARY KEY CLUSTERED 
(
	[EUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnHomeUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnHomeUsers](
	[HUserID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[HomeID] [float] NULL,
	[Grade] [nvarchar](50) NULL,
	[HomeNotes] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[Approved] [bit] NULL,
 CONSTRAINT [PK_HUserID_1_10] PRIMARY KEY CLUSTERED 
(
	[HUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnHomework]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnHomework](
	[HomeID] [float] NOT NULL,
	[CourseID] [float] NULL,
	[HWTitle] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[DueDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_HomeID_1_10] PRIMARY KEY CLUSTERED 
(
	[HomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELearnStudents]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELearnStudents](
	[StudentID] [float] NOT NULL,
	[CourseID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[Active] [bit] NULL,
	[UserNotes] [nvarchar](max) NULL,
	[DateEnrolled] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_StudentID_1_10] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailTemplates]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplates](
	[TemplateID] [float] NOT NULL,
	[TemplateName] [nvarchar](100) NULL,
	[EmailSubject] [nvarchar](100) NULL,
	[EmailBody] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_TemplateID_EmailTemplates] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventCalendar]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventCalendar](
	[EventID] [float] NOT NULL,
	[LinkID] [float] NOT NULL,
	[TypeID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[EventDate] [datetime] NULL,
	[BegTime] [varchar](20) NULL,
	[EndTime] [varchar](20) NULL,
	[Subject] [nvarchar](100) NULL,
	[Notes] [nvarchar](max) NULL,
	[Hits] [int] NULL,
	[Shared] [bit] NULL,
	[Recurring] [int] NULL,
	[Duration] [int] NULL,
	[RecurringCycle] [varchar](1) NULL,
	[EventOnlinePrice] [decimal](18, 4) NULL,
	[EventDoorPrice] [decimal](18, 4) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_EventID_1_10] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventTypes]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTypes](
	[TypeID] [float] NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[AccessKeys] [varchar](max) NULL,
	[WriteKeys] [varchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_TypeID_1_10] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAQ](
	[FAQID] [float] NOT NULL,
	[CatID] [float] NULL,
	[Question] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[Weight] [int] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_FAQID_1_10] PRIMARY KEY CLUSTERED 
(
	[FAQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[PageTitle] [nvarchar](100) NULL,
	[PageURL] [varchar](255) NULL,
	[DatePosted] [datetime] NULL,
	[LastAccessed] [datetime] NULL,
	[ModuleID] [int] NULL,
 CONSTRAINT [PK_ID_1_30] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormAnswers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormAnswers](
	[AnswerID] [float] NOT NULL,
	[FormID] [float] NOT NULL,
	[QuestionID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[Answer] [nvarchar](max) NULL,
	[SubmitDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[SubmissionID] [float] NULL,
 CONSTRAINT [PK_AnswerID_1_10] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormOptions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormOptions](
	[OptionID] [float] NOT NULL,
	[QuestionID] [float] NULL,
	[OptionValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_OptionID_1_20] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormQuestions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormQuestions](
	[QuestionID] [float] NOT NULL,
	[SectionID] [float] NOT NULL,
	[FormID] [float] NOT NULL,
	[TypeID] [varchar](3) NULL,
	[Weight] [int] NULL,
	[Mandatory] [bit] NULL,
	[Question] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_QuestionID_1_20] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormReplyIDs]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FormReplyIDs](
	[ReplyID] [float] NOT NULL,
	[FormID] [float] NOT NULL,
	[EmailAddress] [varchar](100) NULL,
	[DateClicks] [datetime] NULL,
 CONSTRAINT [pk_ReplyID_FormReplyIDs] PRIMARY KEY CLUSTERED 
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Forms]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms](
	[FormID] [float] NOT NULL,
	[DatePosted] [datetime] NULL,
	[Available] [bit] NULL,
	[Title] [nvarchar](150) NULL,
	[Email] [nvarchar](50) NULL,
	[CompletionURL] [varchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[ReplyFormID] [float] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_Forms_1_10] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormSections]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormSections](
	[SectionID] [float] NOT NULL,
	[FormID] [float] NOT NULL,
	[Weight] [int] NULL,
	[SectionName] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_SectionID_1_10] PRIMARY KEY CLUSTERED 
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ForumsMessages]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumsMessages](
	[TopicID] [float] NOT NULL,
	[CatID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[Subject] [nvarchar](100) NULL,
	[Message] [nvarchar](max) NULL,
	[ReplyID] [float] NOT NULL,
	[Replies] [int] NULL,
	[Hits] [int] NULL,
	[EmailReply] [bit] NULL,
	[DatePosted] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_TopicID_1_10] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ForumsPolls]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ForumsPolls](
	[PollID] [float] NOT NULL,
	[Question] [varchar](200) NULL,
	[Option1] [varchar](200) NULL,
	[Option2] [varchar](200) NULL,
	[Option3] [varchar](200) NULL,
	[Option4] [varchar](200) NULL,
	[Option5] [varchar](200) NULL,
	[Option6] [varchar](200) NULL,
	[Option7] [varchar](200) NULL,
	[Option8] [varchar](200) NULL,
	[Option9] [varchar](200) NULL,
	[Option10] [varchar](200) NULL,
	[Option1Votes] [int] NULL,
	[Option2Votes] [int] NULL,
	[Option3Votes] [int] NULL,
	[Option4Votes] [int] NULL,
	[Option5Votes] [int] NULL,
	[Option6Votes] [int] NULL,
	[Option7Votes] [int] NULL,
	[Option8Votes] [int] NULL,
	[Option9Votes] [int] NULL,
	[Option10Votes] [int] NULL,
	[DatePosted] [datetime] NULL,
	[RunForDays] [int] NULL,
 CONSTRAINT [pk_PollID_ForumsPolls] PRIMARY KEY CLUSTERED 
(
	[PollID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FriendsList]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FriendsList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[AddedUserID] [varchar](40) NULL,
	[Approved] [bit] NULL,
 CONSTRAINT [pk_ID_FriendsList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Games]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameID] [float] NOT NULL,
	[GameName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageFile] [varchar](255) NULL,
	[WindowHeight] [int] NULL,
	[WindowWidth] [int] NULL,
	[PageName] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameID_1_10] PRIMARY KEY CLUSTERED 
(
	[GameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupLists]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupLists](
	[ListID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[ListName] [nvarchar](100) NULL,
	[DatePosted] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_ListID_GroupLists] PRIMARY KEY CLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupListsUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupListsUsers](
	[ListUserID] [float] NOT NULL,
	[ListID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_ListUserID_GroupListsUsers] PRIMARY KEY CLUSTERED 
(
	[ListUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Guestbook]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guestbook](
	[EntryID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[SiteURL] [varchar](255) NULL,
	[Message] [nvarchar](max) NULL,
	[ExpireDate] [datetime] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_EntryID_1_10] PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[OrderDate] [datetime] NULL,
	[DatePaid] [datetime] NULL,
	[TransactionID] [nvarchar](200) NULL,
	[PaymentMethod] [varchar](50) NULL,
	[AffiliateID] [float] NULL,
	[DiscountCode] [nvarchar](50) NULL,
	[isRecurring] [bit] NULL,
	[RecurringID] [float] NULL,
	[inCart] [bit] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_InvoiceID_1_10] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoices_Products]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices_Products](
	[InvoiceProductID] [float] NOT NULL,
	[InvoiceID] [float] NOT NULL,
	[ProductID] [float] NULL,
	[LinkID] [float] NULL,
	[ModuleID] [int] NULL,
	[ProductName] [nvarchar](100) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[RecurringPrice] [decimal](18, 4) NULL,
	[RecurringCycle] [varchar](3) NULL,
	[isRecurring] [bit] NULL,
	[Handling] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[AffiliateUnitPrice] [decimal](18, 4) NULL,
	[AffiliateRecurringPrice] [decimal](18, 4) NULL,
	[ExcludeAffiliate] [bit] NULL,
	[ShippingMethodID] [float] NULL,
	[StoreID] [float] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_InvoiceProductID_1_10] PRIMARY KEY CLUSTERED 
(
	[InvoiceProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[LibrariesFiles]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibrariesFiles](
	[FileID] [float] NOT NULL,
	[CatID] [float] NULL,
	[Field1] [nvarchar](255) NULL,
	[Field2] [nvarchar](255) NULL,
	[Field3] [nvarchar](max) NULL,
	[Field4] [nvarchar](50) NULL,
	[UserID] [varchar](40) NULL,
	[Downloads] [int] NULL,
	[eDownload] [bit] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_FileID_1_10] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LinksWebSites]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinksWebSites](
	[LinkID] [float] NOT NULL,
	[CatID] [float] NULL,
	[LinkName] [nvarchar](100) NULL,
	[LinkURL] [varchar](255) NULL,
	[Visits] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_LinkID_1_10] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TwilioGroups](
	[GroupID] [float] NOT NULL,
	[GroupName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TwilioGroups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TwilioNumbers](
	[NumberID] [float] NOT NULL,
	[PhoneNumber] [varchar](40) NOT NULL,
	[SID] [varchar](50) NULL,
	[FlowID] [float] NULL,
 CONSTRAINT [PK_TwilioNumbers] PRIMARY KEY CLUSTERED 
(
	[NumberID] ASC,
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TwilioUsers](
	[UserID] [varchar](40) NOT NULL,
	[GroupIDs] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TwilioUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TwilioFlows](
	[FlowID] [float] NOT NULL,
	[FlowName] [varchar](100) NOT NULL,
	[FlowConfig] [varchar](max) NULL,
 CONSTRAINT [PK_TwilioFlows] PRIMARY KEY CLUSTERED 
(
	[FlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TwilioDevices](
	[DeviceID] [float] NOT NULL,
	[DeviceName] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TwilioDevices] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[MatchMaker]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchMaker](
	[ProfileID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[EnableComment] [bit] NULL,
	[EnableMatch] [bit] NULL,
	[AboutMe] [nvarchar](max) NULL,
	[AboutMyMatch] [nvarchar](max) NULL,
	[Views] [int] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_ProfileID_1_10] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Members]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Members](
	[UserID] [varchar](40) NOT NULL,
	[UserName] [nvarchar](25) NULL,
	[Password] [nvarchar](100) NULL,
	[Secret_Question] [nvarchar](255) NULL,
	[Secret_Answer] [nvarchar](100) NULL,
	[PasswordResetID] [float] NULL,
	[PasswordResetDate] [datetime] NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Male] [bit] NULL,
	[BirthDate] [datetime] NULL,
	[StreetNumber] [nvarchar](20) NULL,
	[StreetAddress] [nvarchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](10) NULL,
	[Country] [nvarchar](2) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[PayPal] [nvarchar](100) NULL,
	[AccessKeys] [varchar](max) NULL,
	[ClassChanged] [datetime] NULL,
	[UserNotes] [nvarchar](max) NULL,
	[LastLogin] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[ApproveFriends] [varchar](3) NULL,
	[IPAddress] [varchar](50) NULL,
	[AffiliateID] [float] NULL,
	[ReferralID] [float] NULL,
	[WebsiteURL] [varchar](150) NULL,
	[AffiliatePaid] [datetime] NULL,
	[SkinFolder] [varchar](50) NULL,
	[CustomerID] [float] NULL,
	[Lang] [varchar](50) NULL,
	[UserPoints] [int] NULL,
	[PCRCandidateId] float,
	[PCRCompanyId] float,
	[PCRCompanyDesc] [nvarchar](max),
	[SugarID] [varchar](40) NULL,
	[SuiteID] [varchar](40) NULL,
	[SmarterTrackID] [varchar](40) NULL,
	[WHCMSID] [varchar](40) NULL,
	[HideTips] [bit] NULL,
	[AsteriskExt] [varchar](12) NULL,
	[Status] [int] NULL,
	[Facebook_Token] [varchar](255) NULL,
	[Facebook_Id] [varchar](50) NULL,
	[Facebook_User] [varchar](50) NULL,
	[LinkedInID] [varchar](50) NULL,
	[DateDeleted] [datetime] NULL,
	[AccessClass] [float] NULL,
	[SiteID] [float] NULL,
	[ForumSignature] [text] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_UserID_1_10] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MembersInvite]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MembersInvite](
	[InviteID] [float] NOT NULL,
	[EmailAddress] [varchar](100) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[ClassID] [float] NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [pk_InviteID_MembersInvite] PRIMARY KEY CLUSTERED 
(
	[InviteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Messenger]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messenger](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToUserID] [varchar](40) NULL,
	[FromUserID] [varchar](40) NULL,
	[Subject] [nvarchar](100) NULL,
	[Message] [nvarchar](max) NULL,
	[ReadNew] [bit] NULL,
	[DateSent] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
 CONSTRAINT [pk_ID_Messenger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessengerBlocked]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessengerBlocked](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[FromUserID] [varchar](40) NULL,
 CONSTRAINT [pk_ID_MessengerBlocked] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessengerSent]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessengerSent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromUserID] [varchar](40) NULL,
	[ToUserID] [varchar](40) NULL,
	[Subject] [nvarchar](100) NULL,
	[Message] [nvarchar](max) NULL,
	[DateSent] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
 CONSTRAINT [pk_ID_MessengerSent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModulesNPages]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModulesNPages](
	[ModuleID] [int] NULL,
	[PageID] [int] NULL,
	[PageTitle] [nvarchar](150) NULL,
	[LinkText] [nvarchar](100) NULL,
	[PageText] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[MenuID] [int] NULL,
	[Keywords] [nvarchar](max) NULL,
	[Weight] [int] NULL,
	[Activated] [bit] NULL,
	[UserPageName] [varchar](200) NULL,
	[AdminPageName] [varchar](20) NULL,
	[TargetWindow] [varchar](50) NULL,
	[AccessKeys] [varchar](max) NULL,
	[Visits] [int] NULL,
	[EditKeys] [varchar](max) NULL,
	[DateDeleted] [datetime] NULL,
	[Status] [int] NULL,
	[UniqueID] [float] NOT NULL,
 CONSTRAINT [PK_UniqueID_ModulesNPages] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [float] NOT NULL,
	[CatID] [float] NULL,
	[Topic] [nvarchar](255) NULL,
	[Headline] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[DisplayType] [nvarchar](150) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DatePosted] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_NewsID_1_10] PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Newsletters]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Newsletters](
	[LetterID] [float] NOT NULL,
	[NewsletName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[JoinKeys] [varchar](max) NULL,
	[PortalIDs] [varchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_LetterID_1_10] PRIMARY KEY CLUSTERED 
(
	[LetterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewslettersSent]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewslettersSent](
	[SentID] [float] NOT NULL,
	[LetterID] [float] NOT NULL,
	[EmailSubject] [nvarchar](100) NULL,
	[EmailBody] [nvarchar](max) NULL,
	[DateSent] [datetime] NULL,
	[Status] [int] NULL,
	[AccessKeys] [text] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_SentID_1_10] PRIMARY KEY CLUSTERED 
(
	[SentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewslettersUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewslettersUsers](
	[NUserID] [float] NOT NULL,
	[LetterID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[EmailAddress] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_NUserID_1_10] PRIMARY KEY CLUSTERED 
(
	[NUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OnlineUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[Location] [nvarchar](100) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[LastActive] [datetime] NULL,
	[isChatting] [bit] NULL,
	[RoomID] [float] NULL,
	[CurrentStatus] [nvarchar](20) NULL,
 CONSTRAINT [pk_ID_OnlineUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoAlbums]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoAlbums](
	[AlbumID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[AlbumName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[SharedAlbum] [bit] NULL,
	[Password] [nvarchar](20) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_AlbumID_1_10] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [dbo].[PNQVotes](
	[PollID] [Float] NOT NULL,
	[OptionID] [Float] NOT NULL,
	[PortalID] [Float] NOT NULL,
	[NumVotes] [int] NOT NULL,
 CONSTRAINT [pk_PollID_PNQVotes] PRIMARY KEY CLUSTERED 
(
	[PollID] ASC,[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PNQOptions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PNQOptions](
	[OptionID] [float] NOT NULL,
	[PollID] [float] NOT NULL,
	[PollOption] [nvarchar](100) NULL,
	[SelectedCount] [int] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_OptionID_1_10] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PNQQuestions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PNQQuestions](
	[PollID] [float] NOT NULL,
	[Question] [nvarchar](255) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[UserID] [varchar](40) NULL,
	[VoteKeys] [text] NULL,
 CONSTRAINT [PK_PollID_1_10] PRIMARY KEY CLUSTERED 
(
	[PollID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PortalPages]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalPages](
	[PageID] [int] NULL,
	[LinkText] [nvarchar](100) NULL,
	[PageText] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[MenuID] [int] NULL,
	[Keywords] [nvarchar](max) NULL,
	[Weight] [int] NULL,
	[UserPageName] [varchar](200) NULL,
	[PageTitle] [nvarchar](150) NULL,
	[Visits] [int] NULL,
	[ViewPage] [bit] NULL,
	[TargetWindow] [varchar](50) NULL,
	[PortalIDs] [varchar](max) NULL,
	[PortalID] [float] NULL,
	[Status] [int] NULL,
	[UniqueID] [float] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_UniqueID_PortalPages] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Portals]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portals](
	[CatID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[PortalTitle] [nvarchar](50) NULL,
	[DomainName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[ManageKeys] [varchar](max) NULL,
	[HideList] [bit] NULL,
	[LoginKeys] [varchar](max) NULL,
	[Status] [int] NULL,
	[PortalID] [float] NOT NULL,
	[PlanID] [float] NULL,
	[FriendlyName] [nvarchar](50) NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_PortalID_Portals] PRIMARY KEY CLUSTERED 
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PortalScripts]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalScripts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ListUnder] [int] NULL,
	[ScriptName] [nvarchar](50) NULL,
	[ScriptText] [nvarchar](max) NULL,
	[ScriptType] [varchar](50) NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_ID_PortalScripts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PricingOptions]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PricingOptions](
	[PriceID] [float] NOT NULL,
	[UniqueID] [float] NOT NULL,
	[ModuleID] [int] NULL,
	[Featured] [bit] NULL,
	[BoldTitle] [bit] NULL,
	[Highlight] [bit] NULL,
 CONSTRAINT [PK_PriceID_1_10] PRIMARY KEY CLUSTERED 
(
	[PriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profiles](
	[ProfileID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[EnableComment] [bit] NULL,
	[AboutMe] [nvarchar](max) NULL,
	[Views] [int] NULL,
	[ProfileType] [int] NULL,
	[HotOrNot] [bit] NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[BGColor] [varchar](7) NULL,
	[TextColor] [varchar](7) NULL,
	[LinkColor] [varchar](7) NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_ProfileID_2_10] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UniqueID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[ModuleID] [int] NULL,
	[Stars] [int] NULL,
	[IPAddress] [varchar](20) NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [pk_ID_Ratings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecycleBin]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecycleBin](
	[UniqueID] [varchar](40) NOT NULL,
	[ModuleID] [int] NULL,
	[ModuleName] [varchar](50) NULL,
	[Title] [text] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_UniqueID_RecycleBin] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReferralAddresses]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferralAddresses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToEmailAddress] [nvarchar](100) NULL,
	[FromEmailAddress] [nvarchar](100) NULL,
	[Visited] [bit] NULL,
 CONSTRAINT [pk_ID_ReferralAddresses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReferralStats]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferralStats](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromEmailAddress] [nvarchar](100) NULL,
	[Visitors] [int] NULL,
 CONSTRAINT [pk_ID_ReferralStats] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Weight] [int] NULL,
	[Question] [nvarchar](250) NULL,
	[ModuleIDs] [varchar](max) NULL,
	[DatePosted] [datetime] NULL,
	[ReviewID] [float] NOT NULL,
 CONSTRAINT [PK_ReviewID_Reviews] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReviewsUsers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewsUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[TotalStars] [int] NULL,
	[TotalUsers] [int] NULL,
	[ReviewID] [float] NOT NULL,
 CONSTRAINT [pk_ID_ReviewsUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RStateAgents]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RStateAgents](
	[AgentID] [float] NOT NULL,
	[BrokerID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_AgentID_1_10] PRIMARY KEY CLUSTERED 
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RStateBrokers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RStateTenants](
	[TenantID] [Float] NOT NULL,
	[TenantName] [nvarchar] (200) NULL,
	[TenantNumber] [nvarchar] (50) NULL,
	[BirthDate] [datetime] NULL,
	[DateAdded] [datetime] NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_TenantID_RStateTenants] PRIMARY KEY CLUSTERED 
(
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RStateTenantProperties](
	[TenantID] [Float] NOT NULL,
	[PropertyID] [Float] NOT NULL,
	[DateAdded] [datetime] NULL,
	[MovedIn] [datetime] NULL,
	[MovedOut] [datetime] NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_TenantID_RStateTenantProperties] PRIMARY KEY CLUSTERED 
(
	[TenantID] ASC,[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RStateReviews](
	[ReviewID] [Float] NOT NULL,
	[TenantID] [Float] NOT NULL,
	[PropertyID] [Float] NOT NULL,
	[IsTenant] [bit] NULL,
	[Rating] [int] NULL,
	[Complaints] [ntext] NULL,
	[Compliments] [ntext] NULL,
	[DatePosted] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_Review_RStateReviews] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[RStateBrokers](
	[BrokerID] [float] NOT NULL,
	[BrokerName] [nvarchar](100) NULL,
	[Approval] [bit] NULL,
	[ShowCommission] [bit] NULL,
	[UserID] [varchar](40) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_BrokerID_1_10] PRIMARY KEY CLUSTERED 
(
	[BrokerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RStateProperty]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RStateProperty](
	[PropertyID] [float] NOT NULL,
	[AgentID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[ListingID] [float] NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [nvarchar](50) NULL,
	[RecurringCycle] [varchar](3) NULL,
	[ForSale] [bit] NULL,
	[PropertyType] [int] NULL,
	[Status] [int] NULL,
	[StreetAddress] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[County] [nvarchar](100) NULL,
	[Country] [nvarchar](2) NULL,
	[YearBuilt] [varchar](4) NULL,
	[NumBedrooms] [decimal] NULL,
	[NumBathrooms] [decimal] NULL,
	[NumRooms] [int] NULL,
	[SQFeet] [nvarchar](20) NULL,
	[Type] [int] NULL,
	[Style] [int] NULL,
	[SizeMBedroom] [nvarchar](20) NULL,
	[SizeLivingRoom] [nvarchar](20) NULL,
	[SizeDiningRoom] [nvarchar](20) NULL,
	[SizeKitchen] [nvarchar](20) NULL,
	[SizeLot] [nvarchar](20) NULL,
	[Garage] [nvarchar](40) NULL,
	[Heating] [nvarchar](40) NULL,
	[FeatureInterior] [nvarchar](max) NULL,
	[FeatureExterior] [nvarchar](max) NULL,
	[MLSNumber] [nvarchar](50) NULL,
	[DatePosted] [datetime] NULL,
	[Visits] [int] NULL,
	[PortalID] [float] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_PropertyID_1_10] PRIMARY KEY CLUSTERED 
(
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Scripts]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scripts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleIDs] [varchar](max) NULL,
	[CatIDs] [varchar](max) NULL,
	[UserID] [varchar](40) NULL,
	[PortalIDs] [varchar](max) NULL,
	[ScriptType] [varchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[ScriptText] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [pk_ID_Scripts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SEOTitles]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SEOTitles](
	[PageURL] [varchar](255) NULL,
	[PageTitle] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[TagID] [float] NOT NULL,
 CONSTRAINT [PK_TagID_SEOTitles] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShopProducts]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShopProducts](
	[ProductID] [float] NOT NULL,
	[CatID] [float] NULL,
	[ProductName] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[SalePrice] [decimal](18, 4) NULL,
	[RecurringPrice] [decimal](18, 4) NULL,
	[RecurringCycle] [varchar](15) NULL,
	[AssocID] [varchar](max) NULL,
	[NewsletID] [varchar](max) NULL,
	[ItemWeight] [decimal](18, 0) NULL,
	[WeightType] [varchar](10) NULL,
	[DimH] [decimal](18, 0) NULL,
	[DimW] [decimal](18, 0) NULL,
	[DimL] [decimal](18, 0) NULL,
	[DatePosted] [datetime] NULL,
	[UseInventory] [bit] NULL,
	[Inventory] [int] NULL,
	[MinQuantity] [int] NULL,
	[MaxQuantity] [int] NULL,
	[ShipOption] [varchar](12) NULL,
	[TaxExempt] [bit] NULL,
	[Handling] [decimal](18, 4) NULL,
	[Manufacture] [nvarchar](50) NULL,
	[ModelNumber] [nvarchar](25) NULL,
	[SKU] [nvarchar](25) NULL,
	[ShortDesc] [nvarchar](max) NULL,
	[AffiliateUnitPrice] [decimal](18, 4) NULL,
	[AffiliateRecurringPrice] [decimal](18, 4) NULL,
	[ExcludeAffiliate] [bit] NULL,
	[ModuleID] [int] NULL,
	[SalesStartDate] [datetime] NULL,
	[SalesEndDate] [datetime] NULL,
	[UserID] [varchar](40) NULL,
	[StoreID] [float] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
	[UPC_Code] [nvarchar](25) NULL,
	[isRefurbished] [bit] NULL,
	[ExcludeDiscount] [bit] NULL,
	[ImportSource] [nvarchar](50) NULL,
	[ImportID] [nvarchar](50) NULL,
	[FeedID] [float] NULL,
	[APIID] [float] NULL,
 CONSTRAINT [PK_ProductID_2_10] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [dbo].[ShopStores](
	[StoreID] [Float] NOT NULL,
	[StoreName] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[CompanyName] [nvarchar](100) NULL,
	[StreetAddress] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Country] [varchar](2) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[FaxNumber] [nvarchar](30) NULL,
	[WebsiteURL] [varchar](250) NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[UserID] [varchar](40) NULL,
	[DateAdded] [datetime] NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_StoreID_ShopStores] PRIMARY KEY CLUSTERED 
(
	[StoreID] ASC,[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ShopShippingMethods](
	[MethodID] [Float] NOT NULL,
	[MethodName] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[Carrier] [nvarchar](50) NULL,
	[ShippingService] [nvarchar](100) NULL,
	[DeliveryTime] [nvarchar](100) NULL,
	[WeightLimit] [nvarchar](50) NULL,
	[StoreID] [Float] NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_MethodID_ShopShippingMethods] PRIMARY KEY CLUSTERED 
(
	[MethodID] ASC,[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteTemplates]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SiteTemplates](
	[TemplateID] [float] NOT NULL,
	[TemplateName] [varchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[FolderName] [varchar](30) NULL,
	[DatePosted] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[useTemplate] [bit] NULL,
	[AccessKeys] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[enableUserPage] [bit] NULL,
 CONSTRAINT [pk_TemplateID_SiteTemplates] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Speakers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speakers](
	[SpeakerID] [float] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[Title] [nvarchar](255) NULL,
	[Cred] [nvarchar](255) NULL,
	[Bio] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_SpeakerID_1_10] PRIMARY KEY CLUSTERED 
(
	[SpeakerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpeakSpeeches]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpeakSpeeches](
	[SpeechID] [float] NOT NULL,
	[SpeakerID] [float] NOT NULL,
	[TopicID] [float] NOT NULL,
	[Subject] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_SpeechID_1_10] PRIMARY KEY CLUSTERED 
(
	[SpeechID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpeakTopics]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpeakTopics](
	[TopicID] [float] NOT NULL,
	[TopicName] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_TopicID_2_10] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](40) NULL,
	[Symbols] [nvarchar](max) NULL,
 CONSTRAINT [pk_ID_Stocks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TargetZones]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetZones](
	[ZoneID] [float] NOT NULL,
	[ModuleID] [int] NULL,
	[ZoneName] [nvarchar](30) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_ZoneID_TargetZones] PRIMARY KEY CLUSTERED 
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaxCalculator]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxCalculator](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaxName] [nvarchar](50) NULL,
	[TaxPercent] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_ID_TaxCalculator] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UPagesGuestbook]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UPagesGuestbook](
	[EntryID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[SiteURL] [varchar](255) NULL,
	[Message] [nvarchar](max) NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [PK_EntryID_2_10] PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UPagesPages]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UPagesPages](
	[PageID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[MenuID] [int] NULL,
	[PageName] [nvarchar](50) NULL,
	[PageTitle] [nvarchar](100) NULL,
	[PageText] [nvarchar](max) NULL,
	[TemplateID] [float] NULL,
	[Weight] [int] NULL,
 CONSTRAINT [PK_PageID_1_10] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UPagesSites]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UPagesSites](
	[SiteID] [float] NOT NULL,
	[CatID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[SiteName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[TemplateID] [float] NOT NULL,
	[Visits] [int] NULL,
	[Guestbook] [bit] NULL,
	[ShowList] [bit] NULL,
	[InviteOnly] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[SiteSlogan] [nvarchar](200) NULL,
	[Status] [int] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_SiteID_1_10] PRIMARY KEY CLUSTERED 
(
	[SiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Uploads]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Uploads](
	[UploadID] [float] NOT NULL,
	[UniqueID] [float] NULL,
	[UserID] [varchar](40) NULL,
	[ModuleID] [int] NULL,
	[FileName] [nvarchar](255) NULL,
	[FileSize] [nvarchar](20) NULL,
	[ContentType] [nvarchar](100) NULL,
	[isTemp] [bit] NULL,
	[Approved] [bit] NULL,
	[DatePosted] [datetime] NULL,
	[FileData] [varbinary](max) NULL,
	[ControlID] [nvarchar](200) NULL,
	[UserRates] [int] NULL,
	[TotalRates] [int] NULL,
	[Weight] [int] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_UploadID_1_10] PRIMARY KEY CLUSTERED 
(
	[UploadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Vouchers]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[VoucherID] [float] NOT NULL,
	[CatID] [float] NOT NULL,
	[BuyTitle] [nvarchar](100) NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[LongDescription] [nvarchar](max) NULL,
	[SalePrice] [decimal](18, 2) NULL,
	[RegularPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[MaxNumPerUser] [int] NULL,
	[RedemptionStart] [datetime] NULL,
	[RedemptionEnd] [datetime] NULL,
	[PurchaseCode] [nvarchar](25) NULL,
	[BusinessName] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](15) NULL,
	[Country] [nvarchar](3) NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[ContactName] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[Disclaimer] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[UserID] [varchar](40) NULL,
	[TotalPurchases] [int] NULL,
	[BuyEndDate] [datetime] NULL,
	[FinePrint] [nvarchar](max) NULL,
	[BuyEmailID] [float] NULL,
	[ApproveEmailID] [float] NULL,
	[AdminEmailID] [float] NULL,
	[TwitterHTML] [nvarchar](max) NULL,
	[CategoryURL] [varchar](200) NULL,
	[DatePosted] [datetime] NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_Vouchers_1_10] PRIMARY KEY CLUSTERED 
(
	[VoucherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VouchersPurchased]    Script Date: 3/18/2014 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VouchersPurchased](
	[PurchaseID] [float] NOT NULL,
	[VoucherID] [float] NOT NULL,
	[UserID] [varchar](40) NULL,
	[PurchaseCode] [nvarchar](50) NULL,
	[Redeemed] [bit] NULL,
	[CartID] [float] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [PK_VouchersPurchased_1_10] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [PrivateClass]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PrivateClass] ON [dbo].[AccessClasses]
(
	[PrivateClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AccessClasses]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AccessKeys]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ActType]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ActType] ON [dbo].[Activities]
(
	[ActType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Activities]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Activities]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Activities]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Activities]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IPAddress]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX IPAddress ON [dbo].[Activities]
(
	IPAddress ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[Advertisements]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[Advertisements]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Advertisements]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Advertisements]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ZoneID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ZoneID] ON [dbo].[Advertisements]
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[AffiliatePaid]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [InvoiceID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [InvoiceID] ON [dbo].[AffiliatePaid]
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [ApproveID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ApproveID] ON [dbo].[ApprovalEmails]
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ApprovalEmails]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Approvals]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ApproveID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ApproveID] ON [dbo].[ApprovalXML]
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ApprovalXML]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ApprovalXML]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[ApprovalXML]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Articles]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [End_Date]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [End_Date] ON [dbo].[Articles]
(
	[End_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Articles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Start_Date]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Start_Date] ON [dbo].[Articles]
(
	[Start_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Articles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Articles]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Associations]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Associations]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Associations]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[AuctionAds]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[AuctionAds]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[AuctionAds]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[AuctionAds]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AuctionAds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[AuctionAds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [BORS]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [BORS] ON [dbo].[AuctionFeedback]
(
	[BORS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[AuctionFeedback]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AdID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AdID] ON [dbo].[AuctionFeedback]
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AuctionFeedback]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[AuctionFeedback]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ProcessID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ProcessID] ON [dbo].[BG_Emails]
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[BG_Processes]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ProcessID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ProcessID] ON [dbo].[BG_SMS]
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Blog]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Blog]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Blog]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[BusinessListings]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ClaimID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ClaimID] ON [dbo].[BusinessListings]
(
	[ClaimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[BusinessListings]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[BusinessListings]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[BusinessListings]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[BusinessListings]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExcPortalSecurity]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExcPortalSecurity] ON [dbo].[Categories]
(
	[ExcPortalSecurity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ListUnder]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ListUnder] ON [dbo].[Categories]
(
	[ListUnder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Sharing]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Sharing] ON [dbo].[Categories]
(
	[Sharing] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ShowList]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ShowList] ON [dbo].[Categories]
(
	[ShowList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Categories]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Categories]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesModules]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CategoriesModules]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CategoriesModules]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesPages]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CategoriesPages]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CategoriesPages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesPortals]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CategoriesPortals]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ClassifiedsAds]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ClassifiedsAds]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[ClassifiedsAds]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ClassifiedsAds]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Soldout]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Soldout] ON [dbo].[ClassifiedsAds]
(
	[Soldout] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [SoldUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SoldUserID] ON [dbo].[ClassifiedsAds]
(
	[SoldUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ClassifiedsAds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ClassifiedsAds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AdID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AdID] ON [dbo].[ClassifiedsFeedback]
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [BORS]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [BORS] ON [dbo].[ClassifiedsFeedback]
(
	[BORS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[ClassifiedsFeedback]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ClassifiedsFeedback]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[ClassifiedsFeedback]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Comments]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ReplyID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ReplyID] ON [dbo].[Comments]
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Comments]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Comments]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CommentID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CommentID] ON [dbo].[CommentsLikes]
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CommentsLikes]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[CommentsLikes]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ContentRotator]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ZoneID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ZoneID] ON [dbo].[ContentRotator]
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FieldID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FieldID] ON [dbo].[CustomFieldOptions]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomFieldOptions]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Searchable]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Searchable] ON [dbo].[CustomFields]
(
	[Searchable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [SectionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SectionID] ON [dbo].[CustomFields]
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomFields]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomFields]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FieldID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FieldID] ON [dbo].[CustomFieldUsers]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CustomFieldUsers]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CustomFieldUsers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomFieldUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[CustomFieldUsers]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[CustomFieldUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomSections]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomSections]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[DiscountSystem]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [DiscountCode]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [DiscountCode] ON [dbo].[DiscountSystem]
(
	[DiscountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[DiscountSystem]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[DiscountSystem]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Quantity]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Quantity] ON [dbo].[DiscountSystem]
(
	[Quantity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ShowWeb]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ShowWeb] ON [dbo].[DiscountSystem]
(
	[ShowWeb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[DiscountSystem]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[DiscountSystem]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ELearnCourses]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ELearnCourses]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ELearnCourses]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[ELearnCourses]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnCourses]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[ELearnExamGrades]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamGrades]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnExamGrades]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamQuestions]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnExams]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ELearnExams]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[ELearnExams]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnExams]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamUsers]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Graded]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Graded] ON [dbo].[ELearnExamUsers]
(
	[Graded] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[ELearnExamUsers]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnExamUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[ELearnHomeUsers]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [HomeID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [HomeID] ON [dbo].[ELearnHomeUsers]
(
	[HomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnHomeUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnHomework]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [DueDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [DueDate] ON [dbo].[ELearnHomework]
(
	[DueDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnHomework]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Active]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Active] ON [dbo].[ELearnStudents]
(
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnStudents]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnStudents]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnStudents]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EmailTemplates]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EmailTemplates]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Duration]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Duration] ON [dbo].[EventCalendar]
(
	[Duration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EventDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EventDate] ON [dbo].[EventCalendar]
(
	[EventDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[EventCalendar]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EventCalendar]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Recurring]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Recurring] ON [dbo].[EventCalendar]
(
	[Recurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Shared]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Shared] ON [dbo].[EventCalendar]
(
	[Shared] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EventCalendar]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [TypeID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [TypeID] ON [dbo].[EventCalendar]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[EventCalendar]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EventTypes]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EventTypes]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[FAQ]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[FAQ]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FAQ]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FAQ]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Favorites]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Favorites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormAnswers]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[FormAnswers]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormAnswers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [SubmissionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SubmissionID] ON [dbo].[FormAnswers]
(
	[SubmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FormAnswers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[FormOptions]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormQuestions]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [SectionID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SectionID] ON [dbo].[FormQuestions]
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormQuestions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FormQuestions]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormReplyIDs]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Available]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Available] ON [dbo].[Forms]
(
	[Available] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Forms]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ReplyFormID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ReplyFormID] ON [dbo].[Forms]
(
	[ReplyFormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Forms]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormSections]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormSections]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FormSections]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ForumsMessages]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EmailReply]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EmailReply] ON [dbo].[ForumsMessages]
(
	[EmailReply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ForumsMessages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ReplyID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ReplyID] ON [dbo].[ForumsMessages]
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ForumsMessages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ForumsMessages]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [AddedUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AddedUserID] ON [dbo].[FriendsList]
(
	[AddedUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[FriendsList]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FriendsList]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[GroupLists]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[GroupLists]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ListID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ListID] ON [dbo].[GroupListsUsers]
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[GroupListsUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[GroupListsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[Guestbook]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Guestbook]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Guestbook]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Guestbook]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[Invoices]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [inCart]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [inCart] ON [dbo].[Invoices]
(
	[inCart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isRecurring]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [isRecurring] ON [dbo].[Invoices]
(
	[isRecurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Invoices]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [RecurringID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [RecurringID] ON [dbo].[Invoices]
(
	[RecurringID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Invoices]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Invoices]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExcludeAffiliate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExcludeAffiliate] ON [dbo].[Invoices_Products]
(
	[ExcludeAffiliate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [InvoiceID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [InvoiceID] ON [dbo].[Invoices_Products]
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isRecurring]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [isRecurring] ON [dbo].[Invoices_Products]
(
	[isRecurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[Invoices_Products]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Invoices_Products]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Invoices_Products]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ProductID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ProductID] ON [dbo].[Invoices_Products]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Invoices_Products]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[LibrariesFiles]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[LibrariesFiles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[LibrariesFiles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[LinksWebSites]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[LinksWebSites]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[LinksWebSites]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[LinksWebSites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[MatchMaker]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[MatchMaker]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[MatchMaker]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[Members]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CustomerID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CustomerID] ON [dbo].[Members]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PasswordResetID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PasswordResetID] ON [dbo].[Members]
(
	[PasswordResetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Members]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ReferralID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ReferralID] ON [dbo].[Members]
(
	[ReferralID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Members]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [SugarID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SugarID] ON [dbo].[Members]
(
	[SugarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [HideTips] ON [dbo].[Members]
(
	[HideTips]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserName]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserName] ON [dbo].[Members]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Facebook_Id]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Facebook_Id] ON [dbo].[Members]
(
	[Facebook_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Facebook_User]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Facebook_User] ON [dbo].[Members]
(
	[Facebook_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO

/****** Object:  Index [ExpireDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[Messenger]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[Messenger]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[Messenger]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[MessengerBlocked]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[MessengerBlocked]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[MessengerSent]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[MessengerSent]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[MessengerSent]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Activated]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Activated] ON [dbo].[ModulesNPages]
(
	[Activated] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [MenuID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [MenuID] ON [dbo].[ModulesNPages]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ModulesNPages]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PageID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PageID] ON [dbo].[ModulesNPages]
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ModulesNPages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[ModulesNPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[News]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[News]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Newsletters]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LetterID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LetterID] ON [dbo].[NewslettersSent]
(
	[LetterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[NewslettersSent]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[NewslettersSent]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LetterID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [LetterID] ON [dbo].[NewslettersUsers]
(
	[LetterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[NewslettersUsers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[NewslettersUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[NewslettersUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CurrentStatus]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CurrentStatus] ON [dbo].[OnlineUsers]
(
	[CurrentStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isChatting]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [isChatting] ON [dbo].[OnlineUsers]
(
	[isChatting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [RoomID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [RoomID] ON [dbo].[OnlineUsers]
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[OnlineUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PhotoAlbums]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [SharedAlbum]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [SharedAlbum] ON [dbo].[PhotoAlbums]
(
	[SharedAlbum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PhotoAlbums]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[PhotoAlbums]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PollID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PollID] ON [dbo].[PNQOptions]
(
	[PollID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PNQOptions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[PNQQuestions]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[PNQQuestions]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PNQQuestions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [MenuID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [MenuID] ON [dbo].[PortalPages]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PageID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PageID] ON [dbo].[PortalPages]
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PortalPages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PortalPages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ViewPage]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ViewPage] ON [dbo].[PortalPages]
(
	[ViewPage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[PortalPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Portals]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [HideList]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [HideList] ON [dbo].[Portals]
(
	[HideList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Portals]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Portals]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [DomainName]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [DomainName] ON [dbo].[Portals]
(
	[DomainName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ListUnder]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ListUnder] ON [dbo].[PortalScripts]
(
	[ListUnder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PortalScripts]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ScriptType]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ScriptType] ON [dbo].[PortalScripts]
(
	[ScriptType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[PricingOptions]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[PricingOptions]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [HotOrNot]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [HotOrNot] ON [dbo].[Profiles]
(
	[HotOrNot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Profiles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ProfileType]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ProfileType] ON [dbo].[Profiles]
(
	[ProfileType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Profiles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Profiles]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Ratings]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Ratings]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Ratings]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[RecycleBin]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromEmailAddress]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromEmailAddress] ON [dbo].[ReferralAddresses]
(
	[FromEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToEmailAddress]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ToEmailAddress] ON [dbo].[ReferralAddresses]
(
	[ToEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromEmailAddress]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [FromEmailAddress] ON [dbo].[ReferralStats]
(
	[FromEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Reviews]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ReviewID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ReviewID] ON [dbo].[ReviewsUsers]
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ReviewsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BrokerID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [BrokerID] ON [dbo].[RStateAgents]
(
	[BrokerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateAgents]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateAgents]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateAgents]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Approval]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Approval] ON [dbo].[RStateBrokers]
(
	[Approval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateBrokers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateBrokers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateBrokers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AgentID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AgentID] ON [dbo].[RStateProperty]
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ForSale]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ForSale] ON [dbo].[RStateProperty]
(
	[ForSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateProperty]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PropertyType]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PropertyType] ON [dbo].[RStateProperty]
(
	[PropertyType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateProperty]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateProperty]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ScriptType]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ScriptType] ON [dbo].[Scripts]
(
	[ScriptType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Scripts]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ShopProducts]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ShopProducts]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ShopProducts]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ShopProducts]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SiteTemplates]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [useTemplate]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [useTemplate] ON [dbo].[SiteTemplates]
(
	[useTemplate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Speakers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Speakers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[SpeakTopics]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SpeakTopics]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Stocks]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[TargetZones]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[TargetZones]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [State]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [State] ON [dbo].[TaxCalculator]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[TaxCalculator]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesGuestbook]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [TemplateID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [TemplateID] ON [dbo].[UPagesPages]
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesPages]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[UPagesPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[UPagesSites]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [TemplateID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [TemplateID] ON [dbo].[UPagesSites]
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesSites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[Uploads]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isTemp]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [isTemp] ON [dbo].[Uploads]
(
	[isTemp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Uploads]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Uploads]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Uploads]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Uploads]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [AdminEmailID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [AdminEmailID] ON [dbo].[Vouchers]
(
	[AdminEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ApproveEmailID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [ApproveEmailID] ON [dbo].[Vouchers]
(
	[ApproveEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BuyEmailID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [BuyEmailID] ON [dbo].[Vouchers]
(
	[BuyEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Vouchers]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Vouchers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [RedemptionEnd]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [RedemptionEnd] ON [dbo].[Vouchers]
(
	[RedemptionEnd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [RedemptionStart]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [RedemptionStart] ON [dbo].[Vouchers]
(
	[RedemptionStart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Vouchers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Vouchers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CartID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [CartID] ON [dbo].[VouchersPurchased]
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[VouchersPurchased]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Redeemed]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [Redeemed] ON [dbo].[VouchersPurchased]
(
	[Redeemed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[VouchersPurchased]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [VoucherID]    Script Date: 3/18/2014 9:43:34 AM ******/
CREATE NONCLUSTERED INDEX [VoucherID] ON [dbo].[VouchersPurchased]
(
	[VoucherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccessClasses] ADD  DEFAULT ('1') FOR [ClassID]
GO
ALTER TABLE [dbo].[AccessKeys] ADD  DEFAULT ('1') FOR [KeyID]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ('1') FOR [CommentID]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ('0') FOR [ReplyID]
GO
ALTER TABLE [dbo].[ModulesNPages] ADD  DEFAULT ('1') FOR [UniqueID]
GO
ALTER TABLE [dbo].[PortalPages] ADD  DEFAULT ('1') FOR [UniqueID]
GO
ALTER TABLE [dbo].[Portals] ADD  DEFAULT ('0') FOR [PortalID]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT ('1') FOR [ReviewID]
GO
ALTER TABLE [dbo].[ReviewsUsers] ADD  DEFAULT ('1') FOR [ReviewID]
GO
ALTER TABLE [dbo].[SEOTitles] ADD  DEFAULT ('1') FOR [TagID]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Members] FOREIGN KEY([UserID])
REFERENCES [dbo].[Members] ([UserID])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Members]
GO
CREATE NONCLUSTERED INDEX enableUserPage ON SiteTemplates (enableUserPage);
GO
CREATE NONCLUSTERED INDEX PCRCandidateId ON Members (PCRCandidateId);
GO
CREATE NONCLUSTERED INDEX PCRCompanyId ON Members (PCRCompanyId);
GO

CREATE TABLE [dbo].[Geo](
	[CountryCode] [varchar](20) NULL,
	[PostalCode] [varchar] (180) NULL,
	[RegionName] [varchar] (100) NULL,
	[RegionCode] [nvarchar](20) NULL,
	[ProvinceName] [varchar] (100) NULL,
	[ProvinceCode] [varchar] (20) NULL,
	[CommunityName] [varchar] (100) NULL,
	[CommunityCode] [varchar] (20) NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Accuracy] [float] NOT NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CountryCode] ON [dbo].[Geo]
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [PostalCode] ON [dbo].[Geo]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [RegionName] ON [dbo].[Geo]
(
	[RegionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [RegionCode] ON [dbo].[Geo]
(
	[RegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProvinceName] ON [dbo].[Geo]
(
	[ProvinceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProvinceCode] ON [dbo].[Geo]
(
	[ProvinceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CommunityName] ON [dbo].[Geo]
(
	[CommunityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CommunityCode] ON [dbo].[Geo]
(
	[CommunityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Latitude] ON [dbo].[Geo]
(
	[Latitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Longitude] ON [dbo].[Geo]
(
	[Longitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Feeds](
	[FeedID] [Float] NOT NULL,
	[UniqueID] [Float] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[UserID] [varchar](40) NULL,
	[MoreLink] [varchar](255) NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [PK_FeedID_Feeds] PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[FeedsUsers](
	[FeedID] [Float] NOT NULL,
	[UpVote] [int] NULL,
	[DownVote] [int] NULL,
	[UserID] [varchar](40) NULL,
	[Comment] [varchar](500) NULL,
	[isFavorite] [bit] NULL,
	[isDeleted] [bit] NULL,
	[DatePosted] [datetime] NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Feeds]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Feeds]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Feeds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FeedID] ON [dbo].[FeedsUsers]
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FeedsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [isFavorite] ON [dbo].[FeedsUsers]
(
	[isFavorite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [isDeleted] ON [dbo].[FeedsUsers]
(
	[isDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Country] ON [dbo].[Advertisements]
(
	[Country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [State] ON [dbo].[Advertisements]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[Blog]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[Blog]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[News]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[News]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [SiteID] ON [dbo].[Members]
(
	[SiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ShowList] ON [dbo].[UPagesSites]
(
	[ShowList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [InviteOnly] ON [dbo].[UPagesSites]
(
	[InviteOnly] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
UPDATE UPagesSites SET ShowList='1'
GO
UPDATE UPagesSites SET InviteOnly='0'
GO
ALTER TABLE UPagesPages Add [Password] [nvarchar] (50)
GO
CREATE NONCLUSTERED INDEX [Password] ON [dbo].[UPagesPages]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE INDEX [BannedIPs] ON [Activities]([IPAddress],[ActType],[ModuleID],[Status])
GO
CREATE INDEX [LastUpdated] ON [Scripts]([ScriptType],[DatePosted])
GO
CREATE INDEX [CustomPage] ON [ModulesNPages]([UniqueID],[PageID])
GO
CREATE INDEX [BGProgress] ON [BG_Processes]([ProcessName],[Status])
GO
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Uploads]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
CREATE INDEX [PageLookup1] ON [PortalPages]([PortalID],[UniqueID],[Status])
GO
CREATE INDEX [PageLookup] ON [CategoriesPages]([ModuleID],[CatID],[PortalID])
GO
CREATE INDEX [PageLookup2] ON [PortalPages]([PageID],[PortalID],[Status])
GO
CREATE INDEX [Status] ON [UPagesSites]([Status])
GO
CREATE INDEX [LookupSite] ON [UPagesSites]([UserID],[Status])
GO
CREATE INDEX [LookupPage] ON [UPagesPages]([PageName],[UserID])
GO
CREATE INDEX [LookupCat] ON [CategoriesPortals]([CatID],[PortalID])
GO
CREATE INDEX [LookupCat] ON [CategoriesPages]([CatID],[ModuleID],[PortalID])
GO
CREATE INDEX [LookupCat] ON [CategoriesModules]([CatID],[ModuleID],[Status])
GO
CREATE INDEX [LookupProduct] ON [ShopProducts]([CatID],[Status],[ModuleID])
GO
CREATE INDEX [ImportID] ON [ShopProducts]([ImportID])
GO
CREATE TABLE [dbo].[Wholesale2b_Feeds](
	[FeedID] [float] NOT NULL,
	[FeedName] [nvarchar](100) NULL,
	[FeedURL] [nvarchar](2048) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[LastRunTime] [datetime] null,
	[AccessKeys] [varchar](4000) null,
	[AccessHide] [bit] null,
	[ExcPortalSecurity] [bit] null,
	[Sharing] [bit] null,
	[PortalIDs] [varchar](max) null,
 CONSTRAINT [PK_FeedID_Wholesale2b_Feeds] PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE INDEX [LastRunTime] ON [Wholesale2b_Feeds]([LastRunTime])
GO
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Wholesale2b_Feeds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE INDEX [CatID] ON [News]([CatID])
GO
CREATE INDEX [CatID] ON [UPagesSites]([CatID])
GO
CREATE INDEX [CatID] ON [Blog]([CatID])
GO
CREATE INDEX [FeedID] ON [Categories]([FeedID])
GO
CREATE INDEX [FeedID] ON [ShopProducts]([FeedID])
GO
CREATE INDEX [ExcludeDiscount] ON [ShopProducts]([ExcludeDiscount])
GO
CREATE INDEX [ShippingMethodID] ON [Invoices_Products]([ShippingMethodID])
GO
CREATE INDEX [StoreID] ON [Invoices_Products]([StoreID])
GO
CREATE INDEX [PortalID] ON [PNQVotes]([PortalID])
GO
CREATE INDEX [StoreID] ON [ShopShippingMethods]([StoreID])
GO
CREATE INDEX [StoreID] ON [ShopProducts]([StoreID])
GO
CREATE INDEX [UserID] ON [ShopStores]([UserID])
GO
CREATE INDEX [UserID] ON [ShopProducts]([UserID])
GO
CREATE INDEX [PortalID] ON [ShopStores]([PortalID])
GO

CREATE NONCLUSTERED INDEX [IX_Uploads] ON [dbo].[Uploads]
(
	[ModuleID],[UniqueID],[isTemp],[Approved]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Activities_BannedIP] ON [dbo].[Activities]
(
	[IPAddress],[ActType],[ModuleID],[Status]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ChangeLog](
	[ChangeID] [float] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[UniqueID] varchar(40) NOT NULL,
	[PortalID] [float] NOT NULL,
	[Subject] varchar(100) NULL,
	[ChangedData] varchar(max) NULL,
	[DateChanged] [datetime] NULL,
 CONSTRAINT [PK_ChangeID_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_ChangeLog_Lookup] ON [dbo].[ChangeLog]
(
	[ModuleID],[UniqueID]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE INDEX [MenuID] ON [UPagesPages]([MenuID])
GO

CREATE NONCLUSTERED INDEX [IX_PortalScripts] ON [dbo].[PortalScripts]
(
	[ScriptType],[PortalID]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LDAPGroups](
	[KeyID] [float] NOT NULL,
	[LDAP_GUID] varchar(40) NOT NULL,
	[Group_Name] varchar(40) NOT NULL,
	[DateAdded] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
 CONSTRAINT [PK_KeyID_LDAPGroups] PRIMARY KEY CLUSTERED 
(
	[KeyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_LDAPGroups_Lookup] ON [dbo].[LDAPGroups]
(
	[KeyID],[LDAP_GUID]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE INDEX [LDAP_GUID] ON [LDAPGroups]([LDAP_GUID])
GO

CREATE INDEX [FriendlyName] ON [Portals]([FriendlyName])
GO

CREATE INDEX [UserIDStatus] ON [Activities]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Advertisements]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Articles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [AuctionAds]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Blog]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [BusinessListings]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ClassifiedsAds]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [CustomFieldUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [DiscountSystem]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ELearnStudents]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [EventCalendar]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [FormAnswers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ForumsMessages]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [GroupLists]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [GroupListsUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Guestbook]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Invoices]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [LibrariesFiles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [LinksWebSites]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [MatchMaker]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Members]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [NewslettersUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [PhotoAlbums]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [PNQQuestions]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Portals]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Profiles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateAgents]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateBrokers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateProperty]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ShopProducts]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ShopStores]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [UPagesSites]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Vouchers]([UserID],[Status])
GO
CREATE INDEX [UserNameStatus] ON [Members]([UserName],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Articles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [AuctionAds]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Blog]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [BusinessListings]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ClassifiedsAds]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [CustomFieldUsers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [DiscountSystem]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ELearnCourses]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EmailTemplates]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EventCalendar]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EventTypes]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [FAQ]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Forms]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ForumsMessages]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Guestbook]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Invoices]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Invoices_Products]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [LibrariesFiles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [LinksWebSites]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [MatchMaker]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Members]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [News]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [NewslettersSent]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [NewslettersUsers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [PhotoAlbums]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [PortalPages]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Portals]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Profiles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateAgents]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateBrokers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateProperty]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopProducts]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopShippingMethods]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopStores]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Speakers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [SpeakSpeeches]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [SpeakTopics]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [UPagesSites]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Vouchers]([PortalID],[Status])
GO
create nonclustered index [PageSelection] on PortalPages ([MenuID],[Status]) INCLUDE ([PageID],[PortalIDs])
GO
create nonclustered index [EventSelection] on EventCalendar ([PortalID],[Status]) INCLUDE ([UserID],[EventDate],[Shared])
GO
create nonclustered index [MembersReferral] on Members ([Status]) INCLUDE ([ReferralID])
GO
CREATE INDEX [CategoriesListUnderStatus] ON [Categories] ([ListUnder],[Status])
GO
CREATE INDEX [MembersStatusCountry] ON [Members] ([Status]) INCLUDE ([Country]);
GO
CREATE INDEX [MembersStatusPortalIDCreateDate] ON [Members] ([Status], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersMaleStatusPortalID] ON [Members] ([Male], [Status], [PortalID])
GO
CREATE INDEX [MembersMaleStatusPortalID_reateDate] ON [Members] ([Male], [Status], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersMalePortalIDCreateDate] ON [Members] ([Male], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersStatusPortalIDCountry] ON [Members] ([Status], [PortalID],[Country])
GO
CREATE INDEX [MembersStatusHideTips] ON [Members] ([Status]) INCLUDE ([HideTips]);
GO
CREATE INDEX [MembersStatusCreateDate] ON [Members] ([Status],[CreateDate])
GO

/****** Object:  Table [dbo].[VideoConfig]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].VideoConfig(
	[ContactOnline] [bit] NULL,
	[SundayAvailableStart] [varchar] (10) NULL,
	[SundayAvailableEnd] [varchar] (10) NULL,
	[MondayAvailableStart] [varchar] (10) NULL,
	[MondayAvailableEnd] [varchar] (10) NULL,
	[TuesdayAvailableStart] [varchar] (10) NULL,
	[TuesdayAvailableEnd] [varchar] (10) NULL,
	[WednesdayAvailableStart] [varchar] (10) NULL,
	[WednesdayAvailableEnd] [varchar] (10) NULL,
	[ThursdayAvailableStart] [varchar] (10) NULL,
	[ThursdayAvailableEnd] [varchar] (10) NULL,
	[FridayAvailableStart] [varchar] (10) NULL,
	[FridayAvailableEnd] [varchar] (10) NULL,
	[SaturdayAvailableStart] [varchar] (10) NULL,
	[SaturdayAvailableEnd] [varchar] (10) NULL,
	[UserID] [varchar] (40) NOT NULL,
 CONSTRAINT [PK_UserID_VideoConfig] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[VideoSchedule]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].VideoSchedule(
	[MeetingID] [float] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Subject] [nvarchar] (100) NOT NULL,
	[Message] [ntext] NULL,
	[Accepted] [bit] NULL,
	[DateAccepted] [datetime] NULL,
	[Notes] [ntext] NULL,
	[FromUserID] [varchar] (40) NOT NULL,
	[UserID] [varchar] (40) NOT NULL,
 CONSTRAINT [PK_UserID_Schedule] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[APICalls]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].APICalls(
	[APIID] [float] NOT NULL,
	[Method] [varchar] (20) NOT NULL,
	[ApiURL] [varchar] (2048) NOT NULL,
	[ApiHeaders] [ntext] NULL,
	[ApiBody] [ntext] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_APIID_APICalls] PRIMARY KEY CLUSTERED 
(
	[APIID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE INDEX [IX_BusinessListings_Status_9C7BA] ON [BusinessListings] ([Status]) INCLUDE ([CatID], [LinkID])
GO
CREATE INDEX [IX_EventCalendar_PortalID_EventDate_Status_628EC] ON [EventCalendar] ([PortalID],[EventDate], [Status]) INCLUDE ([LinkID], [TypeID], [UserID], [BegTime], [EndTime], [Subject])
GO
CREATE INDEX [IX_EventCalendar_PortalID_Status_44748] ON [EventCalendar] ([PortalID],[Status]) INCLUDE ([LinkID], [TypeID], [UserID], [EventDate], [BegTime], [EndTime], [Subject])
GO
CREATE INDEX [IX_LibrariesFiles_UserID_Status_6FEA5] ON [LibrariesFiles] ([UserID], [Status]) INCLUDE ([CatID], [PortalID])
GO
CREATE INDEX [IX_Members_Status_9F3BD] ON [Members] ([Status]) INCLUDE ([UserName], [EmailAddress], [FirstName], [LastName], [City], [State], [LastLogin], [CreateDate])
GO
CREATE INDEX [IX_Members_PortalID_AA18C] ON [Members] ([PortalID]) INCLUDE ([UserName])
GO
CREATE INDEX [IX_Members_Status_A0F6B] ON [Members] ([Status]) INCLUDE ([Male])
GO
CREATE INDEX [IX_Members_Status_74E9E] ON [Members] ([Status]) INCLUDE ([ZipCode])
GO
CREATE INDEX [IX_Members_Status_A29ED] ON [Members] ([Status]) INCLUDE ([AccessClass])
GO
CREATE INDEX [IX_NewslettersUsers_LetterID_EmailAddress_A1782] ON [NewslettersUsers] ([LetterID], [EmailAddress])
GO
CREATE INDEX [IX_NewslettersUsers_EmailAddress_810C1] ON [NewslettersUsers] ([EmailAddress])
GO
CREATE INDEX [IX_ShopProducts_ModuleID_PortalID_SalePrice_76F30] ON [ShopProducts] ([ModuleID], [PortalID],[SalePrice])
GO
CREATE INDEX [IX_ShopProducts_Status_AF1DB] ON [ShopProducts] ([Status]) INCLUDE ([CatID])
GO
CREATE INDEX [IX_ShopProducts_ModuleID_SalePrice_Status_2BFEC] ON [ShopProducts] ([ModuleID],[SalePrice], [Status]) INCLUDE ([CatID], [ProductName], [UnitPrice], [RecurringPrice], [RecurringCycle], [DatePosted], [Handling], [ShortDesc], [StoreID])
GO
CREATE INDEX [IX_ShopProducts_ModuleID_Status_SalePrice_BC972] ON [ShopProducts] ([ModuleID], [Status],[SalePrice]) INCLUDE ([CatID], [ProductName], [UnitPrice], [RecurringPrice], [RecurringCycle], [DatePosted], [Handling], [ShortDesc], [StoreID])
GO

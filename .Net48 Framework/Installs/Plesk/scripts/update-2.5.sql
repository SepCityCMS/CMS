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
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [pk_Review_RStateReviews] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

DROP TABLE JobListCompanies
GO
DROP TABLE JobListings
GO
DROP TABLE JobListResumes
GO
DROP TABLE JobListTitles
GO
DELETE FROM ModulesNPages WHERE ModuleID='48'
GO
DELETE FROM PortalPages WHERE PageID='48'
GO

DROP TABLE LiveChat
GO
DROP TABLE LiveChatLogs
GO
DROP TABLE LiveChatUsers
GO
DELETE FROM ModulesNPages WHERE ModuleID='27'
GO
DELETE FROM PortalPages WHERE PageID='27'
GO
DROP TABLE GalleryPhotos
GO
DROP TABLE PostCards
GO
DROP TABLE PostCardsCat
GO
DELETE FROM ModulesNPages WHERE ModuleID='51'
GO
DELETE FROM PortalPages WHERE PageID='51'
GO
DELETE FROM ModulesNPages WHERE ModuleID='52'
GO
DELETE FROM PortalPages WHERE PageID='52'
GO
DROP TABLE Sports
GO
DROP TABLE SportsCoaches
GO
DROP TABLE SportsGames
GO
DROP TABLE SportsPlayers
GO
DROP TABLE UserReviewAnswers
GO
DROP TABLE UserReviewCustomOptions
GO
DROP TABLE UserReviewQuestions
GO
DELETE FROM ModulesNPages WHERE ModuleID='45'
GO
DELETE FROM PortalPages WHERE PageID='45'
GO
DELETE FROM ModulesNPages WHERE ModuleID='36'
GO
DELETE FROM PortalPages WHERE PageID='36'
GO
DROP TABLE Alumni
GO
DELETE FROM ModulesNPages WHERE ModuleID='49'
GO
DELETE FROM PortalPages WHERE PageID='49'
GO
drop table MembersKeywords
GO
ALTER TABLE FAQ DROP COLUMN DatePosted
GO
ALTER TABLE FAQ ADD DatePosted [datetime]
GO
UPDATE FAQ SET DatePosted=GETDATE()
GO
ALTER TABLE EventCalendar ADD EventOnlinePrice [decimal](18, 0) NULL
GO
ALTER TABLE EventCalendar ADD EventDoorPrice [decimal](18, 0) NULL
GO

ALTER TABLE RStateReviews ADD [DatePosted] [datetime] NOT NULL
GO
ALTER TABLE Members ADD [LinkedInID] [varchar] (50)
GO

UPDATE ModulesNPages SET AdminPageName='careers.aspx' where ModuleID='66'
GO

ALTER TABLE Portals ADD PlanID [float] NULL
GO
ALTER TABLE Portals ADD FriendlyName [varchar] (50) NULL
GO
CREATE INDEX [FriendlyName] ON [Portals]([FriendlyName])
GO
DROP TABLE LocZips
GO
DROP TABLE LocStates
GO

-----------------------------------------------------------------------------------------------
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

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('26767762', '22', '0', 'Twilio Panel', '', '', '0', '', '1', '', '', '', '', '1', '100')
GO

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

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('978438678966744', '67', '67', 'Support Center', '<h1>Support Center</h1>', '', '3', '', '1', 'knowledgebase.aspx', '', '', '', '1', '100')
GO



ALTER TABLE UPagesPages ADD MenuID [int] NULL
GO

UPDATE UPagesPages SET MenuID='0'
GO

CREATE INDEX [MenuID] ON [UPagesPages]([MenuID])
GO

CREATE NONCLUSTERED INDEX [IX_PortalScripts] ON [dbo].[PortalScripts]
(
	[ScriptType],[PortalID]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('934009937411209', '68', '68', 'LDAP Integration', '', '', '3', '', '1', '', '', '', '', '0', '100')
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

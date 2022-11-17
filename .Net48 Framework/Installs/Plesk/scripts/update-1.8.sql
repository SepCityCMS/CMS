ALTER TABLE [SiteTemplates] DROP Column SiteTheme
GO
DROP INDEX [SiteTemplates].UseMobileTemplate
GO
ALTER TABLE [SiteTemplates] DROP Column UseMobileTemplate
GO
delete from SiteTemplates where TemplateID='938475665938413'
GO
delete from SiteTemplates where TemplateID='938475665938433'
GO
ALTER TABLE Members ADD [HideTips] [bit]
GO

CREATE NONCLUSTERED INDEX [HideTips] ON [dbo].[Members]
(
	[HideTips]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO

UPDATE Members SET HideTips='0'
GO
ALTER TABLE SiteTemplates ADD [AccessKeys] [text]
GO
UPDATE SiteTemplates SET AccessKeys='|2|,|3|,|4|'
GO

ALTER TABLE Uploads ADD [Weight] [int]
GO

CREATE NONCLUSTERED INDEX [Weight] ON Uploads ([Weight]);
GO

UPDATE Uploads SET Weight='99'
GO

insert into PortalPages 
(PageID, LinkText, PageText, Description, MenuID, Keywords, Weight, UserPageName, TargetWindow, Visits, PageTitle, ViewPage, PortalIDs, PortalID, Status, UniqueID) 
SELECT '66', 'Job Board', '<h1>Job Board</h1>', '', '3', '', '0', 'careers.aspx', '_parent', '0', 'Job Board', '0', '', Portals.PortalID, '1', '9232373' + Portals.PortalID FROM Portals
GO
insert into PortalPages (UniqueID, PortalID,PageID,LinkText,PageText,Description,MenuID,Keywords,Weight,UserPageName,TargetWindow, Status) VALUES ('50','0', '66', 'Job Board', '<h1>Job Board</h1>', '', '3', '', '100',  'careers.aspx', '', '1')
GO

CREATE NONCLUSTERED INDEX IPAddress ON Activities (IPAddress);
GO

CREATE NONCLUSTERED INDEX DomainName ON Portals (DomainName);
GO

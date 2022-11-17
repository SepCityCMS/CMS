ALTER TABLE UPagesSites ADD [Status] int
GO

CREATE Function [CalculateDistance]
	(@Longitude1 Decimal(8,5), 
	@Latitude1   Decimal(8,5),
	@Longitude2  Decimal(8,5),
	@Latitude2   Decimal(8,5))
Returns Float
As
Begin
Declare @Temp Float

Set @Temp = sin(@Latitude1/57.2957795130823) * sin(@Latitude2/57.2957795130823) + cos(@Latitude1/57.2957795130823) * cos(@Latitude2/57.2957795130823) * cos(@Longitude2/57.2957795130823 - @Longitude1/57.2957795130823)

if @Temp > 1 
	Set @Temp = 1
Else If @Temp < -1
	Set @Temp = -1

Return (3958.75586574 * acos(@Temp)	) 

End
GO

Create Function [LatitudePlusDistance](@StartLatitude Float, @Distance Float) Returns Float
As
Begin
    Return (Select @StartLatitude + Sqrt(@Distance * @Distance / 4766.8999155991))
End
GO

Create Function [LongitudePlusDistance]
    (@StartLongitude Float,
    @StartLatitude Float,
    @Distance Float)
Returns Float
AS
Begin
    Return (Select @StartLongitude + Sqrt(@Distance * @Distance / (4784.39411916406 * Cos(2 * @StartLatitude / 114.591559026165) * Cos(2 * @StartLatitude / 114.591559026165))))
End
GO

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('932848473000837', '66', '66', 'Job Board', '<h1>Job Board</h1>', '', '3', '', '1', 'careers.aspx', '', '', '', '1', '100')
GO

DROP INDEX Members.PCRID
GO

ALTER TABLE Members DROP COLUMN [PCRID]
GO

ALTER TABLE Members ADD [PCRCandidateId] float
GO
ALTER TABLE Members ADD [PCRCompanyId] float
GO
ALTER TABLE Members ADD [PCRCompanyDesc] ntext
GO

ALTER TABLE SiteTemplates ADD [enableUserPage] [bit]
GO

CREATE NONCLUSTERED INDEX enableUserPage ON SiteTemplates (enableUserPage);
GO

UPDATE SiteTemplates SET enableUserPage='1'
GO

ALTER TABLE UPagesSites ADD [SiteSlogan] [nvarchar] (200)
GO

INSERT INTO SiteTemplates (TemplateID, TemplateName, Description, FolderName, DatePosted, DateUpdated, useTemplate, useMobileTemplate, SiteTheme, Status, DateDeleted) VALUES('384720909828111', 'Business Casual', '', 'BusinessCasual', GetDATE(), GetDATE(), '0', '0', 'js/BootStrap/bootstrap.min.css', '1', '')
GO

DROP Table UPagesTemplates
GO

CREATE NONCLUSTERED INDEX PCRCandidateId ON Members (PCRCandidateId);
GO

CREATE NONCLUSTERED INDEX PCRCompanyId ON Members (PCRCompanyId);
GO

DROP INDEX Members.VTigerID
GO
DROP INDEX Members.VTigerLead
GO
ALTER TABLE Members DROP COLUMN [VTigerID]
GO
ALTER TABLE Members DROP COLUMN [VTigerLead]
GO
ALTER TABLE Members ADD [SuiteID] [varchar] (40)
GO
ALTER TABLE Members ADD [SmarterTrackID] [varchar] (40)
GO
ALTER TABLE Members ADD [WHCMSID] [varchar] (40)
GO
insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('927832734543', '67', '67', 'Customer Support', '<h1>Customer Support</h1>', '', '3', '', '1', 'crm.aspx', '', '', '', '1', '100')
GO
ALTER TABLE Vouchers ALTER COLUMN SalePrice decimal(18,2)
GO
ALTER TABLE Vouchers ALTER COLUMN RegularPrice decimal(18,2)
GO
WITH CTE AS(
   SELECT CatID, ModuleID,
       RN = ROW_NUMBER()OVER(PARTITION BY CatID,ModuleID ORDER BY CatID,ModuleID)
   FROM CategoriesModules
)
DELETE FROM CTE WHERE RN > 1
GO

WITH CTE AS(
   SELECT CatID, PortalID,
       RN = ROW_NUMBER()OVER(PARTITION BY CatID,PortalID ORDER BY CatID,PortalID)
   FROM CategoriesPortals
)
DELETE FROM CTE WHERE RN > 1
GO

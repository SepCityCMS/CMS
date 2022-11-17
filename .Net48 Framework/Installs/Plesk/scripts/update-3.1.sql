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
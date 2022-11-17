ALTER TABLE RStateProperty ADD RecurringCycle [varchar] (3) NULL
GO
update activities set status='1'
GO

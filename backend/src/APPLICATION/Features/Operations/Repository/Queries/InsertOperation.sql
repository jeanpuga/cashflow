
INSERT INTO [Operations]
           ([DateRef]
           ,[AccountId]
           ,[Describe]
           ,[Value]
           ,[TransactionTypeId])
     VALUES
           (getdate()
           ,@AccountId
           ,@Describe
           ,@Value
           ,@Type)


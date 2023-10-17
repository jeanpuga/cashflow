
select [Id]
      ,[DateRef]
      ,[Describe]
      ,[Value]
      ,[TransactionTypeId] as Type
  from [Operations] (nolock)
  where 
      convert(date, [DateRef]) = convert(date, getdate())
  and [AccountId] = @AccountId
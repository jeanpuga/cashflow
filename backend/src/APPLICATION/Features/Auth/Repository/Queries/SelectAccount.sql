select [Id] as AccountNumber
      ,[Name] as Username
      ,[Email]
  from [Cashflow].[dbo].[Account] (nolock)
 where [Email] = @Email
   and [Password] = @Password
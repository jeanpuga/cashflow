 select ROW_NUMBER() over(order by convert(date, DateRef) ) id, 
		convert(date, DateRef) DateRef, 
		sum(
			case [TransactionTypeId]
				when 1 then  Value
				when 2 then  Value *-1
			end
		) as Value
	from [Cashflow].[dbo].[Operations] (nolock)
	where [AccountId] = @AccountId
	group by
		convert(date, DateRef), 
		AccountId
	order by 1;
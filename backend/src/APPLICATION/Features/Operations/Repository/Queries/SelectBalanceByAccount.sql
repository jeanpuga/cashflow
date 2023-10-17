	
	select 
		sum(
			case [TransactionTypeId]
				when 1 then  Value
				when 2 then  Value *-1
			end
		) as Value
	from [Operations] (nolock)
	where AccountId = @AccountId
	group by
		AccountId

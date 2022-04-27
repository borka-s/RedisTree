if #ARGV ~= 3 then
  return redis.error_reply("ERR wrong number of arguments for 'tpath' command")
end

local to = @to

return getPath(id, to)

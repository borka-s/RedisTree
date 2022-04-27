local getParents
getParents = function(id, depth)
    local currentNode = id
    local level = 1
    local parents = {}

    if depth < 1 then
        return parents
    end

    while true do
        local members = redis.call('smembers', prefix .. currentNode .. '::P')

        if #members == 0 then
            break
        end

        table.insert(parents, members[1])
        currentNode = members[1]

        if depth == level then
            break
        end

        level = level + 1
    end

    return parents
end

return getParents(id, tonumber(@depth))

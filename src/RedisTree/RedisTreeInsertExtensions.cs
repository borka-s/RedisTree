using RedisTree.Commands;
using RedisTree.Options;
using StackExchange.Redis;

namespace RedisTree;

public static class RedisTreeInsertExtensions
{
    public static async Task TreeInsert(
        this IDatabaseAsync database,
        RedisKey key,
        RedisValue parent,
        RedisValue node,
        InsertOption option = InsertOption.Index,
        int pivot = -1)
    {
        var prepared = await Utils.PrepareCommand(TreeCommand.Insert);

        await database.ScriptEvaluateAsync(prepared, new
        {
            key,
            parent,
            node,
            option = option.ToString().ToUpperInvariant(),
            pivot,
        });
    }

    public static async Task<RedisValue[]> TreePath(
        this IDatabaseAsync database,
        RedisKey key,
        RedisValue from,
        RedisValue to)
    {
        var prepared = await Utils.PrepareCommand(TreeCommand.Path);

        var result = await database.ScriptEvaluateAsync(prepared, new
        {
            key,
            parent = from,
            to,
        });

        if (result.IsNull)
        {
            return null;
        }

        return (RedisValue[])result;
    }

    public static async Task TreeMoveChildren(
        this IDatabaseAsync database,
        RedisKey key,
        RedisValue source,
        RedisValue target,
        MoveOption option = MoveOption.Append)
    {
        var prepared = await Utils.PrepareCommand(TreeCommand.MoveChildren);
        await database.ScriptEvaluateAsync(prepared, new
        {
            key,
            parent = source,
            target,
            option = option.ToString().ToUpperInvariant(),
        });
    }
}
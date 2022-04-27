using RedisTree.Commands;
using RedisTree.Options;
using StackExchange.Redis;

namespace RedisTree;

public static class RedisTreeWalkingExtensions
{
    // public static async Task<ChildResult[]> TreeChildren(this IDatabaseAsync database, RedisKey key, RedisValue node, int depth = -1)
    // {
    //     var prepared = await Utils.PrepareCommand(TreeCommand.Children);
    //
    //     var result = await database.ScriptEvaluateAsync(prepared, new
    //     {
    //         key,
    //         parent = node,
    //         option = WalkingOptions.Level.ToString().ToUpperInvariant(),
    //         depth,
    //     });
    //
    //     if (result.IsNull)
    //     {
    //         return null;
    //     }
    //
    //     var results = (RedisResult[])result;
    //
    //     foreach (var redisResult in results)
    //     {
    //     }
    // }

    private static void ConvertNode()
    {
    }

    public static async Task<RedisValue[]> TreeParents(this IDatabaseAsync database, RedisKey key, RedisValue node, int depth = -1)
    {
        var prepared = await Utils.PrepareCommand(TreeCommand.Parents, true);

        var result = await database.ScriptEvaluateAsync(prepared, new
        {
            key,
            parent = node,
            depth
        });

        return (RedisValue[])result;
    }
}

public class ChildResult
{
    public string Node { get; set; }

    public bool HasChild => Children.Any();

    public List<ChildResult> Children { get; set; }
}
namespace RedisTree.Commands;

[AttributeUsage(AttributeTargets.Field)]
internal class CommandTypeAttribute : Attribute
{
    public CommandType Type { get; }

    public CommandTypeAttribute(CommandType type)
    {
        Type = type;
    }
}
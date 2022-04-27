namespace RedisTree.Commands;

internal enum TreeCommand
{
    [CommandType(CommandType.Insert)]
    Insert,

    [CommandType(CommandType.Insert)]
    Path,

    [CommandType(CommandType.Insert)]
    MoveChildren,

    [CommandType(CommandType.Other)]
    Children,

    [CommandType(CommandType.Other)]
    Parents,
}
using System.Reflection;
using System.Text;
using RedisTree.Commands;
using StackExchange.Redis;

namespace RedisTree;

internal static class Utils
{
    private static string _head;
    private static string _deleteReference;
    private static string _getPathScript;

    public static async Task<LuaScript> PrepareCommand(TreeCommand name, bool clearCache = false)
    {
        await LoadHead();
        await LoadGetPath();
        await LoadDeleteReference();

        var commandScript = await LoadScriptFileAsync($"t{name.ToString().ToLowerInvariant()}");

        if (clearCache)
        {
            LuaScript.PurgeCache();
        }

        return LuaScript.Prepare(GetHeaderScriptsForCommand(name) + commandScript);
    }

    private static string GetHeaderScriptsForCommand(TreeCommand command)
    {
        var sb = new StringBuilder(_head);

        var type = command.GetType();
        var name = Enum.GetName(type, command);

        var commandAttribute = type.GetField(name).GetCustomAttribute<CommandTypeAttribute>();

        if (commandAttribute != null)
        {
            switch (commandAttribute.Type)
            {
                case CommandType.Insert:
                    sb.Append(_getPathScript);

                    break;
                case CommandType.Delete:
                    sb.Append(_deleteReference);

                    break;
            }
        }

        return sb.ToString();
    }

    private static async Task<string> LoadScriptFileAsync(string name)
    {
        return await File.ReadAllTextAsync($"Lua/{name}.lua", Encoding.UTF8);
    }

    private static async Task LoadHead()
    {
        if (_head != null)
        {
            return;
        }

        _head = await LoadHeaderScript("_head");
    }

    private static async Task LoadGetPath()
    {
        if (_getPathScript != null)
        {
            return;
        }

        _getPathScript = await LoadHeaderScript("_get_path");
    }

    private static async Task LoadDeleteReference()
    {
        if (_deleteReference != null)
        {
            return;
        }

        _deleteReference = await LoadHeaderScript("_delete_reference");
    }

    private static async Task<string> LoadHeaderScript(string scriptName)
    {
        var script = await LoadScriptFileAsync(scriptName);

        return string.Join("\r\n", script.Split("\r\n").Where(IsNotCommand)) + " ";
    }

    private static bool IsNotCommand(string line)
    {
        var trimmed = line.Trim();

        if (trimmed.Length < 2)
        {
            return false;
        }

        return trimmed[..2] != "--";
    }
}
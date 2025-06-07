// using TreeView = System.Windows.Forms.TreeView; // REMOVED

// Namespace and other necessary using statements (System, System.Collections.Generic, System.IO, System.Linq)
// should be preserved if they were there and are still needed by remaining methods.
// Assuming they are part of the original file structure.
using Toolkit_Launcher.Models;
namespace Toolkit_Launcher.Utilities; // Ensure this is correct

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public static class Utility
{
    // TreeView-specific methods like AddGroup, AddSubGroup, LoadAll (both overloads),
    // SelectNode, SelectNodeChild have been REMOVED.

    public static int GetMissingNumber(List<int> listNumber)
    {
        try
        {
            if (listNumber == null || !listNumber.Any()) return 0; // Handle empty or null list
            var heightValue = listNumber.Max() + 2; // Max() will throw on empty list if not checked
            var result = Enumerable.Range(0, heightValue).Except(listNumber);
            return result.ElementAt(0);
        }
        catch
        {
            return 0;
        }
    }

    public static int FindIndex(this ListInfo list, string contains)
    {
        if (list == null) return -1;
        return list.ListGroup.FindIndex(info => info.Identify.FullPath.Contains(contains));
    }

    public static int FindIndex(this GroupInfo list, string contains)
    {
        if (list == null) return -1;
        return list.ListGroup.FindIndex(info => info.Identify.FullPath.Contains(contains));
    }

    public static int GetNewIndex(this MyEnums.MoveType move, int index)
    {
        return move == MyEnums.MoveType.MoveUp ? index - 1 : index + 1;
    }

    public static MyEnums.TargetType GetTarget(this string value)
    {
        if (string.IsNullOrEmpty(value) || !File.Exists(value) && !Directory.Exists(value))
        {
            return MyEnums.TargetType.All; // Or handle as an error/unknown
        }

        FileAttributes attr = File.GetAttributes(value);
        if (attr.HasFlag(FileAttributes.Directory))
            return MyEnums.TargetType.Directory;

        return Path.GetExtension(value)?.ToLowerInvariant() switch // Added ToLowerInvariant for consistency
        {
            ".exe" => MyEnums.TargetType.Exe,
            ".dll" => MyEnums.TargetType.Dll,
            _ => MyEnums.TargetType.All
        };
    }

    public static bool IsTarget(this MyEnums.TargetType targetType, List<bool> enable)
    {
        if (enable == null || enable.Count < 4) return false; // Basic validation

        return targetType switch
        {
            MyEnums.TargetType.Exe => enable[0],
            MyEnums.TargetType.Dll => enable[1],
            MyEnums.TargetType.Directory => enable[2],
            MyEnums.TargetType.All => enable[3], // Assuming index 3 is for 'All'
            _ => false
        };
    }
}

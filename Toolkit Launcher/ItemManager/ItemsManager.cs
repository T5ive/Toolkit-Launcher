using TreeView = System.Windows.Forms.TreeView;

namespace Toolkit_Launcher.Helper;

public static class ItemsManager
{
    #region Add Group

    public static void AddGroup(MyEnums.GroupType type, TreeView treeView, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var selectedNode = treeView.SelectedNode;
        var fullPath = selectedNode == null ? "" : treeView.SelectedNode.FullPath;
        GroupInfo mainGroup;
        var newGroup = new GroupInfo();
        if (string.IsNullOrWhiteSpace(fullPath))
        {
            mainGroup = GroupInfo(list, type);
            list.ListGroup.Add(mainGroup);
        }
        else
        {
            mainGroup = GetGroupInfo(fullPath, list);
            newGroup = GroupInfo(mainGroup, type, fullPath);
            mainGroup.ListGroup.Add(newGroup);
        }

        if (selectedNode == null)
        {
            treeView.Nodes.Add(Utility.AddGroup(mainGroup, mainGroup.Value.ImageID));
        }
        else
        {
            selectedNode.Nodes.Add(Utility.AddSubGroup(newGroup));
            selectedNode.Expand();
        }
    }

    #endregion Add Group

    #region Create New Class

    private static GroupInfo GroupInfo(ListInfo list, MyEnums.GroupType type)
    {
        var index = GetNewIndex(list);
        var name = $"{type}: {index}";
        return new GroupInfo
        {
            Identify = Identify(index, name, name),
            Value = Value(type),
            Type = type,
            Advance = Advance()
        };
    }

    private static GroupInfo GroupInfo(GroupInfo list, MyEnums.GroupType type, string fullPath)
    {
        var index = GetNewIndex(list);
        var name = $"{type}: {index}";
        fullPath = $"{fullPath}\\{name}";
        return new GroupInfo
        {
            Identify = Identify(index, name, fullPath),
            Value = Value(type),
            Type = type,
            Advance = Advance()
        };
    }

    private static Identify Identify(int index, string name, string fullPath)
    {
        return new Identify
        {
            Index = index,
            Name = name,
            FullPath = fullPath
        };
    }

    private static ValueInfo Value(MyEnums.GroupType type)
    {
        return new ValueInfo
        {
            ImageID = type switch
            {
                MyEnums.GroupType.Header => 1,
                MyEnums.GroupType.Item => 2,
                MyEnums.GroupType.Separator => 3,
                _ => 0
            }
        };
    }

    private static AdvanceInfo Advance()
    {
        return new AdvanceInfo
        {
            TargetTypes = new List<bool>
            {
                true,
                true,
                false,
                false
            }
        };
    }

    #endregion Create New Class

    #region Get New Index

    private static int GetNewIndex(ListInfo list)
    {
        var listIndex = new List<int>();
        listIndex.AddRange(list.ListGroup.Select(group => group.Identify.Index));

        return Utility.GetMissingNumber(listIndex);
    }

    private static int GetNewIndex(GroupInfo list)
    {
        var listIndex = new List<int>();
        listIndex.AddRange(list.ListGroup.Select(group => group.Identify.Index));
        return Utility.GetMissingNumber(listIndex);
    }

    #endregion Get New Index

    #region Get Class Info

    private static GroupInfo GetParentGroupInfo(string fullPath, ListInfo list)
    {
        var result = new GroupInfo();
        var index = fullPath.LastIndexOf("\\", StringComparison.Ordinal);
        if (index >= 0)
            fullPath = fullPath[..index];

        foreach (var group in list.ListGroup)
        {
            if (fullPath == group.Identify.FullPath)
            {
                return group;
            }
            result = GetGroupInfoChild(fullPath, group);
            if (result.Identify != null!)
            {
                return result;
            }
        }
        return result;
    }

    private static GroupInfo GetGroupInfo(string fullPath, ListInfo list)
    {
        var result = new GroupInfo();
        foreach (var group in list.ListGroup)
        {
            if (fullPath == group.Identify.FullPath)
            {
                return group;
            }
            result = GetGroupInfoChild(fullPath, group);
            if (result.Identify != null!)
            {
                return result;
            }
        }
        return result;
    }

    private static GroupInfo GetGroupInfoChild(string fullPath, GroupInfo list)
    {
        foreach (var group in list.ListGroup)
        {
            if (fullPath == group.Identify.FullPath)
            {
                return group;
            }

            var result = GetGroupInfoChild(fullPath, group);
            if (result.Identify != null!)
            {
                return result;
            }
        }
        return new GroupInfo();
    }

    #endregion Get Class Info

    #region Get Type

    public static MyEnums.GroupType GetType(string fullPath, ListInfo list = null!)
    {
        list ??= PublicClass.TempListInfo;
        foreach (var group in list.ListGroup)
        {
            if (group.Identify.FullPath == fullPath)
                return group.Type;

            if (group.Type == MyEnums.GroupType.Header)
            {
                var result = GetTypeChild(fullPath, group);
                if (GetTypeChild(fullPath, group) != MyEnums.GroupType.Null)
                {
                    return result;
                }
            }
        }

        return MyEnums.GroupType.Null;
    }

    private static MyEnums.GroupType GetTypeChild(string fullPath, GroupInfo list)
    {
        foreach (var group in list.ListGroup)
        {
            if (group.Identify.FullPath == fullPath)
                return group.Type;

            if (group.Type == MyEnums.GroupType.Header)
            {
                var result = GetTypeChild(fullPath, group);
                if (GetTypeChild(fullPath, group) != MyEnums.GroupType.Null)
                {
                    return result;
                }
            }
        }
        return MyEnums.GroupType.Null;
    }

    #endregion Get Type

    #region Get Detail

    public static GroupInfo GetGroupDetail(TreeView treeView, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        return GetGroupInfo(fullPath, list);
    }

    #endregion Get Detail

    #region Remove

    public static void Remove(TreeView treeView, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        if (MyMessage.OkCancel($"Press \"OK\" to confirm the deletion \"{treeView.SelectedNode.Text}\"\n" +
                                  "Press \"Cancel\" to confirm cancellation"))
        {
            var group = GetGroupInfo(fullPath, list);
            Remove(group, list);
            treeView.SelectedNode.Remove();
        }
    }

    private static void Remove(GroupInfo groupInfo, ListInfo list)
    {
        foreach (var group in list.ListGroup)
        {
            if (groupInfo.Identify.FullPath == group.Identify.FullPath)
            {
                list.ListGroup.Remove(groupInfo);
                break;
            }
            if (group.Type == MyEnums.GroupType.Header)
            {
                var result = RemoveChild(groupInfo, group);
                if (result)
                {
                    break;
                }
            }
        }
    }

    private static bool RemoveChild(GroupInfo groupInfo, GroupInfo list)
    {
        foreach (var group in list.ListGroup)
        {
            if (groupInfo.Identify.FullPath == group.Identify.FullPath)
            {
                list.ListGroup.Remove(groupInfo);
                return true;
            }
            if (group.Type == MyEnums.GroupType.Header)
            {
                var result = RemoveChild(groupInfo, group);
                if (result)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion Remove

    #region Move Up/Down

    public static void MoveUpDown(MyEnums.MoveType move, TreeView treeView, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        MoveUpDown(move, group, list);
        Utility.LoadAll(treeView, list);
        treeView.SelectedNode.Expand();
    }

    private static void MoveUpDown(MyEnums.MoveType move, GroupInfo groupInfo, ListInfo list)
    {
        for (var i = 0; i < list.ListGroup.Count; i++)
        {
            var group = list.ListGroup[i];
            if (groupInfo.Identify.FullPath == group.Identify.FullPath)
            {
                var oldIndex = list.FindIndex(group.Identify.FullPath);
                var newIndex = move.GetNewIndex(oldIndex);

                list.ListGroup.RemoveAt(oldIndex);
                if (move == MyEnums.MoveType.MoveDown)
                {
                    list.ListGroup[oldIndex].Identify.Index = oldIndex;
                }
                else
                {
                    list.ListGroup[newIndex].Identify.Index = oldIndex;
                }
                group.Identify.Index = newIndex;

                list.ListGroup.Insert(newIndex, group);
                break;
            }

            if (group.Type == MyEnums.GroupType.Header)
            {
                var result = MoveUpDownChild(move, groupInfo, group);
                if (result)
                {
                    break;
                }
            }
        }
    }

    private static bool MoveUpDownChild(MyEnums.MoveType move, GroupInfo groupInfo, GroupInfo list)
    {
        for (var i = 0; i < list.ListGroup.Count; i++)
        {
            var group = list.ListGroup[i];
            if (groupInfo.Identify.FullPath == group.Identify.FullPath)
            {
                var oldIndex = list.FindIndex(group.Identify.FullPath);
                var newIndex = move.GetNewIndex(oldIndex);

                list.ListGroup.RemoveAt(oldIndex);
                if (move == MyEnums.MoveType.MoveDown)
                {
                    list.ListGroup[oldIndex].Identify.Index = oldIndex;
                }
                else
                {
                    list.ListGroup[newIndex].Identify.Index = oldIndex;
                }
                group.Identify.Index = newIndex;
                list.ListGroup.Insert(newIndex, group);
                break;
            }

            var result = MoveUpDownChild(move, groupInfo, group);
            if (result)
            {
                return true;
            }
        }
        return false;
    }

    #endregion Move Up/Down

    #region Utility

    private static GroupInfo _lastGroupInfo = null!;
    public static void UpdateIdentify(TreeView treeView, string newName, string path, string iconPath, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        group.Value.FilePath = path;
        group.Value.IconPath = iconPath;

        Rename(treeView, group, fullPath, newName, list, false);
    }

    public static bool Rename(TreeView treeView, string newName, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        return Rename(treeView, group, fullPath, newName, list);
    }

    private static bool Rename(TreeView treeView, GroupInfo group, string fullPath, string newName, ListInfo list, bool duplicate = true)
    {
        var isDuplicate = fullPath.Contains('\\') ?
            newName.IsDuplicate(GetParentGroupInfo(fullPath, list)) :
            newName.IsDuplicate(list);
        if (isDuplicate)
        {
            if (duplicate)
            {
                MyMessage.ShowError("Found Duplicate " + newName + @", Please Check it again before rename!!!");
            }
            return false;
        }

        treeView.SelectedNode.Text = newName;
        group.Identify.Name = newName;
        group.Identify.FullPath = treeView.SelectedNode.FullPath;
        return true;
    }

    public static void ChangePath(TreeView treeView, string path, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        group.Value.FilePath = path;
    }

    public static void ChangeIconPath(TreeView treeView, string iconPath, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        group.Value.IconPath = iconPath;
    }

    public static void ChangeBothPath(TreeView treeView, string path, string iconPath, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        var group = GetGroupInfo(fullPath, list);
        group.Value.FilePath = path;
        group.Value.IconPath = iconPath;
    }

    public static void UpdateInfo(TreeView treeView, string command, List<bool> targetType, bool admin, bool cmd,
        MyEnums.CMDType type, ListInfo? list = null)
    {
        list ??= PublicClass.TempListInfo;
        var fullPath = treeView.SelectedNode.FullPath;
        GroupInfo group;
        if (_lastGroupInfo != null! && _lastGroupInfo.Identify.FullPath == fullPath)
        {
            group = _lastGroupInfo;
        }
        else
        {
            group = GetGroupInfo(fullPath, list);
        }

        group.Advance.Commands = command;
        group.Advance.TargetTypes = targetType;
        group.Advance.Admin = admin;
        group.Advance.CMD = cmd;
        group.Advance.CmdType = type;
        _lastGroupInfo = group;
    }

    #endregion Utility
}
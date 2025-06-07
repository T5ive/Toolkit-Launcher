using Toolkit_Launcher.Models;
namespace Toolkit_Launcher.ItemManager; // Changed namespace

using System;
using System.Collections.Generic;
using System.Linq;
using Toolkit_Launcher; // For MyEnums
using Toolkit_Launcher.Models; // For ListInfo, GroupInfo etc. (already added by script)
using Toolkit_Launcher.Utilities; // For Utility.GetMissingNumber
// TreeView using removed as it's no longer directly manipulated.

public class ItemsManager // Changed to instance class
{
    private ListInfo _listInfo;
    private GroupInfo _lastGroupInfo; // Instance field

    public event EventHandler ItemsChanged; // General event for reloading the tree
    // More specific events can be added later if needed (e.g., ItemAdded, ItemRenamed with specific args)

    public ItemsManager(ListInfo listInfo)
    {
        _listInfo = listInfo ?? throw new ArgumentNullException(nameof(listInfo));
    }

    protected virtual void OnItemsChanged()
    {
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    #region Add Group
    public GroupInfo AddGroup(MyEnums.GroupType type, string parentFullPath = null)
    {
        GroupInfo newGroup;
        if (string.IsNullOrWhiteSpace(parentFullPath))
        {
            newGroup = CreateNewGroupInfo(_listInfo, type);
            _listInfo.ListGroup.Add(newGroup);
        }
        else
        {
            var parentGroup = GetGroupInfoByPath(parentFullPath, _listInfo);
            if (parentGroup == null) throw new ArgumentException("Parent group not found", nameof(parentFullPath));
            newGroup = CreateNewGroupInfo(parentGroup, type, parentFullPath);
            parentGroup.ListGroup.Add(newGroup);
        }
        OnItemsChanged();
        return newGroup; // Return the new group so UI can select/focus it
    }
    #endregion

    #region Create New Class (internal logic, largely unchanged but uses instance _listInfo if needed)
    private GroupInfo CreateNewGroupInfo(ListInfo list, MyEnums.GroupType type)
    {
        var index = GetNewIndex(list.ListGroup);
        var name = $"{type}: {index}";
        return new GroupInfo
        {
            Identify = CreateIdentify(index, name, name),
            Value = CreateValueInfo(type),
            Type = type,
            Advance = CreateAdvanceInfo()
        };
    }

    private GroupInfo CreateNewGroupInfo(GroupInfo parentGroup, MyEnums.GroupType type, string parentFullPath)
    {
        var index = GetNewIndex(parentGroup.ListGroup);
        var name = $"{type}: {index}";
        var newFullPath = $"{parentFullPath}\{name}";
        return new GroupInfo
        {
            Identify = CreateIdentify(index, name, newFullPath),
            Value = CreateValueInfo(type),
            Type = type,
            Advance = CreateAdvanceInfo()
        };
    }

    private Identify CreateIdentify(int index, string name, string fullPath)
    {
        return new Identify { Index = index, Name = name, FullPath = fullPath };
    }

    private ValueInfo CreateValueInfo(MyEnums.GroupType type)
    {
        return new ValueInfo
        {
            ImageID = type switch // This ImageID might need to be re-evaluated or managed by ImageCacheService if it's for UI.
            {
                MyEnums.GroupType.Header => 1,
                MyEnums.GroupType.Item => 2,
                MyEnums.GroupType.Separator => 3,
                _ => 0
            }
        };
    }

    private AdvanceInfo CreateAdvanceInfo()
    {
        return new AdvanceInfo { TargetTypes = new List<bool> { true, true, false, false } };
    }
    #endregion

    #region Get New Index
    private int GetNewIndex(List<GroupInfo> groupList)
    {
        if (!groupList.Any()) return 0;
        var listIndex = groupList.Select(group => group.Identify.Index).ToList();
        return Utility.GetMissingNumber(listIndex); // Assuming Utility.GetMissingNumber is static and accessible
    }
    #endregion

    #region Get Class Info
    public GroupInfo GetParentGroupInfoByPath(string fullPath) => GetParentGroupInfoRecursive(fullPath, _listInfo);
    private GroupInfo GetParentGroupInfoRecursive(string fullPath, ListInfo list)
    {
        var parentPath = "";
        var lastSeparatorIndex = fullPath.LastIndexOf("\\", StringComparison.Ordinal); // Corrected backslash
        if (lastSeparatorIndex >= 0)
            parentPath = fullPath.Substring(0, lastSeparatorIndex);
        else
            return null; // Top-level item, no parent in the context of GroupInfo

        return GetGroupInfoByPath(parentPath, list);
    }

    public GroupInfo GetGroupInfoByPath(string fullPath) => GetGroupInfoByPath(fullPath, _listInfo);
    private GroupInfo GetGroupInfoByPath(string fullPath, ListInfo list) /* Was GetGroupInfo */
    {
        foreach (var group in list.ListGroup)
        {
            if (fullPath == group.Identify.FullPath) return group;
            var foundInChild = GetGroupInfoByPathRecursive(fullPath, group);
            if (foundInChild != null) return foundInChild;
        }
        return null;
    }

    private GroupInfo GetGroupInfoByPathRecursive(string fullPath, GroupInfo parentGroup) /* Was GetGroupInfoChild */
    {
        foreach (var group in parentGroup.ListGroup)
        {
            if (fullPath == group.Identify.FullPath) return group;
            var foundInChild = GetGroupInfoByPathRecursive(fullPath, group);
            if (foundInChild != null) return foundInChild;
        }
        return null;
    }
    #endregion

    #region Get Type
    public MyEnums.GroupType GetGroupType(string fullPath)
    {
        var group = GetGroupInfoByPath(fullPath, _listInfo);
        return group?.Type ?? MyEnums.GroupType.Null;
    }
    #endregion

    #region Remove
    public bool RemoveGroup(string fullPath)
    {
        var groupToRemove = GetGroupInfoByPath(fullPath, _listInfo);
        if (groupToRemove == null) return false;

        var parentPath = "";
        var lastSeparatorIndex = fullPath.LastIndexOf("\\", StringComparison.Ordinal); // Corrected backslash
        if (lastSeparatorIndex >= 0)
        {
            parentPath = fullPath.Substring(0, lastSeparatorIndex);
            var parentGroup = GetGroupInfoByPath(parentPath, _listInfo);
            if (parentGroup != null)
            {
                bool removed = parentGroup.ListGroup.Remove(groupToRemove);
                if (removed) OnItemsChanged();
                return removed;
            }
        }
        else // Top-level item
        {
            bool removed = _listInfo.ListGroup.Remove(groupToRemove);
            if (removed) OnItemsChanged();
            return removed;
        }
        return false;
    }
    #endregion

    #region Move Up/Down
    public bool MoveGroup(string fullPath, MyEnums.MoveType moveType)
    {
        List<GroupInfo> parentList;
        var groupToMove = GetGroupInfoByPath(fullPath, _listInfo);
        if (groupToMove == null) return false;

        var parentPath = "";
        var lastSeparatorIndex = fullPath.LastIndexOf("\\", StringComparison.Ordinal); // Corrected backslash
        if (lastSeparatorIndex >= 0)
        {
            parentPath = fullPath.Substring(0, lastSeparatorIndex);
            var parentGroup = GetGroupInfoByPath(parentPath, _listInfo);
            if (parentGroup == null) return false;
            parentList = parentGroup.ListGroup;
        }
        else
        {
            parentList = _listInfo.ListGroup;
        }

        var currentIndex = parentList.IndexOf(groupToMove);
        if (currentIndex < 0) return false;

        var newIndex = moveType == MyEnums.MoveType.MoveUp ? currentIndex - 1 : currentIndex + 1;

        if (newIndex < 0 || newIndex >= parentList.Count) return false;

        // Simple swap for now; actual index property update might be more complex if they are not just for ordering
        var temp = parentList[newIndex];
        parentList[newIndex] = groupToMove;
        parentList[currentIndex] = temp;

        // Update Identify.Index if it's meant to reflect order
        for(int i=0; i<parentList.Count; i++) parentList[i].Identify.Index = i;

        OnItemsChanged();
        return true;
    }
    #endregion

    #region Update/Rename/Change Path
    public bool RenameGroup(string fullPath, string newName)
    {
        var group = GetGroupInfoByPath(fullPath, _listInfo);
        if (group == null) return false;

        // Check for duplicates
        var parentGroup = GetParentGroupInfoByPath(fullPath);
        bool isDuplicate;
        if (parentGroup != null)
        {
            isDuplicate = parentGroup.ListGroup.Any(g => g != group && g.Identify.Name == newName);
        }
        else
        {
            isDuplicate = _listInfo.ListGroup.Any(g => g != group && g.Identify.Name == newName);
        }

        if (isDuplicate)
        {
            // Consider throwing an exception or returning a specific result for duplication
            return false;
        }

        var oldName = group.Identify.Name;
        group.Identify.Name = newName;

        // Update FullPath for the renamed group and all its children
        var oldFullPathPrefix = group.Identify.FullPath;
        var newFullPath = "";
        if (parentGroup != null) {
            newFullPath = $"{parentGroup.Identify.FullPath}\{newName}";
        } else {
            newFullPath = newName;
        }
        group.Identify.FullPath = newFullPath;
        UpdateChildFullPaths(group, oldFullPathPrefix, newFullPath);

        OnItemsChanged();
        return true;
    }

    private void UpdateChildFullPaths(GroupInfo parentGroup, string oldParentFullPath, string newParentFullPath)
    {
        foreach (var child in parentGroup.ListGroup)
        {
            var oldChildFullPath = child.Identify.FullPath;
            // The child's path started with oldParentFullPath, now it needs to start with newParentFullPath
            // The relative part of the child's path is oldChildFullPath.Substring(oldParentFullPath.Length)
            var relativePath = oldChildFullPath.Substring(oldParentFullPath.Length);
            child.Identify.FullPath = $"{newParentFullPath}{relativePath}"; // This assumes direct concatenation, might need separator logic

            // If the child's name itself defined part of the old path, this needs careful handling.
            // For now, assuming FullPath is constructed like "ParentPath\ChildName"
            // A simpler way if only the name changed:
            // child.Identify.FullPath = $"{newParentFullPath}\{child.Identify.Name}";


            UpdateChildFullPaths(child, oldChildFullPath, child.Identify.FullPath);
        }
    }


    public bool UpdateGroupValuePaths(string fullPath, string newFilePath, string newIconPath)
    {
        var group = GetGroupInfoByPath(fullPath, _listInfo);
        if (group == null || group.Type == MyEnums.GroupType.Separator) return false;

        bool changed = false;
        if (group.Value.FilePath != newFilePath)
        {
            group.Value.FilePath = newFilePath;
            changed = true;
        }
        if (group.Value.IconPath != newIconPath)
        {
            group.Value.IconPath = newIconPath;
            changed = true;
        }
        if (changed) OnItemsChanged(); // Or a more specific event like ItemDataChanged
        return true;
    }

    public bool UpdateGroupAdvanceInfo(string fullPath, string command, List<bool> targetTypes, bool isAdmin, bool isCmd, MyEnums.CMDType cmdType)
    {
        var group = GetGroupInfoByPath(fullPath, _listInfo);
        if (group == null) return false;

        // Cache _lastGroupInfo for potential optimization if UpdateInfo is called repeatedly for the same group
        // Though with instance methods, this might be less necessary or handled differently by the caller.
        _lastGroupInfo = group;

        group.Advance.Commands = command;
        group.Advance.TargetTypes = targetTypes;
        group.Advance.Admin = isAdmin;
        group.Advance.CMD = isCmd;
        group.Advance.CmdType = cmdType;

        OnItemsChanged(); // Or a more specific event
        return true;
    }

    #endregion
}

using TreeView = System.Windows.Forms.TreeView;

namespace Toolkit_Launcher;

public static class Utility
{
    public static TreeNode AddGroup(GroupInfo groupInfo, int image)
    {
        var node = new TreeNode(groupInfo.Identify.Name, image, image);

        foreach (var group in groupInfo.ListGroup)
        {
            if (group.Type == MyEnums.GroupType.Header)
            {
                node.Nodes.Add(AddGroup(group, image));
            }
            else
            {
                node.Nodes.Add(group.Type.ToString(), group.Identify.Name, image);
            }
        }

        return node;
    }

    public static TreeNode AddSubGroup(GroupInfo groupInfo)
    {
        var node = new TreeNode(groupInfo.Identify.Name, groupInfo.Value.ImageID, groupInfo.Value.ImageID);
        return node;
    }

    public static void LoadAll(TreeView treeView, ListInfo listInfo)
    {
        var tempNode = "";
        var isSelected = treeView.SelectedNode == null;
        if (!isSelected)
        {
            tempNode = treeView.SelectedNode.FullPath;
        }
        treeView.Nodes.Clear();

        foreach (var group in listInfo.ListGroup)
        {
            if (group.Type == MyEnums.GroupType.Header)
            {
                treeView.Nodes.Add(LoadAll(group));
            }
            else
            {
                treeView.Nodes.Add(group.Type.ToString(), group.Identify.Name, group.Value.ImageID);
            }
        }

        if (!isSelected)
        {
            SelectNode(treeView, tempNode);
        }
    }

    public static TreeNode LoadAll(GroupInfo groupInfo)
    {
        var node = new TreeNode(groupInfo.Identify.Name, groupInfo.Value.ImageID, groupInfo.Value.ImageID);

        foreach (var group in groupInfo.ListGroup)
        {
            if (group.Type == MyEnums.GroupType.Header)
            {
                node.Nodes.Add(LoadAll(group));
            }
            else
            {
                node.Nodes.Add(group.Type.ToString(), group.Identify.Name, group.Value.ImageID);
            }
        }

        return node;
    }

    private static void SelectNode(TreeView treeView, string oldNode)
    {
        foreach (var obj in treeView.Nodes)
        {
            var node = (TreeNode)obj;
            if (node.FullPath == oldNode)
            {
                treeView.SelectedNode = node;
                break;
            }

            var result = SelectNodeChild(node, treeView, oldNode);
            if (result)
            {
                break;
            }
        }
    }

    private static bool SelectNodeChild(TreeNode treeNode, TreeView treeView, string oldNode)
    {
        foreach (var obj in treeNode.Nodes)
        {
            var node = (TreeNode)obj;
            if (node.FullPath == oldNode)
            {
                treeView.SelectedNode = node;
                return true;
            }
            var result = SelectNodeChild(node, treeView, oldNode);
            if (result)
            {
                return true;
            }
        }

        return false;
    }

    public static int GetMissingNumber(List<int> listNumber)
    {
        try
        {
            var heightValue = listNumber.Max(t => t) + 2;
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
        return list.ListGroup.FindIndex(info => info.Identify.FullPath.Contains(contains));
    }

    public static int FindIndex(this GroupInfo list, string contains)
    {
        return list.ListGroup.FindIndex(info => info.Identify.FullPath.Contains(contains));
    }

    public static int GetNewIndex(this MyEnums.MoveType move, int index)
    {
        return move == MyEnums.MoveType.MoveUp ? index - 1 : index + 1;
    }

    public static Icon IconFromFilePath(string filePath)
    {
        var result = (Icon)null!;
        try
        {
            if (!File.Exists(filePath))
            {
                return result;
            }

            result = Icon.ExtractAssociatedIcon(filePath);
        }
        catch (Exception)
        {
            // swallow and return nothing. You could supply a default Icon here as well
        }

        return result!;
    }

    public static Image GetImage(this string? filePath)
    {
        if (filePath == null) return null!;
        if (!File.Exists(filePath)) return null!;
        if (filePath.EndWith(".exe") || filePath.EndWith(".dll"))
        {
            return IconFromFilePath(filePath).ToBitmap();
        }
        return new Bitmap(filePath);
    }

    public static Image GetImage(this MyEnums.GroupType type)
    {
        var image = type switch
        {
            MyEnums.GroupType.Header => 1,
            MyEnums.GroupType.Item => 2,
            MyEnums.GroupType.Separator => 3,
            _ => 0
        };
        return PublicClass.ImageList.Images[image];
    }

    public static MyEnums.TargetType GetTarget(this string value)
    {
        if (value.EndWith(".exe"))
        {
            return MyEnums.TargetType.Exe;
        }

        if (value.EndWith(".dll"))
        {
            return MyEnums.TargetType.Dll;
        }

        if (value.Contains('.'))
        {
            return MyEnums.TargetType.All;
        }

        return MyEnums.TargetType.Directory;
    }

    public static bool IsTarget(this MyEnums.TargetType targetType, List<bool> enable)
    {
        return targetType switch
        {
            MyEnums.TargetType.Exe => enable[0],
            MyEnums.TargetType.Dll => enable[1],
            MyEnums.TargetType.Directory => enable[2],
            MyEnums.TargetType.All => enable[3],
            _ => false
        };
    }
}
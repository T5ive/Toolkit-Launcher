using Toolkit_Launcher.Models;
namespace Toolkit_Launcher.UI.Helpers;

using System.Windows.Forms;
using Toolkit_Launcher.Services; // For ImageCacheService
using Toolkit_Launcher.ItemManager; // For ItemsManager namespace
using Toolkit_Launcher; // Added for ListInfo, GroupInfo, MyEnums

public static class TreeViewHelper
{
    public static void LoadAll(TreeView treeView, ListInfo listInfo, ImageCacheService imageCacheService)
    {
        string selectedNodePath = treeView.SelectedNode?.Tag as string; // Use Tag for path

        treeView.BeginUpdate();
        treeView.Nodes.Clear();
        treeView.ImageList?.Images.Clear(); // Clear existing images if any, or manage them better

        if (listInfo == null)
        {
            treeView.EndUpdate();
            return;
        }

        // Ensure default images are in the ImageList for consistency if ImageCacheService provides them this way
        // This part depends on how ImageCacheService and ImageList are designed to work together.
        // For now, assuming GetImageIndex will add images as needed.
        // A more robust approach would be to pre-populate ImageList from ImageCacheService for known types.


        foreach (var group in listInfo.ListGroup)
        {
            treeView.Nodes.Add(CreateTreeNode(group, imageCacheService, treeView.ImageList));
        }
        treeView.EndUpdate();

        if (!string.IsNullOrEmpty(selectedNodePath))
        {
            SelectNodeByPath(treeView, selectedNodePath);
        }
    }

    private static TreeNode CreateTreeNode(GroupInfo groupInfo, ImageCacheService imageCacheService, ImageList imageList)
    {
        Image nodeImage = null;
        if (!string.IsNullOrEmpty(groupInfo.Value.IconPath) && groupInfo.Type != MyEnums.GroupType.Header) // Headers might use type-based icon
        {
            nodeImage = imageCacheService.GetImage(groupInfo.Value.IconPath);
        }

        if (nodeImage == null || nodeImage == imageCacheService.GetImage(MyEnums.GroupType.Null)) // If no specific icon, or specific icon failed to load
        {
            nodeImage = imageCacheService.GetImage(groupInfo.Type);
        }

        var imageIndex = GetImageIndex(imageList, nodeImage, groupInfo.Identify.FullPath); // Pass a key for the image

        var treeNode = new TreeNode(groupInfo.Identify.Name, imageIndex, imageIndex)
        {
            Tag = groupInfo.Identify.FullPath // Store full path in Tag for easy retrieval
        };

        foreach (var subGroup in groupInfo.ListGroup)
        {
            treeNode.Nodes.Add(CreateTreeNode(subGroup, imageCacheService, imageList));
        }
        return treeNode;
    }

    private static int GetImageIndex(ImageList imageList, Image image, string key)
    {
        if (image == null) return 0; // Default image index if truly null
        if (imageList == null) return -1; // Should not happen if treeView has an ImageList

        if (imageList.Images.ContainsKey(key))
        {
            return imageList.Images.IndexOfKey(key);
        }
        else
        {
            imageList.Images.Add(key, image);
            return imageList.Images.Count - 1;
        }
    }


    public static void SelectNodeByPath(TreeView treeView, string path)
    {
        foreach (TreeNode node in treeView.Nodes)
        {
            var foundNode = FindNodeRecursive(node, path);
            if (foundNode != null)
            {
                treeView.SelectedNode = foundNode;
                foundNode.EnsureVisible();
                treeView.Focus();
                break;
            }
        }
    }

    private static TreeNode FindNodeRecursive(TreeNode parentNode, string path)
    {
        if ((string)parentNode.Tag == path)
        {
            return parentNode;
        }
        foreach (TreeNode childNode in parentNode.Nodes)
        {
            var found = FindNodeRecursive(childNode, path);
            if (found != null) return found;
        }
        return null;
    }

    public static TreeNode AddTreeNode(TreeView treeView, GroupInfo newGroupInfo, ImageCacheService imageCacheService, string parentNodePath = null)
    {
        TreeNode newNode = CreateTreeNode(newGroupInfo, imageCacheService, treeView.ImageList);
        TreeNode parentNode = null;

        if (!string.IsNullOrEmpty(parentNodePath))
        {
            // FindNodeRecursive needs to search all root nodes if parentNodePath is multi-level.
            // This simplified version assumes parentNodePath is directly under a root node or is a root node's child.
            // For deeply nested, a more robust search from treeView.Nodes might be needed.
            foreach(TreeNode rootNode in treeView.Nodes) {
                parentNode = FindNodeRecursive(rootNode, parentNodePath);
                if (parentNode != null) break;
            }
        }

        if (parentNode != null)
        {
            parentNode.Nodes.Add(newNode);
            parentNode.Expand();
        }
        else
        {
            treeView.Nodes.Add(newNode);
        }
        treeView.SelectedNode = newNode;
        newNode.EnsureVisible();
        return newNode;
    }
}

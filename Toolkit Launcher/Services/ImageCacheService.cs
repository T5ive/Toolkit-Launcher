namespace Toolkit_Launcher.Services;

using System.Drawing;
using System.Windows.Forms;
using System.IO;

public class ImageCacheService
{
    private readonly ImageList _imageList;
    private readonly Dictionary<string, Image> _fileImageCache;

    public ImageCacheService()
    {
        _imageList = new ImageList();
        _fileImageCache = new Dictionary<string, Image>();
        InitializeDefaultImages();
    }

    private void InitializeDefaultImages()
    {
        // Placeholder for default images - actual images would need to be added to ImageList
        // For now, using small dummy bitmaps.
        // These indices (0, 1, 2, 3) should correspond to MyEnums.GroupType values if used directly.
        _imageList.Images.Add(new Bitmap(16,16)); // 0 - Null
        _imageList.Images.Add(new Bitmap(16,16)); // 1 - Header
        _imageList.Images.Add(new Bitmap(16,16)); // 2 - Item
        _imageList.Images.Add(new Bitmap(16,16)); // 3 - Separator
    }

    public Image GetImage(MyEnums.GroupType groupType)
    {
        int imageIndex = groupType switch
        {
            MyEnums.GroupType.Header => 1,
            MyEnums.GroupType.Item => 2,
            MyEnums.GroupType.Separator => 3,
            _ => 0 // Default or Null
        };
        if (imageIndex >= 0 && imageIndex < _imageList.Images.Count)
        {
            return _imageList.Images[imageIndex];
        }
        return _imageList.Images[0]; // Return default image
    }

    private Icon IconFromFilePath(string filePath)
    {
        Icon result = null;
        try
        {
            if (File.Exists(filePath))
            {
                result = Icon.ExtractAssociatedIcon(filePath);
            }
        }
        catch (Exception)
        {
            // Log error or handle as needed
        }
        return result;
    }

    public Image GetImage(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return GetImage(MyEnums.GroupType.Null); // Return a default image

        if (_fileImageCache.TryGetValue(filePath, out var cachedImage))
        {
            return cachedImage;
        }

        if (!File.Exists(filePath))
        {
             _fileImageCache[filePath] = GetImage(MyEnums.GroupType.Null); // Cache a default "not found" image
             return _fileImageCache[filePath];
        }

        Image image = null;
        try
        {
            string extension = Path.GetExtension(filePath)?.ToLowerInvariant();
            if (extension == ".exe" || extension == ".dll")
            {
                Icon icon = IconFromFilePath(filePath);
                if (icon != null)
                {
                    image = icon.ToBitmap();
                }
            }
            else
            {
                // For other image types like .png, .jpg, .ico
                image = new Bitmap(filePath);
            }
        }
        catch (Exception)
        {
            // Log error
            image = null; // Ensure image is null if loading failed
        }

        if (image == null)
        {
            image = GetImage(MyEnums.GroupType.Null); // Use a default image if loading failed
        }

        _fileImageCache[filePath] = image;
        return image;
    }

    // Provide access to the underlying ImageList for controls that require it (like ContextMenuStrip)
    public ImageList GetSystemImageList()
    {
        // This is tricky, as ImageList is typically populated with images that have keys.
        // For now, returning the internal one, but ideally, ContextMenuStrip would also use GetImage(key)
        // or icons would be added to this _imageList with specific keys if needed by FrmMain.
        // This might need refinement based on how FrmMain uses it.
        return _imageList;
    }
}

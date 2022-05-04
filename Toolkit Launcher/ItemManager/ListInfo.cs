namespace Toolkit_Launcher;

public class ListInfo
{
    public List<GroupInfo> ListGroup { get; set; } = new List<GroupInfo>();
}

public class GroupInfo
{
    public Identify Identify { get; set; } = null!;
    public ValueInfo Value { get; set; } = new ValueInfo();
    public AdvanceInfo Advance { get; set; } = new AdvanceInfo();
    public List<GroupInfo> ListGroup { get; set; } = new List<GroupInfo>();
    public MyEnums.GroupType Type { get; set; } = MyEnums.GroupType.Null;
}

public class Identify
{
    public int Index { get; set; }

    public string FullPath { get; set; } = null!;

    public string Name { get; set; } = null!;
}

public class ValueInfo
{
    public string FilePath { get; set; } = null!;
    public string? IconPath { get; set; }

    public int ImageID { get; set; }
}

public class AdvanceInfo
{
    public string? Commands { get; set; }

    public List<bool> TargetTypes { get; set; } = new List<bool>();

    public bool Admin { get; set; }
    public bool CMD { get; set; }
    public MyEnums.CMDType CmdType { get; set; }
}
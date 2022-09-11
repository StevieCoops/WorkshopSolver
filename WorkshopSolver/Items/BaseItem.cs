using ImGuiScene;
using Lumina.Excel.GeneratedSheets;

namespace WorkshopSolver;

public abstract class BaseItem {
    public string Name;
    public Item Item;
    public uint ItemID;
    public TextureWrap Icon;
    public uint RowID;
    public byte UIIndex;
}

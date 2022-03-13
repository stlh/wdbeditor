using System;

namespace Net.Zxnn.Dnd.Core;
public class CharacterSheet
{

    public Character Character { get; set; }
    private TablePrinter _tp = new TablePrinter();


    public CharacterSheet(Character c)
    {
        this.Character = c;
    }

    public String PrintCharacter()
    {
        _tp.ph()
            .p(TablePrinter.V)
            .p(String.Format("{0,-39}", "DUGENOUS & DRAGONS"))
            .p(TablePrinter.V)
            .p(String.Format("{0,-19}", this.Character.Class.Name + ' ' + this.Character.Level))
            .p(String.Format("{0,-19}", this.Character.Name))
            .pl(TablePrinter.V)
            .p(TablePrinter.BottomLeft)
            .p(TablePrinter.H, 78)
            .p(TablePrinter.BottomRight);

        return _tp.ToString();
    }
}
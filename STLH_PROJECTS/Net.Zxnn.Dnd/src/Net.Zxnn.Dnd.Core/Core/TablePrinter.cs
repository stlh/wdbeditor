using System;
using System.Text;

namespace Net.Zxnn.Dnd.Core;
public class TablePrinter
{
    private StringBuilder _sb;

    public TablePrinter()
    {
        _sb = new StringBuilder();
    }

    public const char H = '─';
    public const char V = '│';
    public const char TopLeft = '┌';
    public const char TopMid = '┬';
    public const char TopRight = '┐';
    public const char MidLeft = '├';
    public const char MidMid = '┼';
    public const char MidRight = '┤';
    public const char BottomLeft = '└';
    public const char BottomMid = '┴';
    public const char BottomRight = '┘';
    public const char TreeIcon = '├';
    public const char TreeLink = '│';
    public const char TreeLast = '└';

    public TablePrinter ph()
    {
        this.p(TopLeft)
            .p(H, 78)
            .pl(TopRight);
        
        return this;
    }

    public TablePrinter p(char ch)
    {
        _sb.Append(ch);

        return this;
    }

    public TablePrinter p(char ch, int n)
    {
        _sb.Append(new String(ch, n));

        return this;
    }

    public TablePrinter p(String str)
    {
        _sb.Append(str);

        return this;
    }

    public TablePrinter pl(String str)
    {
        _sb.AppendLine(str);

        return this;
    }

    public TablePrinter pl(char ch)
    {
        _sb.AppendLine(new String(ch, 1));

        return this;
    }

    override public String ToString()
    {
        return _sb.ToString();
    }
}
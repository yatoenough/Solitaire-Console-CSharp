using System.Text;
using Solitaire.Core.MenuCore;
using Solitaire.Core.MenuCore.Implementations;
using Solitaire.I18n;

namespace Solitaire;

internal static class Program
{
    
    static Program()
    {
        Console.Title = GameStrings.app_name;
        Console.OutputEncoding = Encoding.UTF8;
    }
    
    private static void Main()
    {
        Menu.Handle(new MainMenu(), infinite: true);
    }
}
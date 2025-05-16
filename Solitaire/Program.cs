using System.Text;
using Solitaire.I18n;
using Solitaire.MenuCore;
using Solitaire.MenuCore.Implementations;

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
        Menu.HandleSubMenuInteraction(new MainMenu(), infinite: true);
    }
}
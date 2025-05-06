namespace Solitaire.Core.Menu;

public class StartGameMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Play";
    }

    public void Execute()
    {
        var game = new Game();
        game.Start();
    }
}
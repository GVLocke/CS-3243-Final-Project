namespace FinalProject;

public class ProgressBar(int total)
{
    private int _current;

    public void Increment()
    {
        _current++;
        Draw();
    }

    private void Draw()
    {
        Console.CursorLeft = 0;
        Console.Write("[");
        Console.Write(new string('#', (int)((double)_current / total * 50)));
        Console.Write(new string(' ', 50 - (int)((double)_current / total * 50)));
        Console.Write("] ");
        Console.Write((int)((double)_current / total * 100) + "%");
    }
}
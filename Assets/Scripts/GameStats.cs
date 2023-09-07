
public class GameStats : GenericSingleton<GameStats>
{
    private int moveCount = 1;
    internal int currentLevel;

    internal int IncreaseMoveCount()
    {
        return moveCount++;
    } 
}

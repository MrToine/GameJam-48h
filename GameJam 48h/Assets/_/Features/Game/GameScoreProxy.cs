using UnityEngine;

public static class GameScoreProxy
{
    public static void AddScore(int value)
    {
        var gameManager = Object.FindObjectOfType<Game.GameManager>();
        if (gameManager != null)
            gameManager.AddScore(value);
    }
}
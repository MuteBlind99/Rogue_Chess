using Chess.Core;
using Chess.Presentation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardView boardView;
    
    private GameState game;
    private void Start()
    {
        game = new GameState(Board.CreateStandard());
        boardView.Refresh(game.Board);
    }
    
}

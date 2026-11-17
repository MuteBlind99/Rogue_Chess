using System.Collections.Generic;
using Chess.Core;
using Chess.Presentation;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardView boardView;

    private GameState game;

    private Coord? selected;

    private List<Move> selectedMoves = new List<Move>();

    private void Start()
    {
        game = new GameState(Board.CreateStandard());
        boardView.Refresh(game.Board);
    }

    private void Update()
    {
        if (game.IsOver)
            return;
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Coord clicked = ScreenToCoord();
        if (!game.Board.Contains(clicked))
        {
            Deselect();
            return;
        }

        if (selected.HasValue)
        {
            Move move = new Move(selected.Value, clicked);
            if (selectedMoves.Contains(move))
            {
                PlayMove(move);
                return;
            }
        }

        Piece piece = game.Board.Get(clicked);
        if (piece != null && piece.Side == game.SideToMove)
            Select(clicked);
        else
            Deselect();
    }

    private void Select(Coord coord)
    {
        selected = coord;
        selectedMoves = MoveGenerator.GetMoves(game.Board, coord);
        boardView.ShowHighlights(selectedMoves);
    }

    private void Deselect()
    {
        selected = null;
        selectedMoves.Clear();
        boardView.ClearHighlights();
    }

    private void PlayMove(Move move)
    {
        game.Play(move);
        Deselect();
        boardView.Refresh(game.Board);

        if (game.IsOver)
            Debug.Log($"Partie terminée : victoire des {game.Winner}");
    }

    private Coord ScreenToCoord()
    {
        Vector2 screen = Mouse.current.position.ReadValue();

        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));

        return new Coord(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y));
    }
}
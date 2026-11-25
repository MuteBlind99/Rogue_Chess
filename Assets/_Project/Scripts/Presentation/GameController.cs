using System.Collections;
using System.Collections.Generic;
using Chess.AI;
using Chess.Core;
using Chess.Presentation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chess.Presentation
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BoardView boardView;
        [SerializeField] private Side aiSide = Side.Black;
        [SerializeField] private float aiDelay = 0.6f;
        [SerializeField, Range(1, 4)] private int aiDepth = 3;

        private GameState _game;

        private Coord? _selected;

        private List<Move> _selectedMoves = new List<Move>();

        private bool _aiThinking;

        private void Start()
        {
            _game = new GameState(Board.CreateStandard());
            boardView.Refresh(_game.Board);
        }

        private void Update()
        {
            if (_game.IsOver)
                return;

            if (_game.SideToMove == aiSide)
            {
                if (!_aiThinking)
                    StartCoroutine(AITurn());
                // PlayAIMove();
                return;
            }

            if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
                return;

            Coord clicked = ScreenToCoord();
            if (!_game.Board.Contains(clicked))
            {
                Deselect();
                return;
            }

            if (_selected.HasValue)
            {
                Move move = new Move(_selected.Value, clicked);
                if (_selectedMoves.Contains(move))
                {
                    PlayMove(move);
                    return;
                }
            }

            Piece piece = _game.Board.Get(clicked);
            if (piece != null && piece.Side == _game.SideToMove)
                Select(clicked);
            else
                Deselect();
        }

        private IEnumerator AITurn()
        {
            _aiThinking = true;

            yield return new WaitForSeconds(aiDelay);

            PlayAIMove();
            _aiThinking = false;
        }

        private void PlayAIMove()
        {
            //Move? move = GreedyAI.ChooseMove(_game.Board, aiSide);
            Move? move = MinimaxAI.ChooseMove(_game.Board, aiSide,aiDepth);

            if (move.HasValue)
                PlayMove(move.Value);
            else
                Debug.LogWarning($"L'IA ({aiSide}) n'a aucun coup possible.");
        }

        private void Select(Coord coord)
        {
            _selected = coord;
            _selectedMoves = MoveGenerator.GetMoves(_game.Board, coord);
            boardView.ShowHighlights(_selectedMoves);
        }

        private void Deselect()
        {
            _selected = null;
            _selectedMoves.Clear();
            boardView.ClearHighlights();
        }

        private void PlayMove(Move move)
        {
            _game.Play(move);
            Deselect();
            boardView.Refresh(_game.Board);

            if (_game.IsOver)
                Debug.Log($"Partie terminée : victoire des {_game.Winner}");
        }

        private Coord ScreenToCoord()
        {
            Vector2 screen = Mouse.current.position.ReadValue();

            Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));

            return new Coord(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y));
        }
    }
}
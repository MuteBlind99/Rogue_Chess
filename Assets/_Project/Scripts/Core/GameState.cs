using System;
using System.Collections.Generic;
using Chess.Core;

public class GameState
{
    public Board Board { get; }

    public Side SideToMove { get; private set; }
    public bool IsOver { get; private set; } // vaut false par défaut
    public Side? Winner { get; private set; } // vaut null par défaut : pas encore de gagnant

    public GameState(Board board)
    {
        Board = board;
        SideToMove = Side.White;
    }

    public List<Move> GetAllMoves()
    {
        List<Move> moves = new List<Move>();
        //Partie terminée
        if(IsOver)
            return moves;
        
        foreach (var (coord,piece) in Board.AllPieces())
        {
            if (piece.Side != SideToMove)
                continue;
            moves.AddRange(MoveGenerator.GetMoves(Board, coord));
        }
        return moves;
    }

    public void Play(Move move)
    {
        // 1. Partie terminée : interdit de jouer.
        if (IsOver)
            throw new InvalidOperationException($"La partie est terminée, impossible de jouer {move}.");

        // 2. Le coup doit faire partie des coups possibles du camp au trait.
        //    Ça couvre à la fois "mauvaise couleur" et "coup impossible".
        if (!GetAllMoves().Contains(move))
            throw new InvalidOperationException($"Coup illégal : {move}");

        // 3. On joue, en gardant la pièce capturée.
        MoveRecord record = Board.MakeMove(move);

        // 4. Roi capturé : fin de partie, le camp qui vient de jouer gagne.
        if (record.Captured != null && record.Captured.Type == PieceType.King)
        {
            IsOver = true;
            Winner = SideToMove;
            return;     // on sort : pas de changement de tour, la partie est finie
        }

        // 5. Sinon, c'est au tour de l'adversaire.
        SideToMove = SideToMove.Opponent();
    }
}
using System;
using System.Collections.Generic;
using Chess.Core;
using UnityEngine;

namespace Chess.Presentation
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Sprite squareSprite;
        [SerializeField] private Color lightColor = new Color(0.9f, 0.85f, 0.71f);
        [SerializeField] private Color darkColor = new Color(0.55f, 0.40f, 0.30f);

        [SerializeField] private Sprite[] whiteSprites = new Sprite[6];
        [SerializeField] private Sprite[] blackSprites = new Sprite[6];

        private readonly List<GameObject> pieceObjects = new List<GameObject>();
        public static Vector3 ToWorld(Coord c)
        {
            return new Vector3(c.File, c.Rank, 0f);
        }

        private void Start()
        {
            CreateSquare(Board.DefaultSize);
        }

        public void Refresh(Board board)
        {
            foreach (GameObject obj in pieceObjects)
                Destroy(obj);
            pieceObjects.Clear();
            
            foreach (var (coord, piece) in board.AllPieces())
            {
                GameObject pieceObject = new GameObject($"{piece} {coord}");
                pieceObject.transform.SetParent(transform);
                pieceObject.transform.position = ToWorld(coord);
                
                SpriteRenderer sr = pieceObject.AddComponent<SpriteRenderer>();
                sr.sprite = GetSprite(piece);
                sr.sortingOrder = 1;
                
                pieceObjects.Add(pieceObject);
            }
        }

        private Sprite GetSprite(Piece piece)
        {
            Sprite[] sprites= piece.Side == Side.White ? whiteSprites : blackSprites;
            
            return sprites[(int)piece.Type];
        }
        
        private void CreateSquare(int size)
        {
            for (int file = 0; file < size; file++)
            {
                for (int rank = 0; rank < size; rank++)
                {
                    Coord coord = new Coord(file, rank);

                    GameObject square = new GameObject(coord.ToString());

                    square.transform.SetParent(transform);
                    square.transform.position = ToWorld(coord);

                    SpriteRenderer sr = square.AddComponent<SpriteRenderer>();
                    sr.sprite = squareSprite;

                    bool isDark = (file + rank) % 2 == 0;
                    sr.color = isDark ? darkColor : lightColor;
                }
            }
        }
    }
}
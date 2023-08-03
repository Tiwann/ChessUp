using System;
using UnityEngine;

namespace Chess.Core
{
    [Flags][Serializable]
    public enum ChessPieceType : short
    {
        None = 0,
        Black = 1 << 7,
        White = 1 << 8,
        King = 1 << 1,
        Queen = 1 << 2,
        Knight = 1 << 3,
        Bishop = 1 << 4,
        Rook = 1 << 5,
        Pawn = 1 << 6,
    }

    public class ChessPiece : MonoBehaviour
    {
        [SerializeField] private ChessPieceType m_Type;
        [SerializeField] private Camera m_Camera;
        [SerializeField] private ChessBoard m_Board;

        public override string ToString()
        {
            return Type.GetName();
        }

        public ChessPieceType Type
        {
            get => m_Type;
            set => m_Type = value;
        }

        public ChessBoard Board
        {
            get => m_Board;
            set => m_Board = value;
        }

        public bool IsBlack() => Type.HasFlag(ChessPieceType.Black);
        public bool IsWhite() => Type.HasFlag(ChessPieceType.White);

        private void Start()
        {
            m_Camera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                m_Camera.transform.rotation.eulerAngles.z);
        }



    }

    public static class ChessPieceHelpers
    {
        public static ChessPieceType GetTypeFromCharacter(this char Character)
        {
            return Character switch
            {
                'k' => ChessPieceType.Black | ChessPieceType.King,
                'q' => ChessPieceType.Black | ChessPieceType.Queen,
                'n' => ChessPieceType.Black | ChessPieceType.Knight,
                'b' => ChessPieceType.Black | ChessPieceType.Bishop,
                'r' => ChessPieceType.Black | ChessPieceType.Rook,
                'p' => ChessPieceType.Black | ChessPieceType.Pawn,
                'K' => ChessPieceType.White | ChessPieceType.King,
                'Q' => ChessPieceType.White | ChessPieceType.Queen,
                'N' => ChessPieceType.White | ChessPieceType.Knight,
                'B' => ChessPieceType.White | ChessPieceType.Bishop,
                'R' => ChessPieceType.White | ChessPieceType.Rook,
                'P' => ChessPieceType.White | ChessPieceType.Pawn,
                _ => ChessPieceType.None
            };
        }
        public static Sprite GetSprite(this ChessPieceType Piece, ChessPieceStyle Style)
        {
            return Piece switch
            {
                ChessPieceType.Black | ChessPieceType.King => Style.BlackPieces.King,
                ChessPieceType.Black | ChessPieceType.Queen => Style.BlackPieces.Queen,
                ChessPieceType.Black | ChessPieceType.Knight => Style.BlackPieces.Knight,
                ChessPieceType.Black | ChessPieceType.Bishop => Style.BlackPieces.Bishop,
                ChessPieceType.Black | ChessPieceType.Rook => Style.BlackPieces.Rook,
                ChessPieceType.Black | ChessPieceType.Pawn => Style.BlackPieces.Pawn,
                ChessPieceType.White | ChessPieceType.King => Style.WhitePieces.King,
                ChessPieceType.White | ChessPieceType.Queen => Style.WhitePieces.Queen,
                ChessPieceType.White | ChessPieceType.Knight => Style.WhitePieces.Knight,
                ChessPieceType.White | ChessPieceType.Bishop => Style.WhitePieces.Bishop,
                ChessPieceType.White | ChessPieceType.Rook => Style.WhitePieces.Rook,
                ChessPieceType.White | ChessPieceType.Pawn => Style.WhitePieces.Pawn,
                _ => null
            };
        }
        
        public static string GetName(this ChessPieceType Piece)
        {
            return Piece switch
            {
                ChessPieceType.Black | ChessPieceType.King => "Black King",
                ChessPieceType.Black | ChessPieceType.Queen => "Black Queen",
                ChessPieceType.Black | ChessPieceType.Knight => "Black Knight",
                ChessPieceType.Black | ChessPieceType.Bishop => "Black Bishop",
                ChessPieceType.Black | ChessPieceType.Rook => "Black Rook",
                ChessPieceType.Black | ChessPieceType.Pawn => "Black Pawn",
                ChessPieceType.White | ChessPieceType.King => "White King",
                ChessPieceType.White | ChessPieceType.Queen => "White Queen",
                ChessPieceType.White | ChessPieceType.Knight => "White Knight",
                ChessPieceType.White | ChessPieceType.Bishop => "White Bishop",
                ChessPieceType.White | ChessPieceType.Rook => "White Rook",
                ChessPieceType.White | ChessPieceType.Pawn => "White Pawn",
                _ => null
            };
        }
    }
}
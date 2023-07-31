using System;
using UnityEngine;


namespace Chess.Core
{
    [Serializable]
    public struct ChesspieceSet
    {
        public Sprite King;
        public Sprite Queen;
        public Sprite Knight;
        public Sprite Bishop;
        public Sprite Rook;
        public Sprite Pawn;
    }
    
    [CreateAssetMenu(menuName = "Chess/Piece Style")]
    public class ChessPieceStyle : ScriptableObject
    {
        public ChesspieceSet BlackPieces;
        public ChesspieceSet WhitePieces;
    }
}
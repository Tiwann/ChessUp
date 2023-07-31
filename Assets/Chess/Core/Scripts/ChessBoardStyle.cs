using UnityEngine;

namespace Chess.Core
{
    [CreateAssetMenu(menuName = "Chess/Board Style", fileName = "NewChessboardStyle")]
    public class ChessBoardStyle : ScriptableObject
    {
        public Color DarkColor;
        public Color LightColor;
    }
}

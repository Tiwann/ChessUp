using System.IO;
using System.Linq;
using Chess.Core;
using UnityEditor;
using UnityEngine;
using Directory = System.IO.Directory;

namespace Chess.Editor
{
    [CustomEditor(typeof(ChessPieceStyle))]
    public class ChessPieceStyleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ChessPieceStyle Style = target as ChessPieceStyle;
            if (!Style) return;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply Sprites"))
            {
                string Folder = EditorUtility.OpenFolderPanel("Select a folder",
                    Path.Combine(Application.dataPath, "Chess", "Core", "Sprites", "Pieces"), null);

                string[] Assets = Directory.EnumerateFiles(Folder, "*.png").ToArray();
                foreach (string Asset in Assets)
                {
                    string Filename = Path.GetFileNameWithoutExtension(Asset);
                    switch (Filename)
                    {
                        case "BlackKing": Style.BlackPieces.King = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "BlackQueen": Style.BlackPieces.Queen = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "BlackKnight": Style.BlackPieces.Knight = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "BlackBishop": Style.BlackPieces.Bishop = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "BlackRook": Style.BlackPieces.Rook = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "BlackPawn": Style.BlackPieces.Pawn = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhiteKing": Style.WhitePieces.King = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhiteQueen": Style.WhitePieces.Queen = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhiteKnight": Style.WhitePieces.Knight = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhiteBishop": Style.WhitePieces.Bishop = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhiteRook": Style.WhitePieces.Rook = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                        case "WhitePawn": Style.WhitePieces.Pawn = AssetDatabase.LoadAssetAtPath<Sprite>(Path.GetRelativePath(Application.dataPath, Asset)); break;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}

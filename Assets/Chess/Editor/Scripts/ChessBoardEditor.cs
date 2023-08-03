using System;
using Chess.Core;
using UnityEditor;
using UnityEngine;

namespace Chess.Editor
{
    [CustomEditor(typeof(ChessBoard))]
    public sealed class ChessBoardEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ChessBoard Board = target as ChessBoard;
            if (!Board) return;

            GameObject AlreadyGeneratedBoard = GameObject.Find(ChessBoard.GeneratedBoardName);
            string ButtonText = AlreadyGeneratedBoard ? "Regenerate Board" : "Generate Board";

            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button(ButtonText))
            {
                if(AlreadyGeneratedBoard) DestroyImmediate(AlreadyGeneratedBoard);
                GameObject GeneratedBoard = new(ChessBoard.GeneratedBoardName);
                GeneratedBoard.transform.position = Board.transform.position;
                Board.GenerateBoard(GeneratedBoard);
            }

            if (AlreadyGeneratedBoard)
            {
                if (GUILayout.Button("Destroy Generated Board"))
                {
                    DestroyImmediate(AlreadyGeneratedBoard);
                }
            }
            EditorGUILayout.EndHorizontal();
            
        }
    }
}

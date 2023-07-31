using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chess.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Chess.Editor
{
    [CustomEditor(typeof(ChessPieceStyle))]
    public class ChessPieceStyleEditor : UnityEditor.Editor
    {

        public string Directory;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ChessPieceStyle Style = target as ChessPieceStyle;
            if (!Style) return;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply Sprites"))
            {
                
            }
            GUILayout.EndHorizontal();
        }
    }
}

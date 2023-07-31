using System;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using TMPro;


namespace Chess.Core
{
    public class ChessBoard : MonoBehaviour
    {
        [Header("Style")]
        [SerializeField] private ChessBoardStyle m_BoardStyle;
        [SerializeField] private ChessPieceStyle m_PieceStyle;
        [SerializeField] private GameObject m_SquarePrefab;
        [SerializeField] private float m_SquareSize = 10.0f;
        [SerializeField] private float m_TextMargin = 0.5f;
        [SerializeField] private float m_FontSize = 24.0f;
        [SerializeField] private float m_FontOpacity = 0.5f;
        
        
        public float SquareSize => m_SquareSize;
        public float HalfSquareSize => m_SquareSize / 2.0f;
        public static readonly string GeneratedBoardName = "GeneratedBoard";
        public static readonly string StartPositions = "rnbqkbnr" +
                                                       "pppppppp" +
                                                       "        " +
                                                       "        " +
                                                       "        " +
                                                       "        " +
                                                       "PPPPPPPP" +
                                                       "RNBQKBNR";

        private GameObject[] Squares = new GameObject[64];
        

        private void Awake()
        {
            GameObject GeneratedBoard = GameObject.Find(GeneratedBoardName);
            if(GeneratedBoard) DestroyImmediate(GeneratedBoard);
            GenerateBoard(new GameObject(GeneratedBoardName));
            GeneratePieces(StartPositions);
        }
        
        public void GenerateBoard(GameObject Parent)
        {
            if (!m_SquarePrefab) return;

            for (int i = 0; i < 64; i++)
            {
                int Row = i / 8;
                int Column = i % 8;
                
                float XPosition = Column * m_SquareSize;
                float YPosition = Row * m_SquareSize;
                Vector3 Position = new(XPosition, YPosition, 0.0f);

                GameObject GameObject = Instantiate(m_SquarePrefab, Parent.transform);
                GameObject.name = $"ChessSquare {(char)(Column + 65)}{Row + 1}";
                GameObject.transform.position = Position;
                Squares[i] = GameObject;

                Quad Quad = GameObject.GetComponent<Quad>();
                Quad.BlendMode = ShapesBlendMode.Opaque;
                Quad.A = new Vector3(-HalfSquareSize, +HalfSquareSize);
                Quad.B = new Vector3(+HalfSquareSize, +HalfSquareSize);
                Quad.C = new Vector3(+HalfSquareSize, -HalfSquareSize);
                Quad.D = new Vector3(-HalfSquareSize, -HalfSquareSize);
                Quad.Color = GetSquareColor(Row, Column);
                
                
                if (Column == 0)
                {
                    GameObject Text = AddText(Quad.transform, "NumberText", $"{Row + 1}", TextAlignmentOptions.TopLeft);
                    TextMeshPro TextComponent = Text.GetComponent<TextMeshPro>();
                    TextComponent.color = GetSquareColor(Column, Row, true).WithOpacity(m_FontOpacity);
                }

                if (Row == 0)
                {
                    GameObject Text = AddText(Quad.transform, "LetterText", $"{(char)(Column + 65)}", TextAlignmentOptions.BottomRight);
                    TextMeshPro TextComponent = Text.GetComponent<TextMeshPro>();
                    TextComponent.color = GetSquareColor(Column, Row, true).WithOpacity(m_FontOpacity);
                }
            }
        }

        private void GeneratePieces(string Fen)
        {
            for (int i = 0; i < Fen.Length; i++)
            {
                ChessPieceType PieceType = Fen[i].GetTypeFromCharacter();
                if (PieceType is ChessPieceType.None) continue;
                
                GameObject Piece = new GameObject(PieceType.GetName());
                ChessPiece PieceComponent = Piece.AddComponent<ChessPiece>();
                PieceComponent.Type = PieceType;
                PieceComponent.Board = this;
                Piece.transform.SetParent(Squares[i].transform);
                Piece.transform.localPosition = Vector3.zero;
                
                SpriteRenderer PieceSprite = Piece.AddComponent<SpriteRenderer>();
                PieceSprite.sprite = PieceType.GetSprite(m_PieceStyle);
                BoxCollider Collider = Piece.AddComponent<BoxCollider>();
                Collider.size = new Vector3(SquareSize, SquareSize, 1.0f);

                Piece.AddComponent<Animator>();
            }
        }

        private GameObject AddText(Transform Parent, string Name, string Text, TextAlignmentOptions AlignmentOptions)
        {
            GameObject TextGO = new(Name);
            TextGO.transform.SetParent(Parent);
            TextMeshPro TextComponent = TextGO.AddComponent<TextMeshPro>();
            TextComponent.SetText(Text);
            TextComponent.rectTransform.anchorMin = Vector2.zero;
            TextComponent.rectTransform.anchorMax = Vector2.one;
            TextComponent.rectTransform.anchoredPosition3D = new(-HalfSquareSize, -HalfSquareSize, -0.1f);
            TextComponent.rectTransform.sizeDelta = new(m_SquareSize, m_SquareSize);
            TextComponent.rectTransform.pivot = Vector2.zero;
            TextComponent.fontSize = m_FontSize;
            TextComponent.alignment = AlignmentOptions;
            TextComponent.margin = new Vector4(m_TextMargin, m_TextMargin, m_TextMargin, m_TextMargin);
            return TextGO;
        }
        
        private Color GetSquareColor(int Row, int Column, bool inverted = false)
        {
            if (m_BoardStyle)
                if (inverted)
                {
                    return Column % 2 == 0 ? Row % 2 == 0 ? m_BoardStyle.LightColor : m_BoardStyle.DarkColor :
                        Row % 2 == 0 ? m_BoardStyle.DarkColor : m_BoardStyle.LightColor;
                }
                else
                {
                    return Column % 2 == 0 ? Row % 2 == 0 ? m_BoardStyle.DarkColor : m_BoardStyle.LightColor :
                        Row % 2 == 0 ? m_BoardStyle.LightColor : m_BoardStyle.DarkColor;
                }
            return Color.black;
        }
    }
}


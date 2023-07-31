using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Chess.Core
{
    public class ChessBoardAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject m_ChessBoard;
        [SerializeField] private RuntimeAnimatorController m_Controller;
        [SerializeField] private float m_TimeBetweenSquares = 0.1f;
        [SerializeField] private float m_TimeAfterSquares = 0.1f;
        [SerializeField] private bool m_InvertPieces = true;
    
        private void Start()
        {
            m_ChessBoard = GameObject.Find(ChessBoard.GeneratedBoardName);
            if (!m_ChessBoard) return;
            StartCoroutine(AnimateSquares());
        }

        private IEnumerator AnimateSquares()
        {
            foreach (Transform t in m_ChessBoard.transform)
            {
                t.localScale = Vector3.zero;
            }
            var Pieces = m_InvertPieces ? FindObjectsOfType<ChessPiece>().Reverse().ToArray() : FindObjectsOfType<ChessPiece>().ToArray();
            
            foreach (ChessPiece ChessPiece in Pieces)
            {
                ChessPiece.transform.localScale = Vector3.zero;
            }
        
            foreach (Transform t in m_ChessBoard.transform)
            {
                Animator Animator = t.GetComponent<Animator>();
                if (!Animator) yield return null;
                Animator.runtimeAnimatorController = m_Controller;
                Animator.Play("SquareAppear");
                yield return new WaitForSeconds(m_TimeBetweenSquares);
            }

            yield return new WaitForSeconds(m_TimeAfterSquares);

            
            
            foreach (ChessPiece ChessPiece in Pieces)
            {
                Animator Animator = ChessPiece.GetComponent<Animator>();
                if (!Animator) yield return null;
                Animator.runtimeAnimatorController = m_Controller;
                Animator.Play("SquareAppear");
                yield return new WaitForSeconds(m_TimeBetweenSquares);
            }
        }
    }
}


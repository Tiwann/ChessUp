// (C) Erwann Messoah 2023
using UnityEngine;

namespace Chess.Core
{
    public class PieceMover : MonoBehaviour
    {
        [Header("Mouse Ray")]
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_MouseRayDistance = 10.0f;
        [SerializeField] private AudioClip m_MoveSound;
        [SerializeField] private AudioClip m_CaptureSound;
        [SerializeField] private AudioSource m_AudioSource;

        [Header("Cursors")] 
        [SerializeField] private Texture2D m_ArrowCursor;
        [SerializeField] private Texture2D m_HandCursor;
        [SerializeField] private Texture2D m_HandGrabCursor;
        
        private Ray MouseRay;
        private ChessPiece ClickedPiece;
        private ChessPiece LastHoveredPiece;
        private bool Hover;

        #region Events
        
        /// Triggered once when the mouse hover a chess piece
        public event OnHoverBegin OnHoverBeginEvent;
        public delegate void OnHoverBegin(ChessPiece Piece);

        /// Triggered every frames when the mouse hover a chess piece
        public event OnHover OnHoverEvent;
        public delegate void OnHover(ChessPiece Piece);
        
        /// Triggered once when the mouse stopped hovering a chess piece
        public event OnHoverEnd OnHoverEndEvent;
        public delegate void OnHoverEnd();
        
        /// Triggered once when a piece is taken
        public event OnPieceCaptured OnPieceCapturedEvent;
        public delegate void OnPieceCaptured(ChessPiece CapturedPiece, ChessPiece AttackPiece);

        /// Triggered once when a move was cancelled
        public event OnMoveCancelled OnMoveCancelledEvent;
        public delegate void OnMoveCancelled();

        // TODO: Better move handling
        public delegate void OnMove(bool Capture);
        public event OnMove OnMoveEvent;
        
        #endregion Events

        private void Awake()
        {
            m_Camera = Camera.main;
            OnPieceCapturedEvent += delegate(ChessPiece CapturedPiece, ChessPiece AttackPiece)
            {
                Debug.Log($"{CapturedPiece} was taken by {AttackPiece}");
            };
        }

        private void OnEnable()
        {
            OnHoverBeginEvent += OnPieceHoverBegin;
            OnHoverEndEvent += OnPieceHoverEnd;
        }

        private void OnDisable()
        {
            OnHoverBeginEvent -= OnPieceHoverBegin;
            OnHoverEndEvent -= OnPieceHoverEnd;
        }
        
        private void OnPieceHoverBegin(ChessPiece Piece)
        {
            Cursor.SetCursor(m_HandCursor, m_HandCursor.Center(), CursorMode.ForceSoftware);
        }
        
        private void OnPieceHoverEnd()
        {
            Cursor.SetCursor(m_ArrowCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        private void Update()
        {
            // Get a Ray from mouse position in World Space to forward direction
            MouseRay = m_Camera.ScreenPointToRay(Input.mousePosition).WithDirection(Vector3.forward);
            
            // Test if we hover a chess piece and handle events
            if (Physics.Raycast(MouseRay, out RaycastHit MouseHitInfo, m_MouseRayDistance, LayerMask.GetMask("Pieces")))
            {
                ChessPiece HoveredPiece = MouseHitInfo.collider.GetComponent<ChessPiece>();
                if (!Hover || LastHoveredPiece != HoveredPiece)
                {
                    OnHoverBeginEvent?.Invoke(HoveredPiece);
                }
                
                Hover = true;
                LastHoveredPiece = HoveredPiece;
                OnHoverEvent?.Invoke(HoveredPiece);
                
                if (Input.GetMouseButtonDown(0))
                    ClickedPiece = HoveredPiece;
            }
            else
            {
                if (Hover)
                {
                    Hover = false;
                    LastHoveredPiece = null;
                    OnHoverEndEvent?.Invoke();
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if (ClickedPiece)
                {
                    Ray Ray = new Ray(ClickedPiece.transform.position, Vector3.forward);
                    if (Physics.Raycast(Ray, out RaycastHit PieceHitInfo, 10.0f, LayerMask.GetMask("Squares")))
                    {
                        ChessPiece FoundPiece = PieceHitInfo.collider.GetComponentInChildren<ChessPiece>();
                        OnMoveEvent?.Invoke(FoundPiece && FoundPiece != ClickedPiece);
                        
                        ClickedPiece.transform.SetParent(PieceHitInfo.collider.transform);
                        ClickedPiece.transform.localPosition = Vector3.back;

                        if (FoundPiece)
                        {
                            if (FoundPiece == ClickedPiece)
                            {
                                m_AudioSource.PlayOneShot(m_MoveSound);
                            }
                            else
                            {
                                OnPieceCapturedEvent?.Invoke(FoundPiece, ClickedPiece);
                                Destroy(FoundPiece.gameObject);
                                m_AudioSource.PlayOneShot(m_CaptureSound);
                            }
                        }
                        else
                        {
                            m_AudioSource.PlayOneShot(m_MoveSound);
                        }
                    }
                    ClickedPiece = null;
                }
            }

            if (ClickedPiece)
            {
                ClickedPiece.transform.position = new Vector3(MouseRay.origin.x, MouseRay.origin.y,
                    ClickedPiece.transform.position.z)
                .Clamped(new Vector3(-ClickedPiece.Board.HalfSquareSize, -ClickedPiece.Board.HalfSquareSize, -1.0f), 
                    new Vector3(8 * ClickedPiece.Board.SquareSize - ClickedPiece.Board.HalfSquareSize, 8 * ClickedPiece.Board.SquareSize - ClickedPiece.Board.HalfSquareSize, 1.0f));
            }
        }
    }
}
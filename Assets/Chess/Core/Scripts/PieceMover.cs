using Shapes;
using UnityEngine;

namespace Chess.Core
{
    public class PieceMover : ImmediateModeShapeDrawer
    {
        [Header("Mouse Ray")]
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_MouseRayDistance = 10.0f;
        [SerializeField] private float m_MouseRayThickness = 2.0f;
        [SerializeField] private Color m_MouseRayColor = Color.red;
        private Ray MouseRay;
        

        private ChessPiece ClickedPiece;

        private void Awake()
        {
            m_Camera = Camera.main;
        }


        public override void DrawShapes(Camera cam)
        {
            using (Draw.Command(cam))
            {
                Draw.Line(MouseRay.origin, MouseRay.origin + MouseRay.direction * m_MouseRayDistance, m_MouseRayThickness, LineEndCap.Round, m_MouseRayColor);
            }
        }

        private void Update()
        {
            MouseRay = m_Camera.ScreenPointToRay(Input.mousePosition).WithDirection(Vector3.forward);
            if (Physics.Raycast(MouseRay, out RaycastHit HitInfo, m_MouseRayDistance))
            {
                if (Input.GetMouseButtonDown(0))
                    ClickedPiece = HitInfo.collider.GetComponent<ChessPiece>();
            }
            
            if (Input.GetMouseButtonUp(0))
                ClickedPiece = null;

            if (ClickedPiece)
            {
                ClickedPiece.transform.position = new Vector3(MouseRay.origin.x, MouseRay.origin.y,
                    ClickedPiece.transform.position.z)
                .Clamped(new Vector3(-ClickedPiece.Board.HalfSquareSize, -ClickedPiece.Board.HalfSquareSize), 
                    new Vector3(8 * ClickedPiece.Board.SquareSize - ClickedPiece.Board.HalfSquareSize, 8 * ClickedPiece.Board.SquareSize - ClickedPiece.Board.HalfSquareSize));
            }
        }
    }
}
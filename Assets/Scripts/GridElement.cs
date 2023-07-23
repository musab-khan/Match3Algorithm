using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour
{
    private PieceType m_Piece;
    public PieceType Type
    {
        get { return m_Piece; }
    }
    [SerializeField] private Image m_HighlightImage;
    [SerializeField] private Image m_PieceImage;

    private void OnEnable()
    {
        m_HighlightImage.enabled = false;
    }

    public void DisplayElement(PieceType type, Sprite shape = null)
    {
        if (type == PieceType.None)
        {
            m_Piece = type;
            m_PieceImage.enabled = false;
        }
        
        else
        {
            m_Piece = type;
            m_PieceImage.sprite = shape;
        }
    }

    public void EnableHint()
    {
        m_HighlightImage.enabled = true;
    }
}

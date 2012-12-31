using UnityEngine;
using System.Collections;

public class HexItem : DoctorObject {
	
	public int m_nColumn = -1;
	public int m_nRow = -1;
	
	public UISprite m_pImage;
//	public UILabel m_pLabel;
	
	public bool m_bIsHealing = false;
	
	public delegate void HandleClick(HexItem pItem);
	private HandleClick m_pClick;
	
	public delegate void HandleDrop(HexItem pFirst, HexItem pSecond);
	private HandleDrop m_pOnDrop;
	
	private Impermanent m_pChild;
	public Impermanent Child {
		get
		{
			return m_pChild;
		}
		set
		{
			m_pChild = value;
			m_pChild.transform.parent = this.transform;
			TweenPosition.Begin(m_pChild.gameObject, 0.1f, new Vector3(0,0,0) );
		}
	}
	
	public void Initialize (int nCol, int nRow, HandleDrop pDrop, HandleClick pClick)
	{
		base.Initialize ();
		
		m_nColumn = nCol;
		m_nRow = nRow;
		
		m_pClick = pClick;
		m_pOnDrop = pDrop;
//		m_pLabel.text = string.Format("{0},{1}", nCol, nRow);
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
//		m_bIsHealing = true;
		if( m_pChild != null)
			m_pChild.UpdateObject(dt, m_bIsHealing);
		m_bIsHealing = false;
	}
	
	public void OnClick()
	{
		if( m_pClick != null)
			m_pClick(this);
	}
	
	public void OnDrop(GameObject drag)
	{
		if( m_pOnDrop != null)
		{
			HexItem item = drag.GetComponent<HexItem>();
			if( item != null)
				m_pOnDrop(item, this);
		}
	}
	
	private bool IsEven(int nVal)
	{
		return (nVal % 2 == 0);
	}
	
	public bool IsAdjacent(HexItem item)
	{
		bool bResult = false;
		
		if( m_nColumn == item.m_nColumn)
		{
			bResult = Mathf.Abs(m_nRow - item.m_nRow) == 1;
		}
		if( m_nRow == item.m_nRow)
		{
			bResult = Mathf.Abs(m_nColumn - item.m_nColumn) == 1;
		}
		
//		int nDeltaCol = Mathf.Abs(m_nColumn - item.m_nColumn);
//		
//		if( nDeltaCol == 1)
//		{
//			if( IsEven(m_nColumn) && m_nRow - 1 == item.m_nRow)
//				bResult = true;
//			if( IsEven(m_nColumn) == false && m_nRow + 1 == item.m_nRow)
//				bResult = true;
//		}
		
		int nDeltaRow = Mathf.Abs(m_nRow - item.m_nRow);
		if( nDeltaRow == 1)
		{
			if( IsEven(m_nRow) && m_nColumn + 1 == item.m_nColumn)
				bResult = true;
			if( IsEven(m_nRow) == false && m_nColumn - 1 == item.m_nColumn)
				bResult = true;
		}
		
		return bResult;
	}
	
	public void SetColor( Color32 col )
	{
		if( m_pImage != null)
			m_pImage.color = col;
	}
}

using UnityEngine;
using System.Collections;

public class HexItem : DoctorObject {
	
	public int m_nColumn = -1;
	public int m_nRow = -1;
	
	public UISprite m_pImage;
	
	public bool m_bIsHealing = false;
	
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
	
	public void Initialize (int nCol, int nRow, HandleDrop pDrop)
	{
		base.Initialize ();
		
		m_nColumn = nCol;
		m_nRow = nRow;
		
		m_pOnDrop = pDrop;
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
		m_bIsHealing = true;
		if( m_pChild != null)
			m_pChild.UpdateObject(dt, m_bIsHealing);
	}
	
	public void OnClick()
	{
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
		
		int nDeltaCol = Mathf.Abs(m_nColumn - item.m_nColumn);
//		int nDeltaRow = Mathf.Abs(m_nRow - item.m_nRow) * 2;
		
		if( nDeltaCol == 1)
		{
			if( IsEven(nDeltaCol) && m_nRow + 1 == item.m_nRow)
				bResult = true;
			if( IsEven(nDeltaCol) == false && m_nRow -1 == item.m_nRow)
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

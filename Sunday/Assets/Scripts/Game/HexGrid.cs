using UnityEngine;
using System.Collections;

public class HexGrid : DoctorObject {
	
	public int m_nColumns = 25;
	public int m_nRows = 14;
	
	public int m_nHexWidth = 50;
	public int m_nHexHeight = 50;
	
	public Color32 m_cHealingColor;
	public Color32 m_cBaseColor;
	public Color32 m_cTargetColor;
	public Color32 m_cFromColor;
	
	public HexItem m_pHexPrefab;
	
	private HexItem[,] m_pHexes;
	
	public override void Initialize ()
	{
		base.Initialize ();
		m_pHexes = new HexItem[m_nColumns,m_nRows];
		float nStartX = m_nColumns * m_nHexWidth * -0.5f + m_nHexWidth * 0.5f;
		float nStartY = m_nRows * m_nHexHeight * -0.5f + m_nHexHeight * 0.5f;
		
		int nOdd = 1;
		for (int i = 0; i < m_nColumns; i++) {
			for (int j = 0; j < m_nRows; j++) {
				GameObject go = NGUITools.AddChild(this.gameObject, m_pHexPrefab.gameObject);
				go.name = string.Format("HexItem {0}, {1}", i, j);
				go.transform.localPosition = new Vector3(nStartX + i * m_nHexWidth + nOdd * m_nHexWidth * 0.5f, nStartY + j * m_nHexHeight);
				
				HexItem item = go.GetComponent<HexItem>();
				item.Initialize(i, j, HandleDrag,HandleClick);
				item.SetColor(m_cBaseColor);
				m_pHexes[i,j] = item;
				
				nOdd = 1 - nOdd;
			}
		}
		
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
		
		for (int i = 0; i < m_nColumns; i++) {
			for (int j = 0; j < m_nRows; j++) {
				m_pHexes[i,j].UpdateObject(dt);
			}
		}
		GameObject go = null;
		if( Input.GetMouseButton(0) )
			go = PickObject();
		
		if( go != null)
		{
			HexItem pItem = go.GetComponent<HexItem>();
			if( pItem != null)
			{
				pItem.m_bIsHealing = true;
				for (int i = 0; i < m_nColumns; i++) {
					for (int j = 0; j < m_nRows; j++) {
						if( m_pHexes[i,j] == pItem)
							continue;
						else 
						{
							if( m_pHexes[i,j].IsAdjacent(pItem) )
							{
								m_pHexes[i,j].m_bIsHealing = true;
							}
						}
					}
				}
			}
		}
		
		for (int i = 0; i < m_nColumns; i++) {
			for (int j = 0; j < m_nRows; j++) {
				if( m_pHexes[i,j].m_bIsHealing )
				{
					m_pHexes[i,j].SetColor(m_cHealingColor);
				}
				else
				{
					m_pHexes[i,j].SetColor(m_cBaseColor);
				}
			}
		}
	}
	
	public static GameObject PickObject()
    {
		Vector3 screenPos = Input.mousePosition;
		if( screenPos == null)
			return null;
        Ray ray = Camera.main.ScreenPointToRay( screenPos );
        RaycastHit hit;

        if( Physics.Raycast( ray, out hit ) )
            return hit.collider.gameObject;

        return null;
    }
	
	public void SetChildDefault(Impermanent pChild)
	{
		if( pChild != null && pChild.m_nDefaultCol >= 0 && pChild.m_nDefaultRow >= 0)
			m_pHexes[pChild.m_nDefaultCol, pChild.m_nDefaultRow].Child = pChild;
	}
	
	public void HandleClick(HexItem pItem)
	{
		
	}
	
	public void HandleDrag(HexItem pFirst, HexItem pSecond)
	{
		if( pSecond.IsAdjacent(pFirst) )
		{
			pSecond.SetColor(m_cTargetColor);
//			pFirst.SetColor(m_cBaseColor);
		}
		else
		{
//			pFirst.SetColor(m_cBaseColor);
			pSecond.SetColor(m_cFromColor);
		}
	}
}

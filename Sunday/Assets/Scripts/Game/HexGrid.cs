using UnityEngine;
using System.Collections;

public class HexGrid : DoctorObject {
	
	public int m_nColumns = 25;
	public int m_nRows = 14;
	
	public int m_nHexWidth = 50;
	public int m_nHexHeight = 50;
	
	public Color32 m_cTargetColor;
	public Color32 m_cFromColor;
	
	public HexItem m_pHexPrefab;
	
	private HexItem[,] m_pHexes;
	
	public override void Initialize ()
	{
		base.Initialize ();
		m_pHexes = new HexItem[m_nColumns,m_nRows];
		float nStartX = m_nColumns * m_nHexWidth * -0.5f;
		float nStartY = m_nRows * m_nHexHeight * -0.5f;
		
		int nOdd = 0;
		for (int i = 0; i < m_nColumns; i++) {
			for (int j = 0; j < m_nRows; j++) {
				GameObject go = NGUITools.AddChild(this.gameObject, m_pHexPrefab.gameObject);
				go.name = string.Format("HexItem {0}, {1}", i, j);
				go.transform.localPosition = new Vector3(nStartX + i * m_nHexWidth + nOdd * m_nHexWidth * 0.5f, nStartY + j * m_nHexHeight);
				
				HexItem item = go.GetComponent<HexItem>();
				item.Initialize(i, j, HandleDrag);
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
	}
	
	
	public void HandleDrag(HexItem pFirst, HexItem pSecond)
	{
		if( pSecond.IsAdjacent(pFirst) )
			pSecond.SetColor(m_cTargetColor);
		else
			pFirst.SetColor(m_cFromColor);
	}
}

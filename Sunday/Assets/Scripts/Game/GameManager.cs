using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public HexGrid m_pHexGrid;
	public GameObject m_pDefaultThings;
	public UILabel m_pScoreLabel;
	public UILabel m_pGameOverLabel;
	
	private Impermanent[] m_pImps;
	
	private int m_nScore;
	
	
	// Use this for initialization
	void Start () {
		m_pHexGrid.Initialize();
		m_pImps = m_pDefaultThings.GetComponentsInChildren<Impermanent>();
		for (int i = 0; i < m_pImps.Length; i++) {
			m_pImps[i].Initialize();
			m_pHexGrid.SetChildDefault(m_pImps[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		if( m_pHexGrid != null )
			m_pHexGrid.UpdateObject(dt);
	}
	
	public void OnItemDeath(Impermanent pItem)
	{
		HexItem hItem = pItem.transform.parent.GetComponent<HexItem>();
		if( hItem == null)
			return;
		
		hItem.Child = null;
	}
	
	public void OnTeleported(Impermanent pItem)
	{
		HexItem hItem = pItem.transform.parent.GetComponent<HexItem>();
		if( hItem == null)
			return;
		
		hItem.Child = null;
		
		pItem.transform.localPosition = new Vector3(0,-1000,0);
		NGUITools.SetActive(pItem.gameObject, false);
	}
}

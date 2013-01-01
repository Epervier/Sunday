using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public HexGrid m_pHexGrid;
	public GameObject m_pDefaultThings;
	public UILabel m_pScoreLabel;
	public UILabel m_pGameOverLabel;
	public int m_nBaseScore = 0;
	public float m_fGameOverTime = 3.0f;
	
	private Impermanent[] m_pImps;
	private Patient[] m_pPatients;
	
	private int m_nScore;
	private float m_fGameOverTimer = 0.0f;
	
	private bool m_bInitialized = false;
	// Use this for initialization
	void Start () {
		m_pHexGrid.Initialize();
		
		m_pPatients = m_pDefaultThings.GetComponentsInChildren<Patient>();
		m_pImps = m_pDefaultThings.GetComponentsInChildren<Impermanent>();
		for (int i = 0; i < m_pImps.Length; i++) {
			m_pImps[i].Initialize(OnItemDeath, OnTeleported);
			m_pHexGrid.SetChildDefault(m_pImps[i]);
		}
		m_bInitialized = true;
	}
	
	// Update is called once per frame
	void Update () {
		if( m_bInitialized == false)
			return;
		
		float dt = Time.deltaTime;
		if( m_pHexGrid != null )
			m_pHexGrid.UpdateObject(dt);
		
		UpdateScore();
		
		if( m_fGameOverTimer > 0.0f)
		{
			m_fGameOverTimer -= dt;
			if( m_fGameOverTimer <= 0.0f)
			{
				Application.LoadLevel("MainMenu");
			}
		}
		else if( m_nScore < 0 )
		{
			m_fGameOverTimer = m_fGameOverTime;
			m_pGameOverLabel.text = "You Lost, but hey, you played the game.";
		}
		else if ( PlayersAlive() == false )
		{
			m_fGameOverTimer = m_fGameOverTime;
			m_pGameOverLabel.text = "You...won.  Great.";
		}
		
	}
	
	public void OnItemDeath(Impermanent pItem)
	{
		HexItem hItem = pItem.transform.parent.GetComponent<HexItem>();
		if( hItem == null)
			return;
		
		hItem.Child = null;
		if( pItem.GetImpType() == Impermanent.eImpType.Patient)
		{
			ChangeScore(-pItem.m_nDeathCost);
		}
	}
	
	public void OnTeleported(Impermanent pItem)
	{
		HexItem hItem = pItem.transform.parent.GetComponent<HexItem>();
		if( hItem == null)
			return;
		
		hItem.Child = null;
		
		
		pItem.transform.localPosition = new Vector3(0,-1000,0);
		NGUITools.SetActive(pItem.gameObject, false);
		
		ChangeScore(pItem.m_nSaveBonus);
	}
	
	private void ChangeScore(int nChange)
	{
		m_nBaseScore += nChange;
	}
	
	private void UpdateScore()
	{
		if( m_pPatients == null )
			return;
		
		float fCount = 0;
		for (int i = 0; i < m_pPatients.Length; i++) {
			if( m_pPatients[i] != null )
				fCount += m_pPatients[i].m_fCurrentHealth;
		}
		m_nScore = m_nBaseScore + (int)fCount;
		m_pScoreLabel.text = string.Format("Score: {0}", m_nScore);
	}
	
	private bool PlayersAlive()
	{
		bool bResult = false;
		for (int i = 0; i < m_pPatients.Length; i++) {
			if( m_pPatients[i] != null && m_pPatients[i].m_bIsDead == false)
			{
				bResult = true;
				break;
			}
		}
		
		return bResult;
	}
	
	
}

using UnityEngine;
using System.Collections;

public class Impermanent : DoctorObject {
	
	public enum eImpType {Obstacle,Patient,Teleporter};
	
	public int m_nDefaultCol = -1;
	public int m_nDefaultRow = -1;
	
	public float m_fDeathPerSecond = 2.0f;
	public float m_fHealPerSecond = 5.0f;
	public float m_fMaxHealth = 100.0f;
	public float m_fCurrentHealth = 75.0f;
	public int m_nDeathCost = 0;
	public int m_nSaveBonus = 0;
	
	public bool m_bIsDead = false;
	
	public Visual m_pVisualPrefab;
	
	public delegate void OnItemDeath(Impermanent pItem);
	protected OnItemDeath m_pItemDeath;
	
	public delegate void OnTeleported(Impermanent pItem);
	protected OnTeleported m_pTeleported;
	
	protected eImpType m_eType;
	protected Visual m_pVisual;
	
	private float m_fMoveBaseTime = 0.1f;
	private float m_fMoveTimer = 0.0f;
	
	private bool m_bIsHealing;
	
	public virtual void Initialize (OnItemDeath pOnDeath, OnTeleported pOnTele)
	{
		base.Initialize ();
		
		m_pItemDeath = pOnDeath;
		m_pTeleported = pOnTele;
		
		if( m_pVisualPrefab != null)
		{
			GameObject go = NGUITools.AddChild(this.gameObject, m_pVisualPrefab.gameObject);
			go.name = "Visual";
			m_pVisual = go.GetComponent<Visual>();
			m_pVisual.Initialize();
			m_pVisual.transform.localPosition = new Vector3(0,0,-10);
		}
	}
	
	public void UpdateObject (float dt, bool bIsHealing)
	{
		base.UpdateObject (dt);
		if( m_bIsDead == true)
			return;
		
		m_bIsHealing = bIsHealing;
		
		if( m_bIsHealing )
		{
			m_fCurrentHealth += m_fHealPerSecond * dt;
			if( m_fCurrentHealth > m_fMaxHealth )
				m_fCurrentHealth = m_fMaxHealth;
		}
		else
		{
			m_fCurrentHealth -= m_fDeathPerSecond * dt;
		}
		
		if( m_fCurrentHealth <= 0 )
		{
			OnDeath();
		}
		
		m_pVisual.SetAlpha(m_fCurrentHealth / m_fMaxHealth);
		if( m_fMoveTimer > 0.0f)
		{
			m_fMoveTimer -= dt;
		}
	}
	
	public void MoveItem(Vector3 pos)
	{
		if( m_fMoveTimer > 0.0f)
			return;
		
		TweenPosition.Begin(this.gameObject, m_fMoveBaseTime, pos);
		m_fMoveTimer = m_fMoveBaseTime;
	}
	
	public eImpType GetImpType()
	{
		return m_eType;
	}
	
	public virtual void OnDeath()
	{
		m_bIsDead = true;
		m_pItemDeath(this);
	}
	
	public virtual void OnTeleport()
	{
		
	}
}

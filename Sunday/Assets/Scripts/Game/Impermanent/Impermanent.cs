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
	
	public Visual m_pVisualPrefab;
	
	protected eImpType m_eType;
	
	private Visual m_pVisual;
	private bool m_bIsHealing;
	private bool m_bIsDead = false;
	
	public override void Initialize ()
	{
		base.Initialize ();
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
		
	}
	
	public eImpType GetImpType()
	{
		return m_eType;
	}
	
	public virtual void OnDeath()
	{
		m_bIsDead = true;
	}
	
	public virtual void OnTeleport()
	{
		
	}
}

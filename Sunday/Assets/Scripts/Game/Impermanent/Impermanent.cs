using UnityEngine;
using System.Collections;

public class Impermanent : DoctorObject {
	
	public float m_fDeathPerSecond = 1.0f;
	public float m_fHealPerSecond = 3.0f;
	public float m_fMaxHealth = 100.0f;
	public float m_fCurrentHealth = 50.0f;
	
	public Visual m_pVisual;
	
	private bool m_bIsHealing;
	
	public override void Initialize ()
	{
		base.Initialize ();
	}
	
	public void UpdateObject (float dt, bool bIsHealing)
	{
		base.UpdateObject (dt);
		m_bIsHealing = bIsHealing;
		
		if( m_bIsHealing )
		{
			m_fCurrentHealth = m_fHealPerSecond * dt;
			if( m_fCurrentHealth > m_fMaxHealth )
				m_fCurrentHealth = m_fMaxHealth;
		}
		else
		{
			m_fCurrentHealth = m_fDeathPerSecond * dt;
		}
		
		if( m_fCurrentHealth <= 0 )
		{
			OnDeath();
		}
		
	}
	
	public virtual void OnDeath()
	{
		
	}
}

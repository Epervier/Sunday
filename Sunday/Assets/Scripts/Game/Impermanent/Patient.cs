using UnityEngine;
using System.Collections;

public class Patient : Impermanent {

	public override void Initialize (OnItemDeath pOnDeath, OnTeleported pOnTele)
	{
		base.Initialize (pOnDeath, pOnTele);
		m_eType = Impermanent.eImpType.Patient;
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
	}
	
	public override void OnTeleport()
	{
		m_pTeleported(this);
		m_pVisual.SetAlpha(0);
		m_bIsDead = true;
	}
}

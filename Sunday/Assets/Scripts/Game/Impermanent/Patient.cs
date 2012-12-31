using UnityEngine;
using System.Collections;

public class Patient : Impermanent {

	public override void Initialize ()
	{
		base.Initialize ();
		m_eType = Impermanent.eImpType.Patient;
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
	}
	
	public virtual void OnTeleport()
	{
		
	}
}

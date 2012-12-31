using UnityEngine;
using System.Collections;

public class Teleporter : Impermanent {

	public override void Initialize(OnItemDeath pOnDeath, OnTeleported pOnTele)
	{
		base.Initialize (pOnDeath, pOnTele);
		m_eType = Impermanent.eImpType.Teleporter;
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
	}
}

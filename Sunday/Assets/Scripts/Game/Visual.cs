using UnityEngine;
using System.Collections;

public class Visual : DoctorObject {
	
	public UISprite m_pSprite;
	
	public override void Initialize ()
	{
		base.Initialize ();
	}
	
	public override void UpdateObject (float dt)
	{
		base.UpdateObject (dt);
	}
	
	public void SetAlpha(float fAlpha)
	{
		m_pSprite.alpha = fAlpha;
	}
}

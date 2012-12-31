using UnityEngine;
using System.Collections;

public class MainMenu : MenuBase {
	
	public SimpleClick m_pPlayHit;
	
	public override void Initialize (MenuSystem menuSystem)
	{
		base.Initialize(menuSystem);
		m_pPlayHit.Initialize(PlayClicked);
	}
	
	public void PlayClicked()
	{
		
	}
}

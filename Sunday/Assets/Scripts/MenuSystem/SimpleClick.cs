using UnityEngine;
using System.Collections;

public class SimpleClick : MonoBehaviour {
	
	public delegate void ClickHandler();
	private ClickHandler m_pClick;
	
	public void Initialize(ClickHandler pHandler)
	{
		m_pClick = pHandler;
	}
	
	public void OnClick()
	{
		if( m_pClick != null)
			m_pClick();
	}
}

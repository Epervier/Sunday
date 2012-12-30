using UnityEngine;
using System.Collections;

public class MenuButton : MenuObject {
	
	public MenuDefinitions.ButtonActions m_eAction;
	public MenuDefinitions.Menus	m_eActionTarget;
	
	public int m_nClick = 0;
	public int m_nSubClick = 0;
	public bool m_bSubMenuButton = false;
	
	public delegate void ClickReciever(int nAction, int nClick, int nSubClick);
	protected ClickReciever m_pOnClick;
		
	public virtual void Initialize(ClickReciever pClick)
	{
		m_pOnClick = pClick;
	}
	
	public void OnClick()
	{
		ClickHandler();
	}
	
	public virtual void ClickHandler()
	{
		if( m_pOnClick != null )
		{
			if( m_eAction == MenuDefinitions.ButtonActions.ChangeMenu )
			{
				m_pOnClick( (int)m_eAction, (int)m_eActionTarget, m_nSubClick);
			}
			else
				m_pOnClick((int)m_eAction, m_nClick, m_nSubClick);
		}
	}
	
}

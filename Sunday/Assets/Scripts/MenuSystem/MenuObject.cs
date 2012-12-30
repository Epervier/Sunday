using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour {
	
	#region Transitions
	
	public MenuDefinitions.TransitionTypes 	m_eTransitionInType;
	public MenuDefinitions.Directions		m_eTransitionInDirection;
	public float 							m_fTransitionInDuration = 0.15f;
	public MenuDefinitions.TransitionTypes 	m_eTransitionOutType;
	public MenuDefinitions.Directions		m_eTransitionOutDirection;
	public float 							m_fTransitionOutDuration = 0.25f;
	public delegate void TransitionEnd();
	protected TransitionEnd m_pTransitionEnd;
	
	#endregion
	
	protected bool m_bState = true;
	
	public virtual void SetState(bool bState)
	{
		if( bState != m_bState )
		{
			m_bState = bState;
			NGUITools.SetActive(gameObject, m_bState);
			
			m_pTransitionEnd = null;
		}
	}
	
	public virtual void SetState(bool bState, TransitionEnd pTransition)
	{
		if( pTransition == null)
		{
			SetState(bState);
			return;
		}
		
		if( bState != m_bState )
		{
			m_bState = bState;
			m_pTransitionEnd = pTransition;
			if( m_bState == true )
			{
				NGUITools.SetActive(gameObject, m_bState);
				StartTransition(m_eTransitionInType, m_eTransitionInDirection, m_fTransitionInDuration, true);
			}
			else
			{
				StartTransition(m_eTransitionOutType, m_eTransitionOutDirection, m_fTransitionOutDuration, false);
			}
			
			
		}
	}
	
	public virtual void StartTransition( MenuDefinitions.TransitionTypes eType, MenuDefinitions.Directions eDirection, float fDuration, bool bForwards)
	{
		UITweener tween = null;
		this.gameObject.transform.localScale = new Vector3(1, 1, 1);
		this.gameObject.transform.localPosition = DirectionToPosition(MenuDefinitions.Directions.Center, this.gameObject.transform.localPosition.z);
		
		switch (eType) {
		case  MenuDefinitions.TransitionTypes.Fade:
			tween = TweenAlpha.Begin(this.gameObject, fDuration, 1.0f);
			break;
		case  MenuDefinitions.TransitionTypes.Scale:
		{
			Vector3 targetScale = this.gameObject.transform.localScale;
				
			if( bForwards == true )
			{
				this.gameObject.transform.localScale = new Vector3(0.00001f, 0.00001f, 0.00001f);
				targetScale = new Vector3(1,1,1);
			}
			else
			{
				this.gameObject.transform.localScale = new Vector3(1, 1, 1);
				targetScale = new Vector3(0.00001f, 0.00001f, 0.00001f);
			}
			tween = TweenScale.Begin(this.gameObject, fDuration, targetScale);	
		}
			break;
		case  MenuDefinitions.TransitionTypes.Spin:
			tween = TweenRotation.Begin(this.gameObject, fDuration, Quaternion.FromToRotation(new Vector3(1, -1, 1), new Vector3(0,0,0) ) );
			break;		
		case  MenuDefinitions.TransitionTypes.Translate:
		{
			Vector3 targetPosition = this.gameObject.transform.localPosition;
			
			if( bForwards == true )
			{
				this.gameObject.transform.localPosition = DirectionToPosition(eDirection, targetPosition.z);
				targetPosition = DirectionToPosition(MenuDefinitions.Directions.Center, targetPosition.z);
			}
			else
			{
				this.gameObject.transform.localPosition = DirectionToPosition(MenuDefinitions.Directions.Center, targetPosition.z);
				targetPosition = DirectionToPosition(eDirection, targetPosition.z);
			}
			
			tween = TweenPosition.Begin(this.gameObject, fDuration, targetPosition);
		}
			break;
		default:
		break;
		}
		
		if( tween != null)
		{
			tween.style = UITweener.Style.Once;
			tween.method = UITweener.Method.EaseInOut;
			tween.onFinished = TransitionEndInternal;
		}
	}
	
	protected virtual Vector3 DirectionToPosition(MenuDefinitions.Directions eDirection, float nZ)
	{
		Vector3 result = new Vector3(0,0,nZ);
		switch (eDirection) {
		case MenuDefinitions.Directions.Center:
			break;
		case MenuDefinitions.Directions.Down:
			result = new Vector3(0, -1000, nZ);
			break;
		case MenuDefinitions.Directions.Up:
			result = new Vector3(0, 1000, nZ);
			break;
		case MenuDefinitions.Directions.Right:
			result = new Vector3(2000, 0, nZ);
			break;
		case MenuDefinitions.Directions.Left:
			result = new Vector3(-2000, 0, nZ);
			break;
		default:
		break;
		}
		return result;
	}
	
	protected virtual void TransitionEndInternal(UITweener finished)
	{
		if( m_bState == false)
		{
			NGUITools.SetActive(gameObject, m_bState);
		}
		
		if( m_pTransitionEnd != null)
			m_pTransitionEnd();
		m_pTransitionEnd = null;
		
	}
	
	public virtual void UpdateObject(float dt)
	{
		
	}
}

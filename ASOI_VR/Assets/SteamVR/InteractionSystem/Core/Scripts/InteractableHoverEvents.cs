//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sends UnityEvents for basic hand interactions
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class InteractableHoverEvents : MonoBehaviour
	{
		public UnityEvent onHandHoverBegin;
		public UnityEvent onHandHoverEnd;
		public UnityEvent onAttachedToHand;
		public UnityEvent onDetachedFromHand;

		//-------------------------------------------------
		protected virtual void OnHandHoverBegin()
		{
			onHandHoverBegin.Invoke();
		}


		//-------------------------------------------------
		protected virtual void OnHandHoverEnd()
		{
			onHandHoverEnd.Invoke();
		}


        //-------------------------------------------------
        protected virtual void OnAttachedToHand( Hand hand )
		{
			onAttachedToHand.Invoke();
		}


        //-------------------------------------------------
        protected virtual void OnDetachedFromHand( Hand hand )
		{
			onDetachedFromHand.Invoke();
		}
	}
}

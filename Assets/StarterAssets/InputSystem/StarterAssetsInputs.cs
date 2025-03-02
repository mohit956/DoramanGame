using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			// if(cursorInputForLook)
			// {
				LookInput(value.Get<Vector2>());
			// }
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif

		/// <summary>
		/// Updates the movement input vector.
		/// </summary>
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		/// <summary>
		/// Updates the look input vector.
		/// </summary>
		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		/// <summary>
		/// Updates the jump state.
		/// </summary>
		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		/// <summary>
		/// Updates the sprint state.
		/// </summary>
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		/// <summary>
		/// Updates the look input using drag delta from UIVirtualTouchZone.
		/// This is called when the user is actively dragging.
		/// </summary>
		public void OnTouchLookInput(Vector2 dragDelta)
		{
			// Set the look input based on drag delta
			look = dragDelta;
		}

		/// <summary>
		/// Resets the look input to zero when the user stops dragging.
		/// This is called by UIVirtualTouchZone's OnPointerUp event.
		/// </summary>
		public void ResetLookInput()
		{
			// Reset the look input to zero
			look = Vector2.zero;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}






// using UnityEngine;
// #if ENABLE_INPUT_SYSTEM
// using UnityEngine.InputSystem;
// #endif

// namespace StarterAssets
// {
// 	public class StarterAssetsInputs : MonoBehaviour
// 	{
// 		[Header("Character Input Values")]
// 		public Vector2 move;
// 		public Vector2 look;
// 		public bool jump;
// 		public bool sprint;

// 		[Header("Movement Settings")]
// 		public bool analogMovement;

// 		[Header("Mouse Cursor Settings")]
// 		public bool cursorLocked = true;
// 		public bool cursorInputForLook = true;

// #if ENABLE_INPUT_SYSTEM
// 		public void OnMove(InputValue value)
// 		{
// 			MoveInput(value.Get<Vector2>());
// 		}

// 		public void OnLook(InputValue value)
// 		{
// 			if(cursorInputForLook)
// 			{
// 				LookInput(value.Get<Vector2>());
// 			}
// 		}

// 		public void OnJump(InputValue value)
// 		{
// 			JumpInput(value.isPressed);
// 		}

// 		public void OnSprint(InputValue value)
// 		{
// 			SprintInput(value.isPressed);
// 		}
// #endif


// 		public void MoveInput(Vector2 newMoveDirection)
// 		{
// 			move = newMoveDirection;
// 		} 

// 		public void LookInput(Vector2 newLookDirection)
// 		{
// 			look = newLookDirection;
// 		}

// 		public void JumpInput(bool newJumpState)
// 		{
// 			jump = newJumpState;
// 		}

// 		public void SprintInput(bool newSprintState)
// 		{
// 			sprint = newSprintState;
// 		}

// 		private void OnApplicationFocus(bool hasFocus)
// 		{
// 			SetCursorState(cursorLocked);
// 		}

// 		private void SetCursorState(bool newState)
// 		{
// 			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
// 		}
// 	}
	
// }
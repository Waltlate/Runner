using UnityEngine;

namespace WarriorAnimsFREE
{
	/// <summary>
	/// Placeholder script.  Extract the actual script from the "InputSystem Support - Requires InputSystem Package".
	/// </summary>
	public class WarriorInputSystemController:MonoBehaviour
	{
		// Placeholder inputs.
		[HideInInspector] public bool inputAttack;
		[HideInInspector] public bool inputJump;
		[HideInInspector] public float inputHorizontal = 0;
		[HideInInspector] public float inputVertical = 0;

        //[HideInInspector] public Vector3 moveInput;
        public Vector3 moveInput { get { return CameraRelativeInput(inputHorizontal, inputVertical); } }

        /// <summary>
        /// Movement based off camera facing.
        /// </summary>
        private Vector3 CameraRelativeInput(float inputX, float inputZ)
        {
            // Forward vector relative to the camera along the x-z plane.  
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            // Right vector relative to the camera always orthogonal to the forward vector.
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            Vector3 relativeVelocity = inputHorizontal * right + inputVertical * forward;

            // Reduce input for diagonal movement.
            if (relativeVelocity.magnitude > 1) { relativeVelocity.Normalize(); }

            return relativeVelocity;
        }
    }
}


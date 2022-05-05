using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    public Vector3 Movement => Vector3.up * verticalVelocity;

    float verticalVelocity;

    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            //When on ground (setting the value, wont get bigger or smaller)
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            //When not on ground
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
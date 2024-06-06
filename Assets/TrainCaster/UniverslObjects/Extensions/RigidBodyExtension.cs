using UnityEngine;

public static class RigidBodyExtension
{
    public static Vector3 HorizontalVelocity(this Rigidbody body)
    {
        Vector3 velocity = body.velocity;
        velocity.y = 0f;

        return velocity;
    }
}
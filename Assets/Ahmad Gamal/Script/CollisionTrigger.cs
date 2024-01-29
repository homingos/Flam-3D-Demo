using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public Animator objectBAnimator; // Animator component attached to Object B
    public AnimationClip idleAnimation; // Idle animation clip for Object B
    public AnimationClip collisionAnimation; // Animation clip to play after collision

    private bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.collider.CompareTag("ObjectB"))
        {
            // Trigger the collision animation
            objectBAnimator.Play(collisionAnimation.name);
            hasCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasCollided && collision.collider.CompareTag("ObjectB"))
        {
            // Reset to idle animation when the collision ends
            objectBAnimator.Play(idleAnimation.name);
            hasCollided = false;
        }
    }
}

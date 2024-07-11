using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    protected Entity character;
    protected Vector3 moveDirection;
    protected Animator animator;
    protected bool isRunning = false;
    protected float runMultiplier = 2f;

    public virtual void StartRunning()
    {
        isRunning = true;
        character.SetSpeed(character.GetSpeed() * runMultiplier);
    }
    public virtual void StopRunning()
    {
        isRunning = false;
        character.SetSpeed(character.GetSpeed() / runMultiplier);
    }
}

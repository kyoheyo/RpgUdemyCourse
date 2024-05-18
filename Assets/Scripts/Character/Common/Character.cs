using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(FlipSprite))]
[RequireComponent(typeof(CollisionDetection))]
public class Character : MonoBehaviour
{
    [Header("Attack Colider")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Knockback Value")]
    public float knockbackXSpeed = 3f;
    public float knockbackYSpeed = 3f;
    public GameObject damageFrom;

    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public FlipSprite Flip { get; private set; }
    public CollisionDetection ColDetect { get; private set; }

    public FSM Fsm { get; private set; }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Flip = GetComponent<FlipSprite>();
        ColDetect = GetComponent<CollisionDetection>();

        Fsm = new FSM();
    }

    protected virtual void Update()
    {
        Fsm.CurrentState?.Update();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour, ISavable
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody2D rb;

    public event Action<Collider2D> OnEnterEnemyView;

    private Vector2 movement;

    private Animator animator;

    [SerializeField] private GameLayers layers;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }

        movement.Set(InputManager.movement.x, InputManager.movement.y);
        

        rb.velocity = movement * playerSpeed;

        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat (vertical, movement.y);

        if(movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
        }

        
        OnMove();
    }

    public void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat(horizontal), animator.GetFloat(vertical));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, layers.InteractableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    public void OnMove()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, GameLayers.i.TriggerableLayers);

        foreach(var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if(triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }

    private void CheckIfInEnemyView()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.FovLayer);
        if (collider != null)
        {
            Debug.Log("See the player");
            OnEnterEnemyView?.Invoke(collider);
        }
    }

    public object CaptureState()
    {
        float[] position = new float[] {transform.position.x, transform.position.y};
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);
    }
}

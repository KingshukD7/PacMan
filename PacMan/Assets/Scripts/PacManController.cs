using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 3.0f; 
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 lastInput = Vector3.zero;
    public ParticleSystem dustParticleSystem;

    private void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
        else
        {
            MoveToTargetPosition();
        }
    }

    private void PlayDustEffect()
    {
        dustParticleSystem.Play();
    }
    private void HandleInput()
    {
        
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            inputDirection = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            inputDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            inputDirection = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            inputDirection = Vector3.right;
        }

        
        if (inputDirection != Vector3.zero)
        {
            lastInput = inputDirection;
            TurnPacStudent(lastInput);
        }

        
        TryMove(lastInput);
    }
    private void TurnPacStudent(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void TryMove(Vector3 direction)
    {
        
        targetPosition = transform.position + direction;
        isMoving = true;
        PlayDustEffect();
    }

    private void MoveToTargetPosition()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}

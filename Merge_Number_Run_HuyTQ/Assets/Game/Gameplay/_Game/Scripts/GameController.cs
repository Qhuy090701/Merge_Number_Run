using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float speedTouch = 5f;
    [SerializeField] private float swipeThreshold = 20f;

    private PlayerControllerState currentState;
    private PlayerController playerController;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private enum PlayerControllerState
    {
        StartGame,
        RunGame,
        EndGame
    }

    private void Start()
    {
        //transform.position = startPoint.position;
        currentState = PlayerControllerState.StartGame;
        //getcomponent player controller 
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        switch (currentState)
        {
            case PlayerControllerState.StartGame:
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = PlayerControllerState.RunGame;
                }
                break;
            case PlayerControllerState.RunGame:
                TouchMove();
                Move();
                break;
            case PlayerControllerState.EndGame:
                break;
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * speedTouch * Time.deltaTime;
    }

    private void TouchMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            float distance = Vector3.Distance(startPosition, endPosition);

            if (distance > swipeThreshold)
            {
                Vector3 direction = endPosition - startPosition;

                if (direction.x > 0)
                {
                    isMovingRight = true;
                    isMovingLeft = false;
                }
                else
                {
                    isMovingLeft = true;
                    isMovingRight = false;
                }
            }
            else
            {
                isMovingLeft = false;
                isMovingRight = false;
            }
        }

        if (isMovingLeft)
        {
            transform.position += Vector3.left * speedTouch * Time.deltaTime;
        }
        else if (isMovingRight)
        {
            transform.position += Vector3.right * speedTouch * Time.deltaTime;
        }
    }
}

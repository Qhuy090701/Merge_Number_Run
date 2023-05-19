﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject createNumber0;
    [SerializeField] private PortalState portalState;   
    private PlayerRun playerrun;

    private void Start()
    {
        playerrun = GetComponent<PlayerRun>();
    }
    private enum PortalState
    {
        LevelUp,
        LevelDown,
        SpeedBulletDown,
        SpeedBulletUp,
        CreateNumber,
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Debug.Log("chay vao trigger");
            PlayerRun player = other.GetComponent<PlayerRun>();
            switch (portalState)
            {
                case PortalState.LevelUp:
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    other.gameObject.tag = Constant.TAG_PLAYER;
                    player.LevelUpNumber();
                    Debug.Log("chay vao switch casse");
                    break;
                case PortalState.LevelDown:
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    other.gameObject.tag = Constant.TAG_PLAYER;
                    player.LevelDownNumber();
                    Debug.Log("Level Down");
                    break;
                case PortalState.SpeedBulletUp:
                    Destroy(gameObject);
                    other.gameObject.tag = Constant.TAG_PLAYER;
                    player.SpeedBulletUp();
                    Debug.Log("Speed Bullet Up");
                    break;
                case PortalState.SpeedBulletDown:
                    Destroy(gameObject);
                    other.gameObject.tag = Constant.TAG_PLAYER;
                   //player.SpeedBulletDown();
                    Debug.Log("Speed Bullet Down");
                    break;
                case PortalState.CreateNumber:
                    Destroy(gameObject);
                    GameObject number = Instantiate(createNumber0, gameObject.transform.position, Quaternion.identity);
                    number.transform.SetParent(playerrun.parent.transform);
                    Debug.Log("Create Number");
                    break;
            }
        }
    }

}
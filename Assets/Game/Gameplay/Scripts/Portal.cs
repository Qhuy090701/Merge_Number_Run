﻿using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject createNumber0;
    [SerializeField] private PortalState portalState;
    private PlayerRun playerRun;
    private RunningGame runningGame;

    [SerializeField] private TextMesh textName;

    private void Start()
    {
        runningGame = FindObjectOfType<RunningGame>();
        playerRun = GetComponent<PlayerRun>();
        textName.text = portalState.ToString();
    }

    private enum PortalState
    {
        LevelUp,
        LevelDown,
        SpeedBulletDown,
        SpeedBulletUp,
        CreateBullet
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            PlayerRun player = other.GetComponent<PlayerRun>();
            if (player != null)
            {
                switch (portalState)
                {
                    case PortalState.LevelUp:
                        Destroy(other.gameObject);
                        Destroy(gameObject);
                        other.gameObject.tag = Constant.TAG_PLAYER;
                        player.LevelUpNumber();
                        break;
                    case PortalState.LevelDown:
                        Destroy(other.gameObject);
                        Destroy(gameObject);
                        other.gameObject.tag = Constant.TAG_PLAYER;
                        player.LevelDownNumber();
                        break;
                    case PortalState.SpeedBulletUp:
                        Destroy(gameObject);
                        other.gameObject.tag = Constant.TAG_PLAYER;
                        player.SpeedBulletUp();
                        break;
                    case PortalState.SpeedBulletDown:
                        Destroy(gameObject);
                        other.gameObject.tag = Constant.TAG_PLAYER;
                        player.SpeedBulletDown();
                        break;
                    case PortalState.CreateBullet:
                        Destroy(gameObject);
                        runningGame.shootType = true;
                        break;
                }
            }
        }
    }
}

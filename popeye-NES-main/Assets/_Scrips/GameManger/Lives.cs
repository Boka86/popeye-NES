using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lives : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image liveImage;
    [SerializeField] private Image heartImage;
    [SerializeField] private Sprite[] liveSprite;
    [SerializeField] private Sprite[] heartsCollected;
    [SerializeField] private TextMeshProUGUI WinLevelText;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int playerLives)
    {
        switch (playerLives)
        {
            case 3:
                liveImage.sprite = liveSprite[3];
                break;
            case 2:
                liveImage.sprite = liveSprite[2];
                break;
                    case 1:
                liveImage.sprite = liveSprite[1];
                break;
            case 0:
                liveImage.sprite = liveSprite[0];
                break;
            default:
                break;
        }

    }

    public void UpdateHeartCollected(int playerHearts)
    {
        switch (playerHearts)
        {
            case 0:
                heartImage.sprite = heartsCollected[0];
                break;
            case 1:
                heartImage.sprite = heartsCollected[1];
              
                break;
            case 2:
                heartImage.sprite = heartsCollected[2];
                break;
            case 3:
                heartImage.sprite = heartsCollected[3];
                break;
            case 4:
                heartImage.sprite = heartsCollected[4];
                break;
            case 5:
                heartImage.sprite = heartsCollected[5];
                break;
            case 6:
                heartImage.sprite = heartsCollected[6];
                break;
            case 7:
                heartImage.sprite = heartsCollected[7];
                break;
            case 8:
                heartImage.sprite = heartsCollected[8];
                break;
            case 9:
                heartImage.sprite = heartsCollected[9];
                break;
            case 10:
                heartImage.sprite = heartsCollected[10];
                break;
            case 11:
                heartImage.sprite = heartsCollected[11];
                break;
            case 12:
                heartImage.sprite = heartsCollected[12];
                break;
            case 13:
                heartImage.sprite = heartsCollected[13];
                break;
            case 14:
                heartImage.sprite = heartsCollected[14];
                break;
            case 15:
                heartImage.sprite = heartsCollected[15];
                break;
            case 16:
                heartImage.sprite = heartsCollected[16];
                WinLevelText.gameObject.SetActive(true);


                break;
            default:
                break;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJolt;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ratingManager : MonoBehaviour
{
    private Controls controls;
    public GameManager gm;

    [Header("Score GUI")]
    public float rating;
    public float score;
    public float HighScore;
    public float HighScoreRaw;
    public TextMeshProUGUI HS;
    public TextMeshProUGUI S;
    public bool signedIn;
    [Header("Slots GUI")]
    public Sprite[] portraits;
    public KarenStatusSlot[] slots;
    public Sprite emptyPortrait;
    public Sprite robberPortrait;
    [Header("Quota")]
    public int quotaMax;
    public int quotaMin;
    public int quota;
    public int quotaHave;
    public float quotaTimeMax;
    public float quotaTimeMin;
    public float quotaTime;
    public TextMeshProUGUI quotaTimer;
    public TextMeshProUGUI quotaItemsHave;
    public TextMeshProUGUI quotaItemsNeeded;
    [Header("Gameover Texts")]
    public bool isGameOver;
    public GameObject notice;
    public TextMeshProUGUI gameoverText;
    [TextArea]
    public string ratingGameover;
    [TextArea]
    public string robberGameover;
    [TextArea]
    public string quotaGameover;
    [Header("Pausing")]
    public GameObject pauseMenu;
    [Header("GJ")]
    public int KarensServed;
    public int TimesFired;
    public BasicGJTrophy[] karensServedTrophys;
    public BasicGJTrophy[] timesFiredTrophys;

    void Awake()
    {
        controls = new Controls();

        controls.Player.Pause.performed += _ => Pause();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        quotaMax = gm.currentStage.maxQuota;
        quotaMin = gm.currentStage.minQuota;
        quotaTimeMax = gm.currentStage.maxQuotaTime;
        quotaTimeMin = gm.currentStage.minQuotaTime;

        if (GameJolt.API.GameJoltAPI.Instance.HasSignedInUser)
            signedIn = true;

        if (signedIn)
        {
            string key = "HIGHSCORE";
            bool isGlobal = false;
            GameJolt.API.DataStore.Get(key, isGlobal, (string value) => {
                if (value != null)
                {
                    HighScore = float.Parse(value);
                    Debug.Log(key + " " + value);
                }
            });

            string key1 = "KARENS_SERVED";
            bool isGlobal1 = false;
            GameJolt.API.DataStore.Get(key1, isGlobal1, (string value) => {
                if (value != null)
                {
                    KarensServed = int.Parse(value);
                    Debug.Log(key1 + " " + value);
                }
            });

            string key2 = "TIMES_FIRED";
            bool isGlobal2 = false;
            GameJolt.API.DataStore.Get(key2, isGlobal2, (string value) => {
                if (value != null)
                {
                    TimesFired = int.Parse(value);
                    Debug.Log(key2 + " " + value);
                }
            });
        } else
        {
            if (PlayerPrefs.HasKey("HIGHSCORE"))
            {
                HighScore = PlayerPrefs.GetFloat("HIGHSCORE");
                HighScoreRaw = PlayerPrefs.GetFloat("HIGHSCORE");
            }
        }      
    }

    void Update()
    {
        CalculateScore();
        CalculateQuota(Time.deltaTime);
    }

    public void ChangeRating(bool correct, Item itemData)
    {
        int toChange = 0;

        for (int i = 0; i < itemData.scene.Length; i++)
        {
            if (itemData.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                if (correct)
                {
                    toChange = itemData.scene[i].score;
                } else
                {
                    toChange = itemData.scene[i].incorrectScore * -1;
                }
            }
        }

        rating += toChange;
    }

    public void ChangeScore(bool correct, Item itemData)
    {
        int toChange = 0;

        for (int i = 0; i < itemData.scene.Length; i++)
        {
            if (itemData.scene[i].sceneName == SceneManager.GetActiveScene().name)
            {
                if (correct)
                {
                    toChange = itemData.scene[i].score * 100;
                }
                else
                {
                    toChange = itemData.scene[i].incorrectScore * -100;
                }
            }
        }

        score += toChange;
    }
    
    public void ChangeScore(int toChange)
    {
        toChange *= 50;

        score += toChange;
    }

    void CalculateScore()
    {
        if (rating >= 10)
        {
            rating = 10;
        }

        if (rating <= 0)
        {
            GameOver(0);
        }

        if (HighScoreRaw < score)
        {
            HighScore = score;
        }
        else
        {
            HighScore = HighScoreRaw;
        }

        HS.text = HighScore.ToString();
        S.text = score.ToString();
    }

    void CalculateQuota(float delta)
    {
        quotaTime -= delta;
        quotaTimer.text = Mathf.Floor(quotaTime).ToString();

        quotaItemsHave.text = quotaHave.ToString();

        if (quotaTime <= 0)
        {
            if (quotaHave >= quota)
            {
                if (quotaHave > quota)
                {
                    int score = quotaHave - quota;
                    ChangeScore(score);
                }

                GetNewQuota();
            } else
            {
                if (!isGameOver)
                    GameOver(2);
            }
        }
    }

    void GetNewQuota()
    {
        quotaTime = Random.Range(quotaTimeMin, quotaTimeMax);
        quotaTimer.text = Mathf.Floor(quotaTime).ToString();
        quota = Random.Range(quotaMin, quotaMax);
        quotaItemsNeeded.text = quota.ToString();
        quotaHave = 0;
    }

    public void ChangeQuota()
    {
        quotaHave++;
    }

    public void GameOver(int reason)
    {
        if (isGameOver)
            return;

        isGameOver = true;

        notice.SetActive(true);

        string reasonForGameover = "[NO ENTRY FOR SPECIFIED GAMEOVER ID!]";

        switch (reason)
        {
            case 0:
                reasonForGameover = ratingGameover;
                break;
            case 1:
                reasonForGameover = robberGameover;
                break;
            case 2:
                reasonForGameover = quotaGameover;
                break;
        }

        gameoverText.text = reasonForGameover;

        if (!signedIn)
        {
            if (PlayerPrefs.HasKey("HIGHSCORE"))
            {
                if (HighScore > PlayerPrefs.GetFloat("HIGHSCORE"))
                {
                    PlayerPrefs.SetFloat("HIGHSCORE", HighScore);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("HIGHSCORE", HighScore);
            }
        } else
        {
            string scoreText = $"{HighScoreRaw} scored!";
            int tableID = 629035;
            string extraData = "";
            GameJolt.API.Scores.Add((int)HighScoreRaw, scoreText, tableID, extraData, (bool success) => {
                Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            });

            string key = "HIGHSCORE";
            string value = HighScore.ToString();
            bool isGlobal = false;
            GameJolt.API.DataStore.Set(key, value, isGlobal, (bool success) => { });

            string key1 = "TIMES_FIRED";
            TimesFired++;
            int value1 = TimesFired;
            bool isGlobal1 = false;
            GameJolt.API.DataStore.Set(key1, value1.ToString(), isGlobal1, (bool success) => { });

            string key2 = "KARENS_SERVED";
            string value2 = KarensServed.ToString();
            bool isGlobal2 = false;
            GameJolt.API.DataStore.Set(key2, value2, isGlobal2, (bool success) => { });

            AwardGJTrophy();
        }

        GameObject player = GameObject.Find("Player");
        Destroy(player);
    }

    void AwardGJTrophy()
    {
        for (int i = 0; i < karensServedTrophys.Length; i++)
        {
            if (KarensServed >= karensServedTrophys[i].requirement)
            {
                GameJolt.API.Trophies.Unlock(karensServedTrophys[i].id, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success!");
                    }
                    else
                    {
                        Debug.Log("Something went wrong");
                    }
                });
            }
        }

        for (int i = 0; i < timesFiredTrophys.Length; i++)
        {
            if (KarensServed >= timesFiredTrophys[i].requirement)
            {
                GameJolt.API.Trophies.Unlock(timesFiredTrophys[i].id, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success!");
                    }
                    else
                    {
                        Debug.Log("Something went wrong");
                    }
                });
            }
        }
    }

    public void RemoveUISpot(bool isBadReason, int karenId)
    {
        slots[karenId].portrait.sprite = emptyPortrait;
        slots[karenId].itemName.text = "-";
        slots[karenId].timer.text = "-";
    }

    public void RegisterUISpot(int karenId, int spriteId)
    {
        slots[karenId].portrait.sprite = portraits[spriteId];
    }

    public void RegisterUISpot(int karenId)
    {
        slots[karenId].portrait.sprite = robberPortrait;
    }

    public void Pause()
    {
        if (isGameOver)
            return;

        Time.timeScale = 0;

        GameObject.Find("Player").GetComponent<ItemManager>().paused = true;

        pauseMenu.SetActive(true);
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}

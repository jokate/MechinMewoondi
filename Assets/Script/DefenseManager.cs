using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefenseManager : MonoBehaviour
{
    public GameObject winFactor, loseFactor, Spawnin, SpawnPoint, enemy, UpGradePanel;
    private float Bosshealth;
    public AudioSource BGM;
    public Button x1, x2, x4;
    public static int Money;
    public List<GameObject> Smalls, Mediums, Larges;
    public List<EnemyStat> enemyStats;
    public static float SmallUpgrade, MedUpgrade, LargeUpgrade;
    public static float UpgradeParts;
    public GameObject Boss, BossHealth;
    public Image BossHealthImg;
    public GameObject obj;
    public static bool isClear = false;

    public Button GatchaButton;
    
    public static int Level = 0;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI GatchaText, RoundText, UpGradeText, PopulationText;
    
   
    public void Start()
    {
        Bosshealth = enemyStats[19].health;
        BGM.Play();
        isClear = false;
        Time.timeScale = 1f;
        UpgradeParts = 0.0f;
        SmallUpgrade = 1;
        MedUpgrade = 1;
        LargeUpgrade = 1;
        Money = 40;
        StartCoroutine(SpawnEnemy());
    }

    public void Update()
    {
        coinText.text = "���� ���� : " + Money.ToString();
        PopulationText.text = "���� ��ü�� : " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpGradeText.text = "���� ���� : " + ((int)UpgradeParts).ToString(); 
        

        if(Money >= 10)
        {
            GatchaButton.enabled = true;
        }
        else
        {
            GatchaButton.enabled = false;
        }
        if(GameObject.FindGameObjectsWithTag("Enemy").Length >= 150)
        {
            x1.enabled = false;
            x2.enabled = false;
            x4.enabled = false;
            Time.timeScale = 0f;
            loseFactor.SetActive(true);
            
        }   

        if(isClear)
        {
            x1.enabled = false;
            x2.enabled = false;
            x4.enabled = false;
            BossHealth.SetActive(false);
            Time.timeScale = 0;
           
            winFactor.SetActive(true);

        }
    }


    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float time = 5.0f;

            RoundText.enabled = true;
            while(time > 0.0f)
            {
                RoundText.text = "���� " + (Level + 1).ToString() + "�� " + ((int)time + 1).ToString() + "�� �ڿ� ���۵˴ϴ�.";
                time -= Time.deltaTime;
                yield return null;
            }
            RoundText.enabled = false;

           
            enemy.GetComponent<Enemy>().enemyStat = enemyStats[Level];
            
            if (Level != 19)
            {
                for (int i = 0; i < 70; i++)
                {
                    Instantiate(enemy, Spawnin.transform);
                    yield return new WaitForSeconds(0.5f);
                }
                Level++;
              
            }
            else
            {
                Boss = Instantiate(enemy, Spawnin.transform);
                
                StartCoroutine(Countdown());
                
                break;
               
            }


        }
    }

    public IEnumerator Countdown()
    {
        float number = 300f;
        RoundText.enabled = true;
        BossHealth.SetActive(true);
        while(number > 0.0f)
        {
            RoundText.text = "���� �ð� : " + (int)(number / 60) + "�� " + (int)(number % 60) + "��";
            number -= Time.deltaTime;
            if(Boss != null) 
                BossHealthImg.fillAmount = Boss.GetComponent<Enemy>().health / Bosshealth;
            if(Boss == null)
            {
                isClear = true;
            }
            yield return null;
        }

        RoundText.enabled = false;
        loseFactor.SetActive(true);

    }

    public static void addMoney(int money)
    {
        Money += money;
    }

    public void TimeSet2()
    {
        Time.timeScale = 2f;
    }
    public void TimeSet4()
    {
        Time.timeScale = 4f;
    }
    public void TimeOrigin()
    {
        Time.timeScale = 1f;
    }
    
    public void Gatcha()
    {
        
        int tempMon = Money - 10;
        if(tempMon < 0)
        {
            Money = 0;
        } else
        {
            Money = tempMon;
        }

        int number = Random.Range(0, 3);
        float percent = Random.Range(0.0f, 1.0001f);


        Debug.Log(percent);
        switch(number)
        {
            case 0:
                if(0 <= percent && percent < 0.5f)
                {
                    Instantiate(Smalls[0], SpawnPoint.transform);
                    GatchaText.text = "���� �Ϲ� (50 %)";
                    GatchaText.color = Color.white;
                    
                } else if(0.5f <= percent && percent < 0.83f)
                {
                    Instantiate(Smalls[1], SpawnPoint.transform);
                    GatchaText.text = "���� ���� (33 %)";
                    GatchaText.color = Color.blue;
                } else if(0.83f <= percent && percent < 0.98f)
                {
                    Instantiate(Smalls[2], SpawnPoint.transform);
                    GatchaText.text = "���� ����ũ (15 %)";
                    GatchaText.color = Color.yellow;
                }
                else if(0.98f <= percent && percent <= 0.995f)
                {
                    GatchaText.text = "���� �������� (1.5 %)";
                    GatchaText.color = Color.green;
                    Instantiate(Smalls[3], SpawnPoint.transform);
                } else
                {
                    GatchaText.text = "���� �ʿ� (0.5 %)";
                    GatchaText.color = Color.red;
                    Instantiate(Smalls[4], SpawnPoint.transform);
                }
                break;

            case 1:
                if (0 <= percent && percent < 0.5f)
                {
                    Instantiate(Mediums[0], SpawnPoint.transform);
                    GatchaText.text = "���� �Ϲ� (50 %)";
                    GatchaText.color = Color.white;
                }
                else if (0.5f <= percent && percent < 0.83f)
                {
                    Instantiate(Mediums[1], SpawnPoint.transform);
                    GatchaText.text = "���� ���� (33 %)";
                    GatchaText.color = Color.blue;
                }
                else if (0.83f <= percent && percent <= 0.98f)
                {
                    Instantiate(Mediums[2], SpawnPoint.transform);
                    GatchaText.text = "���� ����ũ (15 %)"; 
                    GatchaText.color = Color.yellow;
                }
                else if (0.98f <= percent && percent <= 0.995f)
                {
                    Instantiate(Mediums[3], SpawnPoint.transform);
                    GatchaText.text = "���� �������� (1.5 %)";
                    GatchaText.color = Color.green;
                }
                else
                {
                    GatchaText.text = "���� �ʿ� (0.5 %)";
                    GatchaText.color = Color.red;
                    Instantiate(Mediums[4], SpawnPoint.transform);
                }
                break;
            case 2:
                if (0 <= percent && percent < 0.5f)
                {
                    Instantiate(Larges[0], SpawnPoint.transform);
                    GatchaText.text = "���� �Ϲ� (50 %)";
                    GatchaText.color = Color.white;
                }
                else if (0.5f <= percent && percent < 0.83f)
                {
                    Instantiate(Larges[1], SpawnPoint.transform);
                    GatchaText.text = "���� ���� (33 %)";
                     GatchaText.color = Color.blue;
                }
                else if (0.83f <= percent && percent <= 0.98f)
                {
                    Instantiate(Larges[2], SpawnPoint.transform);
                    GatchaText.text = "���� ����ũ (15 %)";
                    GatchaText.color = Color.yellow;
                }
                else if (0.98f <= percent && percent <= 0.995f)
                {
                    Instantiate(Larges[3], SpawnPoint.transform);
                    GatchaText.text = "���� �������� (1.5 %)";
                    GatchaText.color = Color.green;
                }
                else
                {
                    Instantiate(Larges[4], SpawnPoint.transform);
                    GatchaText.text = "���� �ʿ� (0.5 %)"; 
                    GatchaText.color = Color.red;
                }
                break;

 
        }
        Instantiate(GatchaText, obj.transform);


    }
    public void Restart()
    {
        x1.enabled = true;
        x2.enabled = true;
        x4.enabled = true;
        loseFactor.SetActive(false);
        winFactor.SetActive(false);
        Time.timeScale = 1.0f;
        Level = 0;
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void UpGrade(int parts)
    {

        float tempMon = UpgradeParts - 2;
        if(UpgradeParts < 2f)
        {
            return;
        }
        UpgradeParts = tempMon;
        if (parts == 1)
        {
            SmallUpgrade += 0.25f;
        } else if (parts == 2)
        {
            MedUpgrade += 0.25f;
        } else
        {
            LargeUpgrade += 0.25f;
        }
    }

    public void Open()
    {
        UpGradePanel.SetActive(true);
    }
    public void Close()
    {
        UpGradePanel.SetActive(false);
    }
}

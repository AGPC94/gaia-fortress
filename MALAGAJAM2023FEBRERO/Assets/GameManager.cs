using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float treeProgress;
    public float recoursePoints;
    public bool isOver;
    public GameObject allySelected;

    [Header("SpawnEnemies")]

    [Header("Enemies")]
    public GameObject drill;
    public GameObject claws;
    public GameObject tank;
    public GameObject shovel;
    public GameObject spider;

    [Header("Positions")]
    float yMin = -3.5f;
    float yMax = 2;
    float xLeft = -10f;
    float xRight = 10f;

    [Header("Up Points")]
    public float pointsRecover;
    public float timePoints;

    [Header("Points")]
    [SerializeField] Text txtPoints;

    [Header("Health")]
    [SerializeField] float health;

    [Header("Tree")]
    [SerializeField] Image imgtree;
    [SerializeField]  Sprite[] treeSprites;

    [Header("gAME OVER")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] float timeToMainMenu;

    [SerializeField] GameObject transition;

    public float Health { get => health; set => health = value; }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpPoints());
        StartCoroutine(SpawnAllEnemies());

        AudioManager.instance.PlayMusic("Ambiente");
    }

    // Update is called once per frame
    void Update()
    {
        //imgPoints.sprite = sprPoints;
        txtPoints.text = recoursePoints.ToString();
;

        if (!isOver)
        {
            if (health <= 0)
            {
                isOver = true;
                panelGameOver.SetActive(true);
                Invoke("BackToMainMenu", timeToMainMenu);
                Debug.Log("Has perdido");
            }
            else
            {

                if (treeProgress >= 0 && treeProgress < 25)
                    imgtree.sprite = treeSprites[0];

                if (treeProgress >= 25 && treeProgress < 50)
                    imgtree.sprite = treeSprites[1];

                if (treeProgress >= 50 && treeProgress < 75)
                    imgtree.sprite = treeSprites[2];

                if (treeProgress >= 75 && treeProgress < 100)
                    imgtree.sprite = treeSprites[3];

                if (treeProgress >= 100)
                {
                    isOver = true;
                    Debug.Log("Has ganado!");
                    Invoke("GoToVictory", 1);
                    transition.SetActive(true);
                    transition.GetComponent<Animator>().SetTrigger("Transition");
                }
            }

        }
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToVictory()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Victory");
    }

    IEnumerator UpPoints()
    {
        while (!isOver)
        {
            yield return new WaitForSeconds(timePoints);
            recoursePoints += pointsRecover;
            Debug.Log("UpPoints()");
        }
    }

    IEnumerator SpawnAllEnemies()
    {
        yield return new WaitForSeconds(claws.GetComponent<Enemy>().TimeStart);
        StartCoroutine(SpawnEnemy(claws, claws.GetComponent<Enemy>().TimeSpawn));

        yield return new WaitForSeconds(drill.GetComponent<Enemy>().TimeStart);
        StartCoroutine(SpawnEnemy(drill, drill.GetComponent<Enemy>().TimeSpawn));

        yield return new WaitForSeconds(spider.GetComponent<Enemy>().TimeStart);
        StartCoroutine(SpawnEnemy(spider, spider.GetComponent<Enemy>().TimeSpawn));

        yield return new WaitForSeconds(shovel.GetComponent<Enemy>().TimeStart);
        StartCoroutine(SpawnEnemy(shovel, shovel.GetComponent<Enemy>().TimeSpawn));

        yield return new WaitForSeconds(tank.GetComponent<Enemy>().TimeStart);
        StartCoroutine(SpawnEnemy(tank, tank.GetComponent<Enemy>().TimeSpawn));
    }

    IEnumerator SpawnEnemy(GameObject goEnemy, float time)
    {
        while (!isOver)
        {
            int rndX = Random.Range(1, 3);
            float spawnX = 0;
            float spawnY = 0;

            if (rndX == 1)
                spawnX = xLeft;
            else if (rndX == 2)
                spawnX = xRight;

            spawnY = Random.Range(1, -3);

            Vector2 v2 = new Vector2(spawnX, spawnY);

            Instantiate(goEnemy, v2, Quaternion.identity);

            yield return new WaitForSeconds(time);
        }
    }

}

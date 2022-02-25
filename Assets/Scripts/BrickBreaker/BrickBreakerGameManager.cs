using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BrickBreakerGameManager : MonoBehaviour
{
    public static BrickBreakerGameManager Inst;
    public int score = 0;
    private int playeTeamMatch = 0;
    private Coroutine currentOpeningCoroutine;
    private float elapsed = 0;
    Hashtable bricks_collection;


    public float horizontalMin = -7.5f;
    public float horizontalMax = 7.5f;
    public float verticalMin = -3f;
    public float verticalMax = 3f;
    public float generate_interval = 1f;

    public GameObject brickPrefab;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Tooltip("The prefab to use for initiating the player")]
    public GameObject teamOnePrefab;
    public GameObject teamTwoPrefab;


    public IEnumerator StartGameCoroutine(GameObject hockey = null)
    {
        AudioManager.Instance.PlayOpeningAudio(Vector3.zero);
        if (hockey)
        {
            yield return new WaitForSeconds(0.5f);
            hockey.transform.position = Vector3.zero;
            hockey.SetActive(true);
        }
    }

    public void StartGame(GameObject hockey = null)
    {
        if (currentOpeningCoroutine != null)
        {
            StopCoroutine(currentOpeningCoroutine);
        }

        currentOpeningCoroutine = StartCoroutine(StartGameCoroutine(hockey));
    }


    private void Start()
    {
        if (teamOnePrefab == null || teamTwoPrefab == null)
        {
            Debug.LogError(
                "<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",
                this
            );
            return;
        }
        bricks_collection = new Hashtable();

    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= generate_interval)
        {
            elapsed = elapsed % generate_interval;
            GenerateBrick();
        }
    }

    private void GenerateBrick()
    {
        // very ugly, remember to change.
        int hor_pos_int = Random.Range((int)Mathf.Floor(horizontalMin), (int)Mathf.Floor(horizontalMax) + 1);
        float hor_pos = hor_pos_int + 0.5f;
        int ver_pos = Random.Range((int)Mathf.Floor(verticalMin), (int)Mathf.Floor(verticalMax) + 1);
        Vector2 pos = new Vector2((float)hor_pos, ver_pos);

        if (bricks_collection[pos.ToString()] == null)
        {
            GameObject new_brick = Instantiate(brickPrefab, pos, Quaternion.identity);
            bricks_collection[pos.ToString()] = new_brick;
        }
        else
        {
            GameObject current_brick = (GameObject)bricks_collection[pos.ToString()];
            if(current_brick == null)
            {
                bricks_collection.Remove(pos.ToString());
                return;
            }
            if (current_brick.GetComponent<BrickBreakerBrickControler>())
            {
                current_brick.GetComponent<BrickBreakerBrickControler>().ChangeNum(1);
            }
        }


    }
}
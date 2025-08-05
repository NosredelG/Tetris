using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int score = 0;
    [SerializeField] int linesDeleted;
    [SerializeField] int difficulty = 1;
    private static int height = 20;
    private static int width = 10;
    [SerializeField] static Transform[,] grid = new Transform[width, height];
    private int[] indexLine = new int[4];
    [SerializeField] bool isPaused;
    [SerializeField] GameObject pauseGO;
    [SerializeField] bool isGameOver;
    [SerializeField] GameObject gameOverGO;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI poinsText;
    [SerializeField] TextMeshProUGUI difficultyText;

    public static GameControl instance;

    public float Speed { get { return speed; } }
    public int Difficulty { get { return difficulty; }}
    public bool IsGameOver { get { return isGameOver; }}
    public bool IsPaused { get { return isPaused; }}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 4; i++)
        {
            indexLine[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = UpdateScore();
        difficultyText.text = difficulty.ToString();
    }

    public bool InsideGrid(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x < width && (int)position.y >= 0);
    }

    public Vector2 Round(Vector2 nA)
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }

    public Transform TransformGridPosition(Vector2 position)
    {
        if (position.y > height - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)position.x, (int)position.y];
        }
    }

    public void UpdateGrid(PieceMove pieceTetris)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == pieceTetris.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform piece in pieceTetris.transform)
        {
            Vector2 position = Round(piece.position);

            if (position.y < height)
            {
                grid[(int)position.x, (int)position.y] = piece;
            }
        }
    }

    public bool FullLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteSquare(int y)
    {
        AudioController.instance.PlayClip(AudioController.instance.ClipFullLine);
        for (int x = 0; x < width; x++)
        {
            grid[x, y].GetComponent<SpriteRenderer>().enabled = false;
            grid[x, y].GetComponentInChildren<ParticleSystem>().Play();

            Destroy(grid[x, y].gameObject, 0.7f);

            grid[x, y] = null;
        }
    }

    public void MoveLineDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllLinesDown(int y)
    {
        for (int i = y; i < height; i++)
        {
            MoveLineDown(i);
        }
        AudioController.instance.PlayClip(AudioController.instance.ClipPieceStop);
    }

    public void DeleteLine()
    {
        StartCoroutine(WaitingTime());
    }

    public void PauseGame()
    {
        AudioController.instance.PlayClip(AudioController.instance.ClipPause);
        if (isPaused)
        {
            AudioController.instance.PlayMusic();
            Time.timeScale = 1.0f;
            pauseGO.SetActive(false);
            isPaused = false;
        }
        else
        {
            AudioController.instance.StopMusic();
            Time.timeScale = 0;
            pauseGO.SetActive(true);
            isPaused = true;
        }
    }

    public bool OverGrid(PieceMove piece)
    {
        for (int x = 0; x < width; x++)
        {
            foreach (Transform square in piece.transform)
            {
                Vector2 position = Round(square.position);

                if (position.y > height - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void GameOver()
    {
        AudioController.instance.StopMusic();
        AudioController.instance.PlayClip(AudioController.instance.ClipGameOver);
        gameOverGO.SetActive(true);
        isGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public string UpdateScore()
    {
        string scoreTempText = "";

        if (score == 0)
        {
            scoreTempText = "000000";
        }
        else if (score > 0 && score < 100)
        {
            scoreTempText = "0000" + score.ToString();
        }
        else if (score > 0 && score < 1000)
        {
            scoreTempText = "000" + score.ToString();
        }
        else if (score > 0 && score < 10000)
        {
            scoreTempText = "00" + score.ToString();
        }
        else if (score > 0 && score < 100000)
        {
            scoreTempText = "0" + score.ToString();
        }
        else
        {
            scoreTempText = score.ToString();
        }

        return scoreTempText;
    }

    public void AddScore(int points)
    {
        score += points;
    }

    IEnumerator WaitingTime()
    {
        for (int y = 0; y < height; y++)
        {
            if (FullLine(y))
            {
                isPaused = true;
                for (int i = 0; i < indexLine.Length; i++)
                {
                    if (indexLine[i] < 0)
                    {
                        indexLine[i] = y;
                        break;
                    }
                }
                DeleteSquare(y);
                y--;
            }
        }

        yield return new WaitForSeconds(1.1f);
        int points = 0;
        int scoreTotal = 0;

        for (int i = indexLine.Length - 1; i >= 0; i--)
        {
            if (indexLine[i] >= 0)
            {
                points++;
                MoveAllLinesDown(indexLine[i] + 1);
                indexLine[i] = -1;
            }
        }

        switch (points)
        {
            case 1:
                scoreTotal = 100 * difficulty;
                StartCoroutine(PoinsText(scoreTotal));
                score += scoreTotal;
                break;
            case 2:
                scoreTotal = 300 * difficulty;
                StartCoroutine(PoinsText(scoreTotal));
                score += scoreTotal;
                break;
            case 3:
                scoreTotal = 600 * difficulty;
                StartCoroutine(PoinsText(scoreTotal));
                score += scoreTotal;
                break;
            case 4:
                scoreTotal = 1000 * difficulty;
                StartCoroutine(PoinsText(scoreTotal));
                score += scoreTotal;
                break;
        }

        linesDeleted += points;
        if (linesDeleted >= 10)
        {
            linesDeleted -= 10;
            difficulty++;
        }

        isPaused = false;
    }

    IEnumerator PoinsText(int points)
    {
        poinsText.enabled = true;
        poinsText.text = "+" + points.ToString();
        yield return new WaitForSeconds(.5f);
        poinsText.enabled = false;
    }
}

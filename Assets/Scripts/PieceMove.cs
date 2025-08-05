using UnityEngine;

public class PieceMove : MonoBehaviour
{
    [SerializeField] float timeCount;
    float count;

    // public float timeCountDown;
    float countDown;

    // public float timeCountFlip;
    float countFlip;

    [SerializeField] float fall;

    [SerializeField] bool canRotate;
    [SerializeField] bool rotate360;

    [SerializeField] SpawnTetro spawnTetro;
    [SerializeField] GameObject ghost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTetro = GameObject.FindFirstObjectByType<SpawnTetro>();
        spawnTetro.SetNextPieceStatus(false);
        Instantiate(ghost, new Vector3(transform.position.x, transform.position.y - 22, transform.position.z), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.IsPaused && !GameControl.instance.IsGameOver)
        {
            Move();
            Flip();

            count = UpdateTimer(count);
            countDown = UpdateTimer(countDown);
            countFlip = UpdateTimer(countFlip);
        }
        // if (count > 0)
        // {
        //     count -= Time.deltaTime;
        // }

        // if (countDown > 0)
        // {
        //     countDown -= Time.deltaTime;
        // }

        // if (countFlip > 0)
        // {
        //     countFlip -= Time.deltaTime;
        // }
    }

    void FixedUpdate()
    {
        if(!GameControl.instance.IsPaused && !GameControl.instance.IsGameOver)
            PieceFall();
    }

    void Move()
    {
        float horizontal = InputManager.GetMovementInput().x;
        float vertical = InputManager.GetMovementInput().y;

        // if (horizontal != 0 && count <= 0)
        // {
        //     count = timeCount;
        //     transform.position += new Vector3(horizontal, 0, 0);

        //     if (ValidatePosition())
        //     {
        //         GameControl.instance.UpdateGrid(this);
        //     }
        //     else
        //     {
        //         if (horizontal == 1)
        //         {
        //             transform.position += new Vector3(-1, 0, 0);
        //         }
        //         if (horizontal == -1)
        //         {
        //             transform.position += new Vector3(1, 0, 0);
        //         }
        //     }
        // }

        if (horizontal == 1 && count <= 0)
        {
            transform.position += new Vector3(1, 0, 0);
            count = timeCount;

            if (ValidatePosition())
            {
                GameControl.instance.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        if (horizontal == -1 && count <= 0)
        {
            transform.position += new Vector3(-1, 0, 0);
            count = timeCount;

            if (ValidatePosition())
            {
                GameControl.instance.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        if (horizontal == 0)
        {
            count = 0;
        }

        if (vertical == -1 && countDown <= 0)
        {
            transform.position += new Vector3(0, -1, 0);
            countDown = timeCount;

            if (ValidatePosition())
            {
                GameControl.instance.UpdateGrid(this);
            }
            else
            {
                AudioController.instance.PlayClip(AudioController.instance.ClipPieceStop);
                GameControl.instance.AddScore(10);
                transform.position += new Vector3(0, 1, 0);
                GameControl.instance.DeleteLine();

                if (GameControl.instance.OverGrid(this))
                {
                    GameControl.instance.GameOver();
                }

                enabled = false;
                spawnTetro.SetNextPieceStatus(true);
            }
        }


    }

    void Flip()
    {
        if (InputManager.GetFlipInput() && countFlip <= 0)
        {
            CheckRotate();
            countFlip = timeCount;
            //transform.Rotate(0, 0, 90);
        }
    }

    float UpdateTimer(float timer)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        return timer;
    }

    void PieceFall()
    {
        if (Time.time - fall >= (GameControl.instance.Speed/GameControl.instance.Difficulty))
        {
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;

            if (ValidatePosition())
            {
                GameControl.instance.UpdateGrid(this);
            }
            else
            {
                AudioController.instance.PlayClip(AudioController.instance.ClipPieceStop);
                GameControl.instance.AddScore(10);
                transform.position += new Vector3(0, 1, 0);
                GameControl.instance.DeleteLine();

                if (GameControl.instance.OverGrid(this))
                {
                    GameControl.instance.GameOver();
                }

                enabled = false;
                spawnTetro.SetNextPieceStatus(true);
            }
        }
    }

    bool ValidatePosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 blockPos = GameControl.instance.Round(child.position);

            if (!GameControl.instance.InsideGrid(blockPos))
            {
                return false;
            }

            if (GameControl.instance.TransformGridPosition(blockPos) != null
            && GameControl.instance.TransformGridPosition(blockPos).parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    void CheckRotate()
    {
        if (canRotate)
        {
            if (!rotate360)
            {
                if (transform.rotation.z < 0)
                {
                    transform.Rotate(0, 0, 90);
                    if (ValidatePosition())
                    {
                        GameControl.instance.UpdateGrid(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                    if (ValidatePosition())
                    {
                        GameControl.instance.UpdateGrid(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);
                if (ValidatePosition())
                {
                    GameControl.instance.UpdateGrid(this);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SpawnTetro : MonoBehaviour
{
    [SerializeField] Transform[] createPiece;
    [SerializeField] List<GameObject> showPiece;
    [SerializeField] int nextPiece;
    [SerializeField] bool nextPieceSend;
    float count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var piece in createPiece)
        {
            piece.gameObject.SetActive(true);
        }

        nextPiece = Random.Range(0, 7);
        NextPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.IsGameOver)
        {
            if (nextPieceSend && count <= 0)
            {
                NextPiece();
                count = 1;
            }

            if (count > 0)
            {
                count -= Time.deltaTime;
            }
        }
    }

    public void NextPiece()
    {
        Instantiate(createPiece[nextPiece], transform.position, Quaternion.identity);
        nextPiece = Random.Range(0, 7);

        for (int i = 0; i < showPiece.Count; i++)
        {
            showPiece[i].SetActive(false);
        }

        showPiece[nextPiece].SetActive(true);
    }

    public void SetNextPieceStatus(bool status)
    {
        nextPieceSend = status;
    }
}

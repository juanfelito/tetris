using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shape : MonoBehaviour {
    public static float speed = 1;

    private float lastMoveDown = 0;

    void Start () {
        if (!IsInGrid()) {
            SoundManager.Instance.PlayASound(SoundManager.Instance.GameOver);

            Invoke("OpenGameOverScene", 0.5f);
        }

        InvokeRepeating("IncreaseSpeed", 2.0f, 2.0f);
    }

    void OpenGameOverScene() {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void IncreaseSpeed() {
        Shape.speed -= 0.001f;
    }

    void Update() {
        if (Input.GetKeyDown("a")) {
            MoveShape(-1, 0);
        }

        if (Input.GetKeyDown("d")) {
            MoveShape(1, 0);
        }

        if (Input.GetKeyDown("s") || Time.time - lastMoveDown >= Shape.speed) {
            transform.position += new Vector3(0, -1, 0);

            if (!IsInGrid()) {
                transform.position += new Vector3(0, 1, 0);
                GameBoard.DeleteAllFullRows();

                enabled = false;
                FindObjectOfType<ShapeSpawner>().SpawnShape();
            } else {
                UpdateGameBoard();
            }
            lastMoveDown = Time.time;
        }

        if (Input.GetKeyDown("w")) {
            RotateShape(90);
        }

        if (Input.GetKeyDown("e")) {
            RotateShape(-90);
        }
    }

    void MoveShape(int x, int y) {
        transform.position += new Vector3(x, y, 0);

        if (!IsInGrid()) {
            transform.position += new Vector3(x * -1, y * -1, 0);
        } else {
            UpdateGameBoard();
        }
    }

    void RotateShape(int angle) {
        transform.Rotate(0, 0, angle);

        if (!IsInGrid()) {
            transform.Rotate(0, 0, -angle);
        } else {
            UpdateGameBoard();
        }
    }

    public bool IsInGrid() {
        foreach(Transform childCube in transform) {
            Vector2 position = RoundVector(childCube.position);

            if(!IsInBorder(position)) {
                return false;
            }

            if (GameBoard.gameBoard[(int)position.x, (int)position.y] != null && GameBoard.gameBoard[(int)position.x, (int)position.y].parent != transform) {
                return false;
            }
        }
        return true;
    }

    public Vector2 RoundVector(Vector2 vect) {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    public static bool IsInBorder(Vector2 pos) {
        return ((int)pos.x >= 0 && (int)pos.x <= 9 && (int)pos.y >= 0 && (int)pos.y <= 19);
    }

    public void UpdateGameBoard() {
        for (int y = 0; y < 20; ++y) {
            for (int x = 0; x < 10; ++x) {
                if(GameBoard.gameBoard[x, y] != null && GameBoard.gameBoard[x, y].parent == transform) {
                    GameBoard.gameBoard[x, y] = null;
                }
            }
        }

        foreach(Transform childCube in transform) {
            Vector2 position = RoundVector(childCube.position);

            GameBoard.gameBoard[(int)position.x, (int)position.y] = childCube;
        }
    }
}

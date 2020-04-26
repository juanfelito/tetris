using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

    public static Transform[,] gameBoard = new Transform[10, 20];

    public static void DeleteAllFullRows() {

        for (int row = 0; row < 20; row++) {
            if (RowIsFull(row)) {
                DeleteFullRow(row);
                row--;
            }
        }
    }

    public static bool RowIsFull(int row) {
        for (int col = 0; col < 10; col++) {
            if (gameBoard[col, row] == null) {
                return false;
            }
        }

        return true;
    }

    public static void DeleteFullRow(int row) {
        for (int col = 0; col < 10; col++) {
            Destroy(gameBoard[col, row].gameObject);
            gameBoard[col, row] = null;
        }

        SoundManager.Instance.PlayASound(SoundManager.Instance.RowDelete);

        row++;

        for (int j = row; j < 20; j++) {
            for (int i = 0; i < 10; i++) {
                if (gameBoard[i, j] != null) {
                    gameBoard[i, j - 1] = gameBoard[i, j];
                    gameBoard[i, j] = null;

                    gameBoard[i, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

        IncreaseScore();
    }

    static void IncreaseScore() {
        var scoreComp = GameObject.Find("Score").GetComponent<Text>();

        int currentScore = int.Parse(scoreComp.text);

        currentScore++;

        scoreComp.text = currentScore.ToString();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public GameObject GameStageUI;
    public GameObject GameOverUI;

    public void GameOverFunc(int finalScore) {
        scoreText.text = "your score:<br>" + finalScore.ToString();

    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameOverUI.SetActive(false);
        //GameStageUI.SetActive(true);

    }




}

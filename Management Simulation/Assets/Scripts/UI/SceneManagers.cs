using UnityEngine.SceneManagement;
public interface SceneManagers
{
    public enum MoveScene
    {
        None,
        Main,
        InGame,
        EndGame,
    }


/// <summary>
/// 씬이동 함수.
/// </summary>
/// <param name="moveScene"> 이동할 씬 </param>
    public static void LoadScenes(MoveScene moveScene)
    {
        switch (moveScene)
        {
            case MoveScene.None:
                break;
            case MoveScene.Main:
                SceneManager.LoadScene("StartScene");
                break;
            case MoveScene.InGame:
                SceneManager.LoadScene("GamePlay");
                break;
            case MoveScene.EndGame:
                SceneManager.LoadScene("GameOver");
                break;
            default:
                break;
        }
    }

}


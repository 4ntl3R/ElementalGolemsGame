using Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
   public class SceneController : MonoBehaviour
   {
      public void LoadMainScene()
      {
         SceneManager.LoadScene("MainScene");
      }

      public void LoadLostScene()
      {
         GameStatus.GameState = EndingOptions.Lose;
         SceneManager.LoadScene("EndMenu");
      }
      
      public void LoadWinScene()
      {
         GameStatus.GameState = EndingOptions.Win;
         SceneManager.LoadScene("EndMenu");
      }
      
      public void LoadDrawScene()
      {
         GameStatus.GameState = EndingOptions.Draw;
         SceneManager.LoadScene("EndMenu");
      }
   }
}

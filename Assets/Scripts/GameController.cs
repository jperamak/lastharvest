using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public int food;
        public int currentLevel = 2;
        public List<FamilyMember> family;

        //ugly hax
        public SoundEffect collideSound;
        public int numberOfLevels;

        public int Score
        {
            get; private set;
        }

        public void Start()
        {
            tag = Tags.GameController;
            DontDestroyOnLoad(this);
            //StartFamily();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void GoToNextLevel(int levelScore = 0)
        {
            Score += levelScore;
            StartCoroutine(NextLevel());
        }

        public void FailLevel()
        {
            StartCoroutine(Restart());
            //Application.LoadLevel("ScoreScreen");
        }

        private IEnumerator Restart()
        {
            Application.LoadLevel("ScoreScreen");
            yield return new WaitForSeconds(2);
            Score = 0;

            currentLevel = 2;
            Application.LoadLevel("level_tutorial_jumps");
        }

        private IEnumerator NextLevel()
        {
            Application.LoadLevel("ScoreScreen");
            yield return new WaitForSeconds(2);

            if (++currentLevel <= numberOfLevels)
                Application.LoadLevel(currentLevel);
            else
                Application.LoadLevel("EndScreen");
        }
    }
}
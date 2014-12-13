﻿using System.Linq;
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

            //var a = true; // does nothing does not work without, dunno lol
            currentLevel = 2;
            Application.LoadLevel("level_tutorial_jumps");
        }

        private IEnumerator NextLevel()
        {
            //feed family
            //Feed();
            //loading screen
            Application.LoadLevel("ScoreScreen");
            yield return new WaitForSeconds(2);

            var a = true; // does nothing does not work without, dunno lol
            if (++currentLevel <= numberOfLevels + 2)
                Application.LoadLevel(currentLevel);
            else
                Application.LoadLevel("EndScreen");
        }

        private void StartFamily()
        {
            family = new List<FamilyMember>
            {
                new FamilyMember("Billy-Bob", 23),
                new FamilyMember("Wifey", 19),
                new FamilyMember("Babby1", 3),
                new FamilyMember("Babby2", 1)
            };
        }

        private void Feed()
        {
            foreach (FamilyMember fm in family.ToList())
            {
                food = fm.Feed(food);
                if (!fm.DidSurvive())
                    family.Remove(fm);
            }
        }

    }
}
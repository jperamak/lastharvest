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
        public int currentLevel = 3;
        public List<FamilyMember> family;

        public int Score
        {
            get; private set;
        }

        public void Start()
        {
            tag = Tags.GameController;
            DontDestroyOnLoad(this);
            StartFamily();
        }

        public void GoToNextLevel(int levelScore = 0)
        {
            Score += levelScore;
            StartCoroutine(NextLevel());
        }

        public void FailLevel()
        {
            Application.LoadLevel("ScoreScreen");
        }

        private IEnumerator NextLevel()
        {
            //feed family
            Feed();
            //loading screen
            Application.LoadLevel("ScoreScreen");
            yield return new WaitForSeconds(2);

            var a = true;
            Application.LoadLevel(++currentLevel);

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
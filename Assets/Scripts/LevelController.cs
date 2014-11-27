using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    class LevelController : MonoBehaviour
    {
        public float timeInSeconds = 180.0f;

        private GameController _gameController;
        private Player _player;

        private readonly List<Harvestable> _harvestables = new List<Harvestable>(); 

        public void Start()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            _harvestables.AddRange(GameObject.FindGameObjectsWithTag("Harvestable").Select(o => o.GetComponent<Harvestable>()));
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _player.Harvested += OnHarvested;
            tag = Tags.LevelController;
        }

        public void Update()
        {
            timeInSeconds -= Time.deltaTime;
            if (timeInSeconds < 0.0f)
            {
                _gameController.FailLevel();
            }
        }

        private void OnHarvested(object sender, HarvestEventArgs args)
        {
            _harvestables.Remove(args.Harvestable);
            Destroy(args.Harvestable.gameObject);
            if (!_harvestables.Any())
            {
                _gameController.GoToNextLevel();
            }
        }
    }
}

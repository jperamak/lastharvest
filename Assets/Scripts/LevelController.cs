using System;
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
        private readonly List<Spawner> _spawners = new List<Spawner>();
        private readonly List<MovingPlatform> _movingPlatforms = new List<MovingPlatform>(); 

        public void Start()
        {
            _gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
            tag = Tags.LevelController;

            _spawners.AddRange(GameObject.FindGameObjectsWithTag(Tags.Spawner).Select(o => o.GetComponent<Spawner>()));
            _movingPlatforms.AddRange(
                GameObject.FindGameObjectsWithTag(Tags.MovingPlatform)
                    .Where(p => p.GetComponent<MovingPlatform>() != null)
                    .Select(o => o.GetComponent<MovingPlatform>()));

            SpawnPlayerAndItems();
        }

        private void SpawnPlayerAndItems()
        {
            foreach (var spawner in _spawners)
            {
                var thing = spawner.Spawn();
                if (thing.GetComponent<Harvestable>() != null)
                {
                    _harvestables.Add(thing.GetComponent<Harvestable>());
                }
                if (thing.GetComponent<Player>() != null)
                {
                    _player = thing.GetComponent<Player>();
                    _player.Harvested += OnHarvested;
                    _player.Died += OnDied;
                }
            }
        }

        private void OnDied(object sender, EventArgs e)
        {
            _player.Died -= OnDied;
            _player.Harvested -= OnHarvested;
            Destroy(_player.gameObject);
            _player = null;
            _harvestables.ForEach(h => Destroy(h.gameObject));
            _harvestables.Clear();
            _movingPlatforms.ForEach(p => p.Reset());
            SpawnPlayerAndItems();
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
                _gameController.GoToNextLevel(100);
            }
        }
    }
}

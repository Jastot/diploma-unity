﻿using System;
using System.Collections.Generic;
using System.Linq;
using Diploma.Controllers;
using Managers;
using UnityEngine;

namespace GameObjectCreating
{
    public class PoolOfObjects
    {
        private readonly Dictionary<string, HashSet<GameObjectProvider>> _gameObjectPool;
        private readonly int _capacityPool;
        private int _count;
        private Transform _rootPool;
        private GameObjectCreator _gameObjectCreator;
        public PoolOfObjects(int capacityPool,GameObjectFactory gameObjectFactory,GameContextWithLogic gameContext)
        {
            
            _gameObjectCreator = new GameObjectCreator(gameObjectFactory,gameContext);
            _gameObjectPool = new Dictionary<string, HashSet<GameObjectProvider>>();
            _count = 0;
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new
                    GameObject(PoolManager.POOL_OBJECTS).transform;
            }
        }

        public GameObjectProvider GetEnemy(string type)
        {
            GameObjectProvider result;
            switch (type)
            {
                case "GameObject":
                    result = GetAsteroid(GetListEnemies(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type,
                        "Не предусмотрен в программе");
            }
            return result;
        }
        private HashSet<GameObjectProvider> GetListEnemies(string type)
        {
            return _gameObjectPool.ContainsKey(type) ? _gameObjectPool[type] :
                _gameObjectPool[type] = new HashSet<GameObjectProvider>();
        }
        private GameObjectProvider GetAsteroid(HashSet<GameObjectProvider> enemies)
        {
            var enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (enemy == null )
            {
                
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = _gameObjectCreator.CreateGameObjectProvider(_count);
                    ReturnToPool(instantiate.transform);
                    enemies.Add(instantiate);
                    _count++;
                }
                
                GetAsteroid(enemies);
                
            }
            enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
            return enemy;
        }

        public List<GameObjectProvider> GetAllAsteroids()
        {
            GetEnemy("GameObject");
            List<GameObjectProvider> listOfAsteroidProviders  = _gameObjectPool["GameObject"].ToList();
            return listOfAsteroidProviders;
        }
        
        public void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }

        public void RemovePool()
        {
            GameObject.Destroy(_rootPool);
        }
    }
}
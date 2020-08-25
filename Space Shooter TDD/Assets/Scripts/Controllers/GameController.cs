// <copyright file="GameController.cs" company="Climate Conserve">
// Copyright (C) 2019 Matrix Becker. All Rights Reserved.
//
//@ Author: Mr. Saikat Patra
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Becker.MVC;

namespace SpaceShooter
{
    /// <summary>
    /// Root Game Controller
    /// </summary>
    public class GameController : Controller<ApplicationGameManager>
    {

        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("SpawnWaves");
        }

        // Update is called once per frame
        void Update()
        {
            app.view.player.LaunchLeserBolt();            
        }

        /// <summary>
        /// Physics Update
        /// </summary>
        private void FixedUpdate()
        {            
            app.view.player.PlayerMove();
        }


        /// <summary>
        /// Spawn Hazards
        /// </summary>
        IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject hazardsPrefab = Instantiate(app.model.hazard, spawnPosition, spawnRotation);
                    hazardsPrefab.transform.SetParent(app.model.hazardSpawn);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }

        public void SpawnHazards()
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject hazardsPrefab = Instantiate(app.model.hazard, spawnPosition, spawnRotation);
            hazardsPrefab.transform.SetParent(app.model.hazardSpawn);
        }
        /// <summary>
        /// Handle notifications from the application.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        public override void OnNotification(string p_event, Object p_target, params object[] p_data)
        {
            switch (p_event)
            {
                case GameEventNotification.SceneLoad:

                    Utils.Log("SpaceShooter [" + p_data[0] + "][" + p_data[1] + "] loaded");
                    break;
                case GameEventNotification.StartGame:
                    Utils.Log("Start the Game");
                    break;
                case GameEventNotification.GameOver:
                    Utils.Log("Game Over");
                    break;
            }
        }
    }
}

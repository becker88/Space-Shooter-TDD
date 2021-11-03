// <copyright file="PlayerView.cs" company="Climate Conserve">
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
    /// Player Controller
    /// </summary>
    public class PlayerView : View<ApplicationGameManager>
    {
        public Rigidbody playerRigidBody;
        public Boundary boundary;
        public float fireRate;
        private float nextFire;
        public AudioSource playLaserAudio;

        /// <summary>
        /// To Control and Move Player
        /// </summary>
        /// <param name="moveHorizontal"></param>
        /// <param name="moveVertical"></param>
        public void PlayerMove()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            playerRigidBody.velocity = movement * app.model.PlayerSpeed;

            playerRigidBody.position = new Vector3
                (
                    Mathf.Clamp(playerRigidBody.position.x, boundary.xMin, boundary.xMax), 
                    0.0f,
                    Mathf.Clamp(playerRigidBody.position.z, boundary.zMin, boundary.zMax)
                );

            playerRigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, playerRigidBody.velocity.x * -app.model.TiltPlayerBody);
        }

        /// <summary>
        /// Fire Laser Bolt
        /// </summary>
        public void LaunchLeserBolt()
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject shotPrefab = Instantiate(app.model.laserShot, app.model.shotSpawn.position, app.model.shotSpawn.rotation) as GameObject;
                shotPrefab.transform.SetParent(app.model.shotSpawn);
                shotPrefab.GetComponent<BoltMover>().LoadBolt(app.model.LaserSpeed);
                playLaserAudio.Play();
            }
        }
    }

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }
}

// <copyright file="GameModel.cs" company="Climate Conserve">
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
    /// Root Game Model
    /// </summary>
    public class GameModel : Model<ApplicationGameManager>
    {
        private float playerSpeed   = 10.0f; // Speed of Player
        private float laserSpeed    = 20.0f; // Speed of Laser Bolt
        private float tiltPlayerBody = 3.0f;

        public GameObject laserShot;
        public GameObject explosion;
        public GameObject playerExplosion;

        public Transform shotSpawn;


        /// <summary>
        /// Access the Player Speed Data
        /// </summary>
        public float PlayerSpeed {  get  {  return playerSpeed; } private set  {  playerSpeed = value; } }

        /// <summary>
        /// Access the Laser Speed Data
        /// </summary>
        public float LaserSpeed { get { return laserSpeed; } private set { laserSpeed = value; } }


        /// <summary>
        /// Tilt Player
        /// </summary>
        public float TiltPlayerBody { get { return tiltPlayerBody; } private set { tiltPlayerBody = value; } }
       
    }
}

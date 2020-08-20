// <copyright file="BoltMover.cs" company="Climate Conserve">
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
    /// Laser Bolt Controller
    /// </summary>
    public class BoltMover : View<ApplicationGameManager>
    {

        public Rigidbody boltRigidbody;
        private float destroyTime = 1.0f;

        /// <summary>
        /// To Launch the Laser Bolt
        /// </summary>
        public void LoadBolt(float _speed)
        {
            boltRigidbody.velocity = transform.forward * _speed;
        }


        /// <summary>
        /// Auto Destroy Laser Bolt
        /// </summary>
        //private void OnDestroy()
        //{
        //    Destroy(this.gameObject, destroyTime);
        //}
    }
}

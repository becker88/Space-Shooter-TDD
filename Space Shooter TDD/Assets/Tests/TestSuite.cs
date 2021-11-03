﻿// <copyright file="TestSuite.cs" company="Climate Conserve">
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
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace SpaceShooter
{
    public class TestSuite 
    {
        private ApplicationGameManager _app;

        /// <summary>
        /// Set up of Test Env.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            GameObject _rootObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Application"));
            _app = _rootObject.GetComponentInChildren<ApplicationGameManager>();
        }

        /// <summary>
        /// Collapse all object
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_app.gameObject);
        }

        /// <summary>
        /// Testing 01: Spawn Hazards
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator AsteroidsSpawnAndMoveDown()
        {
            _app.gameCtrl.SpawnHazards();
            yield return new WaitForSeconds(3.0f);
        }

        /// <summary>
        /// Testing 02: Launch Fire
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator FireLaserByPlayer()
        {
            _app.view.player.LaunchLeserBolt();
            yield return new WaitForSeconds(3.0f);
        }


        /// <summary>
        /// Testing 03: Destroy Astroid by Laset Contact
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator DestroyAstroidByLaserContact()
        {
            //Write Test Condition
            yield return new WaitForSeconds(3.0f);
        }

        /// <summary>
        /// Testing 04: Destroy Player by Astroid Contact
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator DestroyPalyerOnContactOfAstroid()
        {
            //Write Test Condition
            yield return new WaitForSeconds(3.0f);
        }
    }
}

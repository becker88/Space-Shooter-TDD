// <copyright file="Astroid.cs" company="Climate Conserve">
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
    /// Astroid Controller
    /// </summary>
    public class Astroid : View<ApplicationGameManager>
    {

        public float tumble;
        public float speed;


        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Boundry")
            {
                return;
            }

            Instantiate(app.model.explosion, transform.position, transform.rotation);

            if (other.tag == "Player")
            {
                Instantiate(app.model.playerExplosion, other.transform.position, other.transform.rotation);
                //gameController.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

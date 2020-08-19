// <copyright file="Splash.cs" company="Climate Conserve">
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


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

namespace SpaceShooter
{

public class Splash : MonoBehaviour {


	private float time =5f;
	public Image shadow;

	// Use this for initialization
	void Start () {
		        
        StartCoroutine ("SplashScreen");        
	}
	
	/// <summary>
	/// Splashs of the screen.
	/// </summary>
	/// <returns>The screen.</returns>
	IEnumerator SplashScreen()
	{
		yield return new WaitForSeconds (0.5f);

		float startAlfa = shadow.color.a;

		float rate = 1.0f / 5.0f;
		float progress = 0.0f;

		while (progress < 1.0) {
			Color tmpColor = shadow.color;

			shadow.color = new Color (tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp( startAlfa, 1f, progress));
			progress += rate * Time.deltaTime;
			yield return null;
		}

			float fadeTime = GameObject.Find ("Splash BG").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

		SceneManager.LoadScene ("Main");
	}

  }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder.View {
	class GridView : MonoBehaviour {
		[SerializeField] private float gridScale;
		[SerializeField] private Vector2 originLocation;
		private IntPair cursorPosition;
		[SerializeField]private ShipGrid grid;

		private void Start() {
		}
	}
}

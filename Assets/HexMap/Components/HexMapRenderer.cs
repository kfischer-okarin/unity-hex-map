/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;

namespace HexMapEngine {

    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class HexMapRenderer : MonoBehaviour {

        [SerializeField] HexMap _hexMap;

        public HexMap HexMap {
            get { return _hexMap; }
            set {
                _hexMap = value;
                UpdateMesh();
            }
        }

        void Start() {
            if (_hexMap != null)
                UpdateMesh();
        }

        float _lastGridSize;

        void OnValidate() {
            if (gridSize != _lastGridSize && _hexMap != null) {
                _lastGridSize = gridSize;
                UpdateMesh();
            }
        }

        public Hex ScreenPointToHex(Vector2 screenPoint) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint) - transform.position;
            float q = (worldPoint.x * SQRT_3 * ONE_THIRD + worldPoint.y * ONE_THIRD) / gridSize;
            float r = (-worldPoint.y * 2 * ONE_THIRD) / gridSize;
            return Hex.Round(q, r);
        }

        #region Mesh Generation
        public float gridSize = 1;

        public HexTileset tileset;

        static float SQRT_3 = Mathf.Sqrt(3);
        const float ONE_THIRD = 1f / 3f;
        const float TO_RAD_FACTOR = Mathf.PI / 180;

        // Pointy top, q right, r bottom-right
        Vector2 GetHexCenter(HexData cell) {
            return new Vector2(gridSize * SQRT_3 * (cell.q + cell.r / 2f), gridSize * (-1.5f) * cell.r);
        }

        // 0 bottom right, 1 bottom, .. 5 top right
        Vector2 GetHexCorner(Vector2 center, int corner) {
            var angle_deg = 60 * corner + 30;
            var angle_rad = TO_RAD_FACTOR * angle_deg;
            return new Vector2(center.x + gridSize * Mathf.Cos(angle_rad), center.y - gridSize * Mathf.Sin(angle_rad));
        }

        public void UpdateMesh() {
            var newMesh = new Mesh();
            newMesh.hideFlags = HideFlags.HideAndDontSave;

            _gridPoints.Clear();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            var cornerOffsets = new Vector2[6];
            for (int i = 0; i < 6; i++) {
                cornerOffsets[i] = GetHexCorner(new Vector2(0, 0), i);
            }

            foreach (HexData c in _hexMap.HexData) {
                Vector2 center = GetHexCenter(c);
                vertices.Add(center);

                int centerIndex = vertices.Count - 1;

                for (int i = 0; i < 6; i++) {
                    Vector2 corner = center + cornerOffsets[i];
                    vertices.Add(corner);

                    if (renderGrid)
                        _gridPoints.Add(corner);
                }

                for (int i = 1; i <= 6; i++) {
                    triangles.AddRange(new int[] { centerIndex, centerIndex + i, centerIndex + (i == 6 ? 1 : i + 1) });
                }
            }

            newMesh.SetVertices(vertices);
            newMesh.SetTriangles(triangles, 0);
            GetComponent<MeshFilter>().sharedMesh = newMesh;

            UpdateTexture();
            UpdateUVs();
        }

        public void UpdateTexture() {
            var material = new Material(Shader.Find("Unlit/Texture"));
            material.hideFlags = HideFlags.HideAndDontSave;
            material.mainTexture = tileset.texture;
            GetComponent<MeshRenderer>().sharedMaterial = material;
        }

        public void UpdateUVs() {
            var uvs = new List<Vector2>();

            foreach (HexData c in _hexMap.HexData) {
                uvs.AddRange(tileset.GetUVs(c.tileIndex));
            }

            GetComponent<MeshFilter>().sharedMesh.SetUVs(0, uvs);
        }

        public void UpdateHexUV(Hex position) {
            HexData data = _hexMap.Get(position);
            if (data != null) {
                int index = _hexMap.HexData.IndexOf(data);
                MeshFilter mesh = GetComponent<MeshFilter>();
                Vector2[] meshUVs = mesh.mesh.uv;

                Vector2[] newUVs = tileset.GetUVs(data.tileIndex);
                for (int i = 0; i < 7; i++) {
                    meshUVs[7 * index + i] = newUVs[i];
                }
                mesh.mesh.uv = meshUVs;
            }
        }
        #endregion

        #region Drawing Hex Grid outlines
        public bool renderGrid = true;

        public Material lineMaterial;

        List<Vector2> _gridPoints = new List<Vector2>();

        void OnRenderObject() {
            if (renderGrid) {
                lineMaterial.SetPass(0);

                GL.PushMatrix();
                GL.MultMatrix(transform.localToWorldMatrix);
                GL.Begin(GL.LINES);
                {
                    for (int i = 0; i < _gridPoints.Count / 6; i++) {
                        for (int j = 0; j < 6; j++) {
                            GL.Vertex(_gridPoints[i * 6 + j]);
                            GL.Vertex(_gridPoints[i * 6 + (j == 5 ? 0 : j + 1)]);
                        }
                    }
                }
                GL.End();

                GL.PopMatrix();
            }
        }
        #endregion

    }

}

using UnityEngine;

public class DrawGrid : MonoBehaviour
{
	[SerializeField]
	private Material lineMaterial;

	[SerializeField]
	private int gridSize = 12;

	[SerializeField]
	private float zOffset = 0.01f;

	public void OnRenderObject()
	{
		lineMaterial.SetPass(0);

		GL.PushMatrix();
		GL.Begin(GL.LINES);
		GL.Color(new Color(0.07f, 0.07f, 0.07f, 0.4f));

		int midGrid = gridSize / 2;

		for (int i = -midGrid; i < midGrid; i++)
		{
			GL.Vertex3(i + 0.5f, zOffset, -midGrid + 0.5f);
			GL.Vertex3(i + 0.5f, zOffset, midGrid - 0.5f);

			GL.Vertex3(-midGrid + 0.5f, zOffset, i + 0.5f);
			GL.Vertex3(midGrid - 0.5f, zOffset, i + 0.5f);
		}

		GL.End();
		GL.PopMatrix();
	}
}
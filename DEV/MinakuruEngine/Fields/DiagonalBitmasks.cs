﻿namespace Minakuru.Engine.Fields;

public static class DiagonalBitmasks
{
	// Index 1: direction
	// Index 2: fieldNo
	// The result is an array of ulongs (with a maximum of 8 elements), which can be used as bitmasks
	private static readonly ulong[,][] _diagonalPerDirectionFieldBitmasks = new ulong[64, 4][];
	private static readonly ulong[] _diagonalAllDirectionsFieldBitmasks = new ulong[64];

	static DiagonalBitmasks()
	{
		for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
		{
			var toColumnNo = targetFieldNo % 8;
			var toRowNo = targetFieldNo / 8;
			for (int option = 0; option < 4; option++)
			{
				List<ulong> fieldIds = [];
				int deltaColumn = new int[] { +1, +1, -1, -1 }[option];
				int deltaRow = new int[] { +1, -1, +1, -1 }[option];
				int fromColumn = toColumnNo + deltaColumn;
				int fromRow = toRowNo + deltaRow;

				while (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
				{
					byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
					ulong filter = (ulong)1 << fromFieldNo;
					fieldIds.Add(filter);
					_diagonalAllDirectionsFieldBitmasks[targetFieldNo] |= filter;

					fromColumn = fromColumn + deltaColumn;
					fromRow = fromRow + deltaRow;
				}
				_diagonalPerDirectionFieldBitmasks[targetFieldNo, option] = [.. fieldIds];
			}
		}
	}

	public static ulong[,][] DiagonalPerDirectionFieldBitmasks
	{
		get
		{
			var copy = new ulong[64, 4][];
			for (int i = 0; i < 64; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					copy[i, j] = new ulong[_diagonalPerDirectionFieldBitmasks[i, j].Length];
					Array.Copy(_diagonalPerDirectionFieldBitmasks[i, j], copy[i, j], _diagonalPerDirectionFieldBitmasks[i, j].Length);
				}
			}
			return copy;
		}
	}

	public static ulong[] DiagonalAllDirectionsFieldBitmasks
	{
		get
		{
			var copy = new ulong[64];
			Array.Copy(_diagonalAllDirectionsFieldBitmasks, copy, _diagonalAllDirectionsFieldBitmasks.Length);
			return copy;
		}
	}
}
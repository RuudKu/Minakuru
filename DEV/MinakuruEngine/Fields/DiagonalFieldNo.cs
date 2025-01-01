namespace Minakuru.Engine.Fields;

public static class DiagonalFieldNo
{
	// Index 1: direction
	// Index 2: fieldNo
	// The result is an array of byte (with a maximum of 8 elements), which can be used to access the fields
	private static readonly byte[,][] _diagonalPerDirectionFieldNos = new byte[64, 4][];
	
	static DiagonalFieldNo()
	{
		for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
		{
			var toColumnNo = targetFieldNo % 8;
			var toRowNo = targetFieldNo / 8;
			for (int option = 0; option < 4; option++)
			{
				List<byte> fieldNos = [];
				int deltaColumn = new int[] { +1, +1, -1, -1 }[option];
				int deltaRow = new int[] { +1, -1, +1, -1 }[option];
				int fromColumn = toColumnNo + deltaColumn;
				int fromRow = toRowNo + deltaRow;

				while (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
				{
					byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
					fieldNos.Add(fromFieldNo);

					fromColumn = fromColumn + deltaColumn;
					fromRow = fromRow + deltaRow;
				}
				_diagonalPerDirectionFieldNos[targetFieldNo, option] = [.. fieldNos];
			}
		}
	}

	public static byte[,][] DiagonalPerDirectionFieldNos
	{
		get
		{
			var copy = new byte[64, 4][];
			for (int i = 0; i < 64; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					copy[i, j] = new byte[_diagonalPerDirectionFieldNos[i, j].Length];
					Array.Copy(_diagonalPerDirectionFieldNos[i, j], copy[i, j], _diagonalPerDirectionFieldNos[i, j].Length);
				}
			}
			return copy;
		}
	}
}

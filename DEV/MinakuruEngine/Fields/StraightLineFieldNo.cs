namespace Minakuru.Engine.Fields;

public static class StraightLineFieldNo
{
	// Index 1: direction
	// Index 2: fieldNo
	// The result is an array of byte (with a maximum of 8 elements), which can be used to access the fields
	private static readonly byte[,][] _straightLinePerDirectionFieldNos = new byte[64, 4][];

	static StraightLineFieldNo()
	{
		for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
		{
			var toColumnNo = targetFieldNo % 8;
			var toRowNo = targetFieldNo / 8;
			for (int option = 0; option < 4; option++)
			{
				List<byte> fieldNos = [];
				int deltaColumn = new int[] { +1, 0, -1, 0 }[option];
				int deltaRow = new int[] { 0, +1, 0, -1 }[option];
				int fromColumn = toColumnNo + deltaColumn;
				int fromRow = toRowNo + deltaRow;

				while (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
				{
					byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
					fieldNos.Add(fromFieldNo);

					fromColumn = fromColumn + deltaColumn;
					fromRow = fromRow + deltaRow;
				}
				_straightLinePerDirectionFieldNos[targetFieldNo, option] = [.. fieldNos];
			}
		}
	}

	public static byte[,][] StraightLinePerDirectionFieldNos
	{
		get
		{
			var copy = new byte[64, 4][];
			for (int i = 0; i < 64; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					copy[i, j] = new byte[_straightLinePerDirectionFieldNos[i, j].Length];
					Array.Copy(_straightLinePerDirectionFieldNos[i, j], copy[i, j], _straightLinePerDirectionFieldNos[i, j].Length);
				}
			}
			return copy;
		}
	}
}

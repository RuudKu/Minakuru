namespace Minakuru.Engine.Fields;

public static class KingFieldNo
{
	private static readonly byte[][] _fieldNos = new byte[64][];

	static KingFieldNo()
	{
		for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
		{
			var toColumnNo = targetFieldNo % 8;
			var toRowNo = targetFieldNo / 8;
			List<byte> fromFieldnos = [];
			for (int direction = 0; direction < 8; direction++)
			{
				int deltaColumn = new int[] { -1, -1, -1, 0, 0, +1, +1, +1 }[direction];
				int deltaRow = new int[] { -1, 0, +1, -1, +1, -1, +0, +1 }[direction];
				int fromColumn = toColumnNo + deltaColumn;
				int fromRow = toRowNo + deltaRow;

				if (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
				{
					byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
					fromFieldnos.Add(fromFieldNo);
				}
			}
			_fieldNos[targetFieldNo] = [..fromFieldnos];
		}
	}

	public static byte[][] KingFieldNos
	{
		get
		{
			var copy = new byte[64][];
			Array.Copy(_fieldNos, copy, 64);
			for (int i = 0; i < 4; i++)
			{
				copy[i] = new byte[_fieldNos[i].Length];
				Array.Copy(_fieldNos[i], copy[i], _fieldNos[i].Length);
			}
			return copy;
		}
	}
}

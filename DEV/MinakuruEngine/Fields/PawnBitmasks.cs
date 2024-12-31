
namespace Minakuru.Engine.Fields;

public static class PawnBitmasks
{
	private static readonly ulong[] _bitmasksWhiteAttackedBy = new ulong[64];
	private static readonly ulong[] _bitmasksBlackAttackedBy = new ulong[64];

	static PawnBitmasks()
	{
		ComputeWhiteAttackedBy();
		ComputeBlackAttackedBy();

		static void ComputeWhiteAttackedBy()
		{
			for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
			{
				ulong pawnsFilter = 0L;
				var fromColumnNo = targetFieldNo % 8;
				if (fromColumnNo > 0)
				{
					pawnsFilter |= (ulong)1 << targetFieldNo - 8 - 1;
				}
				if (fromColumnNo < 7)
				{
					pawnsFilter |= (ulong)1 << targetFieldNo - 8 + 1;
				}
				_bitmasksWhiteAttackedBy[targetFieldNo] = pawnsFilter;
			}
		}

		static void ComputeBlackAttackedBy()
		{
			for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
			{
				ulong pawnsFilter = 0L;
				var fromColumnNo = targetFieldNo % 8;
				if (fromColumnNo > 0)
				{
					pawnsFilter |= (ulong)1 << targetFieldNo + 8 - 1;
				}
				if (fromColumnNo < 7)
				{
					pawnsFilter |= (ulong)1 << targetFieldNo + 8 + 1;
				}
				_bitmasksBlackAttackedBy[targetFieldNo] = pawnsFilter;
			}
		}
	}

	public static ulong[] WhiteAttackedByFieldBitmasks
	{
		get
		{
			var copy = new ulong[64];
			Array.Copy(_bitmasksWhiteAttackedBy, copy, 64);
			return copy;
		}
	}

	public static ulong[] BlackAttackedByFieldBitmasks
	{
		get
		{
			var copy = new ulong[64];
			Array.Copy(_bitmasksBlackAttackedBy, copy, 64);
			return copy;
		}
	}
}

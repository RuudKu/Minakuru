
namespace Engine.UnitTests
{
	public static class ReadableMoveExtensions
	{
		public static IEnumerable<ReadableMove> ToReadableMoves(this IEnumerable<Move> moves)
		{
			var enumerator = moves.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var readableMove = enumerator.Current.ToReadableMove();
				yield return readableMove;
			}
		}

		public static ReadableMove ToReadableMove(this Move move)
		{
			char fromColumn = (char)('a' + (move.From % 8));
			char fromRow = (char)('1' + (byte)(move.From / 8));
			char toColumn = (char)('a' + (move.To % 8));
			char toRow = (char)('1' + (byte)(move.To / 8));
			return new ReadableMove(
				new string([fromColumn, fromRow]),
				new string([toColumn, toRow]),
				move.Capture, false);
		}
	}
}

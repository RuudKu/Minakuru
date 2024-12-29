
namespace Minakuru.Engine;

public record Move(byte From, byte To, bool Capture = false, Piece PromotedTo = Piece.None)
{
	public static readonly Move NullMove = new(255, 255);

	public override string ToString()
	{
		char ToColumn(byte c)
		{
			return (char)('a' + c);
		}
		char ToRow(byte r)
		{
			return (char)('1' + r);
		}
		string ToField(byte b)
		{
			byte column = (byte)(b % 8);
			byte row = (byte)(b / 8);
			return new([ToColumn(column), ToRow(row)]);
		}
		string PromotesTo(Piece promotedTo)
		{
			return promotedTo switch
			{
				Piece.None => "",
				Piece.Queen => "Q",
				Piece.Rook => "R",
				Piece.Bishop => "B",
				Piece.Knight => "N",
				_ => throw new NotImplementedException()
			};
		}

		string from = ToField(From);
		string to = ToField(To);
		string separator = Capture ? "x" : "-";
		string promotesTo = PromotesTo(PromotedTo);

		return from + separator + to + promotesTo;
	}
}

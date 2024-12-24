using Engine.ThreatCheckers;
using FluentAssertions;

namespace Engine.UnitTests.ThreatCheckers;

[TestClass]
public class KnightThreatCheckerTests
{
	[DataRow(ColoredPiece.BlackKnight, "b3", true, DisplayName = "Nb3")]
	[DataRow(ColoredPiece.BlackKnight, "b5", true, DisplayName = "Nb5")]
	[DataRow(ColoredPiece.BlackKnight, "c2", true, DisplayName = "Nc2")]
	[DataRow(ColoredPiece.BlackKnight, "c6", true, DisplayName = "Nc6")]
	[DataRow(ColoredPiece.BlackKnight, "e2", true, DisplayName = "Ne2")]
	[DataRow(ColoredPiece.BlackKnight, "f3", true, DisplayName = "Nf3")]
	[DataRow(ColoredPiece.BlackKnight, "f5", true, DisplayName = "Nf5")]
	[DataRow(ColoredPiece.BlackKnight, "e6", true, DisplayName = "Ne6")]
	[DataRow(ColoredPiece.BlackKnight, "b2", false, DisplayName = "Nb2")]
	[DataRow(ColoredPiece.BlackKnight, "g7", false, DisplayName = "Ng7")]
	[DataTestMethod]
	public void KingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "6k1/8/5r2/8/3K4/8/1B6/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new KnightThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.Black);

		actual.Should().Be(expected);
	}
}


using FluentAssertions;
using Minakuru.Engine;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.UnitTests.ThreatCheckers;

[TestClass]
public class KingThreatCheckerTests
{
	[DataRow(ColoredPiece.BlackKing, "c3", true, DisplayName = "Kc3")]
	[DataRow(ColoredPiece.BlackKing, "c4", true, DisplayName = "Kc4")]
	[DataRow(ColoredPiece.BlackKing, "c5", true, DisplayName = "Kc5")]
	[DataRow(ColoredPiece.BlackKing, "d3", true, DisplayName = "Kd3")]
	[DataRow(ColoredPiece.BlackKing, "d5", true, DisplayName = "Kd5")]
	[DataRow(ColoredPiece.BlackKing, "e3", true, DisplayName = "Ke3")]
	[DataRow(ColoredPiece.BlackKing, "e4", true, DisplayName = "Ke4")]
	[DataRow(ColoredPiece.BlackKing, "e5", true, DisplayName = "Ke5")]
	[DataRow(ColoredPiece.BlackKing, "b2", false, DisplayName = "Kb2")]
	[DataRow(ColoredPiece.BlackKing, "f4", false, DisplayName = "Kf4")]
	[DataTestMethod]
	public void KingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "8/8/8/8/3K4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new KingThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.Black);

		actual.Should().Be(expected);
	}
}


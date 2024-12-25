using FluentAssertions;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.UnitTests.ThreatCheckers;

[TestClass]
public class StraightLineThreatCheckerTests
{

	[DataRow(ColoredPiece.BlackQueen, "d1", true, DisplayName = "Qd1")]
	[DataRow(ColoredPiece.BlackRook, "d3", true, DisplayName = "Rd3")]
	[DataRow(ColoredPiece.BlackQueen, "e4", true, DisplayName = "Qe4")]
	[DataRow(ColoredPiece.BlackRook, "h4", true, DisplayName = "Rh4")]
	[DataRow(ColoredPiece.BlackQueen, "a4", false, DisplayName = "Qa4")]
	[DataRow(ColoredPiece.BlackRook, "c4", true, DisplayName = "Rc4")]
	[DataRow(ColoredPiece.BlackQueen, "d5", true, DisplayName = "Qd5")]
	[DataRow(ColoredPiece.BlackRook, "d8", false, DisplayName = "Rd1")]
	[DataRow(ColoredPiece.BlackRook, "e5", false, DisplayName = "Re5")]
	[DataTestMethod]
	public void KingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "6k1/8/3b4/8/1B1K4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new StraightLineThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.Black);

		actual.Should().Be(expected);
	}
}


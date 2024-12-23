using FluentAssertions;

namespace Engine.UnitTests;

[TestClass]
public sealed class FenTests
{
	[TestMethod]
	public void TestStartPositionToFen()
	{
		// Arrange
		var board = Board.Init();

		// Act
		var actual = board.ToFen();

		// Assert
		var expected = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
		actual.Should().Be(expected);
	}

	[TestMethod]
	public void TestFenOfStartPositionToBoard()
	{
		// Arrange
		var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

		// Act
		var actual = fen.ToBoard();
		var boardAsString = actual.ToString();

		// Assert
		var expected = Board.Init();
		actual.Should().Be(expected);
	}

	[DataRow("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", DisplayName = "1. e4")]
	[DataRow("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2", DisplayName = "1. e4 c5 2. Nf3")]
	[DataRow("rnbqkbnr/ppp2ppp/8/3pp3/2PP4/8/PP2PPPP/RNBQKBNR w KQkq e6 0 3", DisplayName = "1. d4 d5 2. c4 e5")]
	[DataRow("r1bqk2r/4bp1p/p1np1p2/1p1Np3/2P1P3/N7/PP3PPP/R2QKB1R b KQkq c3 0 11", DisplayName = "Sveshnikov")]
	[DataTestMethod]
	public void TestFen_e4_c5_Nf3_ToBoard(string originalFen)
	{
		// Arrange

		// Act
		var board = originalFen.ToBoard();
		var fenAgain = board.ToFen();

		// Assert
		fenAgain.Should().Be(originalFen);
	}

}

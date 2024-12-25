using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.UnitTests.MoveGenerators;

[TestClass]
public class PawnMoveGeneratorTests
{
	[TestMethod]
	public void FromD2Test()
	{
		var fen = "7k/8/8/8/8/8/3P4/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d2", "d3"),
			new ("d2", "d4"),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void FromD3Test()
	{
		var fen = "7k/8/8/8/8/3P4/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d3", "d4"),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnD2BlockedByPawnOnD3Test()
	{
		var fen = "7k/8/8/8/8/3p4/3P4/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = Array.Empty<ReadableMove>();

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnD3BlockedByPawnOnD4Test()
	{
		var fen = "7k/8/8/8/3p4/3P4/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = Array.Empty<ReadableMove>();

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesToLowerColumnTest()
	{
		var fen = "7k/8/8/2nN4/3P4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d4", "c5", true),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCantCaptureOwnPieceToLowerColumnTest()
	{
		var fen = "7k/8/8/2Bb4/3P4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = Array.Empty<ReadableMove>();

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesToHigherColumnTest()
	{
		var fen = "7k/8/8/3Nn3/3P4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d4", "e5", true),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCantCaptureOwnPieceToHigherColumnTest()
	{
		var fen = "7k/8/8/3bB3/3P4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = Array.Empty<ReadableMove>();

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnPromotionTest()
	{
		var fen = "7k/3P4/8/8/8/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// all promotions
			new ("d7", "d8", false, Piece.Queen),
			new ("d7", "d8", false, Piece.Rook),
			new ("d7", "d8", false, Piece.Bishop),
			new ("d7", "d8", false, Piece.Knight)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesToLowerColumnAndPromotesTest()
	{
		var fen = "2rr3k/3P4/8/8/8/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// all promotions
			new ("d7", "c8", true, Piece.Queen),
			new ("d7", "c8", true, Piece.Rook),
			new ("d7", "c8", true, Piece.Bishop),
			new ("d7", "c8", true, Piece.Knight)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesToHigherColumnAndPromotesTest()
	{
		var fen = "3rr2k/3P4/8/8/8/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// all promotions
			new ("d7", "e8", true, Piece.Queen),
			new ("d7", "e8", true, Piece.Rook),
			new ("d7", "e8", true, Piece.Bishop),
			new ("d7", "e8", true, Piece.Knight)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesEnPassantToLowerColumnTest()
	{
		var fen = "7k/8/3p4/2pP4/8/8/8/K7 w - c6 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d5", "c6", true),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCapturesEnPassantToHigherColumnTest()
	{
		var fen = "7k/8/3p4/3Pp3/8/8/8/K7 w - e6 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// up field up
			new ("d5", "e6", true),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void PawnCantCaptureEnPassantFromD2Test()
	{
		var fen = "7k/8/8/2p5/8/3p4/3P4/K7 w - c6 0 1";
		var board = fen.ToBoard();

		var sut = new PawnMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = Array.Empty<ReadableMove>();

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

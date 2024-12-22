﻿using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators;

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

}

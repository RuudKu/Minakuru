using FluentAssertions;
using Minakuru.Engine.Evaluators;

namespace Minakuru.Engine.UnitTests.Evaluators;

[TestClass]
public class SimpleMaterialEvaluatorTests
{

	[TestMethod]
	public void WhiteToMoveRookVsKnightUpTest()
	{
		var fen = "4k3/7n/4K3/8/8/8/8/R7 w - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(2000);
	}

	[TestMethod]
	public void BlackToMoveRookVsKnightDownTest()
	{
		var fen = "4k3/7n/4K3/8/8/8/8/R7 b - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(-2000);
	}

	[TestMethod]
	public void WhiteToMoveRookVsBishopUpTest()
	{
		var fen = "4k3/7b/4K3/8/8/8/8/R7 w - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(1500);
	}

	[TestMethod]
	public void BlackToMoveRookVsBishopDownTest()
	{
		var fen = "4k3/7b/4K3/8/8/8/8/R7 b - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(-1500);
	}

	[TestMethod]
	public void WhiteToMoveQueensVsRookUpTest()
	{
		var fen = "4k3/8/6r1/8/8/2Q5/8/4K3 w - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(4000);
	}

	[TestMethod]
	public void BlackToMoveQueensVsRookDownTest()
	{
		var fen = "4k3/8/6r1/8/8/2Q5/8/4K3 b - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(-4000);
	}

	[TestMethod]
	public void WhiteToMoveAndPawnDownTest()
	{
		var fen = "4k3/1p6/p1p5/P2p4/1P6/2P5/8/4K3 w - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(-1000);
	}

	[TestMethod]
	public void BlackToMoveAndPawnUpTest()
	{
		var fen = "4k3/1p6/p1p5/P2p4/1P6/2P5/8/4K3 b - - 0 1";
		Board board = fen.ToBoard();

		var simpleMaterialEvaluator = new SimpleMaterialEvaluator();
		var actual = simpleMaterialEvaluator.Evaluate(board);

		actual.Should().Be(1000);
	}
}

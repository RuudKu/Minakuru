using Shouldly;
using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.UnitTests.Evaluators;

[TestClass]
public class MateStalemateEvaluatorTests
{
	private IEvaluator _evaluator;

	[TestInitialize]
	public void TestInitialize()
	{
		_evaluator = new MateStalemateEvaluator(MoveGeneratorFactory.Create(), ThreatCheckerFactory.Create());
	}

	[TestMethod]
	public void WhiteGaveMateTest()
	{
		var fen = "1R2k3/8/4K3/8/8/8/8/8 b - - 0 1";
		Board board = fen.ToBoard();

		var actual = _evaluator.Evaluate(board);

		actual.ShouldBe(EvaluationConstants.Mate);
	}

	[TestMethod]
	public void BlackGaveMateTest()
	{
		var fen = "4k3/7n/8/8/8/8/1r6/r3K3 w - - 0 1";
		Board board = fen.ToBoard();

		var actual = _evaluator.Evaluate(board);

		actual.ShouldBe(- EvaluationConstants.Mate);
	}

	[TestMethod]
	public void StaleMateTest()
	{
		var fen = "4k3/4P3/4K3/8/8/8/8/8 b - - 0 1";
		Board board = fen.ToBoard();

		var actual = _evaluator.Evaluate(board);

		actual.ShouldBe(EvaluationConstants.StaleMate);
	}

	[TestMethod]
	public void PlayOnTest()
	{
		var fen = "4k3/8/4K3/8/8/8/8/8 b - - 0 1";
		Board board = fen.ToBoard();

		var actual = _evaluator.Evaluate(board);

		actual.ShouldBe(0);
	}
}

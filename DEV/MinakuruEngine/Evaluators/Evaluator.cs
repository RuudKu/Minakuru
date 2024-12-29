namespace Minakuru.Engine.Evaluators;

public class Evaluator(IEvaluator[] evaluators) : IEvaluator
{
	private readonly IEvaluator[] _evaluators = evaluators ?? throw new ArgumentNullException(nameof(evaluators));

	public int Evaluate(Board board)
	{
		var allEvaluations = _evaluators.Select(x => x.Evaluate(board)).ToArray();
		if (allEvaluations.Any(x => x == EvaluationConstants.Mate || x == -EvaluationConstants.Mate))
		{
			return EvaluationConstants.Mate;
		}
		return allEvaluations.Sum();
	}
}

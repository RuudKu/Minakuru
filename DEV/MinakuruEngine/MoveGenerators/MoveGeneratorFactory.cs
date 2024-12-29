using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.MoveGenerators;

public static class MoveGeneratorFactory
{
	public static IMoveGenerator Create()
	{
		return new LegalMovesGenerator(new PseudoLegalMoveGenerator(), ThreatCheckerFactory.Create());
	}
}

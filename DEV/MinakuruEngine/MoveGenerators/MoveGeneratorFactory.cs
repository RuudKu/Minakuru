using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.MoveGenerators;

public static class MoveGeneratorFactory
{
	public static LegalMovesGenerator Create()
	{
		return new LegalMovesGenerator(new PseudoLegalMoveGenerator(), ThreatCheckerFactory.Create());
	}
}

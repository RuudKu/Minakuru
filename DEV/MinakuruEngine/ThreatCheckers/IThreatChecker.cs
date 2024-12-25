namespace Minakuru.Engine.ThreatCheckers;

public interface IThreatChecker
{
	/// <summary>
	/// Determines whether a given field on a chess board is under attack by the given color
	/// </summary>
	/// <param name="board">The chess board</param>
	/// <param name="targetFieldNo">Field number of the field that could be attacked</param>
	/// <param name="color">Playing color that threatens the target field</param>
	/// <returns></returns>
	bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor);
}

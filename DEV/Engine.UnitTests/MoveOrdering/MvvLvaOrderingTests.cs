
using FluentAssertions;
using Minakuru.Engine.MoveOrdering;

namespace Minakuru.Engine.UnitTests.MoveOrdering;

[TestClass]
public class MvvLvaOrderingTests
{
	private IMoveOrdering _moveOrdering;

	[TestInitialize]
	public void TestInitialize()
	{
		_moveOrdering = new MvvLvaOrdering();
	}

	[TestMethod]
	public void CapturesTest()
	{
		var fen = "r1b1kbnr/pp4pp/1qn1pp2/3pP3/3N4/N1P5/PP2BPPP/R1BQK2R b KQkq - 1 8";
		var board = fen.ToBoard();

		var qb6xd4 = new Move(Field.B6FieldNo, Field.D4FieldNo, true);
		var qb6xb2 = new Move(Field.B6FieldNo, Field.B2FieldNo, true);
		var pf6f5 = new Move(Field.F6FieldNo, Field.F5FieldNo);
		var pf6xe5 = new Move(Field.F6FieldNo, Field.E5FieldNo, true);
		var nc6xd4 = new Move(Field.C6FieldNo, Field.D4FieldNo, true);
		var nc6xe5 = new Move(Field.C6FieldNo, Field.E5FieldNo, true);
		Move[] movelist = [
			qb6xd4, qb6xb2,
			pf6f5, pf6xe5,
			nc6xd4, nc6xe5
			];

		var actual = _moveOrdering.Order(board, movelist).ToArray();

		Move[] expected = [
			nc6xd4, // capture Nd4 with N
			qb6xd4, // capture Nd4 with Q
			pf6xe5, // capture pawn e5 with Pawn
			nc6xe5, // capture pawn e5 with K
			qb6xb2, // capture pawn b2 with Q
			pf6f5 // non-capture move
			];

		actual.Should().Equal(expected);


	}
}

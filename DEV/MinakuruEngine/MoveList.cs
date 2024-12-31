using System.Collections;

namespace Minakuru.Engine;

public class MoveList : IEnumerable<Move>
{
	private readonly List<Move> _moveList = new(40);

	public void Add(Move move)
	{
		_moveList.Add(move);
	}

	public void Merge(MoveList moveList)
	{
		_moveList.AddRange(moveList);
	}

	public IEnumerator<Move> GetEnumerator()
	{
		foreach (Move move in _moveList)
		{
			if (move == null)
			{
				break;
			}
			yield return move;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _moveList.GetEnumerator();
	}
}

namespace Minakuru.Engine;

public static class ColorExtensions
{
	public static Color OtherColor(this Color color)
	{
		return color switch
		{
			Color.White => Color.Black,
			Color.Black => Color.White,
			_ => throw new NotSupportedException()

		};
	}
}

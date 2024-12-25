namespace Minakuru.Engine;

public static class FieldConverters
{
	public static byte ToFieldNo(this string fieldName)
	{
		byte columnNo;
		if (fieldName[0] >= 'a' && fieldName[0] <= 'h')
		{
			columnNo = (byte)(fieldName[0] - 'a');
		}
		else if (fieldName[0] >= 'A' && fieldName[0] <= 'B')
		{
			columnNo = (byte)(fieldName[0] - 'A');
		}
		else
		{
			throw new ArgumentException(nameof(fieldName));
		}
		byte rowNo;
		if (fieldName[1] >= '1' && fieldName[1] <= '8')
		{
			rowNo = (byte)(fieldName[1] - '1');
		}
		else
		{
			throw new ArgumentException(nameof(fieldName));
		}
		return (byte)(rowNo * 8 + columnNo);
	}
}

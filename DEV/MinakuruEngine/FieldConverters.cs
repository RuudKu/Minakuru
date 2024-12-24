
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace Engine
{
	public static class FieldConverters
	{
		public static byte ToFieldNo(this string fieldName)
		{
			byte rowNo = 0;
			byte columnNo = 0;

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
			if (fieldName[1] >= '1' && fieldName[1] <= '8')
			{
				rowNo = (byte)(fieldName[1] - '1');
			}
			else
			{
				throw new ArgumentException(nameof(fieldName));
			}
			return (byte) (rowNo * 8 + columnNo);
		}
	}
}

namespace Imageverse.Application.Common.CustomValidators
{
	public static class GuidValidator
	{
		public static bool ValidateGuid(string guid)
		{
			return Guid.TryParse(guid, out _);
		}
	}
}

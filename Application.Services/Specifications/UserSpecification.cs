using Domain.Model.Users;

namespace Application.Services.Specifications
{
	public sealed class UserSpecification : BaseSpecification<Customer>
	{
		public UserSpecification(string identityId) : base(u => u.IdentityId == identityId)
		{
			AddInclude(u => u.RefreshTokens);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrossCutting.Helpers
{
	public interface IUseCaseRequest<out TUseCaseResponse> { }
}

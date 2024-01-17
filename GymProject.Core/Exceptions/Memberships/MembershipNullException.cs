using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymProject.Core.UserExceptions;

namespace GymProject.Core.Exceptions.Memberships
{
	internal class MembershipNullException : CustomException
	{
		public MembershipNullException() : base("Membership is null") { }
	}
}

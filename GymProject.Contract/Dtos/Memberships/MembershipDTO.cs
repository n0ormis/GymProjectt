using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Contract.Dtos
{
	public class MembershipDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public decimal Price { get; set; }
		public int Duration { get; set; }
		public byte[]? Avatar { get; set; }
	}
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GymProject.Contract.Dtos;


namespace GymProject.Contract.Commands.Memberships
{
	public record AddMembershipCommand(string Name, decimal Price, int Duration, string Description) : IRequest;
}

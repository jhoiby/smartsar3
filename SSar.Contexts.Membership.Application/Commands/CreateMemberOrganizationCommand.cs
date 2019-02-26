using MediatR;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMemberOrganizationCommand : IRequest<CommandResult>
    {
        public CreateMemberOrganizationCommand()
        {
        }

        public CreateMemberOrganizationCommand(string fullName, string nickname, string reportingCode)
        {
            FullName = fullName;
            Nickname = nickname;
            ReportingCode = reportingCode;
        }

        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string ReportingCode { get; set; }
    }
}

using MediatR;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMemberOrganizationCommand : IRequest<CommandResult>
    {
        public CreateMemberOrganizationCommand()
        {
        }

        public CreateMemberOrganizationCommand(string fullName, string shortName, string reportingCode)
        {
            FullName = fullName;
            ShortName = shortName;
            ReportingCode = reportingCode;
        }

        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string ReportingCode { get; set; }
    }
}

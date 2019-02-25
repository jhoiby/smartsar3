using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSar.Presentation.WebUI.Services;

namespace SSar.Presentation.WebUI.Areas.Membership.Pages.MemberOrgs
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [BindProperty]
        public Result Data { get; set; }

        public async Task OnGet()
        {
            Data = await _mediator.Send(new Query());
        }

        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public List<MemberOrganization> MemberOrganizations { get; set; }

            public  class MemberOrganization
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly IQueryService _queryService;

            public Handler(IQueryService queryService)
            {
                _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
                => new Result
                {
                    MemberOrganizations = await _queryService.ListQuery<Result.MemberOrganization>(
                        "SELECT _id AS Id, Name_Name AS Name FROM MemberOrganizations")
                };
        }
    }
}
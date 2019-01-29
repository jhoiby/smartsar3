using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSar.Presentation.WebUI.Services;

namespace SSar.Presentation.WebUI.Areas.Identity.Pages.Roles
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

        public async Task OnGetAsync()
        {
            Data = await _mediator.Send(new IndexModel.Query());
        }

        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public List<Role> Roles { get; set; }

            public class Role
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }
        }

        public class Handler : IRequestHandler<IndexModel.Query, IndexModel.Result>
        {
            private readonly IQueryService _queryService;

            public Handler(IQueryService queryService)
            {
                _queryService = queryService ?? throw new ArgumentNullException();
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return
                        new Result
                        {
                            Roles = await _queryService.ListQuery<Result.Role>(
                                "SELECT Id, Name, Description FROM AspNetRoles")
                        };

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
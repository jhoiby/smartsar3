//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using FluentValidation;
//using MediatR;
//using SSar.Contexts.Common.Notifications;
//using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

//namespace SSar.Contexts.Common.RequestBehaviors
//{
//    // Much/most of this class is from the Northwind demo by Jason GT, https://github.com/JasonGT/NorthwindTraders
//    // Licensed under MIT license https://github.com/JasonGT/NorthwindTraders/blob/master/LICENSE.md 

//    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//        where TResponse : CommandResult
//    {
//        private readonly IEnumerable<IValidator<TRequest>> _validators;

//        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
//        {
//            _validators = validators;
//        }

//        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {

//            var context = new ValidationContext(request);

//            var failures = _validators
//                .Select(v => v.Validate(request))
//                .SelectMany(result => result.Errors)
//                .Where(f => f != null)
//                .ToList();

//            if (failures.Count != 0)
//            {
//                return Task.FromResult(failures.ToCommandResult() as TResponse);
//            }

//            return next();
//        }
//    }
//}

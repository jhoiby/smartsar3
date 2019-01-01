using System;

namespace SSar.Contexts.Common.Results
{
    public static class OperationResultExtensions
    {
        public static OperationResult AppendErrorsTo(this OperationResult resultToMerge, OperationResult targetResult)
        {
            if (targetResult == null)
            {
                throw new ArgumentNullException(nameof(targetResult));
            }

            targetResult.Errors.AddErrors(resultToMerge.Errors);

            return targetResult;
        }
    }
}

using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreSample.Application
{
    public record ValidationFailed(IEnumerable<ValidationFailure> errors)
    {
        public ValidationFailed(ValidationFailure error) : this(new[] { error })
        {

        }
    }
}

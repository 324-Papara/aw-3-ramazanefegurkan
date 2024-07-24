﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Para.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Behavior
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .Where(validationResult => !validationResult.IsValid)
                    .SelectMany(validationResult => validationResult.Errors)
                    .Select(validationFailure => new ValidationFailure(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage))
                    .ToList();

                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }
            }
            return await next();
        }

    }
}

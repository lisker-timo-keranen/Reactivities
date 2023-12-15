using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
  public class Create
  {
    // Command luokka ei tule palauttamaan mitään, joten sen ei tarvitse käyttää geneeristä rajapintaa.
    public class Command : IRequest<Result<Unit>>
    {
      // Tämä property on se jonka haluamme vastaanottaa parametrinä API:lta.
      public Activity Activity { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
      }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
    private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
      {
        _context.Activities.Add(request.Activity);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return Result<Unit>.Failure("Failed to create activity");

        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}
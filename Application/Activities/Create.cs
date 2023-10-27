using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        // Command luokka ei tule palauttamaan mitään, joten sen ei tarvitse käyttää geneeristä rajapintaa.
        public class Command : IRequest
        {
            // Tämä property on se jonka haluamme vastaanottaa parametrinä API:lta.
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using TestTemplate13.Application.Questions.Queries;
using TestTemplate13.Common.Exceptions;
using TestTemplate13.Common.Interfaces;
using TestTemplate13.Core.Entities;
using TestTemplate13.Core.Events;

namespace TestTemplate13.Application.Questions.Commands
{
    public class CreateFooCommand : IRequest<FooGetModel>
    {
        public string Text { get; set; }

        private class CreateFooCommandHandler : IRequestHandler<CreateFooCommand, FooGetModel>
        {
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IRepository<Foo> _repository;
            private readonly IMapper _mapper;

            public CreateFooCommandHandler(
                IPublishEndpoint publishEndpoint,
                IRepository<Foo> repository,
                IMapper mapper)
            {
                _publishEndpoint = publishEndpoint;
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<FooGetModel> Handle(CreateFooCommand request, CancellationToken cancellationToken)
            {
                if (await _repository.GetSingleAsync(f => f.Text == request.Text) != null)
                {
                    throw new BusinessException($"Cannot create {nameof(Foo)} with text {request.Text}.");
                }
                var foo = new Foo(request.Text);
                await _repository.AddAsync(foo);

                // sending to queue
                await _publishEndpoint.Publish<IFooCommand>(new { Text = foo.Text });

                // sending to topic
                await _publishEndpoint.Publish<IFooEvent>(new { Text = foo.Text });
                return _mapper.Map<FooGetModel>(foo);
            }
        }
    }
}
